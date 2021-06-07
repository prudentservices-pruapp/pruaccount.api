// <copyright file="BankAccountDetailProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.MappingConfigurations
{
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankAccountDetailProfile.
    /// </summary>
    public class BankAccountDetailMapper
    {
        /// <summary>
        /// PopulateFromModel
        /// BankAccountDetails populated From Model.
        /// </summary>
        /// <param name="bankAccountDetailModel">bankAccountDetailModel.</param>
        /// <returns>BankAccountDetails.</returns>
        public BankAccountDetails PopulateFromModel(BankAccountDetailModel bankAccountDetailModel)
        {
            BankAccountDetails bankAccountDetails = new BankAccountDetails();

            bankAccountDetails.AccountName = bankAccountDetailModel.AccountName;
            bankAccountDetails.BankAccountDetailsId = bankAccountDetailModel.BankAccountDetailsId;
            bankAccountDetails.UniqueId = bankAccountDetailModel.UniqueId;
            bankAccountDetails.ClientBusinessDetailsUniqueId = bankAccountDetailModel.ClientBusinessDetailsUniqueId;
            bankAccountDetails.BankAccountTypeId = bankAccountDetailModel.BankAccountTypeId;
            bankAccountDetails.BankAccountTypeName = bankAccountDetailModel.BankAccountTypeName;
            bankAccountDetails.BankTransactionMethodId = bankAccountDetailModel.BankTransactionMethodId;
            bankAccountDetails.BankTransactionMethodName = bankAccountDetailModel.BankTransactionMethodName;
            bankAccountDetails.LedgerAccountId = bankAccountDetailModel.LedgerAccountId;
            bankAccountDetails.LName = bankAccountDetailModel.LName;
            bankAccountDetails.DName = bankAccountDetailModel.DName;
            bankAccountDetails.NominalCode = bankAccountDetailModel.NominalCode;
            bankAccountDetails.CategoryGroupId = bankAccountDetailModel.CategoryGroupId;
            bankAccountDetails.Category = bankAccountDetailModel.Category;
            bankAccountDetails.VatRateId = bankAccountDetailModel.VatRateId;
            bankAccountDetails.DVatRate = bankAccountDetailModel.DVatRate;
            bankAccountDetails.VatRate = bankAccountDetailModel.VatRate;
            bankAccountDetails.AccountName = bankAccountDetailModel.AccountName;
            bankAccountDetails.SortCode = bankAccountDetailModel.SortCode;
            bankAccountDetails.AccountNumber = bankAccountDetailModel.AccountNumber;
            bankAccountDetails.BicSwift = bankAccountDetailModel.BicSwift;
            bankAccountDetails.IBAN = bankAccountDetailModel.IBAN;
            bankAccountDetails.CardLast4Digits = bankAccountDetailModel.CardLast4Digits;

            return bankAccountDetails;
        }

        /// <summary>
        /// PopulateFromEntity
        /// eBankAccountDetailModel populated From Entity.
        /// </summary>
        /// <param name="bankAccountDetails">bankAccountDetails.</param>
        /// <returns>BankAccountDetailModel.</returns>
        public BankAccountDetailModel PopulateFromEntity(BankAccountDetails bankAccountDetails)
        {
            BankAccountDetailModel bankAccountDetailModel = new BankAccountDetailModel();

            bankAccountDetailModel.BankAccountDetailsId = bankAccountDetails.BankAccountDetailsId;
            bankAccountDetailModel.UniqueId = bankAccountDetails.UniqueId;
            bankAccountDetailModel.ClientBusinessDetailsUniqueId = bankAccountDetails.ClientBusinessDetailsUniqueId;
            bankAccountDetailModel.BankAccountTypeId = bankAccountDetails.BankAccountTypeId;
            bankAccountDetailModel.BankAccountTypeName = bankAccountDetails.BankAccountTypeName;
            bankAccountDetailModel.BankTransactionMethodId = bankAccountDetails.BankTransactionMethodId;
            bankAccountDetailModel.BankTransactionMethodName = bankAccountDetails.BankTransactionMethodName;
            bankAccountDetailModel.LedgerAccountId = bankAccountDetails.LedgerAccountId;
            bankAccountDetailModel.LName = bankAccountDetails.LName;
            bankAccountDetailModel.DName = bankAccountDetails.DName;
            bankAccountDetailModel.NominalCode = bankAccountDetails.NominalCode;
            bankAccountDetailModel.CategoryGroupId = bankAccountDetails.CategoryGroupId;
            bankAccountDetailModel.Category = bankAccountDetails.Category;
            bankAccountDetailModel.VatRateId = bankAccountDetails.VatRateId;
            bankAccountDetailModel.DVatRate = bankAccountDetails.DVatRate;
            bankAccountDetailModel.VatRate = bankAccountDetails.VatRate;
            bankAccountDetailModel.AccountName = bankAccountDetails.AccountName;
            bankAccountDetailModel.SortCode = bankAccountDetails.SortCode;
            bankAccountDetailModel.AccountNumber = bankAccountDetails.AccountNumber;
            bankAccountDetailModel.BicSwift = bankAccountDetails.BicSwift;
            bankAccountDetailModel.IBAN = bankAccountDetails.IBAN;
            bankAccountDetailModel.CardLast4Digits = bankAccountDetails.CardLast4Digits;

            return bankAccountDetailModel;
        }
    }
}
