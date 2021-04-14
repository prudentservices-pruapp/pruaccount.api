// <copyright file="CBFinancialSettingRepository.cs" company="PlaceholderCompany">
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
    using Pruaccount.Api.Entities;

    /// <summary>
    /// CBFinancialSettingRepository.
    /// </summary>
    public class CBFinancialSettingRepository : RepositoryBase, ICBFinancialSettingRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CBFinancialSettingRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public CBFinancialSettingRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="item">CBFinancialSetting.</param>
        /// <returns>NotImplementedException.</returns>
        public CBFinancialSetting Add(CBFinancialSetting item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// FindAll.
        /// </summary>
        /// <returns>NotImplementedException.</returns>
        public IEnumerable<CBFinancialSetting> FindAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// FindAll.
        /// </summary>
        /// <param name="sort">sort.</param>
        /// <param name="orderby">orderby.</param>
        /// <param name="pagenumber">pagenumber.</param>
        /// <param name="rowsperpage">rowsperpage.</param>
        /// <returns>IEnumerable CBFinancialSetting.</returns>
        public IEnumerable<CBFinancialSetting> FindAll(string sort, string orderby, int pagenumber, int rowsperpage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// FindByFID.
        /// </summary>
        /// <param name="fid">ClientBusinessDetailsUniqueId.</param>
        /// <returns>IEnumerable CBFinancialSetting.</returns>
        public IEnumerable<CBFinancialSetting> FindByFID(Guid fid)
        {
            var para = new DynamicParameters();

            if (fid != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", fid);
            }

            return this.Connection.Query<CBFinancialSetting>("[CBFinancialSetting_Select]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">UniqueId.</param>
        /// <returns>CBFinancialSetting.</returns>
        public CBFinancialSetting FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<CBFinancialSetting>("[CBFinancialSetting_Select]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        /// <param name="cbFinancialSetting">CBFinancialSetting.</param>
        /// <returns>Data CBFinancialSetting.</returns>
        public CBFinancialSetting Save(CBFinancialSetting cbFinancialSetting)
        {
            var para = new DynamicParameters();
            para.Add("@UniqueId", cbFinancialSetting.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", cbFinancialSetting.ClientBusinessDetailsUniqueId);

            if (cbFinancialSetting.YearStartDate != default(DateTime))
            {
                para.Add("@YearStartDate", cbFinancialSetting.YearStartDate);
            }

            if (cbFinancialSetting.YearEndDate != default(DateTime))
            {
                para.Add("@YearEndDate", cbFinancialSetting.YearEndDate);
            }

            if (cbFinancialSetting.YearEndLockdownDate != default(DateTime))
            {
                para.Add("@YearEndDate", cbFinancialSetting.YearEndLockdownDate);
            }

            if (!string.IsNullOrEmpty(cbFinancialSetting.YearEndTaxMonth))
            {
                para.Add("@YearEndTaxMonth", cbFinancialSetting.YearEndTaxMonth);
            }

            if (cbFinancialSetting.RetentionPeriod != default(int))
            {
                para.Add("@RetentionPeriod", cbFinancialSetting.RetentionPeriod);
            }

            if (!string.IsNullOrEmpty(cbFinancialSetting.VatScheme))
            {
                para.Add("@VatScheme", cbFinancialSetting.VatScheme);
            }

            if (!string.IsNullOrEmpty(cbFinancialSetting.VatSubmissionRequency))
            {
                para.Add("@VatSubmissionRequency", cbFinancialSetting.VatSubmissionRequency);
            }

            if (!string.IsNullOrEmpty(cbFinancialSetting.VatNumber))
            {
                para.Add("@VatNumber", cbFinancialSetting.VatNumber);
            }

            if (cbFinancialSetting.VatFlatRate != default(int))
            {
                para.Add("@VatFlatRate", cbFinancialSetting.VatFlatRate);
            }

            para.Add("@HMRCUserId", cbFinancialSetting.HMRCUserId);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[CBFinancialSetting_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save client financial setting for {cbFinancialSetting.ClientBusinessDetailsUniqueId}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return cbFinancialSetting;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="item">CBFinancialSetting.</param>
        /// <returns>NotImplementedException.</returns>
        public CBFinancialSetting Update(CBFinancialSetting item)
        {
            throw new NotImplementedException();
        }
    }
}
