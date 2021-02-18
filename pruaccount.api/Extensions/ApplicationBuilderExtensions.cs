// <copyright file="ApplicationBuilderExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Pruaccount.Api.Middleware;

    /// <summary>
    /// ApplicationBuilderExtensions.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// ValidateAntiforgeryTokens.
        /// </summary>
        /// <param name="app">IApplicationBuilder.</param>
        /// <returns>Static IApplicationBuilder.</returns>
        public static IApplicationBuilder ValidateAntiforgeryTokens(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();
        }

        /// <summary>
        /// UseAntiforgeryToken.
        /// </summary>
        /// <param name="app">IApplicationBuilder.</param>
        /// <returns>Static IApplicationBuilder.</returns>
        public static IApplicationBuilder UseAntiforgeryToken(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AntiforgeryTokenMiddleware>();
        }

        /// <summary>
        /// UseAccessToken.
        /// </summary>
        /// <param name="app">IApplicationBuilder.</param>
        /// <returns>Static IApplicationBuilder.</returns>
        public static IApplicationBuilder UseAccessToken(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AccessTokenMiddleware>();
        }
    }
}
