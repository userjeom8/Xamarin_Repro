using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Linq;
using CompucareWard.Models;
using CompucareWard.Views;
using Prism.Navigation;
using Prism.Services;
using CompucareWard.Services;
using Prism.Commands;
using CompucareWard.Services.Settings;
using Xamarin.Essentials;
using CompucareWard.Exceptions;
using CompucareWard.Services.RequestProvider;
using Prism.Events;

namespace CompucareWard.ViewModels
{
    public class MyPatientsViewModel : PatientsBaseViewModel
    {
        public MyPatientsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IInpatientBookingService dataStore, ISettingsService settingsService,
            IEventAggregator eventAggregator, IdentityService identityService)
            : base(navigationService, pageDialogService, dataStore, settingsService, eventAggregator, identityService)
        {

        }

        protected override async Task ExecuteLoadItems()
        {
            if (IsBusy)
                return;

            await HandleAPICall(async () =>
            {
                IsBusy = true;

                var items = await _inpatientBookingService.GetMyPatients(User.NurseId, User.ClinicianId, _settingsService.AuthAccessToken);
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