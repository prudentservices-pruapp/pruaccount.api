// <copyright file="BankStatementMapDetailFileModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;
    using System.Collections.Generic;
    using Pruaccount.Api.Validators.Interfaces;

    /// <summary>
    /// BankStatementMapDetailFileModel.
    /// </summary>
    public class BankStatementMapDetailFileModel : IModelValidation<BankStatementMapDetailFileModel>
    {
        /// <summary>
        /// Gets or sets BankStatementMapDetailFileId.
        /// </summary>
        public int BankStatementMapDetailFileId { get; set; }

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
        /// ValidateModel.
        /// </summary>
        /// <param name="validator">BankStatementMapDetailFileModelValidator.</param>
        /// <param name="brokenRules">list of errors.</param>
        /// <returns>True valid and False for invalid.</returns>
        public bool ValidateModel(IModelValidator<BankStatementMapDetailFileModel> validator, out List<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}
