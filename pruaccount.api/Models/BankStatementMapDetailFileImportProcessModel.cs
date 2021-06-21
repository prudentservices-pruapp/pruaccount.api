// <copyright file="BankStatementMapDetailFileImportProcessModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// BankStatementMapDetailFileImportProcessModel.
    /// </summary>
    public class BankStatementMapDetailFileImportProcessModel
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
    }
}
