// <copyright file="BankAccountMappingLink.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// BankAccountMappingLink.
    /// </summary>
    public class BankAccountMappingLink
    {
        /// <summary>
        /// Gets or sets BankAccountMappingLinkId.
        /// </summary>
        public int BankAccountMappingLinkId { get; set; }

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
        /// Gets or sets AccountName.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets BankAccountTypeId.
        /// </summary>
        public int BankAccountTypeId { get; set; }

        /// <summary>
        /// Gets or sets BankAccountTypeName.
        /// </summary>
        public string BankAccountTypeName { get; set; }

        /// <summary>
        /// Gets or sets BankStatementMapDetailUniqueId.
        /// </summary>
        public Guid BankStatementMapDetailUniqueId { get; set; }

        /// <summary>
        /// Gets or sets MapName.
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets BankStatementMapDetailIsActive.
        /// </summary>
        public bool BankStatementMapDetailIsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets IsActive.
        /// </summary>
        public bool IsActive { get; set; }

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
                return this.BankAccountMappingLinkId == default(int);
            }
        }
    }
}
