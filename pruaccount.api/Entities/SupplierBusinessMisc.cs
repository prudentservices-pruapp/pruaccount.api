// <copyright file="SupplierBusinessMisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// SupplierBusinessMisc.
    /// </summary>
    public class SupplierBusinessMisc
    {
        /// <summary>
        /// Gets or sets SupplierBusinessMiscId.
        /// </summary>
        public int SupplierBusinessMiscId { get; set; }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets ClientBusinessDetailsUniqueId.
        /// </summary>
        public Guid ClientBusinessDetailsUniqueId { get; set; }

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

        /// <summary>
        /// Gets or sets CreatedDateUTC.
        /// </summary>
        public DateTime CreatedDateUTC { get; set; }

        /// <summary>
        /// Gets or sets UpdatedDateUTC.
        /// </summary>
        public DateTime? UpdatedDateUTC { get; set; }

        /// <summary>
        /// Gets or sets TotalRows.
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// Gets a value indicating whether gets IsNew.
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.UniqueId == default(Guid);
            }
        }
    }
}