using CompucareWard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public interface ILocationService
    {
        Task<ObservableCollection<CodeTable>> GetItemsAsync(string token);
    }
}
