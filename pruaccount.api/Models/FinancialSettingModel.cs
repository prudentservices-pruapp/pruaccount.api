// <copyright file="FinancialSettingModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// FinancialSettingModel.
    /// </summary>
    public class FinancialSettingModel
    {
        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets ClientBusinessDetailsUniqueId.
        /// </summary>
        public Guid ClientBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets YearStartDate.
        /// </summary>
        public DateTime YearStartDate { get; set; }

        /// <summary>
        /// Gets or sets YearEndDate.
        /// </summary>
        public DateTime YearEndDate { get; set; }

        /// <summary>
        /// Gets or sets YearEndLockdownDate.
        /// </summary>
        public DateTime YearEndLockdownDate { get; set; }

        /// <summary>
        /// Gets or sets YearEndTaxDate.
        /// </summary>
        public string YearEndTaxMonth { get; set; }

        /// <summary>
        /// Gets or sets RetentionPeriod.
        /// </summary>
        public int RetentionPeriod { get; set; }

        /// <summary>
        /// Gets or sets VatScheme.
        /// </summary>
        public string VatScheme { get; set; }

        /// <summary>
        /// Gets or sets VatSubmissionRequency.
        /// </summary>
        public string VatSubmissionRequency { get; set; }

        /// <summary>
        /// Gets or sets VatNumber.
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// Gets or sets VatFlatRate.
        /// </summary>
        public decimal VatFlatRate { get; set; }

        /// <summary>
        /// Gets or sets HMRCUserId.
        /// </summary>
        public string HMRCUserId { get; set; }
    }
}
