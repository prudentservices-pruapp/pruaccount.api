// <copyright file="BankStatementMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Domain.BankStatement
{
    using System;
    using System.Reflection;
    using Pruaccount.Api.Enums;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementMapper.
    /// </summary>
    public class BankStatementMapper
    {
        private readonly string propertNameStartsWith = "Column";

        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementMapper"/> class.
        /// </summary>
        /// <param name="bankStatementMapDetailSaveModel">BankStatementMapDetailSaveModel.</param>
        public BankStatementMapper(BankStatementMapDetailSaveModel bankStatementMapDetailSaveModel)
        {
            this.BankStatementMapDetailModel = this.PopulateBankStatementMapDetail(bankStatementMapDetailSaveModel);
        }

        /// <summary>
        /// Gets or sets bankStatementMapDetailModel.
        /// </summary>
        public BankStatementMapDetailModel BankStatementMapDetailModel { get; set; }

        /// <summary>
        /// PopulateBankStatementMapDetail.
        /// </summary>
        /// <param name="bankStatementMapDetailSaveModel">bankStatementMapDetailSaveModel.</param>
        /// <returns>returns index of columns.</returns>
        private BankStatementMapDetailModel PopulateBankStatementMapDetail(BankStatementMapDetailSaveModel bankStatementMapDetailSaveModel)
        {
            BankStatementMapDetailModel bankStatementMapDetailModel = new BankStatementMapDetailModel();

            bankStatementMapDetailModel.Dateformat = bankStatementMapDetailSaveModel.Dateformat;
            bankStatementMapDetailModel.DateIndex = this.GetFieldIndex(BankStatementMapColumnTypeEnum.Date, bankStatementMapDetailSaveModel);
            bankStatementMapDetailModel.CreditAmountIndex = this.GetFieldIndex(BankStatementMapColumnTypeEnum.CreditAmount, bankStatementMapDetailSaveModel);
            bankStatementMapDetailModel.DebitAmountIndex = this.GetFieldIndex(BankStatementMapColumnTypeEnum.DebitAmount, bankStatementMapDetailSaveModel);

            // if the index is -1, they will be same.
            if (bankStatementMapDetailModel.IsCreditDebitAmountIndexSame)
            {
                bankStatementMapDetailModel.CreditAmountIndex = this.GetFieldIndex(BankStatementMapColumnTypeEnum.CreditDebitAmount, bankStatementMapDetailSaveModel);
                bankStatementMapDetailModel.DebitAmountIndex = bankStatementMapDetailModel.CreditAmountIndex;
            }

            bankStatementMapDetailModel.DescriptionIndex = this.GetFieldIndex(BankStatementMapColumnTypeEnum.Description, bankStatementMapDetailSaveModel);
            bankStatementMapDetailModel.BalanceIndex = this.GetFieldIndex(BankStatementMapColumnTypeEnum.Balance, bankStatementMapDetailSaveModel);

            return bankStatementMapDetailModel;
        }

        private int GetFieldIndex(int mapColumn, BankStatementMapDetailSaveModel model)
        {
            int feildIndex = -1;

            for (int colIndex = 0; colIndex <= 15; colIndex++)
            {
                try
                {
                    string propertyName = this.propertNameStartsWith + colIndex;
                    PropertyInfo propertyInfo = model.GetType().GetProperty(propertyName);
                    int currentColumnValue = -1;

                    if (propertyInfo != null)
                    {
                        var objectValue = propertyInfo.GetValue(model, null);

                        if (objectValue != null)
                        {
                            int.TryParse(objectValue.ToString(), out currentColumnValue);

                            if (currentColumnValue == mapColumn)
                            {
                                feildIndex = colIndex;
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    feildIndex = -1;
                }
            }

            return feildIndex;
        }
    }
}
