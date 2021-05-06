// <copyright file="CustomerController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Domain.Auth;
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Enums;
    using Pruaccount.Api.Models;

    /// <summary>
    /// CustomerController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBusinessDetailController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly ILogger<CustomerBusinessDetailController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBusinessDetailController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public CustomerBusinessDetailController(IUnitOfWork repository, ILogger<CustomerBusinessDetailController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.uw = repository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// CustomerBusinessDetail.
        /// </summary>
        /// <param name="pid">customerBusinessDetailsUniqueId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("detail/{pid}")]
        public IActionResult CustomerBusinessDetail(Guid pid)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    CustomerBusinessDetails customerBusinessDetails = this.uw.CustomerBusinessDetailsRepository.FindByPID(pid);

                    if (customerBusinessDetails != null && customerBusinessDetails.ClientBusinessDetailsUniqueId == currentTokenUserDetails.CBUniqueId)
                    {
                        CustomerModel model = new CustomerModel()
                        {
                            CustomerBusinessDetails = new CustomerBusinessDetailsModel()
                            {
                                UniqueId = customerBusinessDetails.UniqueId,
                                ClientBusinessDetailsUniqueId = customerBusinessDetails.ClientBusinessDetailsUniqueId,
                                BusinessName = customerBusinessDetails.BusinessName,
                                FirstName = customerBusinessDetails.FirstName,
                                LastName = customerBusinessDetails.LastName,
                                ClientReference = customerBusinessDetails.ClientReference,
                                Email = customerBusinessDetails.Email,
                                Mobile = customerBusinessDetails.Mobile,
                                Phone = customerBusinessDetails.Phone,
                                RegisteredCountry = customerBusinessDetails.RegisteredCountry,
                                RegistrationNumber = customerBusinessDetails.RegistrationNumber,
                                TypeOfBusiness = customerBusinessDetails.TypeOfBusiness,
                                VatNumber = customerBusinessDetails.VatNumber,
                            },
                            CustomerMainBusinessAddress = new CustomerBusinessAddressModel(),
                            CustomerDeliverBusinessAddress = new CustomerBusinessAddressModel(),
                            CustomerBusinessPaymentDetails = new CustomerBusinessPaymentDetailsModel(),
                            CustomerBusinessMisc = new CustomerBusinessMiscModel(),
                        };

                        IEnumerable<CustomerBusinessAddress> customerBusinessAddresses = this.uw.CustomerBusinessAddressRepository.ListAll(currentTokenUserDetails.CBUniqueId, customerBusinessDetails.UniqueId);

                        foreach (CustomerBusinessAddress customerBusinessAddress in customerBusinessAddresses)
                        {
                            if (customerBusinessAddress.AddressType == CustomerBusinessAddressTypeEnum.Main)
                            {
                                model.CustomerMainBusinessAddress = new CustomerBusinessAddressModel()
                                {
                                    UniqueId = customerBusinessAddress.UniqueId,
                                    AddressType = customerBusinessAddress.AddressType,
                                    Address1 = customerBusinessAddress.Line1,
                                    Address2 = customerBusinessAddress.Line2,
                                    TownCity = customerBusinessAddress.City,
                                    County = customerBusinessAddress.County,
                                    Postcode = customerBusinessAddress.PostCode,
                                    Country = customerBusinessAddress.Country,
                                    CustomerBusinessDetailsUniqueId = customerBusinessAddress.CustomerBusinessDetailsUniqueId,
                                };
                            }
                            else if (customerBusinessAddress.AddressType == CustomerBusinessAddressTypeEnum.Delivery)
                            {
                                model.CustomerDeliverBusinessAddress = new CustomerBusinessAddressModel()
                                {
                                    UniqueId = customerBusinessAddress.UniqueId,
                                    AddressType = customerBusinessAddress.AddressType,
                                    Address1 = customerBusinessAddress.Line1,
                                    Address2 = customerBusinessAddress.Line2,
                                    TownCity = customerBusinessAddress.City,
                                    County = customerBusinessAddress.County,
                                    Postcode = customerBusinessAddress.PostCode,
                                    Country = customerBusinessAddress.Country,
                                    CustomerBusinessDetailsUniqueId = customerBusinessAddress.CustomerBusinessDetailsUniqueId,
                                };
                            }
                        }

                        CustomerBusinessPaymentDetails customerBusinessPaymentDetails = this.uw.CustomerBusinessPaymentDetailsRepository.ListAll(currentTokenUserDetails.CBUniqueId, customerBusinessDetails.UniqueId).FirstOrDefault();
                        if (customerBusinessPaymentDetails != null && customerBusinessPaymentDetails.PaymentType == CustomerPaymentTypeEnum.Bank)
                        {
                            model.CustomerBusinessPaymentDetails = new CustomerBusinessPaymentDetailsModel()
                            {
                                UniqueId = customerBusinessPaymentDetails.UniqueId,
                                CustomerBusinessDetailsUniqueId = customerBusinessPaymentDetails.CustomerBusinessDetailsUniqueId,
                                PaymentType = customerBusinessPaymentDetails.PaymentType,
                                AccountName = customerBusinessPaymentDetails.AccountName,
                                AccountNumber = customerBusinessPaymentDetails.AccountNumber,
                                SortCode = customerBusinessPaymentDetails.SortCode,
                                BicSwift = customerBusinessPaymentDetails.BicSwift,
                                IBAN = customerBusinessPaymentDetails.IBAN,
                                CreditLimit = customerBusinessPaymentDetails.CreditLimit,
                                CreditTermInDays = customerBusinessPaymentDetails.CreditTermInDays,
                            };
                        }

                        var customerBusinessMisc = this.uw.CustomerBusinessMiscRepository.ListAll(currentTokenUserDetails.CBUniqueId, customerBusinessDetails.UniqueId).FirstOrDefault();
                        if (customerBusinessMisc != null)
                        {
                            model.CustomerBusinessMisc = new CustomerBusinessMiscModel()
                            {
                                UniqueId = customerBusinessMisc.UniqueId,
                                CustomerBusinessDetailsUniqueId = customerBusinessMisc.CustomerBusinessDetailsUniqueId,
                                Notes = customerBusinessMisc.Notes,
                                TermsAndConditions = customerBusinessMisc.TermsAndConditions,
                            };
                        }

                        return this.Ok(model);
                    }

                    return this.NotFound("Could not get any customer business details.");
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "CustomerController->CustomerDetail Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// CustomerBusinessDetailList.
        /// </summary>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("list")]
        public IActionResult CustomerBusinessDetailList(string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var customerBusinessDetailList = this.uw.CustomerBusinessDetailsRepository.ListAll(currentTokenUserDetails.CBUniqueId, default, default, sort, orderBy, pageNumber, rowsPerPage);

                    var rec = customerBusinessDetailList.FirstOrDefault();

                    var customerBusinessDetailListAPIData = new
                    {
                        total = (rec != null) ? rec.TotalRows : 0,
                        customerBusinessDetails = customerBusinessDetailList,
                    };

                    return this.Ok(customerBusinessDetailListAPIData);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "CustomerController->CustomerBusinessDetailList Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// CustomerBusinessDetailSearch. ?searchTerm=${searchTerm}&sort=${sortby}&orderBy=${order}&pageNumber=${currentPage}&rowsPerPage=${pageSize}.
        /// </summary>
        /// <param name="searchTerm">dname.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("search")]
        public IActionResult CustomerBusinessDetailSearch(string searchTerm, string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var customerBusinessDetailList = this.uw.CustomerBusinessDetailsRepository.Search(currentTokenUserDetails.CBUniqueId, default, default, searchTerm, sort, orderBy, pageNumber, rowsPerPage);

                    var rec = customerBusinessDetailList.FirstOrDefault();

                    var customerBusinessDetailListAPIData = new
                    {
                        total = (rec != null) ? rec.TotalRows : 0,
                        customerBusinessDetails = customerBusinessDetailList,
                    };

                    return this.Ok(customerBusinessDetailListAPIData);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "CustomerController->CustomerBusinessDetailSearch Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// SaveCustomer.
        /// </summary>
        /// <param name="customerDetailModel">CustomerModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("save")]
        public IActionResult SaveCustomer([FromBody] CustomerModel customerDetailModel)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    if (string.IsNullOrEmpty(customerDetailModel.CustomerBusinessDetails.BusinessName))
                    {
                        return this.BadRequest("Mandatory fields not entered.");
                    }

                    CustomerBusinessDetails customerBusinessDetailsRequest = new CustomerBusinessDetails()
                    {
                        UniqueId = customerDetailModel.CustomerBusinessDetails.UniqueId,
                        BusinessName = customerDetailModel.CustomerBusinessDetails.BusinessName,
                        FirstName = customerDetailModel.CustomerBusinessDetails.FirstName,
                        LastName = customerDetailModel.CustomerBusinessDetails.LastName,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                        ClientReference = customerDetailModel.CustomerBusinessDetails.ClientReference,
                        Email = customerDetailModel.CustomerBusinessDetails.Email,
                        Mobile = customerDetailModel.CustomerBusinessDetails.Mobile,
                        Phone = customerDetailModel.CustomerBusinessDetails.Phone,
                        RegisteredCountry = customerDetailModel.CustomerBusinessDetails.RegisteredCountry,
                        RegistrationNumber = customerDetailModel.CustomerBusinessDetails.RegistrationNumber,
                        TypeOfBusiness = customerDetailModel.CustomerBusinessDetails.TypeOfBusiness,
                        VatNumber = customerDetailModel.CustomerBusinessDetails.VatNumber,
                    };

                    CustomerBusinessAddress customerMainBusinessAddressRequest = new CustomerBusinessAddress()
                    {
                        UniqueId = customerDetailModel.CustomerMainBusinessAddress.UniqueId,
                        CustomerBusinessDetailsUniqueId = customerDetailModel.CustomerBusinessDetails.UniqueId,
                        AddressType = CustomerBusinessAddressTypeEnum.Main,
                        Line1 = customerDetailModel.CustomerMainBusinessAddress.Address1,
                        Line2 = customerDetailModel.CustomerMainBusinessAddress.Address2,
                        City = customerDetailModel.CustomerMainBusinessAddress.TownCity,
                        County = customerDetailModel.CustomerMainBusinessAddress.County,
                        PostCode = customerDetailModel.CustomerMainBusinessAddress.Postcode,
                        Country = customerDetailModel.CustomerMainBusinessAddress.Country,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                    };

                    CustomerBusinessAddress customerDeliveryBusinessAddressRequest = new CustomerBusinessAddress()
                    {
                        UniqueId = customerDetailModel.CustomerDeliverBusinessAddress.UniqueId,
                        CustomerBusinessDetailsUniqueId = customerDetailModel.CustomerBusinessDetails.UniqueId,
                        AddressType = CustomerBusinessAddressTypeEnum.Delivery,
                        Line1 = customerDetailModel.CustomerDeliverBusinessAddress.Address1,
                        Line2 = customerDetailModel.CustomerDeliverBusinessAddress.Address2,
                        City = customerDetailModel.CustomerDeliverBusinessAddress.TownCity,
                        County = customerDetailModel.CustomerDeliverBusinessAddress.County,
                        PostCode = customerDetailModel.CustomerDeliverBusinessAddress.Postcode,
                        Country = customerDetailModel.CustomerDeliverBusinessAddress.Country,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                    };

                    CustomerBusinessPaymentDetails customerBusinessPaymentDetails = new CustomerBusinessPaymentDetails()
                    {
                        UniqueId = customerDetailModel.CustomerBusinessPaymentDetails.UniqueId,
                        CustomerBusinessDetailsUniqueId = customerDetailModel.CustomerBusinessDetails.UniqueId,
                        PaymentType = CustomerPaymentTypeEnum.Bank,
                        AccountName = customerDetailModel.CustomerBusinessPaymentDetails.AccountName,
                        AccountNumber = customerDetailModel.CustomerBusinessPaymentDetails.AccountNumber,
                        SortCode = customerDetailModel.CustomerBusinessPaymentDetails.SortCode,
                        BicSwift = customerDetailModel.CustomerBusinessPaymentDetails.BicSwift,
                        IBAN = customerDetailModel.CustomerBusinessPaymentDetails.IBAN,
                        CreditLimit = customerDetailModel.CustomerBusinessPaymentDetails.CreditLimit,
                        CreditTermInDays = customerDetailModel.CustomerBusinessPaymentDetails.CreditTermInDays,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                    };

                    CustomerBusinessMisc customerBusinessMisc = new CustomerBusinessMisc()
                    {
                        UniqueId = customerDetailModel.CustomerBusinessMisc.UniqueId,
                        CustomerBusinessDetailsUniqueId = customerDetailModel.CustomerBusinessDetails.UniqueId,
                        Notes = customerDetailModel.CustomerBusinessMisc.Notes,
                        TermsAndConditions = customerDetailModel.CustomerBusinessMisc.TermsAndConditions,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                    };

                    this.uw.Begin(System.Data.IsolationLevel.Serializable);

                    if (customerBusinessDetailsRequest.UniqueId == default)
                    {
                        this.uw.CustomerBusinessDetailsRepository.SaveNewCustomer(customerBusinessDetailsRequest, customerMainBusinessAddressRequest, customerDeliveryBusinessAddressRequest, customerBusinessPaymentDetails, customerBusinessMisc);
                    }
                    else
                    {
                        this.uw.CustomerBusinessDetailsRepository.SaveCustomer(customerBusinessDetailsRequest, customerMainBusinessAddressRequest, customerDeliveryBusinessAddressRequest, customerBusinessPaymentDetails, customerBusinessMisc);
                    }
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "FinancialSettingController->SaveLedgerAccount Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
            finally
            {
                this.uw.Complete();
            }

            return this.Ok();
        }
    }
}
