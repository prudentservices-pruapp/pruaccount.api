// <copyright file="APIErrorDetails.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.HttpClients
{
    /// <summary>
    /// APIErrorDetails.
    /// </summary>
    public class APIErrorDetails
    {
        /// <summary>
        /// Gets or sets Code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets Details.
        /// </summary>
        public string Details { get; set; }
    }
}
