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
    public abstract class PatientDetailChildBaseViewModel : BaseViewModel
    {
        private InpatientBooking _booking;
        private string _title;
        private bool _isInitialised = false;

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

        public PatientDetailChildBaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService,
            IEventAggregator eventAggregator, IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            _eventAggregator.GetEvent<PatientDetailOpenedEvent>().Subscribe(async (b) => await PatientSelected(b));
            _eventAggregator.GetEvent<PatientDetailRefreshEvent>().Subscribe(async (b) => await Refresh(b));
        }

        async Task PatientSelected(InpatientBooking booking)
        {
            _eventAggregator.GetEvent<PatientDetailOpenedEvent>().Unsubscribe(async (b) => await PatientSelected(b));
            Booking = booking;
            Title = Booking?.Patient.FullnameReverse;

            await TryInitialise();
        }

        async Task Refresh(InpatientBooking booking)
        {
            if (_isInitialised && booking.InpatientBookingId == Booking?.InpatientBookingId)
            {
                _isInitialised = false;
                await PatientSelected(booking);
            }
        }

        protected async override void OnIsActiveChanged(object sender, EventArgs e)
        {
            base.OnIsActiveChanged(sender, e);

            await TryInitialise();
        }

        async Task TryInitialise()
        {
            if (IsActive && Booking != null)
            {
                if (!_isInitialised)
                {
                    _isInitialised = true;
                    await HandleAPICall(Initialise, failedActionSync: () => _isInitialised = false);
                }
            }
        }

        protected abstract Task Initialise();
    }
}
