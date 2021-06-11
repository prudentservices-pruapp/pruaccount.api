// <copyright file="BankStatementFileImport.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// BankStatementFileImport.
    /// </summary>
    public class BankStatementFileImport
    {
        /// <summary>
        /// Gets or sets BankStatementFileImportId.
        /// </summary>
        public int BankStatementFileImportId { get; set; }

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
        /// Gets or sets BankStatementMapDetailUniqueId.
        /// </summary>
        public Guid BankStatementMapDetailUniqueId { get; set; }

        /// <summary>
        /// Gets or sets UploadedFileName.
        /// </summary>
        public string UploadedFileName { get; set; }

        /// <summary>
        /// Gets or sets UploadedFilePath.
        /// </summary>
        public string UploadedFilePath { get; set; }

        /// <summary>
        /// Gets or sets SystemGeneratedFileName.
        /// </summary>
        public string SystemGeneratedFileName { get; set; }

        /// <summary>
        /// Gets or sets FileExtenstion.
        /// </summary>
        public string FileExtenstion { get; set; }

        /// <summary>
        /// Gets or sets FileLengthInBytes.
        /// </summary>
        public decimal FileLengthInBytes { get; set; }

        /// <summary>
        /// Gets or sets CurrentProcessStatus.
        /// </summary>
        public string CurrentProcessStatus { get; set; }

        /// <summary>
        /// Gets or sets CurrentProcessStatusDate.
        /// </summary>
        public DateTime CurrentProcessStatusDate { get; set; }

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
                return this.BankStatementFileImportId == default(int);
            }
        }
    }
}
