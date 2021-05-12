// <copyright file="VatRate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// VatRate.
    /// </summary>
    public class VatRate
    {
        /// <summary>
        /// Gets or sets VatRateId.
        /// </summary>
        public int VatRateId { get; set; }

        /// <summary>
        /// Gets or sets Category.
        /// </summary>
        public string DName { get; set; }

        /// <summary>
        /// Gets or sets Rate.
        /// </summary>
        public decimal Rate { get; set; }

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
                return this.VatRateId == default(int);
            }
        }
    }
}
