using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.HttpClients.AuthValidationClient
{
    public class ValidateUserTokenClientRequest
    {
        public string AntiforgeryTokenCookieHeader { get; set; }
        public string AuthCookie { get; set; }
    }
}
