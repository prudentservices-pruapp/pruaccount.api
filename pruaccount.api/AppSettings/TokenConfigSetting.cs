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
        public string CookieDomain { get; set; }
        public string AuthCookie { get; set; }
    }
}
