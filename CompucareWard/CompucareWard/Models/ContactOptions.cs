using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Plugin.Messaging;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Prism.Services;
using Xamarin.Essentials;
using CompucareWard.Enums;

namespace CompucareWard.Models
{
    public class ContactOptions : BindableBase
    {
        private bool _canMessage;
        private bool _hasPhoneNumber;
        private bool _hasEmail;
        private IList<Contact> _contacts;
        private bool _isResponsibleClinician;
        private bool _isRMO;
        private bool _isResponsibleNurse;

        public bool HasMessaging => _canMessage && HasPhoneNumber;

        public bool HasPhoneNumber
        {
            get => _hasPhoneNumber;
            set => SetProperty(ref _hasPhoneNumber, value, () => RaisePropertyChanged(nameof(HasMessaging)));
        }

        public bool HasEmail
        {
            get => _hasEmail;
            set => SetProperty(ref _hasEmail, value);
        }

        public string FullnameReverse => _contacts?.FirstOrDefault()?.FullnameReverse ?? "-";

        public string RoleText
        {
            get
            {
                string roleText = null;

                if (_isResponsibleNurse)
                    roleText = "Nurse";

                if (_isResponsibleClinician)
                    roleText = $"{(roleText != null ? $"{roleText}/" : "")}Clinician";

                if (_isRMO)
                    roleText = $"{(roleText != null ? $"{roleText}/" : "")}RMO";

                return roleText;
            }
        }

        public IList<Contact> Contacts
        {
            get => _contacts;
            set => SetProperty(ref _contacts, value);
        }

        public async Task ShowContactMenu(IPageDialogService pageDialogService, ContactType? contactTypeFilter = null)
        {
            var contacts = new List<(Contact Contact, string DisplayText)>();

            if (CrossMessaging.Current.PhoneDialer.CanMakePhoneCall && !contactTypeFilter.HasValue)
            {
                foreach (var contact in _contacts)
                {
                    if (contact.IsEmail && CrossMessaging.Current.EmailMessenger.CanSendEmail)
                        contacts.Add((contact, "E-mail"));
                    else
                    {
                        contacts.Add((contact, $"Call - {GetDisplayText(contact.Type)}"));

                        if (CrossMessaging.Current.SmsMessenger.CanSendSms)
                            contacts.Add((contact, $"Message - {GetDisplayText(contact.Type)}"));
                    }
                }
            }
            else
            {
                contacts = _contacts.Where(c => contactTypeFilter == null
                                                || ((c.IsEmail == (contactTypeFilter == ContactType.Email) || (!c.IsEmail == (contactTypeFilter == ContactType.Phone)
                                                    || contactTypeFilter == ContactType.Message))))
                                    .Select(c => (c, c.IsEmail ? c.Value : $"{c.Value} ({GetDisplayText(c.Type)})")).ToList();
            }

            var action = await pageDialogService.DisplayActionSheetAsync($"Escalate to {FullnameReverse}?", "Cancel", null, contacts.Select(c => c.DisplayText).ToArray());

            if (!string.IsNullOrEmpty(action) && action != "Cancel")
            {
                var contact = contacts.Single(c => c.DisplayText == action);

                if (contact.Contact.IsEmail && CrossMessaging.Current.EmailMessenger.CanSendEmail)
                    await Email.ComposeAsync(new EmailMessage(null, null, contact.Contact.Value));
                else if (contact.DisplayText.Contains("Message") && CrossMessaging.Current.SmsMessenger.CanSendSms)
                    await Sms.ComposeAsync(new SmsMessage("", new List<string> { contact.Contact.Value }));
                else if (contact.DisplayText.Contains("Call") && CrossMessaging.Current.PhoneDialer.CanMakePhoneCall)
                    PhoneDialer.Open(contact.Contact.Value);
            }
        }

        static string GetDisplayText(string text) => Regex.Replace(text, "([a-z])([A-Z])", "$1 $2");

        public ContactOptions()
        {

        }

        public ContactOptions(IList<Contact> contacts, InpatientBooking booking) => SetContactOptions(contacts, booking);

        public void SetContactOptions(IList<Contact> contacts, InpatientBooking booking)
        {
            Contacts = contacts;
            _canMessage = CrossMessaging.Current.SmsMessenger.CanSendSms;
            HasPhoneNumber = contacts.Any(c => !c.IsEmail);
            HasEmail = contacts.Any(c => c.IsEmail);

            if (booking != null && contacts.FirstOrDefault() is Contact firstContact)
            {
                _isResponsibleNurse = booking.ResponsibleNurse?.PersonId == firstContact.PersonId;
                _isResponsibleClinician = booking.AttendingClinician?.PersonId == firstContact.PersonId;
                _isRMO = booking.CurrentBed.Location.ResidentMedicalOfficer?.PersonId == firstContact.PersonId;
            }
        }
    }
}
