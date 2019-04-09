using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using CompucareWard.Models;
using CompucareWard.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using System.Threading.Tasks;
using CompucareWard.Services.Settings;
using Prism;
using Prism.Commands;
using CompucareWard.Exceptions;
using CompucareWard.Services.RequestProvider;
using Xamarin.Essentials;
using Microsoft.AppCenter.Crashes;
using Prism.Events;
using CompucareWard.Events;
using CompucareWard.Helpers;

namespace CompucareWard.ViewModels
{
    public class BaseViewModel : BindableBase, INavigatedAware, IActiveAware
    {
        protected readonly INavigationService _navigationService;
        protected readonly IPageDialogService _pageDialogService;
        protected readonly ISettingsService _settingsService;
        protected readonly IEventAggregator _eventAggregator;
        protected readonly IdentityService _identityService;
        bool _isBusy = false;
        User _user;
        private bool _isActive;
        bool _initialNavigation = true;

        public event EventHandler IsActiveChanged;

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, () => OnIsActiveChanged(this, new EventArgs()));
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        protected enum HandleMode
        {
            OKandContinue,
            RetryDiscard,
            RetryCancel
        }

        public BaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IEventAggregator eventAggregator, IdentityService identityService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;
            _identityService = identityService;

            User = _settingsService.User;
        }

        protected virtual void OnIsActiveChanged(object sender, EventArgs e)
        {
            if (IsActiveChanged is EventHandler handler)
                handler(this, e);
        }

        protected async Task<APIActionResult> HandleAPICall(Func<Task> apiCall, HandleMode handleMode = HandleMode.OKandContinue, Func<Task> failedAction = null, Action failedActionSync = null,
            bool refreshToken = true, [CallerMemberName] string caller = "")
        {
            try
            {
                var current = Connectivity.NetworkAccess;                

                if (current == NetworkAccess.Internet)
                {                    
                    await apiCall();
                    //  refresh token - get new token if less than a minute to token expires
                    if (refreshToken && (_settingsService.TokenExpiresAt - DateTime.Now).TotalMinutes < 5)
                    {
                        var token = await _identityService.GetRefreshTokenAsync(_settingsService.RefreshToken);
                        if (!string.IsNullOrEmpty(token.RefreshToken))
                        {
                            _settingsService.AuthAccessToken = token.AccessToken;
                            _settingsService.AuthIdToken = token.IdToken;
                            _settingsService.RefreshToken = token.RefreshToken;
                            _settingsService.TokenExpiresIn = token.ExpiresIn;
                            _settingsService.TokenExpiresAt = DateTime.Now.AddSeconds(token.ExpiresIn);
                        }
                    }
                    return APIActionResult.Successfull;
                }
                else
                    return await HandleConnectivityIssue(handleMode, () => HandleAPICall(apiCall, handleMode, failedAction, failedActionSync, refreshToken, caller), failedAction, failedActionSync);
            }
            catch (ServiceAuthenticationException)
            {
                //Crashes.TrackError(exception);
                _eventAggregator.GetEvent<LoggedOutEvent>().Publish();
                await _navigationService.NavigateAsync(nameof(Views.LoginPage), new NavigationParameters { { "Logout", true } });
                return await CallFailedAction(failedAction, failedActionSync);
            }
            catch (HttpRequestExceptionEx exception)
            {
                Crashes.TrackError(exception, new Dictionary<string, string> { { "CallingClass", this.GetType().FullName }, { "CallingMethod", caller } });
                return await HandleConnectivityIssue(handleMode, () => HandleAPICall(apiCall, handleMode, failedAction, failedActionSync, refreshToken, caller), failedAction, failedActionSync, exception);
            }
        }        

        async Task<APIActionResult> HandleConnectivityIssue(HandleMode handleMode, Func<Task<APIActionResult>> retryAction, Func<Task> failedAction = null, Action failedActionSync = null, Exception exception = null)
        {
            switch (handleMode)
            {
                case HandleMode.RetryCancel:
                    if (await _pageDialogService.DisplayAlertAsync("Connectivity Issue", "No internet access", "Retry", "Cancel"))
                        return await retryAction();
                    else
                        return await CallFailedAction(failedAction, failedActionSync);
                case HandleMode.RetryDiscard:
                    if (await _pageDialogService.DisplayAlertAsync("Connectivity Issue", "No internet access", "Retry", "Discard"))
                        return await retryAction();
                    else
                        return await CallFailedAction(failedAction, failedActionSync);
                case HandleMode.OKandContinue:
                default:
                    await _pageDialogService.DisplayAlertAsync("Connectivity Issue", "No internet access", "OK");
                    await failedAction?.Invoke();
                    return APIActionResult.Failed;
            }
        }

        async Task<APIActionResult> CallFailedAction(Func<Task> failedAction = null, Action failedActionSync = null)
        {
            if (failedAction != null)
                await failedAction();
            else
                failedActionSync?.Invoke();

            return APIActionResult.Failed;
        }

        async void INavigatedAware.OnNavigatedTo(INavigationParameters parameters)
        {
            if (_initialNavigation)
            {
                _initialNavigation = false;
                await OnInitialNavigatedTo(parameters);
            }
            else
                await OnSubsequentNavigatedTo(parameters);
        }

        protected virtual Task OnInitialNavigatedTo(INavigationParameters parameters) => Task.CompletedTask;
        protected virtual Task OnSubsequentNavigatedTo(INavigationParameters parameters) => Task.CompletedTask;
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
    }
}
