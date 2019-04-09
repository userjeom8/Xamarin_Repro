using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using CompucareWard.Models;
using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace CompucareWard.ViewModels
{
    public class MainMasterDetailPageMasterViewModel : BaseViewModel
    {
        private readonly IGlobalSettingsService _globalSettingsService;

        public DelegateCommand SettingsCommand { get; private set; }
        public DelegateCommand HandoverCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand GuidanceCommand { get; private set; }

        public MainMasterDetailPageMasterViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IEventAggregator eventAggregator,
            IdentityService identityService, IGlobalSettingsService globalSettingsService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            _globalSettingsService = globalSettingsService;

            SettingsCommand = new DelegateCommand(async () => await NavigateSettings());
            GuidanceCommand = new DelegateCommand(async () => await GuidanceDocument());
            HandoverCommand = new DelegateCommand(async () => await NavigateHandover(), () => User.NurseId.HasValue);
            LogoutCommand = new DelegateCommand(async () => await LogoutAsync());
        }

        private async Task LogoutAsync()
        {
            IsBusy = true;
            await _navigationService.NavigateAsync(nameof(Views.LoginPage), new NavigationParameters { { "Logout", true } });
            IsBusy = false;
        }

        async Task NavigateSettings()
            => await _navigationService.NavigateAsync($"{nameof(Views.SmallNavigationPage)}/{nameof(Views.SettingsPage)}", useModalNavigation: true);

        async Task GuidanceDocument()
        {
            await HandleAPICall(async () =>
            {
                var newsSettings = await _globalSettingsService.GetNEWSSettings(_settingsService.AuthAccessToken);

                if (!string.IsNullOrEmpty(newsSettings.GuidanceDocumentURL))
                    await Browser.OpenAsync(newsSettings.GuidanceDocumentURL, BrowserLaunchMode.SystemPreferred);
                else
                    await _pageDialogService.DisplayAlertAsync("Alert", "Guidance Document has not be set-up.", "OK");
            }, HandleMode.RetryCancel);
        }

        async Task NavigateHandover()
            => await _navigationService.NavigateAsync($"{nameof(Views.SmallNavigationPage)}/{nameof(Views.HandoverPage)}", useModalNavigation: true);
    }
}
