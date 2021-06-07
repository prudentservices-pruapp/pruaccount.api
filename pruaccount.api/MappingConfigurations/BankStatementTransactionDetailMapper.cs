// <copyright file="BankStatementTransactionDetailMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.MappingConfigurations
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Pruaccount.Api.Extensions;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementTransactionDetailMapper.
    /// </summary>
    public class BankStatementTransactionDetailMapper
    {
        /// <summary>
        /// PopulateFromBankStatementCSVDataModel.
        /// BankStatementTransactionDetailModel Populated From BankStatementCSVDataMode.l
        /// </summary>
        /// <param name="bankStatementCSVDataModel">BankStatementCSVDataModel.</param>
        /// <param name="bankStatementMapDetailModel">BankStatementMapDetailModel.</param>
        /// <returns>Populated BankStatementTransactionDetailModel.</returns>
        public BankStatementTransactionDetailModel PopulateFromBankStatementCSVDataModel(BankStatementCSVDataModel bankStatementCSVDataModel, BankStatementMapDetailModel bankStatementMapDetailModel)
        {
            string currentColumnValue = string.Empty;
            BankStatementCSVDataModel currentRow = bankStatementCSVDataModel;

            BankStatementTransactionDetailModel bankStatementTransactionDetailModel = new BankStatementTransactionDetailModel();
            bankStatementCSVDataModel.GetValueForPropertyEndingWithIndex(bankStatementMapDetailModel.DateIndex);

            currentColumnValue = currentRow.GetValueForPropertyEndingWithIndex(bankStatementMapDetailModel.DateIndex);
            bankStatementTransactionDetailModel.TransactionDate = GetParseDate(currentColumnValue, bankStatementMapDetailModel.Dateformat);

            currentColumnValue = currentRow.GetValueForPropertyEndingWithIndex(bankStatementMapDetailModel.CreditAmountIndex);
            bankStatementTransactionDetailModel.CreditAmount = GetParseAmount(currentColumnValue);

            if (bankStatementMapDetailModel.IsCreditDebitAmountIndexSame)
            {
                bankStatementTransactionDetailModel.DebitAmount = bankStatementTransactionDetailModel.CreditAmount;

                if (bankStatementTransactionDetailModel.DebitAmount < 0)
                {
                    bankStatementTransactionDetailModel.DebitAmount = Math.Abs(bankStatementTransactionDetailModel.DebitAmount);
                }
                else
                {
                    bankStatementTransactionDetailModel.DebitAmount = 0;
                }

                if (bankStatementTransactionDetailModel.CreditAmount < 0)
                {
                    bankStatementTransactionDetailModel.CreditAmount = 0;
                }
            }
            else
            {
                currentColumnValue = currentRow.GetValueForPropertyEndingWithIndex(bankStatementMapDetailModel.DebitAmountIndex);
                bankStatementTransactionDetailModel.DebitAmount = GetParseAmount(currentColumnValue);
            }

            currentColumnValue = currentRow.GetValueForPropertyEndingWithIndex(bankStatementMapDetailModel.BalanceIndex);
            bankStatementTransactionDetailModel.Balance = GetParseAmount(currentColumnValue);

            currentColumnValue = currentRow.GetValueForPropertyEndingWithIndex(bankStatementMapDetailModel.DescriptionIndex);
            bankStatementTransactionDetailModel.Description = currentColumnValue;

            return bankStatementTransactionDetailModel;
        }

        /// <summary>
        /// GetParseDate.
        /// </summary>
        /// <param name="input">string input.</param>
        /// <param name="dateFormat">string dateFormat.</param>
        /// <returns>true if parsed or false.</returns>
        private static DateTime GetParseDate(string input, string dateFormat)
        {
            try
            {
                DateTime parsedDate = DateTime.ParseExact(input, dateFormat, null);
                return parsedDate;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// GetParseAmount.
        /// </summary>
        /// <param name="input">string input.</param>
        /// <returns>true if parsed or false.</returns>
        private static decimal GetParseAmount(string input)
        {
            try
            {
                NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
                CultureInfo provider = new CultureInfo("en-GB");
                decimal number = decimal.Parse(input, style, provider);
                return number;
            }
            catch (Exception)
            {
                return default(decimal);
            }
        }
    }
}
