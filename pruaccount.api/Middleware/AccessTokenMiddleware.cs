﻿// <copyright file="AccessTokenMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Pruaccount.Api.AppSettings;
    using Pruaccount.Api.Domain.Auth;
    using Pruaccount.Api.Extensions;
    using Pruaccount.Api.HttpClients.AuthValidationClient;

    /// <summary>
    /// AccessTokenMiddleware.
    /// </summary>
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate next;
        private readonly TokenConfigSetting tokenConfigSetting;
        private readonly ILogger<AccessTokenMiddleware> logger;
        private readonly IValidateUserTokenClient validateUserTokenClient;
        private readonly ITokenUtils tokenUtils;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessTokenMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate.</param>
        /// <param name="tokenConfigSetting">IOptions TokenConfigSetting.</param>
        /// <param name="validateUserTokenClient">IValidateUserTokenClient.</param>
        /// <param name="tokenUtils">ITokenUtils.</param>
        /// <param name="logger">logger.</param>
        public AccessTokenMiddleware(
                RequestDelegate next,
                IOptions<TokenConfigSetting> tokenConfigSetting,
                IValidateUserTokenClient validateUserTokenClient,
                ITokenUtils tokenUtils,
                ILogger<AccessTokenMiddleware> logger)
        {
            this.next = next;
            this.tokenConfigSetting = tokenConfigSetting.Value;
            this.validateUserTokenClient = validateUserTokenClient;
            this.tokenUtils = tokenUtils;
            this.logger = logger;
        }

        /// <summary>
        /// InvokeAsync.
        /// </summary>
        /// <param name="context">HttpContext.</param>
        /// <returns>Task.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!HttpMethods.IsOptions(context.Request.Method))
            {
                string path = string.Empty;

                try
                {
                    path = context.Request.Path.Value;

                    if (this.tokenUtils.CheckValidTokenForRequest(path))
                    {
                        var authHeader = (string)context.Request.Headers["Authorization"];

                        string tokenString = string.Empty;
                        string reqTokenHeader = authHeader?.ToString().Substring("Bearer ".Length).Trim();

                        string authCookie = context.Request.Cookies[this.tokenConfigSetting.AuthCookie] ?? string.Empty;

                        // if (!string.IsNullOrEmpty(authCookie))
                        if (!string.IsNullOrEmpty(authCookie) && !string.IsNullOrEmpty(reqTokenHeader))
                        {
                            // Call Httpendpoint for checking the access & token
                            var response = this.validateUserTokenClient.ValidateUserToken(new ValidateUserTokenClientRequest()
                            {
                                AuthCookie = authCookie,
                            });

                            context.Items["CurrentTokenUserDetails"] = response.AuthCookieDetails;

                            if (response.Error != null)
                            {
                                this.logger.LogError($"AccessTokenMiddleware context.Request.Path - {path}, auth token validation error - {response.Error.Details}");
                                context.Response.StatusCode = response.Error.Code;
                                return;
                            }
                            else if (!string.IsNullOrEmpty(response.AuthToken) && (!response.AuthToken.Equals(authCookie)))
                            {
                                context.Response.Headers.Add("Set-Authorization", response.AuthToken);
                                context.Response.Cookies.Append(this.tokenConfigSetting.AuthCookie, response.AuthToken, new CookieOptions() { HttpOnly = true, Secure = true, Domain = this.tokenConfigSetting.CookieDomain, SameSite = SameSiteMode.Strict });
                                context.Response.Cookies.Append(this.tokenConfigSetting.AuthUserCookie, response.AuthToken, new CookieOptions() { HttpOnly = false, Secure = true, Domain = this.tokenConfigSetting.CookieDomain, SameSite = SameSiteMode.Strict });
                            }
                        }
                        else
                        {
                            this.logger.LogError($"AccessTokenMiddleware context.Request.Path - {path}, auth token validation error");
                            context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, $"AccessTokenMiddleware context.Request.Path - {path},  internal server error");
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    return;
                }
            }

            await this.next(context);
        }
    }
}