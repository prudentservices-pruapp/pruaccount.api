// <copyright file="ILedgerAccountRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess.Interfaces
{
    using System;
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
        /// <param name="businessDetailsUniqueId">Client businessDetailsUniqueId.</param>
        /// <param name="ledgerAccountId">ledgerAccountId.</param>
        /// <returns>LedgerAccount.</returns>
        LedgerAccount FindByPID(Guid businessDetailsUniqueId, int ledgerAccountId);

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="businessDetailsUniqueId">Client businessDetailsUniqueId.</param>
        void Setup(Guid businessDetailsUniqueId);

        /// <summary>
        /// SearchByNominalCode.
        /// </summary>
        /// <param name="businessDetailsUniqueId">Client businessDetailsUniqueId.</param>
        /// <param name="nominalCode">Client nominalCode.</param>
        /// <returns>LedgerAccount.</returns>
        LedgerAccount SearchByNominalCode(Guid businessDetailsUniqueId, int nominalCode);

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
        IEnumerable<LedgerAccount> SearchLedgerAccounts(Guid businessDetailsUniqueId, string dname, int categoryGroupId, string sort, string orderby, int pagenumber, int rowsperpage);
    }
}
