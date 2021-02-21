// <copyright file="StringExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Extensions
{
    /// <summary>
    /// StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// CheckIfPathNeedValidToken.
        /// </summary>
        /// <param name="args">args.</param>
        /// <param name="path">path.</param>
        /// <returns>True or False.</returns>
        public static bool CheckIfPathNeedValidToken(this string[] args, string path)
        {
            bool needsValidToken = false;

            foreach (string str in args)
            {
                if (path.ToLower().StartsWith(str))
                {
                    needsValidToken = true;
                    break;
                }
            }

            return needsValidToken;
        }
    }
}
