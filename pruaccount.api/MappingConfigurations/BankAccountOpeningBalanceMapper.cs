// <copyright file="BankAccountOpeningBalanceMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.MappingConfigurations
{
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankAccountOpeningBalanceMapper.
    /// </summary>
    public class BankAccountOpeningBalanceMapper
    {
        /// <summary>
        /// PopulateFromModel
        /// BankAccountOpeningBalance populated From Model.
        /// </summary>
        /// <param name="bankAccountOpeningBalanceModel">bankAccountOpeningBalanceModel</param>
        /// <returns>BankAccountOpeningBalance</returns>
        public BankAccountOpeningBalance PopulateFromModel(BankAccountOpeningBalanceModel bankAccountOpeningBalanceModel)
        {
            BankAccountOpeningBalance bankAccountOpeningBalance = new BankAccountOpeningBalance();

            bankAccountOpeningBalance.BankAccountOpeningBalanceId = bankAccountOpeningBalanceModel.BankAccountOpeningBalanceId;
            bankAccountOpeningBalance.UniqueId = bankAccountOpeningBalanceModel.UniqueId;
            bankAccountOpeningBalance.ClientBusinessDetailsUniqueId = bankAccountOpeningBalanceModel.ClientBusinessDetailsUniqueId;
            bankAccountOpeningBalance.LedgerAccountId = bankAccountOpeningBalanceModel.LedgerAccountId;
            bankAccountOpeningBalance.BankAccountDetailsUniqueId = bankAccountOpeningBalanceModel.BankAccountDetailsUniqueId;
            bankAccountOpeningBalance.AccountName = bankAccountOpeningBalanceModel.AccountName;
            bankAccountOpeningBalance.AccountNumber = bankAccountOpeningBalanceModel.AccountNumber;
            bankAccountOpeningBalance.SortCode = bankAccountOpeningBalanceModel.SortCode;
            bankAccountOpeningBalance.BalanceAmount = bankAccountOpeningBalanceModel.BalanceAmount;
            bankAccountOpeningBalance.BalanceDate = bankAccountOpeningBalanceModel.BalanceDate;
            bankAccountOpeningBalance.BalanceTypeId = bankAccountOpeningBalanceModel.BalanceTypeId;
            bankAccountOpeningBalance.BalanceTypeName = bankAccountOpeningBalanceModel.BalanceTypeName;

            return bankAccountOpeningBalance;
        }

        /// <summary>
        /// PopulateFromEntity
        /// BankAccountOpeningBalanceModel populated From Entity.
        /// </summary>
        /// <param name="bankAccountOpeningBalance">bankAccountOpeningBalance.</param>
        /// <returns>BankAccountOpeningBalanceModel.</returns>
        public BankAccountOpeningBalanceModel PopulateFromEntity(BankAccountOpeningBalance bankAccountOpeningBalance)
        {
            BankAccountOpeningBalanceModel bankAccountOpeningBalanceModel = new BankAccountOpeningBalanceModel();

            bankAccountOpeningBalanceModel.BankAccountOpeningBalanceId = bankAccountOpeningBalance.BankAccountOpeningBalanceId;
            bankAccountOpeningBalanceModel.UniqueId = bankAccountOpeningBalance.UniqueId;
            bankAccountOpeningBalanceModel.ClientBusinessDetailsUniqueId = bankAccountOpeningBalance.ClientBusinessDetailsUniqueId;
            bankAccountOpeningBalanceModel.LedgerAccountId = bankAccountOpeningBalance.LedgerAccountId;
            bankAccountOpeningBalanceModel.BankAccountDetailsUniqueId = bankAccountOpeningBalance.BankAccountDetailsUniqueId;
            bankAccountOpeningBalanceModel.AccountName = bankAccountOpeningBalance.AccountName;
            bankAccountOpeningBalanceModel.AccountNumber = bankAccountOpeningBalance.AccountNumber;
            bankAccountOpeningBalanceModel.SortCode = bankAccountOpeningBalance.SortCode;
            bankAccountOpeningBalanceModel.BalanceAmount = bankAccountOpeningBalance.BalanceAmount;
            bankAccountOpeningBalanceModel.BalanceDate = bankAccountOpeningBalance.BalanceDate;
            bankAccountOpeningBalanceModel.BalanceTypeId = bankAccountOpeningBalance.BalanceTypeId;
            bankAccountOpeningBalanceModel.BalanceTypeName = bankAccountOpeningBalance.BalanceTypeName;

            return bankAccountOpeningBalanceModel;
        }
    }
}
