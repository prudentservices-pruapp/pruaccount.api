// <copyright file="IValidateUserTokenClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.HttpClients.AuthValidationClient
{
    /// <summary>
    /// IValidateUserTokenClient.
    /// </summary>
    public interface IValidateUserTokenClient
    {
        /// <summary>
        /// ValidateUserToken.
        /// </summary>
        /// <param name="request">ValidateUserTokenClientRequest.</param>
        /// <returns>ValidateUserTokenClientResponse.</returns>
        ValidateUserTokenClientResponse ValidateUserToken(ValidateUserTokenClientRequest request);
    }
}