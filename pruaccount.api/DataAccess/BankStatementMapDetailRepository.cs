// <copyright file="BankStatementMapDetailRepository.cs" company="PlaceholderCompany">
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
    /// BankStatementMapDetailRepository.
    /// </summary>
    public class BankStatementMapDetailRepository : RepositoryBase, IBankStatementMapDetailRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementMapDetailRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public BankStatementMapDetailRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>BankStatementMapDetail</returns>
        public BankStatementMapDetail FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<BankStatementMapDetail>("[BankStatementMapDetail_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        /// <summary>
        /// ListAll.
        /// </summary>
        /// <param name="businessDetailsUniqueId">not needed.</param>
        /// <param name="masterUniqueId">masterUniqueId not needed.</param>
        /// <param name="parentUniqueId">parentUniqueId not needed.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderby">orderby.</param>
        /// <param name="pagenumber">pagenumber.</param>
        /// <param name="rowsperpage">rowsperpage.</param>
        /// <returns>IEnumerable BankStatementMapDetail.</returns>
        public IEnumerable<BankStatementMapDetail> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
        {
            var para = new DynamicParameters();

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

            return this.Connection.Query<BankStatementMapDetail>("[BankStatementMapDetail_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="bankStatementMapDetail">bankStatementMapDetail.</param>
        /// <returns>BankStatementMapDetail.</returns>
        public BankStatementMapDetail Save(BankStatementMapDetail bankStatementMapDetail)
        {
            var para = new DynamicParameters();
            para.Add("@BankStatementMapDetailId", bankStatementMapDetail.BankStatementMapDetailId);
            para.Add("@UniqueId", bankStatementMapDetail.UniqueId);
            para.Add("@MapName", bankStatementMapDetail.MapName);
            para.Add("@DatePart1", bankStatementMapDetail.DatePart1);
            para.Add("@DatePart2", bankStatementMapDetail.DatePart2);
            para.Add("@DatePart3", bankStatementMapDetail.DatePart3);
            para.Add("@DateSeparator", bankStatementMapDetail.DateSeparator);
            para.Add("@Dateformat", bankStatementMapDetail.Dateformat);
            para.Add("@DateformatValue", bankStatementMapDetail.DateformatValue);
            para.Add("@DateIndex", bankStatementMapDetail.DateIndex);
            para.Add("@CreditAmountIndex", bankStatementMapDetail.CreditAmountIndex);
            para.Add("@DebitAmountIndex", bankStatementMapDetail.DebitAmountIndex);
            para.Add("@DescriptionIndex", bankStatementMapDetail.DescriptionIndex);
            para.Add("@BalanceIndex", bankStatementMapDetail.BalanceIndex);

            para.Add("@BankStatementMapDetailUniqueId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[BankStatementMapDetail_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save bankStatementMapDetail for {bankStatementMapDetail.MapName}");
                }

                bankStatementMapDetail.UniqueId = para.Get<Guid>("@BankStatementMapDetailUniqueId");
            }
            catch (Exception)
            {
                throw;
            }

            return bankStatementMapDetail;
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="businessDetailsUniqueId">businessDetailsUniqueId not needed.</param>
        /// <param name="masterUniqueId">masterUniqueId not needed.</param>
        /// <param name="parentUniqueId">parentUniqueId not needed.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderby">orderby.</param>
        /// <param name="pagenumber">pagenumber.</param>
        /// <param name="rowsperpage">rowsperpage.</param>
        /// <returns>IEnumerable BankStatementFileImport.</returns>
        public IEnumerable<BankStatementMapDetail> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            var para = new DynamicParameters();

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

            return this.Connection.Query<BankStatementMapDetail>("[BankStatementMapDetail_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
