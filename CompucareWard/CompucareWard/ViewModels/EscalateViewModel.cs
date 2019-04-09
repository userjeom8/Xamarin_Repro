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
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Plugin.Messaging;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace CompucareWard.ViewModels
{
    public class EscalateViewModel : BaseViewModel
    {
        private InpatientBooking _booking;
        private FormResult _news;
        private List<ContactOptions> _contactOptions = new List<ContactOptions>();
        private NEWSSettings _newsSettings;

        public InpatientBooking Booking
        {
            get => _booking;
            set => SetProperty(ref _booking, value);
        }

        public FormResult NEWS
        {
            get => _news;
            set => SetProperty(ref _news, value);
        }

        public bool CanEscalate => _contactOptions?.Any(c => c.Contacts?.Any() == true) ?? false;

        public DelegateCommand EscalateCommand { get; private set; }
        public DelegateCommand ContinueCommand { get; private set; }

        public EscalateViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IEventAggregator eventAggregator, IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            EscalateCommand = new DelegateCommand(async () => await Escalate());
            ContinueCommand = new DelegateCommand(async () => await _navigationService.GoBackAsync(new NavigationParameters { { "PatientConfirmed", false } }));
        }

        async Task LoggedOut() => await _navigationService.GoBackAsync();

        async Task Escalate()
        {
            if (_contactOptions.Count == 1)
                await _contactOptions.Single().ShowContactMenu(_pageDialogService);
            else
            {
                var matchingContacts = _contactOptions.Select(c => (Contact:c, DisplayName:$"{c.FullnameReverse} ({c.RoleText})")).ToList();
                var action = await _pageDialogService.DisplayActionSheetAsync($"Who do you want to Escalate to?", "Cancel", null, matchingContacts.Select(c => c.DisplayName).ToArray());

                if (matchingContacts.SingleOrDefault(co => co.DisplayName == action).Contact is ContactOptions selectedOption)
                    await selectedOption.ShowContactMenu(_pageDialogService);
            }
        }

        protected override Task OnInitialNavigatedTo(INavigationParameters parameters)
        {
            Booking = parameters.GetValue<InpatientBooking>("Booking");
            NEWS = parameters.GetValue<FormResult>("NEWS");
            _newsSettings = parameters.GetValue<NEWSSettings>(nameof(NEWSSettings));

            if (parameters.GetValue<ObservableCollection<Contact>>("Contacts") is ObservableCollection<Contact> contacts && contacts.Any())
                _contactOptions.AddRange(contacts.GroupBy(c => c.PersonId).Select(g => new ContactOptions(g.ToList(), Booking)));
            else
                _contactOptions.Add(new ContactOptions(new ObservableCollection<Contact>(), null));

            RaisePropertyChanged(nameof(CanEscalate));
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