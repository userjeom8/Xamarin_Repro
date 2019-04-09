using CompucareWard.Models;
using CompucareWard.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public class LocationService : ILocationService
    {
        private IRequestProvider _requestProvider;
        private const string ApiUrlBase = "locations";

        public LocationService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<CodeTable>> GetItemsAsync(string token)
        {
            return await _requestProvider.GetAsync<ObservableCollection<CodeTable>>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}", token);
        }
    }
}
