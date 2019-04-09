using CompucareWard.Events;
using CompucareWard.Models;
using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CompucareWard.ViewModels
{
    class ObservationFrequencyViewModel : BaseViewModel
    {
        private readonly IInpatientBookingService _inpatientBookingService;
        private InpatientBooking _inpatientBooking;
        private int? _observationFrequency;
        private string _titleName;
        private Frequency _selectedFrequency;
        private ObservableCollection<Frequency> _frequencyList = new ObservableCollection<Frequency>();

        public int? ObservationFrequency
        {
            get => _observationFrequency;
            set => SetProperty(ref _observationFrequency, value);
        }

        public Frequency SelectedFrequency
        {
            get => _selectedFrequency;
            set => SetProperty(ref _selectedFrequency, value);
        }

        public ObservableCollection<Frequency> FrequencyList
        {
            get => _frequencyList;
            set => SetProperty(ref _frequencyList, value);
        }

        public string TitleName
        {
            get => _titleName;
            set => SetProperty(ref _titleName, value);
        }

        public DelegateCommand<Frequency> SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public ObservationFrequencyViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService,
            IPatientService patientService, IEventAggregator eventAggregator, IInpatientBookingService inpatientBookingService, IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            _inpatientBookingService = inpatientBookingService;

            SaveCommand = new DelegateCommand<Frequency>(async (s) => await Save(s));
            CancelCommand = new DelegateCommand(async () => await Cancel());
        }

        async Task Cancel() => await _navigationService.GoBackAsync(useModalNavigation: true);

        async Task Save(Frequency frequency)
        {
            await HandleAPICall(async () =>
            {
                await _inpatientBookingService.SaveObservationFrequency(_inpatientBooking.InpatientBookingId, frequency.Minutes, _settingsService.AuthAccessToken);
                _eventAggregator.GetEvent<ObservationFrequencyChanged>().Publish(_inpatientBooking.InpatientBookingId);
                await _navigationService.GoBackAsync(useModalNavigation: true);
            }, handleMode: HandleMode.RetryDiscard, failedAction: Cancel);
        }

        protected override Task OnInitialNavigatedTo(INavigationParameters parameters)
        {
            FrequencyList = new ObservableCollection<Frequency>() {
                new Frequency { Name = "5 Minutes", Minutes = 5  },
                new Frequency { Name = "10 Minutes", Minutes = 10 },
                new Frequency { Name = "15 Minutes", Minutes = 15 },
                new Frequency { Name = "20 Minutes", Minutes = 20 },
                new Frequency { Name = "30 Minutes", Minutes = 30 },
                new Frequency { Name = "45 Minutes", Minutes = 45 },
                new Frequency { Name = "1 Hour", Minutes = 60 },
                new Frequency { Name = "1 Hour, 30 Minutes", Minutes = 90 },
                new Frequency { Name = "2 Hours", Minutes = 120 },
                new Frequency { Name = "3 Hours", Minutes = 180 },
                new Frequency { Name = "4 Hours", Minutes = 240 },
                new Frequency { Name = "5 Hours", Minutes = 300 },
                new Frequency { Name = "6 Hours", Minutes = 360 }
            };

            _inpatientBooking = parameters["Item"] as InpatientBooking;
            TitleName = $"Currently {(_inpatientBooking?.ObservationFrequencyInMinutes?.ToString() ?? "-")} Minutes";

            return Task.CompletedTask;
        }
    }
}
