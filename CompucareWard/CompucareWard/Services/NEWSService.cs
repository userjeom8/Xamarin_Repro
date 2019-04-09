using CompucareWard.Models;
using CompucareWard.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public class NEWSService : INEWSService
    {
        private IRequestProvider _requestProvider;
        private const string ApiUrlBase = "formresults";

        public NEWSService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<FormResult> GetNewFormResult(int patientId, int bookingId, int episodeOfCareId, string token)
        {
            return await _requestProvider.GetAsync<FormResult>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/NEW/Patient/{patientId}/Booking/{bookingId}/EpisodeOfCare/{episodeOfCareId}", token);
        }

        public async Task<ObservableCollection<FormResultSimplified>> GetNEWSScoresForBooking(int commonBookingId, string token)
        {
            return await _requestProvider.GetAsync<ObservableCollection<FormResultSimplified>>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/commonbooking/{commonBookingId}/", token);
        }

        public async Task SaveItemAsync(FormResult item, string token)
        {
            await _requestProvider.PostAsync($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}", item, token);
        }
    }
}
