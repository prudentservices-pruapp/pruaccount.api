// <copyright file="BankStatementFileImportRepository.cs" company="PlaceholderCompany">
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
    /// BankStatementFileImportRepository.
    /// </summary>
    public class BankStatementFileImportRepository : RepositoryBase, IBankStatementFileImportRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementFileImportRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public BankStatementFileImportRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>BankStatementFileImport.</returns>
        public BankStatementFileImport FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<BankStatementFileImport>("[BankStatementFileImport_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        public IEnumerable<BankStatementFileImport> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default(Guid))
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

            return this.Connection.Query<BankStatementFileImport>("[BankStatementFileImport_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="bankStatementFileImport">bankStatementFileImport.</param>
        /// <returns>BankStatementFileImport.</returns>
        public BankStatementFileImport Save(BankStatementFileImport bankStatementFileImport)
        {
            var para = new DynamicParameters();
            para.Add("@BankStatementFileImportId", bankStatementFileImport.BankStatementFileImportId);
            para.Add("@UniqueId", bankStatementFileImport.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", bankStatementFileImport.ClientBusinessDetailsUniqueId);
            para.Add("@BankAccountDetailsUniqueId", bankStatementFileImport.BankAccountDetailsUniqueId);
            para.Add("@BankStatementMapDetailUniqueId", bankStatementFileImport.BankStatementMapDetailUniqueId);
            para.Add("@UploadedFileName", bankStatementFileImport.UploadedFileName);
            para.Add("@UploadedFilePath", bankStatementFileImport.UploadedFilePath);
            para.Add("@SystemGeneratedFileName", bankStatementFileImport.SystemGeneratedFileName);
            para.Add("@FileExtenstion", bankStatementFileImport.FileExtenstion);
            para.Add("@FileLengthInBytes", bankStatementFileImport.FileLengthInBytes);
            para.Add("@ProcessStatus", bankStatementFileImport.CurrentProcessStatus);
            para.Add("@BankStatementFileImportUniqueId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[BankStatementFileImport_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save bankAccountDetails for {bankStatementFileImport.UploadedFileName}");
                }

                bankStatementFileImport.UniqueId = para.Get<Guid>("@BankStatementFileImportUniqueId");
            }
            catch (Exception)
            {
                throw;
            }

            return bankStatementFileImport;
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
        /// <returns>IEnumerable BankStatementFileImport.</returns>
        public IEnumerable<BankStatementFileImport> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default(Guid))
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

            return this.Connection.Query<BankStatementFileImport>("[BankStatementFileImport_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
