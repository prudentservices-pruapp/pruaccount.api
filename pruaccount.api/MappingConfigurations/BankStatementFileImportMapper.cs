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
    public class BankStatementFileImportMapper
    {
        /// <summary>
        /// PopulateFromModel
        /// BankStatementFileImport populated From Model.
        /// </summary>
        /// <param name="bankStatementFileImportModel">bankStatementFileImportModel.</param>
        /// <returns>BankAccountDetails.</returns>
        public BankStatementFileImport PopulateFromModel(BankStatementFileImportModel bankStatementFileImportModel)
        {
            BankStatementFileImport bankStatementFileImport = new BankStatementFileImport();

            bankStatementFileImport.UniqueId = bankStatementFileImportModel.UniqueId;
            bankStatementFileImport.ClientBusinessDetailsUniqueId = bankStatementFileImportModel.ClientBusinessDetailsUniqueId;
            bankStatementFileImport.BankAccountDetailsUniqueId = bankStatementFileImportModel.BankAccountDetailsUniqueId;
            bankStatementFileImport.BankStatementMapDetailUniqueId = bankStatementFileImportModel.BankStatementMapDetailUniqueId;
            bankStatementFileImport.UploadedFileName = bankStatementFileImportModel.UploadedFileName;
            bankStatementFileImport.UploadedFilePath = bankStatementFileImportModel.UploadedFilePath;
            bankStatementFileImport.SystemGeneratedFileName = bankStatementFileImportModel.SystemGeneratedFileName;
            bankStatementFileImport.FileExtenstion = bankStatementFileImportModel.FileExtenstion;
            bankStatementFileImport.FileLengthInBytes = bankStatementFileImportModel.FileLengthInBytes;
            bankStatementFileImport.CurrentProcessStatus = bankStatementFileImportModel.CurrentProcessStatus;
            bankStatementFileImport.CurrentProcessStatusDate = bankStatementFileImportModel.CurrentProcessStatusDate;

            return bankStatementFileImport;
        }

        /// <summary>
        /// PopulateFromEntity
        /// BankStatementFileImportModel populated From Entity.
        /// </summary>
        /// <param name="bankStatementFileImport">bankStatementFileImport.</param>
        /// <returns>BankStatementFileImport.</returns>
        public BankStatementFileImportModel PopulateFromEntity(BankStatementFileImport bankStatementFileImport)
        {
            BankStatementFileImportModel bankStatementFileImportModel = new BankStatementFileImportModel();

            bankStatementFileImportModel.UniqueId = bankStatementFileImport.UniqueId;
            bankStatementFileImportModel.ClientBusinessDetailsUniqueId = bankStatementFileImport.ClientBusinessDetailsUniqueId;
            bankStatementFileImportModel.BankAccountDetailsUniqueId = bankStatementFileImport.BankAccountDetailsUniqueId;
            bankStatementFileImportModel.BankStatementMapDetailUniqueId = bankStatementFileImport.BankStatementMapDetailUniqueId;
            bankStatementFileImportModel.UploadedFileName = bankStatementFileImport.UploadedFileName;
            bankStatementFileImportModel.UploadedFilePath = bankStatementFileImport.UploadedFilePath;
            bankStatementFileImportModel.SystemGeneratedFileName = bankStatementFileImport.SystemGeneratedFileName;
            bankStatementFileImportModel.FileExtenstion = bankStatementFileImport.FileExtenstion;
            bankStatementFileImportModel.FileLengthInBytes = bankStatementFileImport.FileLengthInBytes;
            bankStatementFileImportModel.CurrentProcessStatus = bankStatementFileImport.CurrentProcessStatus;
            bankStatementFileImportModel.CurrentProcessStatusDate = bankStatementFileImport.CurrentProcessStatusDate;

            return bankStatementFileImportModel;
        }

        /// <summary>
        /// PopulatePartialModelFromEntity.
        /// </summary>
        /// <param name="bankStatementFileImport">bankStatementFileImport.</param>
        /// <returns>BankStatementFileImport.</returns>
        public BankStatementFileImportModel PopulatePartialModelFromEntity(BankStatementFileImport bankStatementFileImport)
        {
            BankStatementFileImportModel bankStatementFileImportModel = new BankStatementFileImportModel();

            bankStatementFileImportModel.UniqueId = bankStatementFileImport.UniqueId;
            bankStatementFileImportModel.BankAccountDetailsUniqueId = bankStatementFileImport.BankAccountDetailsUniqueId;
            bankStatementFileImportModel.BankStatementMapDetailUniqueId = bankStatementFileImport.BankStatementMapDetailUniqueId;
            bankStatementFileImportModel.UploadedFileName = bankStatementFileImport.UploadedFileName;
            bankStatementFileImportModel.FileExtenstion = bankStatementFileImport.FileExtenstion;
            bankStatementFileImportModel.FileLengthInBytes = bankStatementFileImport.FileLengthInBytes;
            bankStatementFileImportModel.CurrentProcessStatus = bankStatementFileImport.CurrentProcessStatus;
            bankStatementFileImportModel.CurrentProcessStatusDate = bankStatementFileImport.CurrentProcessStatusDate;

            return bankStatementFileImportModel;
        }
    }
}
