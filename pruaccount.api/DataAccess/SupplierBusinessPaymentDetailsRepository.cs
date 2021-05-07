// <copyright file="SupplierBusinessPaymentDetailsRepository.cs" company="PlaceholderCompany">
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
    /// SupplierBusinessPaymentDetails.
    /// </summary>
    public class SupplierBusinessPaymentDetailsRepository : RepositoryBase, ISupplierBusinessPaymentDetailsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierBusinessPaymentDetailsRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public SupplierBusinessPaymentDetailsRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">Primary key.</param>
        /// <returns>SupplierBusinessPaymentDetails.</returns>
        public SupplierBusinessPaymentDetails FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<SupplierBusinessPaymentDetails>("[SupplierBusinessPaymentDetails_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        public IEnumerable<SupplierBusinessPaymentDetails> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
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

            return this.Connection.Query<SupplierBusinessPaymentDetails>("[SupplierBusinessPaymentDetails_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// <param name="supplierBusinessPaymentDetails">SupplierBusinessPaymentDetails.</param>
        /// <returns>Supplier BusinessPaymentDetails.</returns>
        public SupplierBusinessPaymentDetails Save(SupplierBusinessPaymentDetails supplierBusinessPaymentDetails)
        {
            var para = new DynamicParameters();
            para.Add("@SupplierBusinessPaymentDetailsId", supplierBusinessPaymentDetails.SupplierBusinessPaymentDetailsId);
            para.Add("@UniqueId", supplierBusinessPaymentDetails.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", supplierBusinessPaymentDetails.ClientBusinessDetailsUniqueId);
            para.Add("@SupplierBusinessDetailsUniqueId", supplierBusinessPaymentDetails.SupplierBusinessDetailsUniqueId);
            para.Add("@PaymentType", supplierBusinessPaymentDetails.PaymentType);
            para.Add("@AccountName", supplierBusinessPaymentDetails.AccountName);
            para.Add("@SortCode", supplierBusinessPaymentDetails.SortCode);
            para.Add("@AccountNumber", supplierBusinessPaymentDetails.AccountNumber);
            para.Add("@BicSwift", supplierBusinessPaymentDetails.BicSwift);
            para.Add("@IBAN", supplierBusinessPaymentDetails.IBAN);
            para.Add("@CreditLimit", supplierBusinessPaymentDetails.CreditLimit);
            para.Add("@CreditTermInDays", supplierBusinessPaymentDetails.CreditTermInDays);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[SupplierBusinessPaymentDetails_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save Supplier business payment details for {supplierBusinessPaymentDetails.PaymentType} - {supplierBusinessPaymentDetails.AccountName}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return supplierBusinessPaymentDetails;
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
        public IEnumerable<SupplierBusinessPaymentDetails> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
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

            return this.Connection.Query<SupplierBusinessPaymentDetails>("[SupplierBusinessPaymentDetails_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}