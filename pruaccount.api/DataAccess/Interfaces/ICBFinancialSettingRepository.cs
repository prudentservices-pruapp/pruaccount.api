// <copyright file="ICBFinancialSettingRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Entities;

    /// <summary>
    /// ICBFinancialSettingRepository.
    /// </summary>
    public interface ICBFinancialSettingRepository : IRepositoryBase<CBFinancialSetting>
    {
        /// <summary>
        /// FindByFID.
        /// </summary>
        /// <param name="fid">ClientBusinessDetailsUniqueId.</param>
        /// <returns>IEnumerable CBFinancialSetting.</returns>
        IEnumerable<CBFinancialSetting> FindByFID(Guid fid);
    }
}
