// <copyright file="BankStatementMapDetailMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pruaccount.Api.MappingConfigurations
{
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementMapDetailMapper.
    /// </summary>
    public class BankStatementMapDetailMapper
    {
        /// <summary>
        /// PopulateFromModel.
        /// </summary>
        /// <param name="bankAccountDetailModel">BankStatementMapDetailModel.</param>
        /// <returns>BankStatementMapDetail.</returns>
        public BankStatementMapDetail PopulateFromModel(BankStatementMapDetailModel bankAccountDetailModel)
        {
            BankStatementMapDetail bankStatementMapDetail = new BankStatementMapDetail();
            bankStatementMapDetail.UniqueId = bankAccountDetailModel.UniqueId;
            bankStatementMapDetail.MapName = bankAccountDetailModel.MapName;
            bankStatementMapDetail.DatePart1 = bankAccountDetailModel.DatePart1;
            bankStatementMapDetail.DatePart2 = bankAccountDetailModel.DatePart2;
            bankStatementMapDetail.DatePart3 = bankAccountDetailModel.DatePart3;
            bankStatementMapDetail.DateSeparator = bankAccountDetailModel.DateSeparator;
            bankStatementMapDetail.Dateformat = bankAccountDetailModel.Dateformat;
            bankStatementMapDetail.DateformatValue = bankAccountDetailModel.DateformatValue;
            bankStatementMapDetail.DateIndex = bankAccountDetailModel.DateIndex;
            bankStatementMapDetail.CreditAmountIndex = bankAccountDetailModel.CreditAmountIndex;
            bankStatementMapDetail.DebitAmountIndex = bankAccountDetailModel.DebitAmountIndex;
            bankStatementMapDetail.DescriptionIndex = bankAccountDetailModel.DescriptionIndex;
            bankStatementMapDetail.BalanceIndex = bankAccountDetailModel.BalanceIndex;

            return bankStatementMapDetail;
        }

        /// <summary>
        /// PopulateFromEntity
        /// eBankAccountDetailModel populated From Entity.
        /// </summary>
        /// <param name="bankStatementMapDetail">BankStatementMapDetail.</param>
        /// <returns>BankStatementMapDetailModel.</returns>
        public BankStatementMapDetailModel PopulateFromEntity(BankStatementMapDetail bankStatementMapDetail)
        {
            BankStatementMapDetailModel bankStatementMapDetailModel = new BankStatementMapDetailModel();

            bankStatementMapDetailModel.UniqueId = bankStatementMapDetail.UniqueId;
            bankStatementMapDetailModel.MapName = bankStatementMapDetail.MapName;
            bankStatementMapDetailModel.DatePart1 = bankStatementMapDetail.DatePart1;
            bankStatementMapDetailModel.DatePart2 = bankStatementMapDetail.DatePart2;
            bankStatementMapDetailModel.DatePart3 = bankStatementMapDetail.DatePart3;
            bankStatementMapDetailModel.DateSeparator = bankStatementMapDetail.DateSeparator;
            bankStatementMapDetailModel.Dateformat = bankStatementMapDetail.Dateformat;
            bankStatementMapDetailModel.DateformatValue = bankStatementMapDetail.DateformatValue;
            bankStatementMapDetailModel.DateIndex = bankStatementMapDetail.DateIndex;
            bankStatementMapDetailModel.CreditAmountIndex = bankStatementMapDetail.CreditAmountIndex;
            bankStatementMapDetailModel.DebitAmountIndex = bankStatementMapDetail.DebitAmountIndex;
            bankStatementMapDetailModel.DescriptionIndex = bankStatementMapDetail.DescriptionIndex;
            bankStatementMapDetailModel.BalanceIndex = bankStatementMapDetail.BalanceIndex;

            return bankStatementMapDetailModel;
        }
    }
}
