// <copyright file="ValidateUserTokenClientRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pruaccount.Api.HttpClients.AuthValidationClient
{
    using Pruaccount.Api.Domain.Auth;

    /// <summary>
    /// ValidateUserTokenClientRequest.
    /// </summary>
    public class ValidateUserTokenClientRequest
    {
        /// <summary>
        /// Gets or sets AntiforgeryTokenCookieHeader.
        /// </summary>
        public string AntiforgeryTokenCookieHeader { get; set; }

        /// <summary>
        /// Gets or sets AuthCookie.
        /// </summary>
        public string AuthCookie { get; set; }
    }
}
