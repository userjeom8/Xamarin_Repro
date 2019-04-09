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
using System.Text;
using System.Threading.Tasks;

namespace CompucareWard.ViewModels
{
    public class PatientConfirmationViewModel : BaseViewModel
    {
        private InpatientBooking _booking;
        public InpatientBooking Booking
        {
            get => _booking;
            set => SetProperty(ref _booking, value);
        }

        public DelegateCommand ConfirmCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public PatientConfirmationViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IEventAggregator eventAggregator, 
            IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            ConfirmCommand = new DelegateCommand(async () => await _navigationService.GoBackAsync(new NavigationParameters { { "PatientConfirmed", true } }));
            CancelCommand = new DelegateCommand(async () => await _navigationService.GoBackAsync(new NavigationParameters { { "PatientConfirmed", false } }));
        }

        async Task LoggedOut() => await _navigationService.GoBackAsync();

        protected override Task OnInitialNavigatedTo(INavigationParameters parameters)
        {
            Booking = parameters.GetValue<InpatientBooking>("Booking");
            _eventAggregator.GetEvent<LoggedOutEvent>().Subscribe(async () => await LoggedOut());
            return Task.CompletedTask;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            _eventAggregator.GetEvent<LoggedOutEvent>().Unsubscribe(async () => await LoggedOut());
        }
    }
}