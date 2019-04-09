using CompucareWard.Models;
using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using Xamarin.Forms;
using Prism.Events;

namespace CompucareWard.ViewModels
{
    public class AllPatientsViewModel : PatientsBaseViewModel
    {
        private CodeTable _selectedWard;
        private readonly ILocationService _locationService;
        private ObservableCollection<CodeTable> _wards = new ObservableCollection<CodeTable>();

        public ObservableCollection<CodeTable> Wards
        {
            get => _wards;
            set => SetProperty(ref _wards, value);
        }

        public CodeTable SelectedWard
        {
            get => _selectedWard;
            set => SetProperty(ref _selectedWard, value, async () => await WardChanged());
        }

        public string SelectedWardName => SelectedWard?.Name;

        public AllPatientsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, ILocationService locationService, IInpatientBookingService inpatientBookingService,
            IEventAggregator eventAggregator, IdentityService identityService)
            : base(navigationService, pageDialogService, inpatientBookingService, settingsService, eventAggregator, identityService)
        {
            _locationService = locationService;
        }

        async Task WardChanged()
        {
            if (SelectedWard != null)
                await ExecuteLoadItems();

            RaisePropertyChanged(nameof(SelectedWardName));
        }

        protected override async Task ExecuteLoadItems()
        {
            if (IsBusy)
                return;

            await HandleAPICall(async () =>
            {
                IsBusy = true;
                var wards = await _locationService.GetItemsAsync(_settingsService.AuthAccessToken);

                if (wards != null)
                {
                    var selectedWardId = SelectedWard?.Id;
                    Wards?.Clear();

                    foreach (var ward in wards)
                        Wards.Add(ward);

                    SelectedWard = Wards.FirstOrDefault(w => w.Id == selectedWardId) ?? Wards.FirstOrDefault(w => w.Id == _settingsService.User.Location.LocationId) ?? Wards.FirstOrDefault();
                }

                var items = await _inpatientBookingService.GetItemsByLocationAsync(SelectedWard?.Id ?? 0, _settingsService.AuthAccessToken);
                await Task.Delay(500);

                if (items != null)
                {
                    Patients.Clear();

                    foreach (var item in items)
                        Patients.Add(item);
                }
            });

            IsBusy = false;
        }
    }
}