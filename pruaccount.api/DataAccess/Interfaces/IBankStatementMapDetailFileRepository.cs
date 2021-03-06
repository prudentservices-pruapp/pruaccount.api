﻿// <copyright file="IBankStatementMapDetailFileRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Entities;

    /// <summary>
    /// IBankStatementMapDetailFileRepository.
    /// </summary>
    public interface IBankStatementMapDetailFileRepository : IRepositoryBase<BankStatementMapDetailFile>
    {
        /// <summary>
        /// FindByMapDetailUniqueId.
        /// </summary>
        /// <param name="mapDetailUniqueId">Bank Statement Map Detail UniqueId.</param>
        /// <returns>BankStatementMapDetailFile.</returns>
        BankStatementMapDetailFile FindByMapDetailUniqueId(Guid mapDetailUniqueId);
    }
}
