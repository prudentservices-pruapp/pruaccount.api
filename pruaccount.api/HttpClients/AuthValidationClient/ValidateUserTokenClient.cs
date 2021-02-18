// <copyright file="ValidateUserTokenClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.HttpClients.AuthValidationClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Pruaccount.Api.AppSettings;

    /// <summary>
    /// ValidateUserTokenClient.
    /// </summary>
    public class ValidateUserTokenClient : IValidateUserTokenClient
    {
        private readonly ILogger<ValidateUserTokenClient> logger;
        private readonly HttpClient client;
        private readonly TokenConfigSetting tokenConfigSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserTokenClient"/> class.
        /// </summary>
        /// <param name="httpClient">HttpClient.</param>
        /// <param name="tokenConfigSetting">IOptions TokenConfigSetting.</param>
        /// <param name="logger">logger.</param>
        public ValidateUserTokenClient(HttpClient httpClient, IOptions<TokenConfigSetting> tokenConfigSetting, ILogger<ValidateUserTokenClient> logger)
        {
            this.client = httpClient;
            this.tokenConfigSetting = tokenConfigSetting.Value;
            this.logger = logger;
        }

        /// <summary>
        /// ValidateUserToken.
        /// </summary>
        /// <param name="request">ValidateUserTokenClientRequest.</param>
        /// <returns>ValidateUserTokenClientResponse.</returns>
        public ValidateUserTokenClientResponse ValidateUserToken(ValidateUserTokenClientRequest request)
        {
            ValidateUserTokenClientResponse response = new ValidateUserTokenClientResponse();

            response.AuthToken = request.AuthCookie;

            IEnumerable<string> authRequestCookies = this.AuthAntiforgeryCookies();
            IEnumerable<string> authResponseCookies = null;

            if (authRequestCookies != null)
            {
                try
                {
                    using (var httpClient = new HttpClient(new HttpClientHandler { UseCookies = false }))
                    {
                        string url = this.tokenConfigSetting.AuthEndpoint + this.tokenConfigSetting.AuthTokenValidationEndpoint;

                        foreach (string sCookie in authRequestCookies)
                        {
                            // if (sCookie.StartsWith(".AspNetCore"))
                            // {
                                // httpClient.DefaultRequestHeaders.Add("cookie", sCookie);
                            // }
                            // else
                            if (sCookie.StartsWith(this.tokenConfigSetting.AntiforgeryAuthTokenCookie))
                            {
                                httpClient.DefaultRequestHeaders.Add(this.tokenConfigSetting.AntiforgeryAuthTokenCookieHeader, sCookie.Split("=")[1]);
                            }
                        }

                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {request.AuthCookie}");

                        HttpResponseMessage reply = httpClient.PostAsync(url, null).Result;

                        if (reply.Headers.TryGetValues("set-cookie", out authResponseCookies))
                        {
                            foreach (string sCookie in authResponseCookies)
                            {
                                this.logger.LogInformation($"Response - Cookie - {sCookie}");

                                if (sCookie.StartsWith(this.tokenConfigSetting.AuthCookie))
                                {
                                    response.AuthToken = sCookie.Split("=")[1].Replace("; domain", string.Empty);
                                }
                            }
                        }

                        var stream = reply.Content.ReadAsStreamAsync().Result;
                        string responseAuth = string.Empty;

                        if (stream != null)
                        {
                            StreamReader reader = new StreamReader(stream);
                            responseAuth = reader.ReadToEnd();
                        }

                        if (reply.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            response.AuthToken = string.Empty;
                            response.Error = new APIErrorDetails()
                            {
                                Code = (int)reply.StatusCode,
                                Details = JsonConvert.SerializeObject(response),
                                Message = "Token Validation Error",
                            };

                            this.logger.LogError($"ValidateUserTokenClient-> ValidateUserToken Response - {reply.StatusCode.ToString()} {JsonConvert.SerializeObject(response)}");
                        }
                        else
                        {
                            response.Error = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.AuthToken = string.Empty;
                    response.Error = new APIErrorDetails()
                    {
                        Code = -1,
                        Details = ex.Message,
                        Message = "Token Validation Error",
                    };

                    this.logger.LogError($"ValidateUserTokenClient-> ValidateUserToken Exception - {ex.Message}");
                }
            }

            return response;
        }

        private IEnumerable<string> AuthAntiforgeryCookies()
        {
            IEnumerable<string> responseCookies = null;

            try
            {
                string url = this.tokenConfigSetting.AuthEndpoint + this.tokenConfigSetting.AntiforgeryAuthCookieEndpoint;
                HttpResponseMessage reply = this.client.GetAsync(url).Result;

                if (reply.Headers.TryGetValues("set-cookie", out responseCookies))
                {
                    foreach (string sCookie in responseCookies)
                    {
                        this.logger.LogInformation($"Response - Cookie - {sCookie}");
                    }
                }

                var stream = reply.Content.ReadAsStreamAsync().Result;

                string response = string.Empty;

                if (stream != null)
                {
                    StreamReader reader = new StreamReader(stream);
                    response = reader.ReadToEnd();
                }

                if (reply.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    this.logger.LogError($"ValidateUserTokenClient-> AuthAntiforgeryCookies Response - {reply.StatusCode.ToString()} {JsonConvert.SerializeObject(response)}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"ValidateUserTokenClient-> AuthAntiforgeryCookies Exception {ex.Message}");
            }

            return responseCookies;
        }
    }
}
