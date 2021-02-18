// <copyright file="TokenConfigSetting.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.AppSettings
{
    /// <summary>
    /// Token Config Setting.
    /// </summary>
    public class TokenConfigSetting
    {
        /// <summary>
        /// Gets or Sets AntiforgeryTokenCookie.
        /// </summary>
        public string AntiforgeryTokenCookie { get; set; }

        /// <summary>
        /// Gets or Sets AntiforgeryTokenCookieHeader.
        /// </summary>
        public string AntiforgeryTokenCookieHeader { get; set; }

        /// <summary>
        /// Gets or Sets AntiforgeryAuthTokenCookie.
        /// </summary>
        public string AntiforgeryAuthTokenCookie { get; set; }

        /// <summary>
        /// Gets or Sets AntiforgeryAuthTokenCookieHeader.
        /// </summary>
        public string AntiforgeryAuthTokenCookieHeader { get; set; }

        /// <summary>
        /// Gets or Sets CookieDomain.
        /// </summary>
        public string CookieDomain { get; set; }

        /// <summary>
        /// Gets or Sets AuthCookie.
        /// </summary>
        public string AuthCookie { get; set; }

        /// <summary>
        /// Gets or Sets AuthUserCookie.
        /// </summary>
        public string AuthUserCookie { get; set; }

        /// <summary>
        /// Gets or Sets AuthEndpoint.
        /// </summary>
        public string AuthEndpoint { get; set; }

        /// <summary>
        /// Gets or Sets AntiforgeryAuthCookieEndpoint.
        /// </summary>
        public string AntiforgeryAuthCookieEndpoint { get; set; }

        /// <summary>
        /// Gets or Sets AuthTokenValidationEndpoint.
        /// </summary>
        public string AuthTokenValidationEndpoint { get; set; }
    }
}