// <copyright file="ILedgerAccountRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess
{
    using System.Collections.Generic;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Entities;

    /// <summary>
    /// ILedgerAccountRepository.
    /// </summary>
    public interface ILedgerAccountRepository : IRepositoryBase<LedgerAccount>
    {
        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="ledgerAccountId">ledgerAccountId.</param>
        /// <returns>LedgerAccount.</returns>
        LedgerAccount FindByPID(int ledgerAccountId);

        /// <summary>
        /// SearchLedgerAccounts.
        /// </summary>
        /// <param name="dname">Display Name.</param>
        /// <param name="categoryGroupId">CategoryGroupId.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderby">orderBy.</param>
        /// <param name="pagenumber">pageNumber.</param>
        /// <param name="rowsperpage">rowsPerPage.</param>
        /// <returns>IEnumerable LedgerAccount.</returns>
        IEnumerable<LedgerAccount> SearchLedgerAccounts(string dname, int categoryGroupId, string sort, string orderby, int pagenumber, int rowsperpage);
    }
}
