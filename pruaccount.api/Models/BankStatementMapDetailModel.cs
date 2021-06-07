// <copyright file="BankStatementMapDetailModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    /// <summary>
    /// BankStatementMapDetailModel.
    /// </summary>
    public class BankStatementMapDetailModel
    {

        /// <summary>
        /// Gets or sets Dateformat.
        /// </summary>
        public string Dateformat { get; set; }

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
        /// Gets a value indicating whether IsCreditDebitAmountIndexSame.
        /// </summary>
        public bool IsCreditDebitAmountIndexSame
        {
            get
            {
                if (this.CreditAmountIndex == this.DebitAmountIndex)
                {
                    return true;
                }

                return false;
            }
        }

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
