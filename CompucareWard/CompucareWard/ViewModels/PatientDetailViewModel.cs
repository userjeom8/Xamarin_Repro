using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CompucareWard.Events;
using CompucareWard.Models;
using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System.Linq;
using Plugin.Messaging;
using System.Threading.Tasks;

namespace CompucareWard.ViewModels
{
    public class PatientDetailViewModel : BaseViewModel
    {
        private InpatientBooking _booking;
        private string _title;
        private bool _isPendingRefresh;
        private readonly IInpatientBookingService _bookingService;

        public InpatientBooking Booking
        {
            get => _booking;
            set => SetProperty(ref _booking, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public DelegateCommand AddNEWSCommand { get; private set; }

        public PatientDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IEventAggregator eventAggregator, 
            IdentityService identityService, IInpatientBookingService bookingService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            _bookingService = bookingService;

            AddNEWSCommand = new DelegateCommand(async () => await _navigationService.NavigateAsync($"{nameof(Views.SmallNavigationPage)}/{nameof(Views.NEWSAddPage)}", 
                                                                                                    new NavigationParameters { { "Item", Booking } }, useModalNavigation: true));

            _eventAggregator.GetEvent<NEWSAddedEvent>().Subscribe(Refresh);
            _eventAggregator.GetEvent<ObservationFrequencyChanged>().Subscribe(Refresh);
            _eventAggregator.GetEvent<NursesHandedOverEvent>().Subscribe(() => Refresh(0));
        }

        void Refresh(int inpatientBookingId)
        {
            if (inpatientBookingId == 0 || (Booking?.InpatientBookingId == inpatientBookingId))
                _isPendingRefresh = true;
        }

        protected override async void OnIsActiveChanged(object sender, EventArgs e)
        {
            base.OnIsActiveChanged(sender, e);

            if (IsActive && Booking != null && _isPendingRefresh)
            {
                await HandleAPICall(async () =>
                {
                    Booking = await _bookingService.GetItemAsync(Booking.InpatientBookingId, _settingsService.AuthAccessToken);
                    Title = Booking?.Patient.FullnameReverse;
                    _eventAggregator.GetEvent<PatientDetailRefreshEvent>().Publish(Booking);
                });
            }
        }

        protected override Task OnInitialNavigatedTo(INavigationParameters parameters)
        {
            if (Booking == null)
            {
                Booking = parameters["Item"] as InpatientBooking;
                Title = Booking?.Patient.FullnameReverse;
                _eventAggregator.GetEvent<PatientDetailOpenedEvent>().Publish(Booking);
            }

            return Task.CompletedTask;
        }
    }
}
