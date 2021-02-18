// <copyright file="AntiforgeryTokenMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Pruaccount.Api.AppSettings;

    /// <summary>
    /// AntiforgeryTokenMiddleware.
    /// </summary>
    public class AntiforgeryTokenMiddleware
    {
        private readonly RequestDelegate next;
        private readonly TokenConfigSetting tokenConfigSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="AntiforgeryTokenMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate.</param>
        /// <param name="tokenConfigSetting">IOptions TokenConfigSetting.</param>
        public AntiforgeryTokenMiddleware(RequestDelegate next, IOptions<TokenConfigSetting> tokenConfigSetting)
        {
            this.next = next;
            this.tokenConfigSetting = tokenConfigSetting.Value;
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

            if (!path.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
            {
                // The request token can be sent as a JavaScript-readable cookie
                // and Angular uses it by default.
                var tokens = antiforgery.GetAndStoreTokens(context);

                // context.Request.Host.Host
                var coptions = new CookieOptions() { HttpOnly = false, Secure = true, };
                string url = $"{context.Request.Scheme}://{context.Request.Host.Host}";
                Uri myUri = new Uri(url);

                if (myUri.HostNameType == UriHostNameType.Dns && myUri.Host != "localhost")
                {
                    var indexofdot = myUri.Host.IndexOf('.');
                    coptions.Domain = myUri.Host.Substring(indexofdot);
                }

                context.Response.Cookies.Append(this.tokenConfigSetting.AntiforgeryTokenCookie, tokens.RequestToken, coptions);
            }

            await this.next(context);
        }
    }
}
