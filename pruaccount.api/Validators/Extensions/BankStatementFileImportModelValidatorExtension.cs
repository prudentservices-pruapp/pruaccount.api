// <copyright file="BankStatementFileImportModelValidatorExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pruaccount.Api.Validators.Extensions
{
    using System.Collections.Generic;
    using Pruaccount.Api.Models;
    using Pruaccount.Api.Validators.Interfaces;

    /// <summary>
    /// BankStatementFileImportModelValidatorExtension.
    /// </summary>
    public static class BankStatementFileImportModelValidatorExtension
    {
        /// <summary>
        /// ValidateModel.
        /// </summary>
        /// <param name="model">BankStatementFileImportModel.</param>
        /// <param name="brokenRules">list of errors.</param>
        /// <returns>True valid and False for invalid.</returns>
        public static bool ValidateModel(this BankStatementFileImportModel model, out List<string> brokenRules)
        {
            IModelValidator<BankStatementFileImportModel> validator = new BankStatementFileImportModelValidator();
            brokenRules = new List<string>();

            return model.ValidateModel(validator, out brokenRules);
        }
    }
}
