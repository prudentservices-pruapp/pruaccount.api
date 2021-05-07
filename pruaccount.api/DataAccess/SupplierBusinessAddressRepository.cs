// <copyright file="SupplierBusinessAddressRepository.cs" company="PlaceholderCompany">
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
    /// SupplierBusinessAddressRepository.
    /// </summary>
    public class SupplierBusinessAddressRepository : RepositoryBase, ISupplierBusinessAddressRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierBusinessAddressRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public SupplierBusinessAddressRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">Primary key.</param>
        /// <returns>SupplierBusinessAddress.</returns>
        public SupplierBusinessAddress FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<SupplierBusinessAddress>("[SupplierBusinessAddress_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        /// <returns>IEnumerable SupplierBusinessAddress.</returns>
        public IEnumerable<SupplierBusinessAddress> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
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

            return this.Connection.Query<SupplierBusinessAddress>("[SupplierBusinessAddress_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="supplierBusinessAddress">SupplierBusinessAddress.</param>
        /// <returns>Supplier BusinessAddress.</returns>
        public SupplierBusinessAddress Save(SupplierBusinessAddress supplierBusinessAddress)
        {
            var para = new DynamicParameters();
            para.Add("@SupplierBusinessAddressId", supplierBusinessAddress.SupplierBusinessAddressId);
            para.Add("@UniqueId", supplierBusinessAddress.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", supplierBusinessAddress.ClientBusinessDetailsUniqueId);
            para.Add("@SupplierBusinessDetailsUniqueId", supplierBusinessAddress.SupplierBusinessDetailsUniqueId);
            para.Add("@AddressType", supplierBusinessAddress.AddressType);
            para.Add("@Line1", supplierBusinessAddress.Line1);
            para.Add("@Line2", supplierBusinessAddress.Line2);
            para.Add("@City", supplierBusinessAddress.City);
            para.Add("@County", supplierBusinessAddress.County);
            para.Add("@PostCode", supplierBusinessAddress.PostCode);
            para.Add("@Country", supplierBusinessAddress.Country);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[SupplierBusinessAddress_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save SupplierBusinessAddress details for {supplierBusinessAddress.AddressType} - {supplierBusinessAddress.Line1}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return supplierBusinessAddress;
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
        public IEnumerable<SupplierBusinessAddress> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
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

            return this.Connection.Query<SupplierBusinessAddress>("[SupplierBusinessAddress_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}