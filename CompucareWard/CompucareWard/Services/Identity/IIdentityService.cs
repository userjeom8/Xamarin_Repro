using CompucareWard.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompucareWard.Services.Identity
{
    public interface IIdentityService
    {
        string CreateAuthorizationRequest();
        string CreateLogoutRequest(string token);
        Task<UserToken> GetTokenAsync(string code);
        Task<UserToken> GetRefreshTokenAsync(string code);
    }
}
