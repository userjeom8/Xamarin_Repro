using CompucareWard.Models;
using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompucareWard.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IEventAggregator eventAggregator, IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {

        }
    }
}
