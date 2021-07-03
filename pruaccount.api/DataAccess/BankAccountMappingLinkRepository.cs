// <copyright file="BankAccountMappingLinkRepository.cs" company="PlaceholderCompany">
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
    /// BankAccountMappingLinkRepository.
    /// </summary>
    public class BankAccountMappingLinkRepository : RepositoryBase, IBankAccountMappingLinkRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountMappingLinkRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public BankAccountMappingLinkRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>BankAccountMappingLink.</returns>
        public BankAccountMappingLink FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<BankAccountMappingLink>("[BankAccountMappingLink_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        /// <summary>
        /// ListAll.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">BankStatementMapDetailUniqueId.</param>
        /// <param name="parentUniqueId">BankAccountDetailsUniqueId.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable BankAccountMappingLink.</returns>
        public IEnumerable<BankAccountMappingLink> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default(Guid))
            {
                para.Add("@BankStatementMapDetailUniqueId", masterUniqueId);
            }

            if (parentUniqueId != default(Guid))
            {
                para.Add("@BankAccountDetailsUniqueId", parentUniqueId);
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

            return this.Connection.Query<BankAccountMappingLink>("[BankAccountMappingLink_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="bankAccountMappingLink">BankAccountMappingLink.</param>
        /// <returns>Bank AccountMappingLink.</returns>
        public BankAccountMappingLink Save(BankAccountMappingLink bankAccountMappingLink)
        {
            var para = new DynamicParameters();
            para.Add("@BankAccountMappingLinkId", bankAccountMappingLink.BankAccountMappingLinkId);
            para.Add("@UniqueId", bankAccountMappingLink.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", bankAccountMappingLink.ClientBusinessDetailsUniqueId);
            para.Add("@BankAccountDetailsUniqueId", bankAccountMappingLink.BankAccountDetailsUniqueId);
            para.Add("@BankStatementMapDetailUniqueId", bankAccountMappingLink.BankStatementMapDetailUniqueId);
            para.Add("@IsActive", bankAccountMappingLink.IsActive);
            para.Add("@BankAccountMappingLinkUniqueId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[BankAccountMappingLink_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save bankAccountMappingLink for BankStatementMapDetailUniqueId - {bankAccountMappingLink.BankStatementMapDetailUniqueId} ");
                }

                bankAccountMappingLink.UniqueId = para.Get<Guid>("@BankAccountMappingLinkUniqueId");
            }
            catch (Exception)
            {
                throw;
            }

            return bankAccountMappingLink;
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="businessDetailsUniqueId">businessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">BankStatementMapDetailUniqueId.</param>
        /// <param name="parentUniqueId">BankAccountDetailsUniqueId.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderby">orderby.</param>
        /// <param name="pagenumber">pagenumber.</param>
        /// <param name="rowsperpage">rowsperpage.</param>
        /// <returns>IEnumerable BankAccountMappingLink.</returns>
        public IEnumerable<BankAccountMappingLink> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default)
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default(Guid))
            {
                para.Add("@BankStatementMapDetailUniqueId", masterUniqueId);
            }

            if (parentUniqueId != default(Guid))
            {
                para.Add("@BankAccountDetailsUniqueId", parentUniqueId);
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

            return this.Connection.Query<BankAccountMappingLink>("[BankAccountMappingLink_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
