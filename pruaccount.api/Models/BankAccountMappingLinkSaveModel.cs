// <copyright file="BankAccountMappingLinkSaveModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// BankAccountMappingLinkSaveModel.
    /// </summary>
    public class BankAccountMappingLinkSaveModel
    {
        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid BankAccountMappingLinkUniqueId { get; set; }

        /// <summary>
        /// Gets or sets BankStatementMapDetailUniqueId.
        /// </summary>
        public Guid BankStatementMapDetailUniqueId { get; set; }

        /// <summary>
        /// Gets or sets BankAccountDetailsUniqueIds.
        /// </summary>
        public Guid[] BankAccountDetailsUniqueIds { get; set; }
    }
}
