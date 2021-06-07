// <copyright file="BankStatementMapValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;
    using Pruaccount.Api.Domain.BankStatement;
    using Pruaccount.Api.Extensions;
    using Pruaccount.Api.MappingConfigurations;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementMapValidator.
    /// </summary>
    public class BankStatementMapValidator
    {
        private BankStatementParser bankStatementParser;
        private BankStatementMapModel bankStatementMapModel;

        private BankStatementMapper bankStatementMapper;
        private BankStatementMapDetailModel bankStatementMapDetailModel;

        private BankStatementTransactionDetailMapper bankStatementTransactionDetailMapper;

        private List<BankStatementTransactionDetailModel> bankStatementTransactionDetailModelList;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementMapValidator"/> class.
        /// </summary>
        /// <param name="bankStatementParser">Bank Statement Data.</param>
        /// <param name="bankStatementMapDetailSaveModel">Bank Statement Map Details Saved.</param>
        public BankStatementMapValidator(BankStatementParser bankStatementParser, BankStatementMapDetailSaveModel bankStatementMapDetailSaveModel)
        {
            this.bankStatementParser = bankStatementParser;
            this.bankStatementMapModel = this.bankStatementParser.GeRowsJson();
            this.bankStatementMapper = new BankStatementMapper(bankStatementMapDetailSaveModel);
            this.bankStatementMapDetailModel = this.bankStatementMapper.BankStatementMapDetailModel;
            this.bankStatementTransactionDetailMapper = new BankStatementTransactionDetailMapper();
            this.bankStatementTransactionDetailModelList = new List<BankStatementTransactionDetailModel>();
        }

        /// <summary>
        /// ValidateStatmentData.
        /// </summary>
        /// <param name="bankStatementTransactionDetailModels">BankStatementTransactionDetailModel List.</param>
        /// <returns>List error string.</returns>
        public List<string> ValidateStatmentData(out List<BankStatementTransactionDetailModel> bankStatementTransactionDetailModels)
        {
            List<string> errorsList = new List<string>();
            List<BankStatementCSVDataModel> bankStatementCSVDataModels = this.bankStatementMapModel.BankStatementCSVDataList;
            bankStatementTransactionDetailModels = new List<BankStatementTransactionDetailModel>();
            string currentColumnValue = string.Empty;

            if (bankStatementCSVDataModels.Count == 0)
            {
                errorsList.Add("Please upload a bank statement with atleast one row.");
            }

            for (int rowIndex = 0; rowIndex < bankStatementCSVDataModels.Count; rowIndex++)
            {
                BankStatementTransactionDetailModel bankStatementTransactionDetailModel = new BankStatementTransactionDetailModel();
                BankStatementCSVDataModel currentRow = bankStatementCSVDataModels[rowIndex];
                bankStatementTransactionDetailModel = this.bankStatementTransactionDetailMapper.PopulateFromBankStatementCSVDataModel(currentRow, this.bankStatementMapDetailModel);
                bankStatementTransactionDetailModel.RowId = rowIndex;
                bankStatementTransactionDetailModels.Add(bankStatementTransactionDetailModel);
            }

            if (bankStatementCSVDataModels.Count == bankStatementTransactionDetailModels.Count)
            {
                bankStatementTransactionDetailModels = bankStatementTransactionDetailModels.OrderByDescending(x => x.RowId).ToList();

                for (int rowIndex = 0; rowIndex < bankStatementTransactionDetailModels.Count; rowIndex++)
                {
                    BankStatementTransactionDetailModel currentRow = bankStatementTransactionDetailModels[rowIndex];

                    if (rowIndex > 0)
                    {
                        BankStatementTransactionDetailModel previousRow = bankStatementTransactionDetailModels[rowIndex - 1];
                        decimal expectedCurrentRowBalance = previousRow.Balance + currentRow.CreditAmount - currentRow.DebitAmount;

                        if (expectedCurrentRowBalance != currentRow.Balance)
                        {
                            errorsList.Add("Please check mapping for Credit and Debit Amount.");
                            break;
                        }
                    }
                }
            }

            return errorsList;
        }
    }
}
