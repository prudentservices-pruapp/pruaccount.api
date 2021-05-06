// <copyright file="LedgerAccountRepository.cs" company="PlaceholderCompany">
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
    /// LedgerAccountRepository.
    /// </summary>
    public class LedgerAccountRepository : RepositoryBase, ILedgerAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LedgerAccountRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public LedgerAccountRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// ListAll.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. customerBusinessDetailsUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId e.g. InvoiceUniqueId.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderby">orderby.</param>
        /// <param name="pagenumber">pagenumber.</param>
        /// <param name="rowsperpage">rowsperpage.</param>
        /// <returns>IEnumerable LedgerAccount.</returns>
        public IEnumerable<LedgerAccount> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string sort, string orderby, int pagenumber, int rowsperpage)
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
                para.Add("@orderBy", orderby);
            }

            if (pagenumber != default(int))
            {
                para.Add("@pageNumber", pagenumber);
            }

            if (rowsperpage != default(int))
            {
                para.Add("@rowsPerPage", rowsperpage);
            }

            return this.Connection.Query<LedgerAccount>("[LedgerAccount_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">Guid.</param>
        /// <returns>NotImplementedException.</returns>
        public LedgerAccount FindByPID(Guid pid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="businessDetailsUniqueId">Client businessDetailsUniqueId.</param>
        /// <param name="ledgerAccountId">ledgerAccountId.</param>
        /// <returns>LedgerAccount.</returns>
        public LedgerAccount FindByPID(Guid businessDetailsUniqueId, int ledgerAccountId)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (ledgerAccountId != default(int))
            {
                para.Add("@LedgerAccountId", ledgerAccountId);
            }

            return this.Connection.Query<LedgerAccount>("[LedgerAccount_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        /// <summary>
        /// SearchLedgerAccounts.
        /// </summary>
        /// <param name="businessDetailsUniqueId">Client businessDetailsUniqueId.</param>
        /// <param name="dname">Display Name.</param>
        /// <param name="categoryGroupId">CategoryGroupId.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderby">orderBy.</param>
        /// <param name="pagenumber">pageNumber.</param>
        /// <param name="rowsperpage">rowsPerPage.</param>
        /// <returns>IEnumerable LedgerAccount.</returns>
        public IEnumerable<LedgerAccount> SearchLedgerAccounts(Guid businessDetailsUniqueId, string dname, int categoryGroupId, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (!string.IsNullOrEmpty(dname?.Trim()))
            {
                para.Add("@DName", dname.Trim());
            }

            if (categoryGroupId != default(int))
            {
                para.Add("@CategoryGroupId", categoryGroupId);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                para.Add("@sort", sort);
            }

            if (!string.IsNullOrEmpty(orderby))
            {
                para.Add("@orderBy", orderby);
            }

            if (pagenumber != default(int))
            {
                para.Add("@pageNumber", pagenumber);
            }

            if (rowsperpage != default(int))
            {
                para.Add("@rowsPerPage", rowsperpage);
            }

            return this.Connection.Query<LedgerAccount>("[LedgerAccount_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Remove.
        /// </summary>
        /// <param name="pid">Primary Key.</param>
        public void Remove(Guid pid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="ledgerAccount">LedgerAccount.</param>
        /// <returns>Saved LedgerAccount.</returns>
        public LedgerAccount Save(LedgerAccount ledgerAccount)
        {
            var para = new DynamicParameters();
            para.Add("@LedgerAccountId", ledgerAccount.LedgerAccountId);
            para.Add("@ClientBusinessDetailsUniqueId", ledgerAccount.ClientBusinessDetailsUniqueId);
            para.Add("@LName", ledgerAccount.LName);
            para.Add("@DName", ledgerAccount.DName);
            para.Add("@NominalCode", ledgerAccount.NominalCode);
            para.Add("@CategoryGroupId", ledgerAccount.CategoryGroupId);

            if (ledgerAccount.VatRateId > 0)
            {
                para.Add("@VatRateId", ledgerAccount.VatRateId);
            }

            para.Add("@IncludeInChart", ledgerAccount.IncludeInChart);
            para.Add("@M_Bank", ledgerAccount.M_Bank);
            para.Add("@M_Sales", ledgerAccount.M_Sales);
            para.Add("@M_Purchase", ledgerAccount.M_Purchase);
            para.Add("@M_Other_Payment", ledgerAccount.M_Other_Payment);
            para.Add("@M_Other_Receipt", ledgerAccount.M_Other_Receipt);
            para.Add("@M_Journals", ledgerAccount.M_Journals);
            para.Add("@M_Reports", ledgerAccount.M_Reports);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[LedgerAccount_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save ledgeraccount details for {ledgerAccount.DName}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ledgerAccount;
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. customerBusinessDetailsUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId e.g. InvoiceUniqueId.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>NotImplementedException.</returns>
        public IEnumerable<LedgerAccount> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="businessDetailsUniqueId">Client businessDetailsUniqueId.</param>
        public void Setup(Guid businessDetailsUniqueId)
        {
            var para = new DynamicParameters();
            para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[LedgerAccount_Setup]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not run LedgerAccount Setup");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
