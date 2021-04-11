// <copyright file="TokenUserDetails.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Domain.Auth
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TokenUserDetails.
    /// </summary>
    public class TokenUserDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenUserDetails"/> class.
        /// </summary>
        public TokenUserDetails()
        {
            this.Products = new List<string>();
        }

        /// <summary>
        /// Gets or sets Products List.
        /// </summary>
        public List<string> Products { get; set; }

        /// <summary>
        /// Gets or sets Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets ABUniqueId.
        /// </summary>
        public Guid ABUniqueId { get; set; }

        /// <summary>
        /// Gets or sets AUniqueId.
        /// </summary>
        public Guid AUniqueId { get; set; }

        /// <summary>
        /// Gets or sets CBUniqueId.
        /// </summary>
        public Guid CBUniqueId { get; set; }

        /// <summary>
        /// Gets or sets CUniqueId.
        /// </summary>
        public Guid CUniqueId { get; set; }

        /// <summary>
        /// Gets or sets AUUniqueId.
        /// </summary>
        public Guid AUUniqueId { get; set; }

        /// <summary>
        /// Gets or sets CUUniqueId.
        /// </summary>
        public Guid CUUniqueId { get; set; }
    }
}
