// <copyright file="BankStatementMapDetailFileRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.DataAccess.Interfaces;
    using Pruaccount.Api.Entities;

    /// <summary>
    /// BankStatementMapDetailFileRepository.
    /// </summary>
    public class BankStatementMapDetailFileRepository : RepositoryBase, IBankStatementMapDetailFileRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementMapDetailFileRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public BankStatementMapDetailFileRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByMapDetailUniqueId.
        /// </summary>
        /// <param name="mapDetailUniqueId">mapDetailUniqueId.</param>
        /// <returns>BankStatementMapDetailFile.</returns>
        public BankStatementMapDetailFile FindByMapDetailUniqueId(Guid mapDetailUniqueId)
        {
            var para = new DynamicParameters();

            if (mapDetailUniqueId != default(Guid))
            {
                para.Add("@BankStatementMapDetailUniqueId", mapDetailUniqueId);
            }

            return this.Connection.Query<BankStatementMapDetailFile>("[BankStatementMapDetailFile_DetailByMapDetailUniqueId]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>BankStatementMapDetailFile.</returns>
        public BankStatementMapDetailFile FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<BankStatementMapDetailFile>("[BankStatementMapDetailFile_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        /// <summary>
        /// ListAll.
        /// </summary>
        /// <param name="businessDetailsUniqueId">businessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">BankAccountDetailsUniqueId.</param>
        /// <param name="parentUniqueId">not needed.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderby">orderby.</param>
        /// <param name="pagenumber">pagenumber.</param>
        /// <param name="rowsperpage">rowsperpage.</param>
        /// <returns>IEnumerable BankStatementFileImport.</returns>
        public IEnumerable<BankStatementMapDetailFile> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default)
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default)
            {
                para.Add("@BankAccountDetailsUniqueId", masterUniqueId);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                para.Add("@sort", sort);
            }

            if (!string.IsNullOrEmpty(orderby))
            {
                para.Add("@orderby", orderby);
            }

            if (pagenumber != default(int))
            {
                para.Add("@pagenumber", pagenumber);
            }

            if (rowsperpage != default(int))
            {
                para.Add("@rowsperpage", rowsperpage);
            }

            return this.Connection.Query<BankStatementMapDetailFile>("[BankStatementMapDetailFile_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Remove.
        /// </summary>
        /// <param name="pid">pid.</param>
        public void Remove(Guid pid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="bankStatementMapDetailFile">bankStatementMapDetailFile.</param>
        /// <returns>BankStatementMapDetailFile.</returns>
        public BankStatementMapDetailFile Save(BankStatementMapDetailFile bankStatementMapDetailFile)
        {
            var para = new DynamicParameters();
            para.Add("@BankStatementMapDetailFileId", bankStatementMapDetailFile.BankStatementMapDetailFileId);
            para.Add("@UniqueId", bankStatementMapDetailFile.UniqueId);

            if (bankStatementMapDetailFile.ClientBusinessDetailsUniqueId != default)
            {
                para.Add("@ClientBusinessDetailsUniqueId", bankStatementMapDetailFile.ClientBusinessDetailsUniqueId);
            }

            if (bankStatementMapDetailFile.BankAccountDetailsUniqueId != default)
            {
                para.Add("@BankAccountDetailsUniqueId", bankStatementMapDetailFile.BankAccountDetailsUniqueId);
            }

            if (bankStatementMapDetailFile.BankStatementMapDetailUniqueId != default)
            {
                para.Add("@BankStatementMapDetailUniqueId", bankStatementMapDetailFile.BankStatementMapDetailUniqueId);
            }

            para.Add("@UploadedFileName", bankStatementMapDetailFile.UploadedFileName);
            para.Add("@UploadedFilePath", bankStatementMapDetailFile.UploadedFilePath);
            para.Add("@SystemGeneratedFileName", bankStatementMapDetailFile.SystemGeneratedFileName);
            para.Add("@FileExtenstion", bankStatementMapDetailFile.FileExtenstion);
            para.Add("@FileLengthInBytes", bankStatementMapDetailFile.FileLengthInBytes);
            para.Add("@ProcessStatus", bankStatementMapDetailFile.CurrentProcessStatus);
            para.Add("@BankStatementMapDetailFileUniqueId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[BankStatementMapDetailFile_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save bankStatementMapDetailFile for {bankStatementMapDetailFile.UploadedFileName}");
                }

                bankStatementMapDetailFile.UniqueId = para.Get<Guid>("@BankStatementMapDetailFileUniqueId");
            }
            catch (Exception)
            {
                throw;
            }

            return bankStatementMapDetailFile;
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="businessDetailsUniqueId">businessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">BankAccountDetailsUniqueId.</param>
        /// <param name="parentUniqueId">not needed.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderby">orderby.</param>
        /// <param name="pagenumber">pagenumber.</param>
        /// <param name="rowsperpage">rowsperpage.</param>
        /// <returns>IEnumerable BankStatementMapDetailFile.</returns>
        public IEnumerable<BankStatementMapDetailFile> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default)
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default)
            {
                para.Add("@BankAccountDetailsUniqueId", masterUniqueId);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                para.Add("@searchTerm", searchTerm);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                para.Add("@sort", sort);
            }

            if (!string.IsNullOrEmpty(orderby))
            {
                para.Add("@orderby", orderby);
            }

            if (pagenumber != default(int))
            {
                para.Add("@pagenumber", pagenumber);
            }

            if (rowsperpage != default(int))
            {
                para.Add("@rowsperpage", rowsperpage);
            }

            return this.Connection.Query<BankStatementMapDetailFile>("[BankStatementMapDetailFile_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
