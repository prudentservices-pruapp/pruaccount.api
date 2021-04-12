// <copyright file="ValidateUserTokenClientResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pruaccount.Api.HttpClients.AuthValidationClient
{
    using Pruaccount.Api.Domain.Auth;

    /// <summary>
    /// ValidateUserTokenClientResponse.
    /// </summary>
    public class ValidateUserTokenClientResponse
    {
        /// <summary>
        /// Gets or sets AuthToken.
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// Gets or sets AuthCookieDetails.
        /// </summary>
        public TokenUserDetails AuthCookieDetails { get; set; }

        /// <summary>
        /// Gets or sets APIErrorDetails.
        /// </summary>
        public APIErrorDetails Error { get; set; }
    }
}
