// <copyright file="CustomerBusinessMiscRepository.cs" company="PlaceholderCompany">
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
    /// CustomerBusinessMiscRepository.
    /// </summary>
    public class CustomerBusinessMiscRepository : RepositoryBase, ICustomerBusinessMiscRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBusinessMiscRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public CustomerBusinessMiscRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">Primary key.</param>
        /// <returns>CustomerBusinessMisc.</returns>
        public CustomerBusinessMisc FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<CustomerBusinessMisc>("[CustomerBusinessMisc_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        /// <returns>IEnumerable CustomerBusinessPaymentDetails.</returns>
        public IEnumerable<CustomerBusinessMisc> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
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

            return this.Connection.Query<CustomerBusinessMisc>("[CustomerBusinessMisc_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="customerBusinessMisc">CustomerBusinessMisc.</param>
        /// <returns>Customer BusinessPaymentDetails.</returns>
        public CustomerBusinessMisc Save(CustomerBusinessMisc customerBusinessMisc)
        {
            var para = new DynamicParameters();
            para.Add("@CustomerBusinessMiscId", customerBusinessMisc.CustomerBusinessMiscId);
            para.Add("@UniqueId", customerBusinessMisc.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", customerBusinessMisc.ClientBusinessDetailsUniqueId);
            para.Add("@CustomerBusinessDetailsUniqueId", customerBusinessMisc.CustomerBusinessDetailsUniqueId);
            para.Add("@TermsAndConditions", customerBusinessMisc.TermsAndConditions);
            para.Add("@Notes", customerBusinessMisc.Notes);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[CustomerBusinessMisc_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save customer business payment details for {customerBusinessMisc.CustomerBusinessDetailsUniqueId}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customerBusinessMisc;
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
        public IEnumerable<CustomerBusinessMisc> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
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

            return this.Connection.Query<CustomerBusinessMisc>("[CustomerBusinessMisc_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
