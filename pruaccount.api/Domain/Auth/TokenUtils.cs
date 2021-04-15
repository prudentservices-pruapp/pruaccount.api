﻿// <copyright file="TokenUtils.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PruAuth.Api.Domain.Auth
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json.Linq;
    using Pruaccount.Api.Domain.Auth;
    using Pruaccount.Api.Extensions;

    /// <summary>
    /// TokenUtils.
    /// </summary>
    public class TokenUtils : ITokenUtils
    {
        private readonly ILogger<TokenUtils> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenUtils"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        public TokenUtils(ILogger<TokenUtils> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// CheckValidTokenForRequest.
        /// </summary>
        /// <param name="requestPath">requestPath.</param>
        /// <returns>bool True or False.</returns>
        public bool CheckValidTokenForRequest(string requestPath)
        {
            bool checkForValidToken = false;

            if (requestPath.Contains("test"))
            {
                return false;
            }

            string[] tokenRequiredForPaths =
            {
                "/api/financialsetting/savesettings",
                "/api/financialsetting/settings",
                "/api/ledgeraccount/list",
                "/api/ledgeraccount/saveledgeraccount",
                "/api/ledgeraccount/search",
                "/api/test/testaccess",
                "/api/test/testuserdetails",
                "/api/test/testposts",
                "/api/test/testserverposts",
            };

            checkForValidToken = tokenRequiredForPaths.CheckIfPathNeedValidToken(requestPath);

            return checkForValidToken;
        }
    }
}