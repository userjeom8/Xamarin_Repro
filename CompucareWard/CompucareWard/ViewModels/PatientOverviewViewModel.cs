using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CompucareWard.Enums;
using CompucareWard.Events;
using CompucareWard.Models;
using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Plugin.Messaging;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace CompucareWard.ViewModels
{
    public class PatientOverviewViewModel : PatientDetailChildBaseViewModel
    {
        private readonly IPersonService _personService;
        private NextOfKin _nextOfKin;
        private readonly IPatientService _patientService;
        private ContactOptions _responsibleClinicianContactOptions = new ContactOptions();
        private ContactOptions _responsibleNurseContactOptions = new ContactOptions();
        private ObservableCollection<Contact> _contacts;
        private ContactOptions _residentMedicalOfficerContactOptions = new ContactOptions();

        public NextOfKin NextOfKin
        {
            get => _nextOfKin;
            set => SetProperty(ref _nextOfKin, value);
        }

        public ContactOptions ResponsibleClinicianContactOptions
        {
            get => _responsibleClinicianContactOptions;
            set => SetProperty(ref _responsibleClinicianContactOptions, value);
        }

        public ContactOptions ResponsibleNurseContactOptions
        {
            get => _responsibleNurseContactOptions;
            set => SetProperty(ref _responsibleNurseContactOptions, value);
        }

        public ContactOptions ResidentMedicalOfficerContactOptions
        {
            get => _residentMedicalOfficerContactOptions;
            set => SetProperty(ref _residentMedicalOfficerContactOptions, value);
        }

        public DelegateCommand<ContactType?> ContactClinicianCommand { get; private set; }
        public DelegateCommand<ContactType?> ContactNurseCommand { get; private set; }
        public DelegateCommand<ContactType?> ContactRMOCommand { get; private set; }
        public DelegateCommand EditFrequencyCommand { get; private set; }

        public PatientOverviewViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IPatientService patientService, 
            IEventAggregator eventAggregator, IdentityService identityService, IPersonService personService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            _patientService = patientService;
            _personService = personService;

            ContactNurseCommand = new DelegateCommand<ContactType?>(async (c) => await Contact(c, ResponsibleNurseContactOptions));
            ContactClinicianCommand = new DelegateCommand<ContactType?>(async (c) => await Contact(c, ResponsibleClinicianContactOptions));
            ContactRMOCommand = new DelegateCommand<ContactType?>(async (c) => await Contact(c, ResidentMedicalOfficerContactOptions));
            EditFrequencyCommand = new DelegateCommand(async () => await EditFrequency());
        }

        async Task EditFrequency()
            => await _navigationService.NavigateAsync($"{nameof(Views.SmallNavigationPage)}/{nameof(Views.ObservationFrequencyPage)}", new NavigationParameters { { "Item", Booking } }, useModalNavigation: true);

        async Task Contact(ContactType? contact, ContactOptions contactOption)
        {
            switch (contact.Value)
            {
                case ContactType.Phone:
                    if (contactOption.Contacts.Count(c => !c.IsEmail) == 1 && CrossMessaging.Current.PhoneDialer.CanMakePhoneCall)
                        PhoneDialer.Open(contactOption.Contacts.Single(c => !c.IsEmail).Value);
                    else
                        await contactOption.ShowContactMenu(_pageDialogService, contactTypeFilter: ContactType.Phone);
                    break;
                case ContactType.Message:
                    if (contactOption.Contacts.Count(c => !c.IsEmail) == 1 && CrossMessaging.Current.SmsMessenger.CanSendSms)
                        await Sms.ComposeAsync(new SmsMessage("", contactOption.Contacts.Where(c => !c.IsEmail).Select(c => c.Value)));
                    else
                        await contactOption.ShowContactMenu(_pageDialogService, contactTypeFilter: ContactType.Message);
                    break;
                case ContactType.Email:
                default:
                    if (contactOption.Contacts.Count(c => !c.IsEmail) == 1 && CrossMessaging.Current.EmailMessenger.CanSendEmail)
                        await Email.ComposeAsync(new EmailMessage(null, null, contactOption.Contacts.Single(c => c.IsEmail).Value));
                    else
                        await contactOption.ShowContactMenu(_pageDialogService, contactTypeFilter: ContactType.Email);
                    break;
            }
        }

        protected override async Task Initialise()
        {
            IsBusy = true;

            NextOfKin = await _patientService.GetPatientsPrimaryNextOfKin(Booking.Patient.PatientId, _settingsService.AuthAccessToken);

            var contacts = new List<int>();

            if (Booking.AttendingClinician != null)
                contacts.Add(Booking.AttendingClinician.PersonId);

            if (Booking.ResponsibleNurse != null)
                contacts.Add(Booking.ResponsibleNurse.PersonId);

            if (Booking.CurrentBed.Location.ResidentMedicalOfficer != null)
                contacts.Add(Booking.CurrentBed.Location.ResidentMedicalOfficer.PersonId);

            if (contacts.Any())
            {
                _contacts = await _personService.GetContactDetails(contacts.ToArray(), _settingsService.AuthAccessToken);
                ResponsibleClinicianContactOptions.SetContactOptions(_contacts.Where(c => c.PersonId == Booking.AttendingClinician?.PersonId).ToList(), null);
                ResponsibleNurseContactOptions.SetContactOptions(_contacts.Where(c => c.PersonId == Booking.ResponsibleNurse?.PersonId).ToList(), null);
                ResidentMedicalOfficerContactOptions.SetContactOptions(_contacts.Where(c => c.PersonId == Booking.CurrentBed.Location.ResidentMedicalOfficer?.PersonId).ToList(), null);
            }

            IsBusy = false;
        }
    }
}
