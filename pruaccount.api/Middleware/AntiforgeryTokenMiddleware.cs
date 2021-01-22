using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using pruaccount.api.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.Middleware
{
    public class AntiforgeryTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenConfigSetting _tokenConfigSetting;

        public AntiforgeryTokenMiddleware(RequestDelegate next, IOptions<TokenConfigSetting> tokenConfigSetting)
        {
            _next = next;
            _tokenConfigSetting = tokenConfigSetting.Value;
        }

        public async Task InvokeAsync(HttpContext context, IAntiforgery antiforgery)
        {
            // Call the next delegate/middleware in the pipeline
            string path = context.Request.Path.Value;

            if (!path.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
            {
                // The request token can be sent as a JavaScript-readable cookie, 
                // and Angular uses it by default.
                var tokens = antiforgery.GetAndStoreTokens(context);

                //context.Request.Host.Host
                var coptions = new CookieOptions() { HttpOnly = false, Secure = true, };
                string url = $"{context.Request.Scheme}://{context.Request.Host.Host}";
                Uri myUri = new Uri(url);

                if (myUri.HostNameType == UriHostNameType.Dns && myUri.Host != "localhost")
                {
                    var indexofdot = myUri.Host.IndexOf('.');
                    coptions.Domain = myUri.Host.Substring(indexofdot);
                }

                context.Response.Cookies.Append(_tokenConfigSetting.AntiforgeryTokenCookie, tokens.RequestToken, coptions);
            }

            await _next(context);
        }
    }
}
