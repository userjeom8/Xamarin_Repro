using CompucareWard.Controls.AttachedProperties;
using CompucareWard.Events;
using CompucareWard.Models;
using CompucareWard.Notifications;
using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Microsoft.AppCenter.Push;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace CompucareWard.ViewModels
{
    public class RemindersViewModel : PatientsBaseViewModel
    {
        private InpatientBooking _item;
        private string _title;
        private int _badge;
        private ObservableCollection<GroupedNotifications> _reminders;
        private bool _isLoading = false;
        private readonly ILocalNotifications _locationNavigationService;
        private bool _isSelected;

        public InpatientBooking Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public int Badge
        {
            get => _badge;
            set => SetProperty(ref _badge, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public ObservableCollection<GroupedNotifications> Reminders
        {
            get => _reminders;
            set => SetProperty(ref _reminders, value);
        }

        public DelegateCommand<InpatientBooking> EditFrequencyCommand { get; private set; }

        public RemindersViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IInpatientBookingService dataStore, ISettingsService settingsService,
            IEventAggregator eventAggregator, IdentityService identityService, ILocalNotifications locationNavigationService)
            : base(navigationService, pageDialogService, dataStore, settingsService, eventAggregator, identityService)
        {
            _locationNavigationService = locationNavigationService;
            EditFrequencyCommand = new DelegateCommand<InpatientBooking>(async (b) => await _navigationService.NavigateAsync($"{nameof(Views.SmallNavigationPage)}/{nameof(Views.ObservationFrequencyPage)}",
                                                                                                                             new NavigationParameters { { "Item", b } }, useModalNavigation: true));
            _eventAggregator.GetEvent<NotificationSelectedEvent>().Subscribe(NotificationSelected);
            _eventAggregator.GetEvent<NotificationReceived>().Subscribe(NotificationReceived);
        }

        void NotificationReceived(int badge)
        {
            Badge = badge;
        }

        void NotificationSelected()
        {
            if (!IsActive)
                IsSelected = true;

            if (Reminders?.SelectMany(r => r.Where(b => b.IsCurrentReminder)).ToList() is IList<InpatientBooking> currentReminders && currentReminders.Count == 1)
                AddNEWSCommand.Execute(currentReminders.Single());
        }

        protected override async Task ExecuteLoadItems()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            await GetReminders();
            IsBusy = false;
        }

        async Task GetReminders()
        {
            if (_isLoading || !User.NurseId.HasValue)
                return;

            await HandleAPICall(async () =>
            {
                _isLoading = true;

                var items = await _inpatientBookingService.GetItemsByRemindersAsync(User.NurseId.Value, _settingsService.AuthAccessToken);

                Reminders = new ObservableCollection<GroupedNotifications>();
                var currentReminders = new GroupedNotifications() { LongName = "Current Observations" };
                var upcomingReminders = new GroupedNotifications() { LongName = "Upcoming Observations" };

                if (items != null && items.Count != 0)
                {
                    currentReminders.Clear();
                    upcomingReminders.Clear();

                    foreach (var item in items)
                    {
                        if (item.IsReminder)
                        {
                            if (item.IsCurrentReminder)
                                currentReminders.Add(item);
                            else
                                upcomingReminders.Add(item);
                        }
                    }

                    if (currentReminders.Count > 0)
                        Reminders.Add(currentReminders);

                    if (upcomingReminders.Count > 0)
                        Reminders.Add(upcomingReminders);
                }

                _locationNavigationService.UpdateBadge(currentReminders.Count);
                Badge = currentReminders.Count;
            }, refreshToken: false);

            _isLoading = false;
        }
    }
}