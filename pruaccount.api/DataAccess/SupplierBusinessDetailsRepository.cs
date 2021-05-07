// <copyright file="SupplierBusinessDetailsRepository.cs" company="PlaceholderCompany">
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
    /// SupplierBusinessDetailsRepository.
    /// </summary>
    public class SupplierBusinessDetailsRepository : RepositoryBase, ISupplierBusinessDetailsRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierBusinessDetailsRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public SupplierBusinessDetailsRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// SupplierBusinessDetails.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>Supplier BusinessDetails.</returns>
        public SupplierBusinessDetails FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<SupplierBusinessDetails>("[SupplierBusinessDetails_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        /// <returns>IEnumerable SupplierBusinessDetails.</returns>
        public IEnumerable<SupplierBusinessDetails> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
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

            return this.Connection.Query<SupplierBusinessDetails>("[SupplierBusinessAddress_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// SupplierBusinessDetails.
        /// </summary>
        /// <param name="supplierBusinessDetails">Supplier BusinessDetails.</param>
        /// <returns>Supplier Business Details.</returns>
        public SupplierBusinessDetails Save(SupplierBusinessDetails supplierBusinessDetails)
        {
            var para = new DynamicParameters();
            para.Add("@SupplierBusinessDetailsId", supplierBusinessDetails.SupplierBusinessDetailsId);
            para.Add("@UniqueId", supplierBusinessDetails.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", supplierBusinessDetails.ClientBusinessDetailsUniqueId);
            para.Add("@BusinessName", supplierBusinessDetails.BusinessName);
            para.Add("@FirstName", supplierBusinessDetails.FirstName);
            para.Add("@LastName", supplierBusinessDetails.LastName);
            para.Add("@ClientReference", supplierBusinessDetails.ClientReference);
            para.Add("@Email", supplierBusinessDetails.Email);
            para.Add("@Phone", supplierBusinessDetails.Phone);
            para.Add("@Mobile", supplierBusinessDetails.Mobile);
            para.Add("@TypeOfBusiness", supplierBusinessDetails.TypeOfBusiness);
            para.Add("@RegistrationNumber", supplierBusinessDetails.RegistrationNumber);
            para.Add("@RegisteredCountry", supplierBusinessDetails.RegisteredCountry);
            para.Add("@VatNumber", supplierBusinessDetails.VatNumber);
            para.Add("@ImportAgentVat", supplierBusinessDetails.ImportAgentVat);
            para.Add("@VATReverseCharge", supplierBusinessDetails.VATReverseCharge);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[SupplierBusinessDetails_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save SupplierBusinessDetails for {supplierBusinessDetails.BusinessName}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return supplierBusinessDetails;
        }

        /// <summary>
        /// SaveSupplier.
        /// </summary>
        /// <param name="supplierBusinessDetails">SupplierBusinessDetails.</param>
        /// <param name="supplierMainBusinessAddress">SupplierMainBusinessAddress.</param>
        /// <param name="supplierBusinessPaymentDetails">SupplierBusinessPaymentDetails.</param>
        /// <param name="supplierBusinessMisc">SupplierBusinessMisc.</param>
        /// <returns>Supplier BusinessDetails.</returns>
        public SupplierBusinessDetails SaveSupplier(SupplierBusinessDetails supplierBusinessDetails, SupplierBusinessAddress supplierMainBusinessAddress, SupplierBusinessPaymentDetails supplierBusinessPaymentDetails, SupplierBusinessMisc supplierBusinessMisc)
        {
            string exceptionMessage = string.Empty;

            var para = new DynamicParameters();
            para.Add("@SupplierBusinessDetailsId", supplierBusinessDetails.SupplierBusinessDetailsId);
            para.Add("@UniqueId", supplierBusinessDetails.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", supplierBusinessDetails.ClientBusinessDetailsUniqueId);
            para.Add("@BusinessName", supplierBusinessDetails.BusinessName);
            para.Add("@FirstName", supplierBusinessDetails.FirstName);
            para.Add("@LastName", supplierBusinessDetails.LastName);
            para.Add("@ClientReference", supplierBusinessDetails.ClientReference);
            para.Add("@Email", supplierBusinessDetails.Email);
            para.Add("@Phone", supplierBusinessDetails.Phone);
            para.Add("@Mobile", supplierBusinessDetails.Mobile);
            para.Add("@TypeOfBusiness", supplierBusinessDetails.TypeOfBusiness);
            para.Add("@RegistrationNumber", supplierBusinessDetails.RegistrationNumber);
            para.Add("@RegisteredCountry", supplierBusinessDetails.RegisteredCountry);
            para.Add("@VatNumber", supplierBusinessDetails.VatNumber);
            para.Add("@ImportAgentVat", supplierBusinessDetails.ImportAgentVat);
            para.Add("@VATReverseCharge", supplierBusinessDetails.VATReverseCharge);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[SupplierBusinessDetails_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save Supplier business details {supplierBusinessDetails.FirstName} - {supplierBusinessDetails.LastName} - BusinessName {supplierBusinessDetails.BusinessName}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage = $"Could not save Supplier business details exception - {ex.Message}" + Environment.NewLine;
            }

            try
            {
                para = new DynamicParameters();
                para.Add("@SupplierBusinessAddressId", supplierMainBusinessAddress.SupplierBusinessAddressId);
                para.Add("@UniqueId", supplierMainBusinessAddress.UniqueId);
                para.Add("@ClientBusinessDetailsUniqueId", supplierMainBusinessAddress.ClientBusinessDetailsUniqueId);
                para.Add("@SupplierBusinessDetailsUniqueId", supplierMainBusinessAddress.SupplierBusinessDetailsUniqueId);
                para.Add("@AddressType", supplierMainBusinessAddress.AddressType);
                para.Add("@Line1", supplierMainBusinessAddress.Line1);
                para.Add("@Line2", supplierMainBusinessAddress.Line2);
                para.Add("@City", supplierMainBusinessAddress.City);
                para.Add("@County", supplierMainBusinessAddress.County);
                para.Add("@PostCode", supplierMainBusinessAddress.PostCode);
                para.Add("@Country", supplierMainBusinessAddress.Country);

                saveStatus = this.Connection.Execute("[SupplierBusinessAddress_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save Supplier business main address {supplierMainBusinessAddress.Line1}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage += $"Could not save Supplier business main address exception - {ex.Message}" + Environment.NewLine;
            }

            try
            {
                para = new DynamicParameters();
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

                saveStatus = this.Connection.Execute("[SupplierBusinessPaymentDetails_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save Supplier payment details {supplierBusinessPaymentDetails.AccountName}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage += $"Could not save Supplier payment details exception - {ex.Message}" + Environment.NewLine;
            }

            try
            {
                para = new DynamicParameters();
                para.Add("@SupplierBusinessMiscId", supplierBusinessMisc.SupplierBusinessMiscId);
                para.Add("@UniqueId", supplierBusinessMisc.UniqueId);
                para.Add("@ClientBusinessDetailsUniqueId", supplierBusinessMisc.ClientBusinessDetailsUniqueId);
                para.Add("@SupplierBusinessDetailsUniqueId", supplierBusinessMisc.SupplierBusinessDetailsUniqueId);
                para.Add("@TermsAndConditions", supplierBusinessMisc.TermsAndConditions);
                para.Add("@Notes", supplierBusinessMisc.Notes);

                saveStatus = this.Connection.Execute("[SupplierBusinessMisc_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save Supplier business misc {supplierBusinessPaymentDetails.AccountName}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage += $"Could not save Supplier business misc exception - {ex.Message}" + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(exceptionMessage))
            {
                throw new Exception(exceptionMessage);
            }

            return supplierBusinessDetails;
        }

        /// <summary>
        /// SaveNewSupplier.
        /// </summary>
        /// <param name="supplierBusinessDetails">SupplierBusinessDetails.</param>
        /// <param name="supplierMainBusinessAddress">SupplierMainBusinessAddress.</param>
        /// <param name="supplierBusinessPaymentDetails">SupplierBusinessPaymentDetails.</param>
        /// <param name="supplierBusinessMisc">SupplierBusinessMisc.</param>
        /// <returns>Supplier BusinessDetails.</returns>
        public SupplierBusinessDetails SaveNewSupplier(SupplierBusinessDetails supplierBusinessDetails, SupplierBusinessAddress supplierMainBusinessAddress, SupplierBusinessPaymentDetails supplierBusinessPaymentDetails, SupplierBusinessMisc supplierBusinessMisc)
        {
            var para = new DynamicParameters();
            para.Add("@ClientBusinessDetailsUniqueId", supplierBusinessDetails.ClientBusinessDetailsUniqueId);
            para.Add("@BusinessName", supplierBusinessDetails.BusinessName);
            para.Add("@FirstName", supplierBusinessDetails.FirstName);
            para.Add("@LastName", supplierBusinessDetails.LastName);
            para.Add("@ClientReference", supplierBusinessDetails.ClientReference);
            para.Add("@Email", supplierBusinessDetails.Email);
            para.Add("@Phone", supplierBusinessDetails.Phone);
            para.Add("@Mobile", supplierBusinessDetails.Mobile);
            para.Add("@TypeOfBusiness", supplierBusinessDetails.TypeOfBusiness);
            para.Add("@RegistrationNumber", supplierBusinessDetails.RegistrationNumber);
            para.Add("@RegisteredCountry", supplierBusinessDetails.RegisteredCountry);
            para.Add("@VatNumber", supplierBusinessDetails.VatNumber);
            para.Add("@ImportAgentVat", supplierBusinessDetails.ImportAgentVat);
            para.Add("@VATReverseCharge", supplierBusinessDetails.VATReverseCharge);
            para.Add("@Main_AddressType", supplierMainBusinessAddress.AddressType);
            para.Add("@Main_Line1", supplierMainBusinessAddress.Line1);
            para.Add("@Main_Line2", supplierMainBusinessAddress.Line2);
            para.Add("@Main_City", supplierMainBusinessAddress.City);
            para.Add("@Main_County", supplierMainBusinessAddress.County);
            para.Add("@Main_PostCode", supplierMainBusinessAddress.PostCode);
            para.Add("@Main_Country", supplierMainBusinessAddress.Country);
            para.Add("@PaymentType", supplierBusinessPaymentDetails.PaymentType);
            para.Add("@AccountName", supplierBusinessPaymentDetails.AccountName);
            para.Add("@SortCode", supplierBusinessPaymentDetails.SortCode);
            para.Add("@AccountNumber", supplierBusinessPaymentDetails.AccountNumber);
            para.Add("@BicSwift", supplierBusinessPaymentDetails.BicSwift);
            para.Add("@IBAN", supplierBusinessPaymentDetails.IBAN);
            para.Add("@CreditLimit", supplierBusinessPaymentDetails.CreditLimit);
            para.Add("@CreditTermInDays", supplierBusinessPaymentDetails.CreditTermInDays);
            para.Add("@TermsAndConditions", supplierBusinessMisc.TermsAndConditions);
            para.Add("@Notes", supplierBusinessMisc.Notes);
            para.Add("@SupplierBusinessDetailsUniqueId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[SupplierBusinessDetails_SaveNewSupplier]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save Supplier business details {supplierBusinessDetails.FirstName} - {supplierBusinessDetails.LastName} - BusinessName {supplierBusinessDetails.BusinessName}");
                }

                supplierBusinessDetails.UniqueId = para.Get<Guid>("@SupplierBusinessDetailsUniqueId");
            }
            catch (Exception)
            {
                throw;
            }

            return supplierBusinessDetails;
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
        /// <returns>IEnumerable SupplierBusinessDetails.</returns>
        public IEnumerable<SupplierBusinessDetails> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            var para = new DynamicParameters();

            if (businessDetailsUniqueId != default(Guid))
            {
                para.Add("@ClientBusinessDetailsUniqueId", businessDetailsUniqueId);
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

            return this.Connection.Query<SupplierBusinessDetails>("[SupplierBusinessDetails_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}