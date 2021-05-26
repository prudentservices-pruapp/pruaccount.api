// <copyright file="BankStatementFileImportProcess.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// BankStatementFileImportProcess.
    /// </summary>
    public class BankStatementFileImportProcess
    {
        /// <summary>
        /// Gets or sets BankStatementFileImportProcessId.
        /// </summary>
        public int BankStatementFileImportProcessId { get; set; }

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
        /// Gets or sets BankStatementFileImportUniqueId.
        /// </summary>
        public Guid BankStatementFileImportUniqueId { get; set; }

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
                return this.BankStatementFileImportProcessId == default(int);
            }
        }
    }
}
