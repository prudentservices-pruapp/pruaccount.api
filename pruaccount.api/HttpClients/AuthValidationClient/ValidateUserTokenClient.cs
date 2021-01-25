using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using pruaccount.api.AppSettings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace pruaccount.api.HttpClients.AuthValidationClient
{
    public class ValidateUserTokenClient : IValidateUserTokenClient
    {
        private readonly ILogger<ValidateUserTokenClient> _logger;
        private readonly HttpClient _client;
        private readonly TokenConfigSetting _tokenConfigSetting;

        public ValidateUserTokenClient(HttpClient httpClient, IOptions<TokenConfigSetting> tokenConfigSetting, ILogger<ValidateUserTokenClient> logger)
        {
            _client = httpClient;
            _tokenConfigSetting = tokenConfigSetting.Value;
            _logger = logger;
        }

        public ValidateUserTokenClientResponse ValidateUserToken(ValidateUserTokenClientRequest request)
        {
            ValidateUserTokenClientResponse response = new ValidateUserTokenClientResponse();

            response.AuthToken = request.AuthCookie;

            IEnumerable<string> authRequestCookies = AuthAntiforgeryCookies();
            IEnumerable<string> authResponseCookies = null;

            if (authRequestCookies != null)
            {
                try
                {
                    using (var httpClient = new HttpClient(new HttpClientHandler { UseCookies = false }))
                    {
                        string url = _tokenConfigSetting.AuthEndpoint + _tokenConfigSetting.AuthTokenValidationEndpoint;

                        foreach (string sCookie in authRequestCookies)
                        {
                            //if (sCookie.StartsWith(".AspNetCore"))
                            //{
                                //httpClient.DefaultRequestHeaders.Add("cookie", sCookie);
                            //}
                            //else 
                            if (sCookie.StartsWith(_tokenConfigSetting.AntiforgeryAuthTokenCookie))
                            {
                                httpClient.DefaultRequestHeaders.Add(_tokenConfigSetting.AntiforgeryAuthTokenCookieHeader, sCookie.Split("=")[1]);
                            }
                        }

                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {request.AuthCookie}");

                        HttpResponseMessage reply = httpClient.PostAsync(url, null).Result;

                        if (reply.Headers.TryGetValues("set-cookie", out authResponseCookies))
                        {
                            foreach (string sCookie in authResponseCookies)
                            {
                                _logger.LogInformation($"Response - Cookie - {sCookie}");

                                if (sCookie.StartsWith(_tokenConfigSetting.AuthCookie))
                                {
                                    response.AuthToken = sCookie.Split("=")[1].Replace("; domain", "");
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
                                Message = "Token Validation Error"
                            };

                            _logger.LogError($"ValidateUserTokenClient-> ValidateUserToken Response - {reply.StatusCode.ToString()} {JsonConvert.SerializeObject(response)}");
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
                        Message = "Token Validation Error"
                    };

                    _logger.LogError($"ValidateUserTokenClient-> ValidateUserToken Exception - {ex.Message}");
                }
            }

            return response;
        }

        private IEnumerable<string> AuthAntiforgeryCookies()
        {
            IEnumerable<string> responseCookies = null;

            try
            {

                string url = _tokenConfigSetting.AuthEndpoint + _tokenConfigSetting.AntiforgeryAuthCookieEndpoint;
                HttpResponseMessage reply = _client.GetAsync(url).Result;

                if (reply.Headers.TryGetValues("set-cookie", out responseCookies))
                {
                    foreach (string sCookie in responseCookies)
                    {
                        _logger.LogInformation($"Response - Cookie - {sCookie}");
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
                    _logger.LogError($"ValidateUserTokenClient-> AuthAntiforgeryCookies Response - {reply.StatusCode.ToString()} {JsonConvert.SerializeObject(response)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ValidateUserTokenClient-> AuthAntiforgeryCookies Exception {ex.Message}");
            }

            return responseCookies;
        }
    }
}
