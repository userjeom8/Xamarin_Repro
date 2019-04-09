using CompucareWard.Models;
using CompucareWard.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public class GlobalSettingsService : IGlobalSettingsService
    {
        private IRequestProvider _requestProvider;
        private const string ApiUrlBase = "globalsettings";

        public GlobalSettingsService(IRequestProvider requestProvider) => _requestProvider = requestProvider;

        async Task<NEWSSettings> IGlobalSettingsService.GetNEWSSettings(string token)
            => await _requestProvider.GetAsync<NEWSSettings>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/NEWS", token);
    }
}
