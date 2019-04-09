using CompucareWard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public interface IPersonService
    {
        Task<User> GetLoggedInUser(string token);
        Task<ObservableCollection<Contact>> GetContactDetails(int[] personIds, string token);
    }
}
