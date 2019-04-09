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
    public class SelectionViewModel : BaseViewModel
    {
        private ObservableCollection<SelectableObject<CodeTable>> _items;
        private string _title;

        public ObservableCollection<SelectableObject<CodeTable>> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public DelegateCommand<SelectableObject<CodeTable>> SelectCommand { get; private set; }

        public SelectionViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IEventAggregator eventAggregator, 
            IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            SelectCommand = new DelegateCommand<SelectableObject<CodeTable>>(async (i) => await _navigationService.GoBackAsync(new NavigationParameters { { ParameterNames.SelectedItem, i.Value } }));
        }

        protected override Task OnInitialNavigatedTo(INavigationParameters parameters)
        {
            var selectedItem = parameters.GetValue<CodeTable>(ParameterNames.SelectedItem);
            Items = new ObservableCollection<SelectableObject<CodeTable>>(parameters.GetValue<ObservableCollection<CodeTable>>(ParameterNames.Items)
                                                                                    .Select(ct => new SelectableObject<CodeTable>(ct) { IsSelected = ct == selectedItem }));
            Title = parameters.GetValue<string>(ParameterNames.Title);
            return Task.CompletedTask;
        }
    }
}