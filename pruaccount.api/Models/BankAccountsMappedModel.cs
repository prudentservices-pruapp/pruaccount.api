// <copyright file="BankAccountsMappedModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// BankAccountsMappedModel.
    /// </summary>
    public class BankAccountsMappedModel
    {
       
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountsMappedModel"/> class.
        /// </summary>
        public BankAccountsMappedModel()
        {
            this.BankAccountDetailsUniqueIds = new List<Guid>();
        }

        /// <summary>
        /// Gets or sets BankStatementMapDetailUniqueId.
        /// </summary>
        public Guid BankStatementMapDetailUniqueId { get; set; }

        /// <summary>
        /// Gets or sets MapName.
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets BankAccountTypeId.
        /// </summary>
        public int BankAccountTypeId { get; set; }

        /// <summary>
        /// Gets or sets BankAccountTypeName.
        /// </summary>
        public string BankAccountTypeName { get; set; }

        /// <summary>
        /// Gets or sets BankAccountDetailsUniqueIds.
        /// </summary>
        public List<Guid> BankAccountDetailsUniqueIds { get; set; }
    }
}
