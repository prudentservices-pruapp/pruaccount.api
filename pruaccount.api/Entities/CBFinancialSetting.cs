// <copyright file="CBFinancialSetting.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// CBFinancialSetting.
    /// </summary>
    public class CBFinancialSetting
    {
        /// <summary>
        /// Gets or sets ClientId.
        /// </summary>
        public int CBFinancialSettingId { get; set; }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets ClientBusinessDetailsUniqueId.
        /// </summary>
        public Guid ClientBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets AccountStartDate.
        /// </summary>
        public DateTime AccountStartDate { get; set; }

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
                return this.CBFinancialSettingId == default(int);
            }
        }
    }
}
