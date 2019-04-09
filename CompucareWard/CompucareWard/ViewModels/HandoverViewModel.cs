using CompucareWard.Events;
using CompucareWard.Helpers;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CompucareWard.ViewModels
{
    public class HandoverViewModel : BaseViewModel
    {
        private readonly INurseService _nursesService;
        private readonly IInpatientBookingService _inpatientBookingService;
        private CodeTable _selectedNurse;
        private ObservableCollection<CodeTable> _nurses = new ObservableCollection<CodeTable>();
        private ObservableCollection<SelectableObject<InpatientBooking>> _bookings;

        public ObservableCollection<SelectableObject<InpatientBooking>> Bookings
        {
            get => _bookings;
            set => SetProperty(ref _bookings, value);
        }

        public CodeTable SelectedNurse
        {
            get => _selectedNurse;
            set => SetProperty(ref _selectedNurse, value);
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SelectNurseCommand { get; private set; }

        public HandoverViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IInpatientBookingService inpatientBookingService,
            IPatientService patientService, IEventAggregator eventAggregator, INurseService nurseService, IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            SaveCommand = new DelegateCommand(async () => await Save(), () => SelectedNurse != null).ObservesProperty(() => SelectedNurse);
            CancelCommand = new DelegateCommand(async () => await Cancel());
            SelectNurseCommand = new DelegateCommand(async () => await SelectNurse());

            _nursesService = nurseService;
            _inpatientBookingService = inpatientBookingService;
        }

        async Task Cancel() => await _navigationService.GoBackAsync(useModalNavigation: true);

        async Task SelectNurse()
        {
            await _navigationService.NavigateAsync(nameof(Views.SelectionPage), new NavigationParameters {
                { ParameterNames.Items, _nurses },
                { ParameterNames.SelectedItem, SelectedNurse },
                { ParameterNames.Title, "Handover Nurse" }
            });
        }

        async Task Save()
        {
            if (Bookings.Where(b => b.IsSelected).Select(b => b.Value.InpatientBookingId).ToArray() is int[] bookingsToHandover && bookingsToHandover.Count() > 0)
            {
                if (await HandleAPICall(async () => await SaveHandover(bookingsToHandover), handleMode: HandleMode.RetryDiscard, failedAction: Cancel) == APIActionResult.Successfull)
                {
                    _eventAggregator.GetEvent<NursesHandedOverEvent>().Publish();

                    await HandleAPICall(async () =>
                    {
                        if (bookingsToHandover.Count() == Bookings.Count)
                            await _navigationService.GoBackAsync(useModalNavigation: true);
                        else
                        {
                            SelectedNurse = null;
                            Bookings = new ObservableCollection<SelectableObject<InpatientBooking>>((await _inpatientBookingService.GetMyPatients(User.NurseId, null, _settingsService.AuthAccessToken))
                                                                                                                                   .Select(b => new SelectableObject<InpatientBooking>(b)));
                        }
                    }, handleMode: HandleMode.OKandContinue, failedAction: Cancel);
                }
            }
            else
                await _pageDialogService.DisplayAlertAsync("Handover", "Must Select at least 1 patient to Handover", "OK");
        }

        async Task SaveHandover(int[] bookingsToHandover) => await _inpatientBookingService.SaveNurseHandover(SelectedNurse.Id, bookingsToHandover, _settingsService.AuthAccessToken);

        protected async override Task OnInitialNavigatedTo(INavigationParameters parameters)
        {
            await HandleAPICall(async () =>
            {
                IsBusy = true;
                Bookings = new ObservableCollection<SelectableObject<InpatientBooking>>((await _inpatientBookingService.GetMyPatients(User.NurseId, null, _settingsService.AuthAccessToken))
                                                                                                                       .Select(b => new SelectableObject<InpatientBooking>(b)));

                if (Bookings.Count == 0)
                {
                    await _pageDialogService.DisplayAlertAsync("Handover", "No Patients to Handover", "OK");
                    await Cancel();
                }
                else
                    _nurses = await _nursesService.GetNursesExcluding(User.NurseId, User.Location.SiteId, _settingsService.AuthAccessToken);

                IsBusy = false;
            }, handleMode: HandleMode.RetryCancel, failedAction: Cancel);
        }

        protected override Task OnSubsequentNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetValue<CodeTable>(ParameterNames.SelectedItem) is CodeTable selectedNurse)
                SelectedNurse = selectedNurse;

            return Task.CompletedTask;
        }
    }
}