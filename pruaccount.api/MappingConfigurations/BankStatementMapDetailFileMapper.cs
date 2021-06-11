// <copyright file="BankStatementMapDetailFileMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.MappingConfigurations
{
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementMapDetailFileMapper.
    /// </summary>
    public class BankStatementMapDetailFileMapper
    {
        /// <summary>
        /// PopulateFromModel
        /// BankStatementMapDetailFile populated From Model.
        /// </summary>
        /// <param name="bankStatementMapDetailFileModel">BankStatementMapDetailFileModel.</param>
        /// <returns>BankStatementMapDetailFile.</returns>
        public BankStatementMapDetailFile PopulateFromModel(BankStatementMapDetailFileModel bankStatementMapDetailFileModel)
        {
            BankStatementMapDetailFile bankStatementMapDetailFile = new BankStatementMapDetailFile();

            bankStatementMapDetailFile.UniqueId = bankStatementMapDetailFileModel.UniqueId;
            bankStatementMapDetailFile.ClientBusinessDetailsUniqueId = bankStatementMapDetailFileModel.ClientBusinessDetailsUniqueId;
            bankStatementMapDetailFile.BankAccountDetailsUniqueId = bankStatementMapDetailFileModel.BankAccountDetailsUniqueId;
            bankStatementMapDetailFile.BankStatementMapDetailUniqueId = bankStatementMapDetailFileModel.BankStatementMapDetailUniqueId;
            bankStatementMapDetailFile.UploadedFileName = bankStatementMapDetailFileModel.UploadedFileName;
            bankStatementMapDetailFile.UploadedFilePath = bankStatementMapDetailFileModel.UploadedFilePath;
            bankStatementMapDetailFile.SystemGeneratedFileName = bankStatementMapDetailFileModel.SystemGeneratedFileName;
            bankStatementMapDetailFile.FileExtenstion = bankStatementMapDetailFileModel.FileExtenstion;
            bankStatementMapDetailFile.FileLengthInBytes = bankStatementMapDetailFileModel.FileLengthInBytes;

            return bankStatementMapDetailFile;
        }

        /// <summary>
        /// PopulateFromEntity
        /// BankStatementMapDetailFileModel populated From Entity.
        /// </summary>
        /// <param name="bankStatementMapDetailFile">BankStatementMapDetailFile.</param>
        /// <returns>BankStatementMapDetailFileModel.</returns>
        public BankStatementMapDetailFileModel PopulateFromEntity(BankStatementMapDetailFile bankStatementMapDetailFile)
        {
            BankStatementMapDetailFileModel bankStatementMapDetailFileModel = new BankStatementMapDetailFileModel();

            bankStatementMapDetailFileModel.UniqueId = bankStatementMapDetailFile.UniqueId;
            bankStatementMapDetailFileModel.ClientBusinessDetailsUniqueId = bankStatementMapDetailFile.ClientBusinessDetailsUniqueId;
            bankStatementMapDetailFileModel.BankAccountDetailsUniqueId = bankStatementMapDetailFile.BankAccountDetailsUniqueId;
            bankStatementMapDetailFileModel.BankStatementMapDetailUniqueId = bankStatementMapDetailFile.BankStatementMapDetailUniqueId;
            bankStatementMapDetailFileModel.UploadedFileName = bankStatementMapDetailFile.UploadedFileName;
            bankStatementMapDetailFileModel.UploadedFilePath = bankStatementMapDetailFile.UploadedFilePath;
            bankStatementMapDetailFileModel.SystemGeneratedFileName = bankStatementMapDetailFile.SystemGeneratedFileName;
            bankStatementMapDetailFileModel.FileExtenstion = bankStatementMapDetailFile.FileExtenstion;
            bankStatementMapDetailFileModel.FileLengthInBytes = bankStatementMapDetailFile.FileLengthInBytes;

            return bankStatementMapDetailFileModel;
        }

        /// <summary>
        /// PopulatePartialModelFromEntity.
        /// </summary>
        /// <param name="bankStatementMapDetailFile">BankStatementMapDetailFile.</param>
        /// <returns>BankStatementMapDetailFileModel.</returns>
        public BankStatementMapDetailFileModel PopulatePartialModelFromEntity(BankStatementMapDetailFile bankStatementMapDetailFile)
        {
            BankStatementMapDetailFileModel bankStatementMapDetailFileModel = new BankStatementMapDetailFileModel();

            bankStatementMapDetailFileModel.UniqueId = bankStatementMapDetailFile.UniqueId;
            bankStatementMapDetailFileModel.BankAccountDetailsUniqueId = bankStatementMapDetailFile.BankAccountDetailsUniqueId;
            bankStatementMapDetailFileModel.BankStatementMapDetailUniqueId = bankStatementMapDetailFile.BankStatementMapDetailUniqueId;
            bankStatementMapDetailFileModel.UploadedFileName = bankStatementMapDetailFile.UploadedFileName;
            bankStatementMapDetailFileModel.FileExtenstion = bankStatementMapDetailFile.FileExtenstion;
            bankStatementMapDetailFileModel.FileLengthInBytes = bankStatementMapDetailFile.FileLengthInBytes;

            return bankStatementMapDetailFileModel;
        }
    }
}
