using CompucareWard.Models;
using CompucareWard.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public class NurseService : INurseService
    {
        private IRequestProvider _requestProvider;
        private const string ApiUrlBase = "nurses";

        public NurseService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<CodeTable>> GetNursesExcluding(int? excludeNurseId, int? siteId, string token)
        {
            return await _requestProvider.GetAsync<ObservableCollection<CodeTable>>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/excludenurse/{excludeNurseId ?? 0}/site/{siteId}", token);
        }
    }
}
