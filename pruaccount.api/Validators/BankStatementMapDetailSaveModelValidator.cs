// <copyright file="BankStatementMapDetailSaveModelValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Pruaccount.Api.Enums;
    using Pruaccount.Api.Extensions;
    using Pruaccount.Api.Models;
    using Pruaccount.Api.Validators.Interfaces;

    /// <summary>
    /// BankStatementMapDetailSaveModelValidator.
    /// </summary>
    public class BankStatementMapDetailSaveModelValidator : IModelValidator<BankStatementMapDetailSaveModel>
    {
        /// <summary>
        /// BrokenRules.
        /// </summary>
        /// <param name="model">BankStatementMapDetailSaveModel.</param>
        /// <returns>List errors.</returns>
        public List<string> BrokenRules(BankStatementMapDetailSaveModel model)
        {
            List<string> errorsList = new List<string>();

            if (string.IsNullOrEmpty(model.Mapname))
            {
                errorsList.Add("Please enter mapping name e.g. (Lloyds Bank).");
            }

            if (string.IsNullOrEmpty(model.Dateformat))
            {
                errorsList.Add("Please select date format for mapping.");
            }

            int dateMappingCount = model.GetBankStatementMapColumnIndexCount(BankStatementMapColumnTypeEnum.Date);

            if (dateMappingCount == 0 || dateMappingCount > 1)
            {
                if (dateMappingCount == 0)
                {
                    errorsList.Add("Please select mapping for Date.");
                }

                if (dateMappingCount > 1)
                {
                    errorsList.Add("Please check mapping for Date, more than one column mapped.");
                }
            }

            int creditAmountMappingCount = model.GetBankStatementMapColumnIndexCount(BankStatementMapColumnTypeEnum.CreditAmount);
            int debitAmountMappingCount = model.GetBankStatementMapColumnIndexCount(BankStatementMapColumnTypeEnum.DebitAmount);
            int creditDebitAmountMappingCount = model.GetBankStatementMapColumnIndexCount(BankStatementMapColumnTypeEnum.CreditDebitAmount);

            if (((creditAmountMappingCount == 0 || creditAmountMappingCount > 1) || (debitAmountMappingCount == 0 || debitAmountMappingCount > 1)) && (creditDebitAmountMappingCount == 0 || creditDebitAmountMappingCount > 1))
            {
                errorsList.Add("Please check mapping for Credit and Debit Amount.");

                if (creditAmountMappingCount > 1)
                {
                    errorsList.Add("Please check mapping for Credit Amount, more than one column mapped.");
                }

                if (debitAmountMappingCount > 1)
                {
                    errorsList.Add("Please check mapping for Debit Amount, more than one column mapped.");
                }

                if (creditDebitAmountMappingCount > 1)
                {
                    errorsList.Add("Please check mapping for Credit/Debit Amount (+/-), more than one column mapped.");
                }
            }

            int balanceMappingCount = model.GetBankStatementMapColumnIndexCount(BankStatementMapColumnTypeEnum.Balance);

            if (balanceMappingCount == 0 || balanceMappingCount > 1)
            {
                if (balanceMappingCount == 0)
                {
                    errorsList.Add("Please select mapping for Balance.");
                }

                if (balanceMappingCount > 1)
                {
                    errorsList.Add("Please check mapping for Balance, more than one column mapped.");
                }
            }

            int descriptionMappingCount = model.GetBankStatementMapColumnIndexCount(BankStatementMapColumnTypeEnum.Description);

            if (descriptionMappingCount > 1)
            {
                if (descriptionMappingCount > 1)
                {
                    errorsList.Add("Please check mapping for Description, more than one column mapped.");
                }
            }

            return errorsList;
        }

        /// <summary>
        /// IsValid.
        /// </summary>
        /// <param name="model">BankStatementMapDetailSaveModel.</param>
        /// <returns>True valid and False for invalid.</returns>
        public bool IsValid(BankStatementMapDetailSaveModel model)
        {
            return this.BrokenRules(model).Count == 0;
        }
    }
}
