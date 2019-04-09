using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CompucareWard.Events;
using CompucareWard.Models;
using CompucareWard.Services;
using CompucareWard.Services.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CompucareWard.ViewModels
{
    public class PatientObservationsViewModel : PatientDetailChildBaseViewModel
    {
        private readonly INEWSService _newsService;
        private ObservableCollection<GroupedFormResult> _newsResults;

        public ObservableCollection<GroupedFormResult> NEWSResults
        {
            get => _newsResults;
            set => SetProperty(ref _newsResults, value);
        }

        public PatientObservationsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISettingsService settingsService, IEventAggregator eventAggregator, INEWSService newsService, IdentityService identityService)
            : base(navigationService, pageDialogService, settingsService, eventAggregator, identityService)
        {
            _newsService = newsService;
        }

        protected async override Task Initialise()
        {
            IsBusy = true;
            var results = await _newsService.GetNEWSScoresForBooking(Booking.CommonBookingId, _settingsService.AuthAccessToken);

            var sortedResults = results.SelectMany(r => r.FormComponentResults.Select(fcr => new SimpleResult { DateTime = r.CreateDate, FormComponentResult = fcr }))
                                       .GroupBy(r => new { r.FormComponentResult.Caption, r.FormComponentResult.UnitAbbreviation })
                                       .Select(g => new GroupedFormResult
                                       {
                                           Caption = g.Key.Caption,
                                           UnitAbbreviation = g.Key.UnitAbbreviation,
                                           Results = new ObservableCollection<SimpleResult>(g.ToList())
                                       })
                                       .Where(g => g.Results.Any(r => !string.IsNullOrEmpty(r.FormComponentResult.Result)))
                                       .ToList();

            NEWSResults = new ObservableCollection<GroupedFormResult>(sortedResults);
            IsBusy = false;
        }
    }
}