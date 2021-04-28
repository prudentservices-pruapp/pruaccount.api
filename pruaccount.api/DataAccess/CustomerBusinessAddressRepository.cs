// <copyright file="CustomerBusinessAddressRepository.cs" company="PlaceholderCompany">
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
    /// CustomerBusinessAddressRepository.
    /// </summary>
    public class CustomerBusinessAddressRepository : RepositoryBase, ICustomerBusinessAddressRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBusinessAddressRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public CustomerBusinessAddressRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">Primary key.</param>
        /// <returns>CustomerBusinessAddress.</returns>
        public CustomerBusinessAddress FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<CustomerBusinessAddress>("[CustomerBusinessAddress_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        /// <summary>
        /// ListAll.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. customerBusinessDetailsUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId not needed.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable CustomerBusinessAddress.</returns>
        public IEnumerable<CustomerBusinessAddress> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default(Guid))
            {
                para.Add("@CustomerBusinessDetailsUniqueId", masterUniqueId);
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

            return this.Connection.Query<CustomerBusinessAddress>("[CustomerBusinessAddress_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="customerBusinessAddress">customerBusinessAddress.</param>
        /// <returns>CustomerBusinessAddress.</returns>
        public CustomerBusinessAddress Save(CustomerBusinessAddress customerBusinessAddress)
        {
            var para = new DynamicParameters();
            para.Add("@CustomerBusinessAddressId", customerBusinessAddress.CustomerBusinessAddressId);
            para.Add("@UniqueId", customerBusinessAddress.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", customerBusinessAddress.ClientBusinessDetailsUniqueId);
            para.Add("@CustomerBusinessDetailsUniqueId", customerBusinessAddress.CustomerBusinessDetailsUniqueId);
            para.Add("@AddressType", customerBusinessAddress.AddressType);
            para.Add("@Line1", customerBusinessAddress.Line1);
            para.Add("@Line2", customerBusinessAddress.Line2);
            para.Add("@City", customerBusinessAddress.City);
            para.Add("@County", customerBusinessAddress.County);
            para.Add("@PostCode", customerBusinessAddress.PostCode);
            para.Add("@Country", customerBusinessAddress.Country);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[CustomerBusinessAddress_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save customerBusinessAddress details for {customerBusinessAddress.AddressType} - {customerBusinessAddress.Line1}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customerBusinessAddress;
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. customerBusinessDetailsUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId not needed.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable CustomerBusinessAddress.</returns>
        public IEnumerable<CustomerBusinessAddress> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
            }

            if (masterUniqueId != default(Guid))
            {
                para.Add("@CustomerBusinessDetailsUniqueId", masterUniqueId);
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

            return this.Connection.Query<CustomerBusinessAddress>("[CustomerBusinessAddress_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
