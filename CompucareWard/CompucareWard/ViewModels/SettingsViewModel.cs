using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CompucareWard.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private string _apiEndpoint;
        private string _identityEndpoint;

        public string ApiEndpoint
        {
            get => _apiEndpoint;
            set => SetProperty(ref _apiEndpoint, value);
        }

        public string IdentityEndpoint
        {
            get => _identityEndpoint;
            set => SetProperty(ref _identityEndpoint, value);
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public SettingsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService,
            IPatientService patientService, IEventAggregator eventAggregator, IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            SaveCommand = new DelegateCommand(async () => await Save());
            CancelCommand = new DelegateCommand(async () => await _navigationService.GoBackAsync(useModalNavigation: true));
        }

        async Task Save()
        {
            var isAPIUrlValid = CheckUri(ApiEndpoint + "/values");
            var isIdentityUrlValid = CheckUri(IdentityEndpoint);

            if (isAPIUrlValid && isIdentityUrlValid)
            {
                Application.Current.Properties["apiEndpoint"] = ApiEndpoint.ToLower();
                Application.Current.Properties["identityEndpoint"] = IdentityEndpoint.ToLower();
                await Application.Current.SavePropertiesAsync();
                await _navigationService.GoBackAsync(useModalNavigation: true);
            }
            else
            {                
                await _pageDialogService.DisplayAlertAsync("Incorrect URL", (isAPIUrlValid? "" : "Please check the API URL") +
                    (isIdentityUrlValid ? "" : Environment.NewLine + "Please check the Identity URL"), "OK");
            }                            
        }

        private bool CheckUri(string url)
        {
            // check is valid URL
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;

            // check connection to URL
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        protected override Task OnInitialNavigatedTo(INavigationParameters parameters)
        {
            if (Application.Current.Properties.ContainsKey("apiEndpoint"))
                ApiEndpoint = Application.Current.Properties["apiEndpoint"] as string;

            if (Application.Current.Properties.ContainsKey("identityEndpoint"))
                IdentityEndpoint = Application.Current.Properties["identityEndpoint"] as string;

            return Task.CompletedTask;
        }
    }
}