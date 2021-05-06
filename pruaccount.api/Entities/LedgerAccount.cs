// <copyright file="LedgerAccount.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// LedgerAccount.
    /// </summary>
    public class LedgerAccount
    {
        /// <summary>
        /// Gets or sets LedgerAccountId.
        /// </summary>
        public int LedgerAccountId { get; set; }

        /// <summary>
        /// Gets or sets ClientBusinessDetailsUniqueId.
        /// </summary>
        public Guid ClientBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets ParentLedgerAccountId.
        /// </summary>
        public int ParentLedgerAccountId { get; set; }

        /// <summary>
        /// Gets or sets LName.
        /// </summary>
        public string LName { get; set; }

        /// <summary>
        /// Gets or sets LName.
        /// </summary>
        public string DName { get; set; }

        /// <summary>
        /// Gets or sets NominalCode.
        /// </summary>
        public int NominalCode { get; set; }

        /// <summary>
        /// Gets or sets CategoryGroupId.
        /// </summary>
        public int CategoryGroupId { get; set; }

        /// <summary>
        /// Gets or sets Category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets Group.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets VatRateId.
        /// </summary>
        public int VatRateId { get; set; }

        /// <summary>
        /// Gets or sets DVatRate.
        /// </summary>
        public string DVatRate { get; set; }

        /// <summary>
        /// Gets or sets VatRate.
        /// </summary>
        public decimal VatRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets IncludeInChart.
        /// </summary>
        public bool IncludeInChart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets M_Bank.
        /// </summary>
        public bool M_Bank { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets M_Sales.
        /// </summary>
        public bool M_Sales { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets M_Purchase.
        /// </summary>
        public bool M_Purchase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets M_Other_Payment.
        /// </summary>
        public bool M_Other_Payment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets M_Other_Receipt.
        /// </summary>
        public bool M_Other_Receipt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets M_Journals.
        /// </summary>
        public bool M_Journals { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets M_Reports.
        /// </summary>
        public bool M_Reports { get; set; }

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
                return this.LedgerAccountId == default(int);
            }
        }
    }
}
