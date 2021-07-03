// <copyright file="BankAccountMappingLinkMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.MappingConfigurations
{
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankAccountMappingLinkMapper.
    /// </summary>
    public class BankAccountMappingLinkMapper
    {
        /// <summary>
        /// PopulateFromModel
        /// BankAccountMappingLink populated From Model.
        /// </summary>
        /// <param name="bankAccountMappingLinkModel">bankAccountMappingLinkModel.</param>
        /// <returns>BankAccountMappingLink.</returns>
        public BankAccountMappingLink PopulateFromModel(BankAccountMappingLinkModel bankAccountMappingLinkModel)
        {
            BankAccountMappingLink bankAccountMappingLink = new BankAccountMappingLink();

            bankAccountMappingLink.UniqueId = bankAccountMappingLinkModel.UniqueId;
            bankAccountMappingLink.ClientBusinessDetailsUniqueId = bankAccountMappingLinkModel.ClientBusinessDetailsUniqueId;
            bankAccountMappingLink.BankAccountDetailsUniqueId = bankAccountMappingLinkModel.BankAccountDetailsUniqueId;
            bankAccountMappingLink.AccountName = bankAccountMappingLinkModel.AccountName;
            bankAccountMappingLink.BankAccountTypeId = bankAccountMappingLinkModel.BankAccountTypeId;
            bankAccountMappingLink.BankAccountTypeName = bankAccountMappingLinkModel.BankAccountTypeName;
            bankAccountMappingLink.BankStatementMapDetailUniqueId = bankAccountMappingLinkModel.BankStatementMapDetailUniqueId;
            bankAccountMappingLink.MapName = bankAccountMappingLinkModel.MapName;
            bankAccountMappingLink.BankStatementMapDetailIsActive = bankAccountMappingLinkModel.BankStatementMapDetailIsActive;
            bankAccountMappingLink.IsActive = bankAccountMappingLinkModel.IsActive;

            return bankAccountMappingLink;
        }

        /// <summary>
        /// PopulateFromEntity
        /// BankAccountMappingLinkModel populated From Entity.
        /// </summary>
        /// <param name="bankAccountMappingLink">bankAccountDetails.</param>
        /// <returns>BankAccountMappingLinkModel.</returns>
        public BankAccountMappingLinkModel PopulateFromEntity(BankAccountMappingLink bankAccountMappingLink)
        {
            BankAccountMappingLinkModel bankAccountMappingLinkModel = new BankAccountMappingLinkModel();

            bankAccountMappingLinkModel.UniqueId = bankAccountMappingLink.UniqueId;
            bankAccountMappingLinkModel.ClientBusinessDetailsUniqueId = bankAccountMappingLink.ClientBusinessDetailsUniqueId;
            bankAccountMappingLinkModel.BankAccountDetailsUniqueId = bankAccountMappingLink.BankAccountDetailsUniqueId;
            bankAccountMappingLinkModel.AccountName = bankAccountMappingLink.AccountName;
            bankAccountMappingLinkModel.BankAccountTypeId = bankAccountMappingLink.BankAccountTypeId;
            bankAccountMappingLinkModel.BankAccountTypeName = bankAccountMappingLink.BankAccountTypeName;
            bankAccountMappingLinkModel.BankStatementMapDetailUniqueId = bankAccountMappingLink.BankStatementMapDetailUniqueId;
            bankAccountMappingLinkModel.MapName = bankAccountMappingLink.MapName;
            bankAccountMappingLinkModel.BankStatementMapDetailIsActive = bankAccountMappingLink.BankStatementMapDetailIsActive;
            bankAccountMappingLinkModel.IsActive = bankAccountMappingLink.IsActive;

            return bankAccountMappingLinkModel;
        }
    }
}
