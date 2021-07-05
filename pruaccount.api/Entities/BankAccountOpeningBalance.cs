// <copyright file="BankAccountOpeningBalance.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// BAOpeningBalance.
    /// </summary>
    public class BankAccountOpeningBalance
    {
        /// <summary>
        /// Gets or sets BankAccountOpeningBalanceId.
        /// </summary>
        public int BankAccountOpeningBalanceId { get; set; }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets ClientBusinessDetailsUniqueId.
        /// </summary>
        public Guid ClientBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets BankAccountDetailsUniqueId.
        /// </summary>
        public Guid BankAccountDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets LedgerAccountId.
        /// </summary>
        public int LedgerAccountId { get; set; }

        /// <summary>
        /// Gets or sets AccountName.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets AccountNumber.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets SortCode.
        /// </summary>
        public string SortCode { get; set; }

        /// <summary>
        /// Gets or sets BalanceDate.
        /// </summary>
        public DateTime BalanceDate { get; set; }

        /// <summary>
        /// Gets or sets BalanceTypeId.
        /// </summary>
        public int BalanceTypeId { get; set; }

        /// <summary>
        /// Gets or sets BalanceTypeName.
        /// </summary>
        public string BalanceTypeName { get; set; }

        /// <summary>
        /// Gets or sets BalanceAmount.
        /// </summary>
        public decimal BalanceAmount { get; set; }

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
                return this.BankAccountOpeningBalanceId == default(int);
            }
        }
    }
}
