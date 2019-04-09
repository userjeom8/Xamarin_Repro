using CompucareWard.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public interface IGlobalSettingsService
    {
        Task<NEWSSettings> GetNEWSSettings(string token);
    }
}
