// <copyright file="BankAccountOpeningBalanceModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// BAOpeningBalanceModel.
    /// </summary>
    public class BankAccountOpeningBalanceModel
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
    }
}
