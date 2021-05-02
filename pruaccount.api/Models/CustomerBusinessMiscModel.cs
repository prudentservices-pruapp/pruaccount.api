// <copyright file="CustomerBusinessMiscModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// CustomerBusinessMiscModel.
    /// </summary>
    public class CustomerBusinessMiscModel
    {
        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets CustomerBusinessDetailsUniqueId.
        /// </summary>
        public Guid CustomerBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets TermsAndConditions.
        /// </summary>
        public string TermsAndConditions { get; set; }

        /// <summary>
        /// Gets or sets Notes.
        /// </summary>
        public string Notes { get; set; }
    }
}
