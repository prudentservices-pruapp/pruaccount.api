// <copyright file="BAOpeningBalanceRepository.cs" company="PlaceholderCompany">
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
    /// BAOpeningBalanceRepository.
    /// </summary>
    public class BAOpeningBalanceRepository : RepositoryBase, IBAOpeningBalanceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BAOpeningBalanceRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public BAOpeningBalanceRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>BAOpeningBalance.</returns>
        public BAOpeningBalance FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<BAOpeningBalance>("[BAOpeningBalance_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        /// <summary>
        /// ListAll.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId not needed.</param>
        /// <param name="parentUniqueId">parentUniqueId not needed.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable BAOpeningBalance.</returns>
        public IEnumerable<BAOpeningBalance> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
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

            return this.Connection.Query<BAOpeningBalance>("[BAOpeningBalance_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="baOpeningBalance">BAOpeningBalance.</param>
        /// <returns>Bank OpeningBalance.</returns>
        public BAOpeningBalance Save(BAOpeningBalance baOpeningBalance)
        {
            var para = new DynamicParameters();
            para.Add("@BankAccountDetailsId", baOpeningBalance.BAOpeningBalanceId);
            para.Add("@UniqueId", baOpeningBalance.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", baOpeningBalance.ClientBusinessDetailsUniqueId);
            para.Add("@BankAccountDetailsUniqueId", baOpeningBalance.BankAccountDetailsUniqueId);

            if (baOpeningBalance.LedgerAccountId != default)
            {
                para.Add("@LedgerAccountId", baOpeningBalance.LedgerAccountId);
            }

            para.Add("@BalanceDate", baOpeningBalance.BalanceDate);
            para.Add("@BAOpeningBalanceTypeId", baOpeningBalance.BAOpeningBalanceTypeId);
            para.Add("@BalanceAmount", baOpeningBalance.BalanceAmount);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[BAOpeningBalance_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save bank opening balance for {baOpeningBalance.BankAccountDetailsUniqueId}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return baOpeningBalance;
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId not needed.</param>
        /// <param name="parentUniqueId">parentUniqueId not needed.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable BAOpeningBalance.</returns>
        public IEnumerable<BAOpeningBalance> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
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

            return this.Connection.Query<BAOpeningBalance>("[BAOpeningBalance_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
