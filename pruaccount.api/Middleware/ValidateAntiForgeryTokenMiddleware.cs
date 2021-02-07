using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using pruaccount.api.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.Middleware
{
    public class ValidateAntiForgeryTokenMiddleware
    {
        //The RequestDelegate represents the next middleware in the pipeline.
        private readonly RequestDelegate _next;
        private readonly TokenConfigSetting _tokenConfigSetting;
        private readonly ILogger<ValidateAntiForgeryTokenMiddleware> _logger;

        public ValidateAntiForgeryTokenMiddleware(RequestDelegate next, IOptions<TokenConfigSetting> tokenConfigSetting, ILogger<ValidateAntiForgeryTokenMiddleware> logger)
        {
            _next = next;
            _tokenConfigSetting = tokenConfigSetting.Value;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IAntiforgery antiforgery)
        {
            // Call the next delegate/middleware in the pipeline
            string path = context.Request.Path.Value;

            if (HttpMethods.IsPost(context.Request.Method) && !path.StartsWith("/mainctrl"))
            {
                try
                {
                    var antiForgeryHeader = (string)context.Request.Headers[_tokenConfigSetting.AntiforgeryTokenCookieHeader];
                    var antiForgeryCookie = (string)context.Request.Cookies[_tokenConfigSetting.AntiforgeryTokenCookie];

                    if (!string.IsNullOrEmpty(antiForgeryHeader) && !string.IsNullOrEmpty(antiForgeryCookie) && antiForgeryHeader == antiForgeryCookie)
                    {

                        await _next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        return;
                    }

                    //await antiforgery.ValidateRequestAsync(context);
                }
                catch (AntiforgeryValidationException ex)
                {
                    _logger.LogError(ex, $"ValidateAntiForgeryTokenMiddleware context.Request.Path - {path}");
                    context.Response.StatusCode = 400;
                    return;
                }
                
            }
            else
            {
                await _next(context);
            }
        }
    }
}
