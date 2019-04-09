using CompucareWard.Models;
using CompucareWard.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CompucareWard.Services
{
    public class InpatientBookingService : IInpatientBookingService
    {
        private IRequestProvider _requestProvider;
        private const string ApiUrlBase = "inpatientbookings";

        public InpatientBookingService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task SaveItemAsync(InpatientBooking item)
        {
            var uri = new UriBuilder(GlobalSettings.Instance.BaseApiEndpoint) { Path = $"{ApiUrlBase}" }.ToString();
            await _requestProvider.PostAsync($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}", item);
        }

        public async Task<InpatientBooking> GetItemAsync(int id, string token)
        {
            return await _requestProvider.GetAsync<InpatientBooking>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/{id}", token);
        }

        public async Task<ObservableCollection<InpatientBooking>> GetItemsByLocationAsync(int locationId, string token)
        {
            return await _requestProvider.GetAsync<ObservableCollection<InpatientBooking>>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/location/{locationId}", token);
        }

        public async Task<ObservableCollection<InpatientBooking>> GetMyPatients(int? responsibleNurseId, int? clinicianId, string token)
        {            
            return await _requestProvider.GetAsync<ObservableCollection<InpatientBooking>>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/responsiblenurse/{responsibleNurseId ?? 0}/clinician/{clinicianId ?? 0}", token);
        }

        public async Task<ObservableCollection<InpatientBooking>> GetItemsByRemindersAsync(int responsibleNurseId, string token)
        {
            return await _requestProvider.GetAsync<ObservableCollection<InpatientBooking>>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/reminders/{responsibleNurseId}", token);
        }

        public async Task SaveNurseHandover(int handoverNurseId, int[] inpatientBookingsIds, string token)
        {
            await _requestProvider.PutAsync($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/handover", new { InpatientBookingIds = inpatientBookingsIds, HandoverNurseId = handoverNurseId }, token);
        }
        
        public async Task SaveObservationFrequency(int inpatientBookingId, int frequencyInMinutes, string token)
        {
            await _requestProvider.PutAsync($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/frequency/{inpatientBookingId}/{frequencyInMinutes}", string.Empty, token);
        }
    }
}