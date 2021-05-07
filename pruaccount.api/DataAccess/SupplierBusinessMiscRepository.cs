// <copyright file="SupplierBusinessMiscRepository.cs" company="PlaceholderCompany">
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
    /// SupplierBusinessMiscRepository.
    /// </summary>
    public class SupplierBusinessMiscRepository : RepositoryBase, ISupplierBusinessMiscRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierBusinessMiscRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public SupplierBusinessMiscRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">Primary key.</param>
        /// <returns>SupplierBusinessMisc.</returns>
        public SupplierBusinessMisc FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<SupplierBusinessMisc>("[SupplierBusinessMisc_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        /// <summary>
        /// ListAll.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. SupplierBusinessDetailsUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId not needed.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable SupplierBusinessPaymentDetails.</returns>
        public IEnumerable<SupplierBusinessMisc> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default(Guid))
            {
                para.Add("@SupplierBusinessDetailsUniqueId", masterUniqueId);
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

            return this.Connection.Query<SupplierBusinessMisc>("[SupplierBusinessMisc_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="supplierBusinessMisc">SupplierBusinessMisc.</param>
        /// <returns>Supplier BusinessPaymentDetails.</returns>
        public SupplierBusinessMisc Save(SupplierBusinessMisc supplierBusinessMisc)
        {
            var para = new DynamicParameters();
            para.Add("@SupplierBusinessMiscId", supplierBusinessMisc.SupplierBusinessMiscId);
            para.Add("@UniqueId", supplierBusinessMisc.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", supplierBusinessMisc.ClientBusinessDetailsUniqueId);
            para.Add("@SupplierBusinessDetailsUniqueId", supplierBusinessMisc.SupplierBusinessDetailsUniqueId);
            para.Add("@TermsAndConditions", supplierBusinessMisc.TermsAndConditions);
            para.Add("@Notes", supplierBusinessMisc.Notes);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[SupplierBusinessMisc_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save Supplier business payment details for {supplierBusinessMisc.SupplierBusinessDetailsUniqueId}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return supplierBusinessMisc;
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. SupplierBusinessDetailsUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId not needed.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable SupplierBusinessAddress.</returns>
        public IEnumerable<SupplierBusinessMisc> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default(Guid))
            {
                para.Add("@SupplierBusinessDetailsUniqueId", masterUniqueId);
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

            return this.Connection.Query<SupplierBusinessMisc>("[SupplierBusinessMisc_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}