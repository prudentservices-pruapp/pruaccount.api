// <copyright file="BankStatementMapDetailFileImportProcessRepository.cs" company="PlaceholderCompany">
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
    /// BankStatementMapDetailFileImportProcessRepository.
    /// </summary>
    public class BankStatementMapDetailFileImportProcessRepository : RepositoryBase, IBankStatementMapDetailFileImportProcessRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementMapDetailFileImportProcessRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public BankStatementMapDetailFileImportProcessRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>BankStatementMapDetailFileImportProcess.</returns>
        public BankStatementMapDetailFileImportProcess FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<BankStatementMapDetailFileImportProcess>("[BankStatementMapDetailFileImportProcess_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        /// <returns>IEnumerable BankStatementMapDetailFileImportProcess.</returns>
        public IEnumerable<BankStatementMapDetailFileImportProcess> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
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

            return this.Connection.Query<BankStatementMapDetailFileImportProcess>("[BankStatementMapDetailFileImportProcess_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="bankStatementMapDetailFileImportProcess">BankStatementMapDetailFileImportProcess.</param>
        /// <returns>BankStatementFileImportProcess.</returns>
        public BankStatementMapDetailFileImportProcess Save(BankStatementMapDetailFileImportProcess bankStatementMapDetailFileImportProcess)
        {
            var para = new DynamicParameters();
            para.Add("@BankStatementMapDetailFileImportProcessId", bankStatementMapDetailFileImportProcess.BankStatementMapDetailFileImportProcessId);
            para.Add("@UniqueId", bankStatementMapDetailFileImportProcess.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", bankStatementMapDetailFileImportProcess.ClientBusinessDetailsUniqueId);
            para.Add("@BankAccountDetailsUniqueId", bankStatementMapDetailFileImportProcess.BankAccountDetailsUniqueId);
            para.Add("@BankStatementFileImportUniqueId", bankStatementMapDetailFileImportProcess.BankStatementMapDetailFileImportUniqueId);
            para.Add("@ProcessStatus", bankStatementMapDetailFileImportProcess.ProcessStatus);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[BankStatementFileImportProcess_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save bankStatementFileImportProcess for {bankStatementMapDetailFileImportProcess.BankStatementMapDetailFileImportUniqueId} - ProcessStatus {bankStatementMapDetailFileImportProcess.ProcessStatus}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bankStatementMapDetailFileImportProcess;
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
        public IEnumerable<BankStatementMapDetailFileImportProcess> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
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

            return this.Connection.Query<BankStatementMapDetailFileImportProcess>("[BankStatementMapDetailFileImportProcess_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
