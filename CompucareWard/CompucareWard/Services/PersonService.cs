using CompucareWard.Models;
using CompucareWard.Services.RequestProvider;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace CompucareWard.Services
{
    public partial class PersonService : IPersonService
    {
        private IRequestProvider _requestProvider;
        private const string ApiUrlBase = "people";

        public PersonService(IRequestProvider requestProvider) => _requestProvider = requestProvider;

        public async Task<User> GetLoggedInUser(string token) => await _requestProvider.GetAsync<User>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/user", token);

        public async Task<ObservableCollection<Contact>> GetContactDetails(int[] personIds, string token)
            => await _requestProvider.GetAsync<ObservableCollection<Contact>>($"{GlobalSettings.Instance.BaseApiEndpoint}/{ApiUrlBase}/Contacts?{string.Join("&", personIds.Select(p => $"personIds={p}"))}", token);
    }
}
