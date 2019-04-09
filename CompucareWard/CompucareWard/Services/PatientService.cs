using CompucareWard.Models;
using CompucareWard.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public class PatientService : IPatientService
    {
        private IRequestProvider _requestProvider;
        private const string ApiUrlBase = "patients";

        public PatientService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<NextOfKin> GetPatientsPrimaryNextOfKin(int patientId, string token)
        {
            return await _requestProvider.GetAsync<NextOfKin>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/{patientId}/nextofkin", token);
        }

        public async Task<ObservableCollection<Alert>> GetPatientAlerts(int patientId, string token)
        {
            return await _requestProvider.GetAsync<ObservableCollection<Alert>>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/{patientId}/alerts", token);
        }
    }
}
