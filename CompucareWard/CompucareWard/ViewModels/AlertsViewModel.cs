using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CompucareWard.Events;
using CompucareWard.Models;
using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;

namespace CompucareWard.ViewModels
{
    public class AlertsViewModel : PatientDetailChildBaseViewModel
    {
        private ObservableCollection<Alert> _alerts;
        private readonly IPatientService _patientService;

        public ObservableCollection<Alert> Alerts
        {
            get => _alerts;
            set => SetProperty(ref _alerts, value);
        }

        public AlertsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IPatientService patientService, IEventAggregator eventAggregator, 
            IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            _patientService = patientService;
        }

        protected override async Task Initialise()
        {
            IsBusy = true;
            Alerts = await _patientService.GetPatientAlerts(Booking.Patient.PatientId, _settingsService.AuthAccessToken);
            IsBusy = false;
        }
    }
}
