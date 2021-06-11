// <copyright file="BankStatementMapDetail.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// BankStatementMapDetail.
    /// </summary>
    public class BankStatementMapDetail
    {
        /// <summary>
        /// Gets or sets BankStatementMapDetailId.
        /// </summary>
        public int BankStatementMapDetailId { get; set; }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets MapName.
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets DatePart1.
        /// </summary>
        public string DatePart1 { get; set; }

        /// <summary>
        /// Gets or sets DatePart2.
        /// </summary>
        public string DatePart2 { get; set; }

        /// <summary>
        /// Gets or sets DatePart3.
        /// </summary>
        public string DatePart3 { get; set; }

        /// <summary>
        /// Gets or sets DateSeparator.
        /// </summary>
        public string DateSeparator { get; set; }

        /// <summary>
        /// Gets or sets Dateformat.
        /// </summary>
        public string Dateformat { get; set; }

        /// <summary>
        /// Gets or sets DateformatValue.
        /// </summary>
        public string DateformatValue { get; set; }

        /// <summary>
        /// Gets or sets DateIndex.
        /// </summary>
        public int DateIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets CreditAmountIndex.
        /// </summary>
        public int CreditAmountIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets DebitAmountIndex.
        /// </summary>
        public int DebitAmountIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets DescriptionIndex.
        /// </summary>
        public int DescriptionIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets BalanceIndex.
        /// </summary>
        public int BalanceIndex { get; set; } = -1;
    }
}
