// <copyright file="BankStatementFileImportProcessModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// BankStatementFileImportProcessModel.
    /// </summary>
    public class BankStatementFileImportProcessModel
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
    }
}
