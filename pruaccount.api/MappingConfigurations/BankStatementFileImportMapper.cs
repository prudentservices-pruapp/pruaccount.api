// <copyright file="BankStatementFileImportMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.MappingConfigurations
{
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementFileImportMapper.
    /// </summary>
    public static class BankStatementFileImportMapper
    {
        /// <summary>
        /// PopulateBankStatementFileImportFromModel.
        /// </summary>
        /// <param name="bankStatementFileImport">bankStatementFileImport.</param>
        /// <param name="bankStatementFileImportModel">bankStatementFileImportModel.</param>
        /// <returns>BankAccountDetails.</returns>
        public static BankStatementFileImport PopulateBankStatementFileImportFromModel(this BankStatementFileImport bankStatementFileImport, BankStatementFileImportModel bankStatementFileImportModel)
        {
            if (bankStatementFileImport != null)
            {
                bankStatementFileImport.UniqueId = bankStatementFileImportModel.UniqueId;
                bankStatementFileImport.ClientBusinessDetailsUniqueId = bankStatementFileImportModel.ClientBusinessDetailsUniqueId;
                bankStatementFileImport.BankAccountDetailsUniqueId = bankStatementFileImportModel.BankAccountDetailsUniqueId;
                bankStatementFileImport.UploadedFileName = bankStatementFileImportModel.UploadedFileName;
                bankStatementFileImport.UploadedFilePath = bankStatementFileImportModel.UploadedFilePath;
                bankStatementFileImport.SystemGeneratedFileName = bankStatementFileImportModel.SystemGeneratedFileName;
                bankStatementFileImport.FileExtenstion = bankStatementFileImportModel.FileExtenstion;
                bankStatementFileImport.FileLengthInBytes = bankStatementFileImportModel.FileLengthInBytes;
                bankStatementFileImport.CurrentProcessStatus = bankStatementFileImportModel.CurrentProcessStatus;
                bankStatementFileImport.CurrentProcessStatusDate = bankStatementFileImportModel.CurrentProcessStatusDate;
            }

            return bankStatementFileImport;
        }

        /// <summary>
        /// PopulateBankStatementFileImportModelFromEntity.
        /// </summary>
        /// <param name="bankStatementFileImportModel">bankStatementFileImportModel.</param>
        /// <param name="bankStatementFileImport">bankStatementFileImport.</param>
        /// <returns>BankStatementFileImport.</returns>
        public static BankStatementFileImportModel PopulateBankStatementFileImportModelFromEntity(this BankStatementFileImportModel bankStatementFileImportModel, BankStatementFileImport bankStatementFileImport)
        {
            if (bankStatementFileImportModel != null)
            {
                bankStatementFileImportModel.UniqueId = bankStatementFileImport.UniqueId;
                bankStatementFileImportModel.ClientBusinessDetailsUniqueId = bankStatementFileImport.ClientBusinessDetailsUniqueId;
                bankStatementFileImportModel.BankAccountDetailsUniqueId = bankStatementFileImport.BankAccountDetailsUniqueId;
                bankStatementFileImportModel.UploadedFileName = bankStatementFileImport.UploadedFileName;
                bankStatementFileImportModel.UploadedFilePath = bankStatementFileImport.UploadedFilePath;
                bankStatementFileImportModel.SystemGeneratedFileName = bankStatementFileImport.SystemGeneratedFileName;
                bankStatementFileImportModel.FileExtenstion = bankStatementFileImport.FileExtenstion;
                bankStatementFileImportModel.FileLengthInBytes = bankStatementFileImport.FileLengthInBytes;
                bankStatementFileImportModel.CurrentProcessStatus = bankStatementFileImport.CurrentProcessStatus;
                bankStatementFileImportModel.CurrentProcessStatusDate = bankStatementFileImport.CurrentProcessStatusDate;
            }

            return bankStatementFileImportModel;
        }

        /// <summary>
        /// PopulatePartialBankStatementFileImportModelFromEntity.
        /// </summary>
        /// <param name="bankStatementFileImportModel">bankStatementFileImportModel.</param>
        /// <param name="bankStatementFileImport">bankStatementFileImport.</param>
        /// <returns>BankStatementFileImport.</returns>
        public static BankStatementFileImportModel PopulatePartialBankStatementFileImportModelFromEntity(this BankStatementFileImportModel bankStatementFileImportModel, BankStatementFileImport bankStatementFileImport)
        {
            if (bankStatementFileImportModel != null)
            {
                bankStatementFileImportModel.UniqueId = bankStatementFileImport.UniqueId;
                bankStatementFileImportModel.BankAccountDetailsUniqueId = bankStatementFileImport.BankAccountDetailsUniqueId;
                bankStatementFileImportModel.UploadedFileName = bankStatementFileImport.UploadedFileName;
                bankStatementFileImportModel.FileExtenstion = bankStatementFileImport.FileExtenstion;
                bankStatementFileImportModel.FileLengthInBytes = bankStatementFileImport.FileLengthInBytes;
                bankStatementFileImportModel.CurrentProcessStatus = bankStatementFileImport.CurrentProcessStatus;
                bankStatementFileImportModel.CurrentProcessStatusDate = bankStatementFileImport.CurrentProcessStatusDate;
            }

            return bankStatementFileImportModel;
        }
    }
}
