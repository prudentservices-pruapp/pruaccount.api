// <copyright file="BankStatementMapDetailFileModelValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Validators
{
    using System;
    using System.Collections.Generic;
    using Pruaccount.Api.Models;
    using Pruaccount.Api.Validators.Interfaces;

    /// <summary>
    /// BankStatementMapDetailFileModelValidator.
    /// </summary>
    public class BankStatementMapDetailFileModelValidator : IModelValidator<BankStatementMapDetailFileModel>
    {
        private readonly IDictionary<string, string> allowedExtensions = new Dictionary<string, string>()
        {
            { ".csv", "text/csv" },
        };

        /// <summary>
        /// BrokenRules.
        /// </summary>
        /// <param name="model">BankStatementMapDetailFileModel.</param>
        /// <returns>List errors.</returns>
        public List<string> BrokenRules(BankStatementMapDetailFileModel model)
        {
            List<string> errorsList = new List<string>();

            if (!this.allowedExtensions.Keys.Contains(model.FileExtenstion))
            {
                errorsList.Add("Please upload .csv bank statement.");
            }

            if (model.FileLengthInBytes <= 0 || model.FileLengthInBytes > 3000000)
            {
                errorsList.Add("Please upload a file less than 3MB.");
            }

            if (model.SystemGeneratedFileName.Length > 255)
            {
                errorsList.Add("System generate file too long.");
            }

            if (string.IsNullOrEmpty(model.UploadedFileName) || string.IsNullOrEmpty(model.FileExtenstion) || string.IsNullOrEmpty(model.SystemGeneratedFileName) || string.IsNullOrEmpty(model.UploadedFilePath))
            {
                errorsList.Add("Populate all the mandatory fields.");
            }

            return errorsList;
        }

        /// <summary>
        /// IsValid.
        /// </summary>
        /// <param name="model">BankStatementMapDetailFileModel.</param>
        /// <returns>True valid and False for invalid.</returns>
        public bool IsValid(BankStatementMapDetailFileModel model)
        {
            return this.BrokenRules(model).Count == 0;
        }
    }
}
