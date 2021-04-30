// <copyright file="CustomerBusinessPaymentDetails.cs" company="PlaceholderCompany">
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
    /// CustomerBusinessPaymentDetails.
    /// </summary>
    public class CustomerBusinessPaymentDetailsRepository : RepositoryBase, ICustomerBusinessPaymentDetailsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBusinessPaymentDetailsRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public CustomerBusinessPaymentDetailsRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">Primary key.</param>
        /// <returns>CustomerBusinessPaymentDetails.</returns>
        public CustomerBusinessPaymentDetails FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<CustomerBusinessPaymentDetails>("[CustomerBusinessPaymentDetails_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        public IEnumerable<CustomerBusinessPaymentDetails> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
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

            return this.Connection.Query<CustomerBusinessPaymentDetails>("[CustomerBusinessPaymentDetails_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="customerBusinessPaymentDetails">CustomerBusinessPaymentDetails.</param>
        /// <returns>Customer BusinessPaymentDetails.</returns>
        public CustomerBusinessPaymentDetails Save(CustomerBusinessPaymentDetails customerBusinessPaymentDetails)
        {
            var para = new DynamicParameters();
            para.Add("@CustomerBusinessPaymentDetailsId", customerBusinessPaymentDetails.CustomerBusinessPaymentDetailsId);
            para.Add("@UniqueId", customerBusinessPaymentDetails.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", customerBusinessPaymentDetails.ClientBusinessDetailsUniqueId);
            para.Add("@CustomerBusinessDetailsUniqueId", customerBusinessPaymentDetails.CustomerBusinessDetailsUniqueId);
            para.Add("@PaymentType", customerBusinessPaymentDetails.PaymentType);
            para.Add("@AccountName", customerBusinessPaymentDetails.AccountName);
            para.Add("@SortCode", customerBusinessPaymentDetails.SortCode);
            para.Add("@AccountNumber", customerBusinessPaymentDetails.AccountNumber);
            para.Add("@BicSwift", customerBusinessPaymentDetails.BicSwift);
            para.Add("@IBAN", customerBusinessPaymentDetails.IBAN);
            para.Add("@CreditLimit", customerBusinessPaymentDetails.CreditLimit);
            para.Add("@CreditTermInDays", customerBusinessPaymentDetails.CreditTermInDays);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[CustomerBusinessPaymentDetails_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save customer business payment details for {customerBusinessPaymentDetails.PaymentType} - {customerBusinessPaymentDetails.AccountName}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customerBusinessPaymentDetails;
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
        public IEnumerable<CustomerBusinessPaymentDetails> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
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

            return this.Connection.Query<CustomerBusinessPaymentDetails>("[CustomerBusinessPaymentDetails_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
