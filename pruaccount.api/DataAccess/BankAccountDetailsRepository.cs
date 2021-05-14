// <copyright file="BankAccountDetailsRepository.cs" company="PlaceholderCompany">
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
    /// BankAccountDetailsRepository.
    /// </summary>
    public class BankAccountDetailsRepository : RepositoryBase, IBankAccountDetailsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountDetailsRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public BankAccountDetailsRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>BankAccountDetails.</returns>
        public BankAccountDetails FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<BankAccountDetails>("[BankAccountDetails_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        /// <returns>IEnumerable BankAccountDetails.</returns>
        public IEnumerable<BankAccountDetails> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
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

            return this.Connection.Query<BankAccountDetails>("[BankAccountDetails_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="bankAccountDetails">BankAccountDetails.</param>
        /// <returns>Bank AccountDetails.</returns>
        public BankAccountDetails Save(BankAccountDetails bankAccountDetails)
        {
            var para = new DynamicParameters();
            para.Add("@BankAccountDetailsId", bankAccountDetails.BankAccountDetailsId);
            para.Add("@UniqueId", bankAccountDetails.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", bankAccountDetails.ClientBusinessDetailsUniqueId);
            para.Add("@BankAccountTypeId", bankAccountDetails.BankAccountTypeId);
            para.Add("@BankTransactionMethodId", bankAccountDetails.BankTransactionMethodId);
            para.Add("@LedgerAccountId", bankAccountDetails.LedgerAccountId);
            para.Add("@AccountName", bankAccountDetails.AccountName);
            para.Add("@SortCode", bankAccountDetails.SortCode);
            para.Add("@AccountNumber", bankAccountDetails.AccountNumber);
            para.Add("@BicSwift", bankAccountDetails.BicSwift);
            para.Add("@IBAN", bankAccountDetails.IBAN);
            para.Add("@CardLast4Digits", bankAccountDetails.CardLast4Digits);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[BankAccountDetails_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save bankAccountDetails for {bankAccountDetails.AccountName}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bankAccountDetails;
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
        /// <returns>IEnumerable BankAccountDetails.</returns>
        public IEnumerable<BankAccountDetails> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
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

            return this.Connection.Query<BankAccountDetails>("[BankAccountDetails_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
