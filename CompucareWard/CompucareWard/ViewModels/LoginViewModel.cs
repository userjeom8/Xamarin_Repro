using CompucareWard.Services;
using CompucareWard.Services.Identity;
using CompucareWard.Services.Settings;
using IdentityModel.Client;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Prism;
using CompucareWard.Models;
using Prism.Mvvm;
using Prism.Commands;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

namespace CompucareWard.ViewModels
{
    public class LoginViewModel : BindableBase, INavigatedAware
    {
        private readonly IIdentityService _identityService;
        private readonly ISettingsService _settingsService;
        private readonly IPersonService _personService;
        private string _loginUrl;
        private bool _isLogin;
        protected readonly INavigationService _navigationService;
        bool isBusy = false;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string LoginUrl
        {
            get => _loginUrl;
            set => SetProperty(ref _loginUrl, value, () => RaisePropertyChanged(nameof(HasLoginUrl)));
        }

        public bool IsLogin
        {
            get => _isLogin;
            set => SetProperty(ref _isLogin, value);
        }

        public bool HasLoginUrl => !string.IsNullOrEmpty(LoginUrl);

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand NavigatedCommand { get; private set; }
        public DelegateCommand SettingsCommand { get; private set; }

        public LoginViewModel(INavigationService navigationService, IIdentityService identityService, ISettingsService settingsService, IPersonService personService)
        {
            _settingsService = settingsService;
            _identityService = identityService;
            _personService = personService;
            _navigationService = navigationService;

            NavigateCommand = new DelegateCommand<string>(async (url) => await NavigateAsync(url));
            NavigatedCommand = new DelegateCommand(Navigated);
            SettingsCommand = new DelegateCommand(async () => await NavigateSettings());
        }

        private void Navigated()
        {
            IsBusy = false;
        }

        private void ShowLogin()
        {
            IsBusy = true;
            LoginUrl = _identityService.CreateAuthorizationRequest();
            IsLogin = true;
        }

        private void Logout()
        {
            var logoutRequest = _identityService.CreateLogoutRequest(_settingsService.AuthIdToken);
            AppCenter.SetUserId(null);
            AppCenter.SetCustomProperties(new CustomProperties().Clear("Licence").Clear("User").Clear("SystemType"));

            if (!string.IsNullOrEmpty(logoutRequest))
            {
                LoginUrl = logoutRequest;
                IsLogin = true;
            }
        }

        private async Task NavigateAsync(string url)
        {
            var unescapedUrl = System.Net.WebUtility.UrlDecode(url);
            IsBusy = true;

            if (unescapedUrl.Equals(GlobalSettings.Instance.LogoutCallback))
            {
                _settingsService.AuthAccessToken = string.Empty;
                _settingsService.AuthIdToken = string.Empty;
                _settingsService.RefreshToken = string.Empty;
                AppCenter.SetUserId(null);
                await Task.Delay(1000); // otherwise you don't see the logged out page
                ShowLogin();
            }
            else if (unescapedUrl.Contains(GlobalSettings.Instance.Callback))
            {
                var authResponse = new AuthorizeResponse(url);
                if (!string.IsNullOrWhiteSpace(authResponse.Code))
                {
                    var userToken = await _identityService.GetTokenAsync(authResponse.Code);
                    string accessToken = userToken.AccessToken;

                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        _settingsService.AuthAccessToken = accessToken;
                        _settingsService.AuthIdToken = authResponse.IdentityToken;
                        _settingsService.RefreshToken = userToken.RefreshToken;
                        _settingsService.TokenExpiresIn = userToken.ExpiresIn;
                        _settingsService.TokenExpiresAt = DateTime.Now.AddSeconds(userToken.ExpiresIn);
                        _settingsService.User = await _personService.GetLoggedInUser(_settingsService.AuthAccessToken);
                        IsLogin = false;
                        AppCenter.SetUserId($"{_settingsService.User.LicenceNumber}/{_settingsService.User.SystemType}/{_settingsService.User.UserId}");
                        AppCenter.SetCustomProperties(new CustomProperties().Set("Licence", _settingsService.User.LicenceNumber)
                                                                            .Set("User", _settingsService.User.UserId.ToString())
                                                                            .Set("SystemType", _settingsService.User.SystemType.ToString()));
                        await _navigationService.NavigateAsync($"/{nameof(Views.MainMasterDetailPage)}/{nameof(Views.MainPage)}");
                    }
                }
            }
        }

        async Task NavigateSettings() => await _navigationService.NavigateAsync($"{nameof(Views.SmallNavigationPage)}/{nameof(Views.SettingsPage)}", useModalNavigation: true);

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Logout") && (bool)parameters["Logout"])
                Logout();
            else
            {
                var apiEndpoint = Application.Current.Properties.ContainsKey("apiEndpoint") ? Application.Current.Properties["apiEndpoint"] as string : null;
                var identityEndpoint = Application.Current.Properties.ContainsKey("identityEndpoint") ? Application.Current.Properties["identityEndpoint"] as string : null;

                if (string.IsNullOrEmpty(apiEndpoint) || string.IsNullOrEmpty(identityEndpoint))
                    await NavigateSettings();
                else
                {
                    GlobalSettings.Instance.BaseApiEndpoint = apiEndpoint;
                    GlobalSettings.Instance.BaseIdentityEndpoint = identityEndpoint;
                    ShowLogin();
                }
            }
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
    }
}