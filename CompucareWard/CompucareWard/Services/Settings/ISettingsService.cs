using CompucareWard.Models;
using System;
using System.Threading.Tasks;

namespace CompucareWard.Services.Settings
{
    public interface ISettingsService
    {
        string AuthAccessToken { get; set; }
        string AuthIdToken { get; set; }
        string RefreshToken { get; set; }
        int TokenExpiresIn { get; set; }
        DateTime TokenExpiresAt { get; set; }
        string IdentityEndpointBase { get; set; }         

        User User { get; set; }

        string NotificationsCount { get; set; }        

        bool GetValueOrDefault(string key, bool defaultValue);
        string GetValueOrDefault(string key, string defaultValue);
        Task AddOrUpdateValue(string key, bool value);
        Task AddOrUpdateValue(string key, string value);
    }
}
