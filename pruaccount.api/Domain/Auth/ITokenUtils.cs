// <copyright file="ITokenUtils.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Domain.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// ITokenUtils.
    /// </summary>
    public interface ITokenUtils
    {
        /// <summary>
        /// CheckValidTokenForRequest.
        /// </summary>
        /// <param name="requestPath">RequestPath.</param>
        /// <returns>bool True or False.</returns>
        bool CheckValidTokenForRequest(string requestPath);
    }
}
