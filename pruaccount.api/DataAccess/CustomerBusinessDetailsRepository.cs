// <copyright file="CustomerBusinessDetailsRepository.cs" company="PlaceholderCompany">
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
    /// CustomerBusinessDetailsRepository.
    /// </summary>
    public class CustomerBusinessDetailsRepository : RepositoryBase, ICustomerBusinessDetailsRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBusinessDetailsRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public CustomerBusinessDetailsRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// CustomerBusinessDetails.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>Customer BusinessDetails.</returns>
        public CustomerBusinessDetails FindByPID(Guid pid)
        {
            var para = new DynamicParameters();

            if (pid != default(Guid))
            {
                para.Add("@UniqueId", pid);
            }

            return this.Connection.Query<CustomerBusinessDetails>("[CustomerBusinessDetails_Detail]", para, this.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        /// <returns>IEnumerable CustomerBusinessDetails.</returns>
        public IEnumerable<CustomerBusinessDetails> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10)
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

            return this.Connection.Query<CustomerBusinessDetails>("[CustomerBusinessAddress_List]", para, this.Transaction, commandType: CommandType.StoredProcedure);
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
        /// CustomerBusinessDetails.
        /// </summary>
        /// <param name="customerBusinessDetails">customerBusinessDetails.</param>
        /// <returns>Customer BusinessDetails.</returns>
        public CustomerBusinessDetails Save(CustomerBusinessDetails customerBusinessDetails)
        {
            var para = new DynamicParameters();
            para.Add("@CustomerBusinessDetailsId", customerBusinessDetails.CustomerBusinessDetailsId);
            para.Add("@UniqueId", customerBusinessDetails.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", customerBusinessDetails.ClientBusinessDetailsUniqueId);
            para.Add("@BusinessName", customerBusinessDetails.BusinessName);
            para.Add("@FirstName", customerBusinessDetails.FirstName);
            para.Add("@LastName", customerBusinessDetails.LastName);
            para.Add("@ClientReference", customerBusinessDetails.ClientReference);
            para.Add("@Email", customerBusinessDetails.Email);
            para.Add("@Phone", customerBusinessDetails.Phone);
            para.Add("@Mobile", customerBusinessDetails.Mobile);
            para.Add("@TypeOfBusiness", customerBusinessDetails.TypeOfBusiness);
            para.Add("@RegistrationNumber", customerBusinessDetails.RegistrationNumber);
            para.Add("@RegisteredCountry", customerBusinessDetails.RegisteredCountry);
            para.Add("@VatNumber", customerBusinessDetails.VatNumber);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[CustomerBusinessDetails_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save customerBusinessDetails for {customerBusinessDetails.BusinessName}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customerBusinessDetails;
        }

        /// <summary>
        /// SaveCustomer.
        /// </summary>
        /// <param name="customerBusinessDetails">customerBusinessDetails.</param>
        /// <param name="customerMainBusinessAddress">customerMainBusinessAddress.</param>
        /// <param name="customerDeliveryBusinessAddress">customerDeliveryBusinessAddress.</param>
        /// <param name="customerBusinessPaymentDetails">customerBusinessPaymentDetails.</param>
        /// <param name="customerBusinessMisc">customerBusinessMisc.</param>
        /// <returns>Customer BusinessDetails.</returns>
        public CustomerBusinessDetails SaveCustomer(CustomerBusinessDetails customerBusinessDetails, CustomerBusinessAddress customerMainBusinessAddress, CustomerBusinessAddress customerDeliveryBusinessAddress, CustomerBusinessPaymentDetails customerBusinessPaymentDetails, CustomerBusinessMisc customerBusinessMisc)
        {
            string exceptionMessage = string.Empty;

            var para = new DynamicParameters();
            para.Add("@CustomerBusinessDetailsId", customerBusinessDetails.CustomerBusinessDetailsId);
            para.Add("@UniqueId", customerBusinessDetails.UniqueId);
            para.Add("@ClientBusinessDetailsUniqueId", customerBusinessDetails.ClientBusinessDetailsUniqueId);
            para.Add("@BusinessName", customerBusinessDetails.BusinessName);
            para.Add("@FirstName", customerBusinessDetails.FirstName);
            para.Add("@LastName", customerBusinessDetails.LastName);
            para.Add("@ClientReference", customerBusinessDetails.ClientReference);
            para.Add("@Email", customerBusinessDetails.Email);
            para.Add("@Phone", customerBusinessDetails.Phone);
            para.Add("@Mobile", customerBusinessDetails.Mobile);
            para.Add("@TypeOfBusiness", customerBusinessDetails.TypeOfBusiness);
            para.Add("@RegistrationNumber", customerBusinessDetails.RegistrationNumber);
            para.Add("@RegisteredCountry", customerBusinessDetails.RegisteredCountry);
            para.Add("@VatNumber", customerBusinessDetails.VatNumber);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[CustomerBusinessAddress_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save customer business details {customerBusinessDetails.FirstName} - {customerBusinessDetails.LastName} - BusinessName {customerBusinessDetails.BusinessName}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage = $"Could not save customer business details exception - {ex.Message}" + Environment.NewLine;
            }

            try
            {
                para = new DynamicParameters();
                para.Add("@CustomerBusinessAddressId", customerMainBusinessAddress.CustomerBusinessAddressId);
                para.Add("@UniqueId", customerMainBusinessAddress.UniqueId);
                para.Add("@ClientBusinessDetailsUniqueId", customerMainBusinessAddress.ClientBusinessDetailsUniqueId);
                para.Add("@CustomerBusinessDetailsUniqueId", customerMainBusinessAddress.CustomerBusinessDetailsUniqueId);
                para.Add("@AddressType", customerMainBusinessAddress.AddressType);
                para.Add("@Line1", customerMainBusinessAddress.Line1);
                para.Add("@Line2", customerMainBusinessAddress.Line2);
                para.Add("@City", customerMainBusinessAddress.City);
                para.Add("@County", customerMainBusinessAddress.County);
                para.Add("@PostCode", customerMainBusinessAddress.PostCode);
                para.Add("@Country", customerMainBusinessAddress.Country);

                saveStatus = this.Connection.Execute("[CustomerBusinessAddress_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save customer business main address {customerMainBusinessAddress.Line1}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage += $"Could not save customer business main address exception - {ex.Message}" + Environment.NewLine;
            }

            try
            {
                para = new DynamicParameters();
                para.Add("@CustomerBusinessAddressId", customerDeliveryBusinessAddress.CustomerBusinessAddressId);
                para.Add("@UniqueId", customerDeliveryBusinessAddress.UniqueId);
                para.Add("@ClientBusinessDetailsUniqueId", customerDeliveryBusinessAddress.ClientBusinessDetailsUniqueId);
                para.Add("@CustomerBusinessDetailsUniqueId", customerDeliveryBusinessAddress.CustomerBusinessDetailsUniqueId);
                para.Add("@AddressType", customerDeliveryBusinessAddress.AddressType);
                para.Add("@Line1", customerDeliveryBusinessAddress.Line1);
                para.Add("@Line2", customerDeliveryBusinessAddress.Line2);
                para.Add("@City", customerDeliveryBusinessAddress.City);
                para.Add("@County", customerDeliveryBusinessAddress.County);
                para.Add("@PostCode", customerDeliveryBusinessAddress.PostCode);
                para.Add("@Country", customerDeliveryBusinessAddress.Country);

                saveStatus = this.Connection.Execute("[CustomerBusinessAddress_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save customer business delivery address {customerDeliveryBusinessAddress.Line1}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage += $"Could not save customer business delivery address exception - {ex.Message}" + Environment.NewLine;
            }

            try
            {
                para = new DynamicParameters();
                para.Add("@CustomerBusinessAddressId", customerDeliveryBusinessAddress.CustomerBusinessAddressId);
                para.Add("@UniqueId", customerDeliveryBusinessAddress.UniqueId);
                para.Add("@ClientBusinessDetailsUniqueId", customerDeliveryBusinessAddress.ClientBusinessDetailsUniqueId);
                para.Add("@CustomerBusinessDetailsUniqueId", customerDeliveryBusinessAddress.CustomerBusinessDetailsUniqueId);
                para.Add("@AddressType", customerDeliveryBusinessAddress.AddressType);
                para.Add("@Line1", customerDeliveryBusinessAddress.Line1);
                para.Add("@Line2", customerDeliveryBusinessAddress.Line2);
                para.Add("@City", customerDeliveryBusinessAddress.City);
                para.Add("@County", customerDeliveryBusinessAddress.County);
                para.Add("@PostCode", customerDeliveryBusinessAddress.PostCode);
                para.Add("@Country", customerDeliveryBusinessAddress.Country);

                saveStatus = this.Connection.Execute("[CustomerBusinessAddress_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save customer business delivery address {customerDeliveryBusinessAddress.Line1}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage += $"Could not save customer business delivery address exception - {ex.Message}" + Environment.NewLine;
            }

            try
            {
                para = new DynamicParameters();
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

                saveStatus = this.Connection.Execute("[CustomerBusinessPaymentDetails_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save customer payment details {customerBusinessPaymentDetails.AccountName}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage += $"Could not save customer payment details exception - {ex.Message}" + Environment.NewLine;
            }

            try
            {
                para = new DynamicParameters();
                para.Add("@CustomerBusinessMiscId", customerBusinessMisc.CustomerBusinessMiscId);
                para.Add("@UniqueId", customerBusinessMisc.UniqueId);
                para.Add("@ClientBusinessDetailsUniqueId", customerBusinessMisc.ClientBusinessDetailsUniqueId);
                para.Add("@CustomerBusinessDetailsUniqueId", customerBusinessMisc.CustomerBusinessDetailsUniqueId);
                para.Add("@TermsAndConditions", customerBusinessMisc.TermsAndConditions);
                para.Add("@Notes", customerBusinessMisc.Notes);

                saveStatus = this.Connection.Execute("[CustomerBusinessMisc_Save]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    exceptionMessage += $"Could not save customer business misc {customerBusinessPaymentDetails.AccountName}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                exceptionMessage += $"Could not save customer business misc exception - {ex.Message}" + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(exceptionMessage))
            {
                throw new Exception(exceptionMessage);
            }

            return customerBusinessDetails;
        }

        /// <summary>
        /// SaveNewCustomer.
        /// </summary>
        /// <param name="customerBusinessDetails">customerBusinessDetails.</param>
        /// <param name="customerMainBusinessAddress">customerMainBusinessAddress.</param>
        /// <param name="customerDeliveryBusinessAddress">customerDeliveryBusinessAddress.</param>
        /// <param name="customerBusinessPaymentDetails">customerBusinessPaymentDetails.</param>
        /// <param name="customerBusinessMisc">customerBusinessMisc.</param>
        /// <returns>CustomerBusinessDetails.</returns>
        public CustomerBusinessDetails SaveNewCustomer(CustomerBusinessDetails customerBusinessDetails, CustomerBusinessAddress customerMainBusinessAddress, CustomerBusinessAddress customerDeliveryBusinessAddress, CustomerBusinessPaymentDetails customerBusinessPaymentDetails, CustomerBusinessMisc customerBusinessMisc)
        {
            var para = new DynamicParameters();
            para.Add("@ClientBusinessDetailsUniqueId", customerBusinessDetails.ClientBusinessDetailsUniqueId);
            para.Add("@BusinessName", customerBusinessDetails.BusinessName);
            para.Add("@FirstName", customerBusinessDetails.FirstName);
            para.Add("@LastName", customerBusinessDetails.LastName);
            para.Add("@ClientReference", customerBusinessDetails.ClientReference);
            para.Add("@Email", customerBusinessDetails.Email);
            para.Add("@Phone", customerBusinessDetails.Phone);
            para.Add("@Mobile", customerBusinessDetails.Mobile);
            para.Add("@TypeOfBusiness", customerBusinessDetails.TypeOfBusiness);
            para.Add("@RegistrationNumber", customerBusinessDetails.RegistrationNumber);
            para.Add("@RegisteredCountry", customerBusinessDetails.RegisteredCountry);
            para.Add("@VatNumber", customerBusinessDetails.VatNumber);
            para.Add("@Main_AddressType", customerMainBusinessAddress.AddressType);
            para.Add("@Main_Line1", customerMainBusinessAddress.Line1);
            para.Add("@Main_Line2", customerMainBusinessAddress.Line2);
            para.Add("@Main_City", customerMainBusinessAddress.City);
            para.Add("@Main_County", customerMainBusinessAddress.County);
            para.Add("@Main_PostCode", customerMainBusinessAddress.PostCode);
            para.Add("@Main_Country", customerMainBusinessAddress.Country);
            para.Add("@Delivery_AddressType", customerDeliveryBusinessAddress.AddressType);
            para.Add("@Delivery_Line1", customerDeliveryBusinessAddress.Line1);
            para.Add("@Delivery_Line2", customerDeliveryBusinessAddress.Line2);
            para.Add("@Delivery_City", customerDeliveryBusinessAddress.City);
            para.Add("@Delivery_County", customerDeliveryBusinessAddress.County);
            para.Add("@Delivery_PostCode", customerDeliveryBusinessAddress.PostCode);
            para.Add("@Delivery_Country", customerDeliveryBusinessAddress.Country);
            para.Add("@PaymentType", customerBusinessPaymentDetails.PaymentType);
            para.Add("@AccountName", customerBusinessPaymentDetails.AccountName);
            para.Add("@SortCode", customerBusinessPaymentDetails.SortCode);
            para.Add("@AccountNumber", customerBusinessPaymentDetails.AccountNumber);
            para.Add("@BicSwift", customerBusinessPaymentDetails.BicSwift);
            para.Add("@IBAN", customerBusinessPaymentDetails.IBAN);
            para.Add("@CreditLimit", customerBusinessPaymentDetails.CreditLimit);
            para.Add("@CreditTermInDays", customerBusinessPaymentDetails.CreditTermInDays);
            para.Add("@TermsAndConditions", customerBusinessMisc.TermsAndConditions);
            para.Add("@Notes", customerBusinessMisc.Notes);
            para.Add("@CustomerBusinessDetailsUniqueId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            int saveStatus = 0;

            try
            {
                saveStatus = this.Connection.Execute("[Insert_AccountantDetails]", para, transaction: this.Transaction, commandType: CommandType.StoredProcedure);

                if (saveStatus != -1)
                {
                    throw new Exception($"Could not save customer business details {customerBusinessDetails.FirstName} - {customerBusinessDetails.LastName} - BusinessName {customerBusinessDetails.BusinessName}");
                }

                customerBusinessDetails.UniqueId = para.Get<Guid>("@CustomerBusinessDetailsUniqueId");
            }
            catch (Exception)
            {
                throw;
            }

            return customerBusinessDetails;
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
        /// <returns>IEnumerable CustomerBusinessDetails.</returns>
        public IEnumerable<CustomerBusinessDetails> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
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

            return this.Connection.Query<CustomerBusinessDetails>("[CustomerBusinessDetails_Search]", para, this.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
