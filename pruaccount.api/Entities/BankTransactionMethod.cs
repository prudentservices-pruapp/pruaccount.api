// <copyright file="BankTransactionMethod.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// BankTransactionMethod.
    /// </summary>
    public class BankTransactionMethod
    {
        /// <summary>
        /// Gets or sets BankTransactionMethodId.
        /// </summary>
        public int BankTransactionMethodId { get; set; }

        /// <summary>
        /// Gets or sets BankTransactionMethodName.
        /// </summary>
        public string BankTransactionMethodName { get; set; }

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
                return this.BankTransactionMethodId == default(int);
            }
        }
    }
}
