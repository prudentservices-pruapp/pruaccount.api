// <copyright file="BankStatementMapDetailFileImportProcess.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// BankStatementMapDetailFileImportProcess.
    /// </summary>
    public class BankStatementMapDetailFileImportProcess
    {
        /// <summary>
        /// Gets or sets BankStatementMapDetailFileImportProcessId.
        /// </summary>
        public int BankStatementMapDetailFileImportProcessId { get; set; }

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
        /// Gets or sets BankStatementMapDetailFileImportUniqueId.
        /// </summary>
        public Guid BankStatementMapDetailFileImportUniqueId { get; set; }

        /// <summary>
        /// Gets or sets ProcessStatus.
        /// </summary>
        public string ProcessStatus { get; set; }

        /// <summary>
        /// Gets or sets PreviousUniqueId.
        /// </summary>
        public Guid PreviousUniqueId { get; set; }

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
                return this.BankStatementMapDetailFileImportProcessId == default(int);
            }
        }
    }
}
