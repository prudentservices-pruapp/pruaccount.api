// <copyright file="ValidateAntiForgeryTokenMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Pruaccount.Api.AppSettings;

    /// <summary>
    /// ValidateAntiForgeryTokenMiddleware.
    /// </summary>
    public class ValidateAntiForgeryTokenMiddleware
    {
        // The RequestDelegate represents the next middleware in the pipeline.
        private readonly RequestDelegate next;
        private readonly TokenConfigSetting tokenConfigSetting;
        private readonly ILogger<ValidateAntiForgeryTokenMiddleware> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateAntiForgeryTokenMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate.</param>
        /// <param name="tokenConfigSetting">IOptions TokenConfigSetting.</param>
        /// <param name="logger">logger.</param>
        public ValidateAntiForgeryTokenMiddleware(RequestDelegate next, IOptions<TokenConfigSetting> tokenConfigSetting, ILogger<ValidateAntiForgeryTokenMiddleware> logger)
        {
            this.next = next;
            this.tokenConfigSetting = tokenConfigSetting.Value;
            this.logger = logger;
        }

        /// <summary>
        /// InvokeAsync.
        /// </summary>
        /// <param name="context">HttpContext.</param>
        /// <param name="antiforgery">IAntiforgery.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context, IAntiforgery antiforgery)
        {
            // Call the next delegate/middleware in the pipeline
            string path = context.Request.Path.Value;

            if (HttpMethods.IsPost(context.Request.Method) && !path.StartsWith("/mainctrl"))
            {
                try
                {
                    var antiForgeryHeader = (string)context.Request.Headers[this.tokenConfigSetting.AntiforgeryTokenCookieHeader];
                    var antiForgeryCookie = (string)context.Request.Cookies[this.tokenConfigSetting.AntiforgeryTokenCookie];

                    if (!string.IsNullOrEmpty(antiForgeryHeader) && !string.IsNullOrEmpty(antiForgeryCookie) && antiForgeryHeader == antiForgeryCookie)
                    {
                        await this.next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        return;
                    }

                    // await antiforgery.ValidateRequestAsync(context);
                }
                catch (AntiforgeryValidationException ex)
                {
                    this.logger.LogError(ex, $"ValidateAntiForgeryTokenMiddleware context.Request.Path - {path}");
                    context.Response.StatusCode = 400;
                    return;
                }
            }
            else
            {
                await this.next(context);
            }
        }
    }
}