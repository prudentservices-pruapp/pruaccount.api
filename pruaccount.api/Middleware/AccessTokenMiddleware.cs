using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using pruaccount.api.AppSettings;
using pruaccount.api.HttpClients.AuthValidationClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.Middleware
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenConfigSetting _tokenConfigSetting;
        private readonly ILogger<AccessTokenMiddleware> _logger;
        private readonly IValidateUserTokenClient _validateUserTokenClient;

        public AccessTokenMiddleware(RequestDelegate next, IOptions<TokenConfigSetting> tokenConfigSetting, 
                IValidateUserTokenClient validateUserTokenClient, ILogger<AccessTokenMiddleware> logger)
        {
            _next = next;
            _tokenConfigSetting = tokenConfigSetting.Value;
            _logger = logger;
            _validateUserTokenClient = validateUserTokenClient;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!HttpMethods.IsOptions(context.Request.Method))
            {
                string path = string.Empty;
                try
                {
                    path = context.Request.Path.Value;

                    if (path.StartsWith("/api/test", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/mainctrl", StringComparison.OrdinalIgnoreCase))
                    {
                        var authHeader = (string)context.Request.Headers["Authorization"];

                        string tokenString = string.Empty;
                        string reqTokenHeader = authHeader?.ToString().Substring("Bearer ".Length).Trim();

                        string authCookie = context.Request.Cookies[_tokenConfigSetting.AuthCookie] ?? string.Empty;

                        if (authCookie == reqTokenHeader && !string.IsNullOrEmpty(authCookie) && !string.IsNullOrEmpty(reqTokenHeader))
                        //if (!string.IsNullOrEmpty(authCookie))
                        {
                            // Call Httpendpoint for checking the access & token
                            var response = _validateUserTokenClient.ValidateUserToken(new ValidateUserTokenClientRequest()
                            {
                                AuthCookie = authCookie
                            });

                            if (response.Error != null)
                            {
                                _logger.LogError($"AccessTokenMiddleware context.Request.Path - {path}, auth token validation error - {response.Error.Details}");
                                context.Response.StatusCode = 401;
                                return;
                            }
                            else if (!string.IsNullOrEmpty(response.AuthToken) && (!response.AuthToken.Equals(authCookie)))
                            {
                                context.Response.Headers.Add("Set-Authorization", response.AuthToken);
                                context.Response.Cookies.Append(_tokenConfigSetting.AuthCookie, response.AuthToken, new CookieOptions() { HttpOnly = true, Secure = true, Domain = _tokenConfigSetting.CookieDomain, SameSite = SameSiteMode.Strict });
                                context.Response.Cookies.Append(_tokenConfigSetting.AuthUserCookie, response.AuthToken, new CookieOptions() { HttpOnly = false, Secure = true, Domain = _tokenConfigSetting.CookieDomain, SameSite = SameSiteMode.Strict });
                            }
                        }
                        else
                        {
                            _logger.LogError($"AccessTokenMiddleware context.Request.Path - {path}, auth token validation error");
                            context.Response.StatusCode = 401;
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"AccessTokenMiddleware context.Request.Path - {path},  internal server error");
                    context.Response.StatusCode = 500;
                    return;
                }
            }

            await _next(context);
        }
    }
}
