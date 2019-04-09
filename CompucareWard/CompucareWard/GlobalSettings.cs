using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CompucareWard
{
    class GlobalSettings
    {
        public const string AzureTag = "Azure";
        public const string MockTag = "Mock";
        //public const string DefaultApiEndpoint = "https://dev8-api.streets-heaver.com/Farm/CompucareWardTestAPI2/api";
        //public const string DefaultIdentityEndpoint = "https://dev8-api.streets-heaver.com/Farm/CompucareIdentity/"; //"http://localhost:5000";        
        private string _baseIdentityEndpoint;
        private string _baseApiEndpoint;

        public static GlobalSettings Instance { get; } = new GlobalSettings();

        public string BaseApiEndpoint
        {
            get { return _baseApiEndpoint; }
            set
            {
                _baseApiEndpoint = value;
                UpdateEndpoint(_baseApiEndpoint);
            }
        }        

        public string BaseIdentityEndpoint
        {
            get { return _baseIdentityEndpoint; }
            set
            {
                _baseIdentityEndpoint = value;
                UpdateEndpoint(_baseIdentityEndpoint);
            }
        }       

        public string ClientId { get { return "ccwardapp"; } }

        public string ClientSecret { get { return "&DqyR59XDNB%YdEPTHnwHX^FFFaB3REn8U4d"; } }

        public string AuthToken { get; set; }

        public string RegisterWebsite { get; set; }

        public string AuthorizeEndpoint { get; set; }

        public string UserInfoEndpoint { get; set; }

        public string TokenEndpoint { get; set; }

        public string LogoutEndpoint { get; set; }

        public string Callback { get; set; }

        public string LogoutCallback { get; set; }

        public string GatewayShoppingEndpoint { get; set; }

        public string GatewayMarketingEndpoint { get; set; }

        private void UpdateEndpoint(string endpoint)
        {
            RegisterWebsite = $"{endpoint}/account/register";
            LogoutCallback = $"{endpoint.ToLower()}/account/logout";

            var connectBaseEndpoint = $"{endpoint}/connect";
            AuthorizeEndpoint = $"{connectBaseEndpoint}/authorize";
            UserInfoEndpoint = $"{connectBaseEndpoint}/userinfo";
            TokenEndpoint = $"{connectBaseEndpoint}/token";
            LogoutEndpoint = $"{connectBaseEndpoint}/endsession";

            var baseUri = ExtractBaseUri(endpoint);            
            Callback = $"http://localhost:5000/xamarincallback";
        }

        private string ExtractBaseUri(string endpoint)
        {
            var uri = new Uri(endpoint);
            var baseUri = uri.GetLeftPart(System.UriPartial.Authority);

            return baseUri;
        }
    }
}
