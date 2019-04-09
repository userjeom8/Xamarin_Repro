using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Linq;
using CompucareWard.Models;
using CompucareWard.Views;
using Prism.Navigation;
using Prism.Services;
using CompucareWard.Services;
using Prism.Commands;
using Prism;
using CompucareWard.Services.Settings;
using Prism.Events;
using CompucareWard.Events;

namespace CompucareWard.ViewModels
{
    public abstract class PatientsBaseViewModel : BaseViewModel
    {
        private ObservableCollection<InpatientBooking> _patients;
        private ObservableCollection<InpatientBooking> _filteredPatients;
        private bool _isPendingRefresh;
        protected readonly IInpatientBookingService _inpatientBookingService;

        public ObservableCollection<InpatientBooking> Patients
        {
            get => _filteredPatients ?? _patients;
            set => SetProperty(ref _patients, value);
        }

        public DelegateCommand<string> SearchCommand { get; private set; }
        public DelegateCommand<InpatientBooking> PatientSelectedCommand { get; private set; }
        public DelegateCommand RefreshCommand { get; private set; }
        public DelegateCommand<InpatientBooking> AddNEWSCommand { get; private set; }

        public PatientsBaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IInpatientBookingService inpatientBookingService, ISettingsService settingsService, 
            IEventAggregator eventAggregator, IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            _inpatientBookingService = inpatientBookingService;

            Patients = new ObservableCollection<InpatientBooking>();

            RefreshCommand = new DelegateCommand(async () => await ExecuteLoadItems());
            SearchCommand = new DelegateCommand<string>(Search);
            PatientSelectedCommand = new DelegateCommand<InpatientBooking>(async (b) => await PatientSelected(b), b => b != null);
            AddNEWSCommand = new DelegateCommand<InpatientBooking>(async (b) => await _navigationService.NavigateAsync($"{nameof(Views.SmallNavigationPage)}/{nameof(Views.NEWSAddPage)}",
                                                                                                                       new NavigationParameters { { "Item", b } }, useModalNavigation: true));

            _eventAggregator.GetEvent<NEWSAddedEvent>().Subscribe((ip) => _isPendingRefresh = true);
            _eventAggregator.GetEvent<ObservationFrequencyChanged>().Subscribe((ip) => _isPendingRefresh = true);
            _eventAggregator.GetEvent<NursesHandedOverEvent>().Subscribe(() => _isPendingRefresh = true);
        }

        protected async override void OnIsActiveChanged(object sender, EventArgs e)
        {
            base.OnIsActiveChanged(sender, e);

            if (IsActive && (Patients.Count == 0 || _isPendingRefresh))
            {
                _isPendingRefresh = false;
                await ExecuteLoadItems();
            }
        }

        void Search(string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
                _filteredPatients = new ObservableCollection<InpatientBooking>(_patients.Where(ib => ib.Patient.FullnameReverse.ToUpper().Contains(search.ToUpper())));
            else
                _filteredPatients = null;

            RaisePropertyChanged(nameof(Patients));
        }

        async Task PatientSelected(InpatientBooking booking)
        {
            if (booking.Patient.HasAlerts)
                await _navigationService.NavigateAsync(nameof(Views.PatientDetailWithAlertsPage), new NavigationParameters { { "Item", booking } });
            else
                await _navigationService.NavigateAsync(nameof(Views.PatientDetailPage), new NavigationParameters { { "Item", booking } });
        }

        protected abstract Task ExecuteLoadItems();
    }
}