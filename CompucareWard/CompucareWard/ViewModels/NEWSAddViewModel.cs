using CompucareWard.Models;
using CompucareWard.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using CompucareWard.Services.Settings;
using DevExpress.Core;
using System.Collections.ObjectModel;
using System.Globalization;
using Prism.Plugin.Popups;
using Prism.Events;
using CompucareWard.Events;

namespace CompucareWard.ViewModels
{
    public class NEWSAddViewModel : BaseViewModel
    {
        private bool _patientConfirmed;
        private readonly INEWSService _newsService;
        private readonly IPersonService _personService;
        private readonly IGlobalSettingsService _globalSettingsService;
        private readonly IInpatientBookingService _bookingService;
        private FormResult _news;
        private InpatientBooking _booking;
        private bool _isSaving;

        public InpatientBooking Booking
        {
            get => _booking;
            set => SetProperty(ref _booking, value);
        }

        public bool IsSaving
        {
            get => _isSaving;
            set => SetProperty(ref _isSaving, value);
        }

        public FormResult NEWS
        {
            get => _news;
            set => SetProperty(ref _news, value);
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public NEWSAddViewModel(INavigationService navigationService, IPageDialogService pageDialogService, INEWSService newsService, ISettingsService settingsService,
            IEventAggregator eventAggregator, IdentityService identityService, IPersonService personService, IGlobalSettingsService globalSettingsService, IInpatientBookingService bookingService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            _newsService = newsService;
            _personService = personService;
            _globalSettingsService = globalSettingsService;
            _bookingService = bookingService;

            SaveCommand = new DelegateCommand(async () => await Save(), () => !IsSaving);
            CancelCommand = new DelegateCommand(async () => await Cancel(), () => !IsSaving);
        }

        async Task Cancel() => await _navigationService.GoBackAsync(useModalNavigation: true);

        async Task Save()
        {
            if (NEWS.Validate())
            {
                await HandleAPICall(async () =>
                {
                    IsSaving = true;

                    try
                    {
                        await _navigationService.NavigateAsync(nameof(Views.SavingPage));

                        foreach (var component in NEWS.FormComponentResults.Single().ChildFormComponentResults)
                        {
                            if (!string.IsNullOrWhiteSpace(component.Result) && decimal.Parse(component.Result) is decimal resultAsDecimal)
                            {
                                var numberFormat = new CultureInfo("en-GB", false).NumberFormat;
                                numberFormat.NumberDecimalDigits = component.Precision ?? 0;
                                component.Result = resultAsDecimal.ToString("F", numberFormat);
                            }
                        }

                        var navigationParameters = new NavigationParameters() { { "Booking", Booking }, { "NEWS", NEWS } };
                        var contacts = new List<int>();

                        if (Booking.AttendingClinician != null)
                            contacts.Add(Booking.AttendingClinician.PersonId);

                        if (Booking.CurrentBed.Location.ResidentMedicalOfficer?.PersonId is int rmoId && rmoId != Booking.AttendingClinician?.PersonId)
                            contacts.Add(rmoId);

                        if (contacts.Any())
                            navigationParameters.Add("Contacts", await _personService.GetContactDetails(contacts.ToArray(), _settingsService.AuthAccessToken));

                        _eventAggregator.GetEvent<NEWSAddedEvent>().Publish(Booking.InpatientBookingId);

                        if (NEWS.ThresholdTrigger != null 
                            && (!Booking.ObservationFrequencyInMinutes.HasValue || Booking.ObservationFrequencyInMinutes > NEWS.ThresholdTrigger.ObservationFrequencyInMinutes))
                        {
                            await _bookingService.SaveObservationFrequency(Booking.InpatientBookingId, NEWS.ThresholdTrigger.ObservationFrequencyInMinutes, _settingsService.AuthAccessToken);
                            Booking.ObservationFrequencyInMinutes = NEWS.ThresholdTrigger.ObservationFrequencyInMinutes;
                        }

                        await _newsService.SaveItemAsync(NEWS, _settingsService.AuthAccessToken);
                        await _navigationService.GoBackAsync();
                        await _navigationService.NavigateAsync(nameof(Views.EscalatePage), navigationParameters);

                        IsSaving = false;
                    }
                    catch
                    {
                        await _navigationService.GoBackAsync();
                        throw;
                    }
                }, handleMode: HandleMode.RetryDiscard, failedAction: Cancel);
            }
        }

        async Task PatientConfirmedCheck()
        {
            if (!_patientConfirmed)
                await _navigationService.NavigateAsync(nameof(Views.PatientConfirmationPage), new NavigationParameters { { "Booking", Booking } });
        }

        protected async override Task OnInitialNavigatedTo(INavigationParameters parameters)
        {
            await HandleAPICall(async () =>
            {
                Booking = parameters["Item"] as InpatientBooking;
                NEWS = await _newsService.GetNewFormResult(Booking.Patient.PatientId, Booking.CommonBookingId, Booking.EpisodeOfCareId, _settingsService.AuthAccessToken);
                NEWS.Settings = await _globalSettingsService.GetNEWSSettings(_settingsService.AuthAccessToken);
                await PatientConfirmedCheck();
                NEWS.HookupEvents();
            }, handleMode: HandleMode.RetryCancel, failedAction: Cancel);
        }

        protected override async Task OnSubsequentNavigatedTo(INavigationParameters parameters)
        {
            var confirmed = parameters.GetValue<bool?>("PatientConfirmed");

            if (confirmed.HasValue)
            {
                if (confirmed == true)
                    _patientConfirmed = true;
                else
                    await Cancel();
            }
            else
                await PatientConfirmedCheck();

            NEWS.HookupEvents();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            NEWS.UnHookEvents();
        }
    }
}
