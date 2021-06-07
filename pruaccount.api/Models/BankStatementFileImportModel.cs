﻿// <copyright file="BankStatementFileImportModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;
    using System.Collections.Generic;
    using Pruaccount.Api.Validators.Interfaces;

    /// <summary>
    /// BankStatementFileImportModel.
    /// </summary>
    public class BankStatementFileImportModel : IModelValidation<BankStatementFileImportModel>
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
        /// ValidateModel.
        /// </summary>
        /// <param name="validator">BankStatementFileImportModelValidator.</param>
        /// <param name="brokenRules">list of errors.</param>
        /// <returns>True valid and False for invalid.</returns>
        public bool ValidateModel(IModelValidator<BankStatementFileImportModel> validator, out List<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}