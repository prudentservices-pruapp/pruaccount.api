// <copyright file="SupplierBusinessMiscModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// SupplierBusinessMiscModel.
    /// </summary>
    public class SupplierBusinessMiscModel
    {
        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets SupplierBusinessDetailsUniqueId.
        /// </summary>
        public Guid SupplierBusinessDetailsUniqueId { get; set; }

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