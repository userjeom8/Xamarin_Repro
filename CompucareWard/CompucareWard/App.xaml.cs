using System;
using Xamarin.Forms;
using CompucareWard.Services;
using CompucareWard.Views;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using CompucareWard.Models;
using CompucareWard.ViewModels;
using CompucareWard.Services.RequestProvider;
using CompucareWard.Services.Identity;
using CompucareWard.Services.Settings;
using Prism.Unity;
using Prism.Plugin.Popups;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CompucareWard
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // This updates INavigationService and registers PopupNavigation.Instance
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<RemindersPage, RemindersViewModel>();
            containerRegistry.RegisterForNavigation<AllPatientsPage, AllPatientsViewModel>();
            containerRegistry.RegisterForNavigation<PatientDetailPage, PatientDetailViewModel>();
            containerRegistry.RegisterForNavigation<PatientDetailWithAlertsPage, PatientDetailViewModel>();
            containerRegistry.RegisterForNavigation<MyPatientsPage, MyPatientsViewModel>();
            containerRegistry.RegisterForNavigation<NEWSAddPage, NEWSAddViewModel>();
            containerRegistry.RegisterForNavigation<MainMasterDetailPage, MainMasterDetailPageMasterViewModel>();
            containerRegistry.RegisterForNavigation<PatientOverviewPage, PatientOverviewViewModel>();
            containerRegistry.RegisterForNavigation<PatientObservationsPage, PatientObservationsViewModel>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SmallNavigationPage>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<AlertsPage, AlertsViewModel>();
            containerRegistry.RegisterForNavigation<PatientConfirmationPage, PatientConfirmationViewModel>();
            containerRegistry.RegisterForNavigation<HandoverPage, HandoverViewModel>();
            containerRegistry.RegisterForNavigation<EscalatePage, EscalateViewModel>();
            containerRegistry.RegisterForNavigation<ObservationFrequencyPage, ObservationFrequencyViewModel>();
            containerRegistry.RegisterForNavigation<SelectionPage, SelectionViewModel>();
            containerRegistry.RegisterForNavigation<SavingPage>();

            containerRegistry.RegisterSingleton<IInpatientBookingService, InpatientBookingService>();
            containerRegistry.RegisterSingleton<ILocationService, LocationService>();
            containerRegistry.RegisterSingleton<IPersonService, PersonService>();
            containerRegistry.RegisterSingleton<INEWSService, NEWSService>();
            containerRegistry.RegisterSingleton<IRequestProvider, RequestProvider>();
            containerRegistry.RegisterSingleton<IIdentityService, IdentityService>();
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();
            containerRegistry.RegisterSingleton<IPatientService, PatientService>();
            containerRegistry.RegisterSingleton<INurseService, NurseService>();
            containerRegistry.RegisterSingleton<IGlobalSettingsService, GlobalSettingsService>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (!AppCenter.Configured)
            {
                Push.PushNotificationReceived += (sender, e) =>
                {
                    // Add the notification message and title to the message
                    var summary = $"Push notification received:" + $"\n\tNotification title: {e.Title}" + $"\n\tMessage: {e.Message}";

                    // If there is custom data associated with the notification, print the entries
                    if (e.CustomData != null)
                    {
                        summary += "\n\tCustom data:\n";
                        foreach (var key in e.CustomData.Keys)
                        {
                            summary += $"\t\t{key} : {e.CustomData[key]}\n";
                        }
                    }

                    // Send the notification summary to debug output
                    System.Diagnostics.Debug.WriteLine(summary);
                };
            }

            AppCenter.Start("ios=c8ab4aba-9fe3-45dd-9513-86a2b9623e34;" + "uwp=e04d7afd-1bd1-4376-8ea0-2787e6694181;", typeof(Analytics), typeof(Crashes), typeof(Push));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
