using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.AppSettings
{
    public class TokenConfigSetting
    {
        public string AntiforgeryTokenCookie { get; set; }
        public string AntiforgeryTokenCookieHeader { get; set; }
        public string AntiforgeryAuthTokenCookie { get; set; }
        public string AntiforgeryAuthTokenCookieHeader { get; set; }
        public string CookieDomain { get; set; }
        public string AuthCookie { get; set; }
        public string AuthUserCookie { get; set; }
        public string AuthEndpoint { get; set; }
        public string AntiforgeryAuthCookieEndpoint { get; set; }
        public string AuthTokenValidationEndpoint { get; set; }
        
    }
}
