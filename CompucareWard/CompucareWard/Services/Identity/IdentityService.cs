using CompucareWard.Models;
using CompucareWard.Services.Identity;
using CompucareWard.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using PCLCrypto;
using static PCLCrypto.WinRTCrypto;
using CompucareWard.Helpers;

namespace CompucareWard.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IRequestProvider _requestProvider;
        private string _codeVerifier;

        public IdentityService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public string CreateAuthorizationRequest()
        {
            if (string.IsNullOrEmpty(GlobalSettings.Instance.AuthorizeEndpoint))
                return string.Empty;

            // Create URI to authorization endpoint
            var authorizeRequest = new AuthorizeRequest(GlobalSettings.Instance.AuthorizeEndpoint);

            // Dictionary with values for the authorize request
            var dic = new Dictionary<string, string>();
            dic.Add("client_id", GlobalSettings.Instance.ClientId);
            dic.Add("client_secret", GlobalSettings.Instance.ClientSecret);
            dic.Add("response_type", "code id_token");
            dic.Add("scope", "openid profile offline_access WardApi");
            dic.Add("redirect_uri", GlobalSettings.Instance.Callback);
            dic.Add("nonce", Guid.NewGuid().ToString("N"));
            dic.Add("code_challenge", CreateCodeChallenge());
            dic.Add("code_challenge_method", "S256");

            // Add CSRF token to protect against cross-site request forgery attacks.
            var currentCSRFToken = Guid.NewGuid().ToString("N");
            dic.Add("state", currentCSRFToken);

            var authorizeUri = authorizeRequest.Create(dic);
            return authorizeUri;
        }

        public string CreateLogoutRequest(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            return string.Format("{0}?id_token_hint={1}&post_logout_redirect_uri={2}",
                GlobalSettings.Instance.LogoutEndpoint,
                token,
                GlobalSettings.Instance.LogoutCallback);
        }

        public async Task<UserToken> GetTokenAsync(string code)
        {
            string data = string.Format("grant_type=authorization_code&code={0}&redirect_uri={1}&code_verifier={2}", code, WebUtility.UrlEncode(GlobalSettings.Instance.Callback), _codeVerifier);
            var token = await _requestProvider.PostAsync<UserToken>(GlobalSettings.Instance.TokenEndpoint, data, GlobalSettings.Instance.ClientId, GlobalSettings.Instance.ClientSecret);
            return token;
        }
        public async Task<UserToken> GetRefreshTokenAsync(string code)
        {
            string data = string.Format("grant_type=refresh_token&refresh_token={0}&redirect_uri={1}&code_verifier={2}", code, WebUtility.UrlEncode(GlobalSettings.Instance.Callback), _codeVerifier);
            var token = await _requestProvider.PostAsync<UserToken>(GlobalSettings.Instance.TokenEndpoint, data, GlobalSettings.Instance.ClientId, GlobalSettings.Instance.ClientSecret);
            return token;
        }

        private string CreateCodeChallenge()
        {
            _codeVerifier = RandomNumberGenerator.CreateUniqueId();
            var sha256 = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha256);
            var challengeBuffer = sha256.HashData(CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes(_codeVerifier)));
            byte[] challengeBytes;
            CryptographicBuffer.CopyToByteArray(challengeBuffer, out challengeBytes);
            return Base64Url.Encode(challengeBytes);
        }   
    }
}
