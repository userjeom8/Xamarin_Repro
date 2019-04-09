using CompucareWard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public interface INurseService
    {
        Task<ObservableCollection<CodeTable>> GetNursesExcluding(int? excludeNurseId, int? siteId, string token);
    }
}
