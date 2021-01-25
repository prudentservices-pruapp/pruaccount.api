using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.HttpClients.AuthValidationClient
{
    public class ValidateUserTokenClientResponse
    {
        public string AuthToken { get; set; }
        public APIErrorDetails Error { get; set; }
    }
}
