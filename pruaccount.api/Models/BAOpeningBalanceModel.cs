// <copyright file="BAOpeningBalanceModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// BAOpeningBalanceModel.
    /// </summary>
    public class BAOpeningBalanceModel
    {
        /// <summary>
        /// Gets or sets BAOpeningBalanceId.
        /// </summary>
        public int BAOpeningBalanceId { get; set; }

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
        /// Gets or sets BAOpeningBalanceTypeId.
        /// </summary>
        public int BAOpeningBalanceTypeId { get; set; }

        /// <summary>
        /// Gets or sets BAOpeningBalanceTypeName.
        /// </summary>
        public string BAOpeningBalanceTypeName { get; set; }

        /// <summary>
        /// Gets or sets BalanceAmount.
        /// </summary>
        public decimal BalanceAmount { get; set; }
    }
}
