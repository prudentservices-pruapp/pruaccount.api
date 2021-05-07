// <copyright file="SupplierController.cs" company="PlaceholderCompany">
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
    /// SupplierController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierBusinessDetailController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly ILogger<SupplierBusinessDetailController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierBusinessDetailController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public SupplierBusinessDetailController(IUnitOfWork repository, ILogger<SupplierBusinessDetailController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.uw = repository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// SupplierBusinessDetail.
        /// </summary>
        /// <param name="pid">SupplierBusinessDetailsUniqueId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("detail/{pid}")]
        public IActionResult SupplierBusinessDetail(Guid pid)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    SupplierBusinessDetails supplierBusinessDetails = this.uw.SupplierBusinessDetailsRepository.FindByPID(pid);

                    if (supplierBusinessDetails != null && supplierBusinessDetails.ClientBusinessDetailsUniqueId == currentTokenUserDetails.CBUniqueId)
                    {
                        SupplierModel model = new SupplierModel()
                        {
                            SupplierBusinessDetails = new SupplierBusinessDetailsModel()
                            {
                                UniqueId = supplierBusinessDetails.UniqueId,
                                ClientBusinessDetailsUniqueId = supplierBusinessDetails.ClientBusinessDetailsUniqueId,
                                BusinessName = supplierBusinessDetails.BusinessName,
                                FirstName = supplierBusinessDetails.FirstName,
                                LastName = supplierBusinessDetails.LastName,
                                ClientReference = supplierBusinessDetails.ClientReference,
                                Email = supplierBusinessDetails.Email,
                                Mobile = supplierBusinessDetails.Mobile,
                                Phone = supplierBusinessDetails.Phone,
                                RegisteredCountry = supplierBusinessDetails.RegisteredCountry,
                                RegistrationNumber = supplierBusinessDetails.RegistrationNumber,
                                TypeOfBusiness = supplierBusinessDetails.TypeOfBusiness,
                                VatNumber = supplierBusinessDetails.VatNumber,
                            },
                            SupplierMainBusinessAddress = new SupplierBusinessAddressModel(),
                            SupplierBusinessPaymentDetails = new SupplierBusinessPaymentDetailsModel(),
                            SupplierBusinessMisc = new SupplierBusinessMiscModel(),
                        };

                        IEnumerable<SupplierBusinessAddress> supplierBusinessAddresses = this.uw.SupplierBusinessAddressRepository.ListAll(currentTokenUserDetails.CBUniqueId, supplierBusinessDetails.UniqueId);

                        foreach (SupplierBusinessAddress supplierBusinessAddress in supplierBusinessAddresses)
                        {
                            if (supplierBusinessAddress.AddressType == SupplierBusinessAddressTypeEnum.Main)
                            {
                                model.SupplierMainBusinessAddress = new SupplierBusinessAddressModel()
                                {
                                    UniqueId = supplierBusinessAddress.UniqueId,
                                    AddressType = supplierBusinessAddress.AddressType,
                                    Address1 = supplierBusinessAddress.Line1,
                                    Address2 = supplierBusinessAddress.Line2,
                                    TownCity = supplierBusinessAddress.City,
                                    County = supplierBusinessAddress.County,
                                    Postcode = supplierBusinessAddress.PostCode,
                                    Country = supplierBusinessAddress.Country,
                                    SupplierBusinessDetailsUniqueId = supplierBusinessAddress.SupplierBusinessDetailsUniqueId,
                                };
                            }
                        }

                        SupplierBusinessPaymentDetails supplierBusinessPaymentDetails = this.uw.SupplierBusinessPaymentDetailsRepository.ListAll(currentTokenUserDetails.CBUniqueId, supplierBusinessDetails.UniqueId).FirstOrDefault();
                        if (supplierBusinessPaymentDetails != null && supplierBusinessPaymentDetails.PaymentType == SupplierPaymentTypeEnum.Bank)
                        {
                            model.SupplierBusinessPaymentDetails = new SupplierBusinessPaymentDetailsModel()
                            {
                                UniqueId = supplierBusinessPaymentDetails.UniqueId,
                                SupplierBusinessDetailsUniqueId = supplierBusinessPaymentDetails.SupplierBusinessDetailsUniqueId,
                                PaymentType = supplierBusinessPaymentDetails.PaymentType,
                                AccountName = supplierBusinessPaymentDetails.AccountName,
                                AccountNumber = supplierBusinessPaymentDetails.AccountNumber,
                                SortCode = supplierBusinessPaymentDetails.SortCode,
                                BicSwift = supplierBusinessPaymentDetails.BicSwift,
                                IBAN = supplierBusinessPaymentDetails.IBAN,
                                CreditLimit = supplierBusinessPaymentDetails.CreditLimit,
                                CreditTermInDays = supplierBusinessPaymentDetails.CreditTermInDays,
                            };
                        }

                        var supplierBusinessMisc = this.uw.SupplierBusinessMiscRepository.ListAll(currentTokenUserDetails.CBUniqueId, supplierBusinessDetails.UniqueId).FirstOrDefault();
                        if (supplierBusinessMisc != null)
                        {
                            model.SupplierBusinessMisc = new SupplierBusinessMiscModel()
                            {
                                UniqueId = supplierBusinessMisc.UniqueId,
                                SupplierBusinessDetailsUniqueId = supplierBusinessMisc.SupplierBusinessDetailsUniqueId,
                                Notes = supplierBusinessMisc.Notes,
                                TermsAndConditions = supplierBusinessMisc.TermsAndConditions,
                            };
                        }

                        return this.Ok(model);
                    }

                    return this.NotFound("Could not get any Supplier business details.");
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "SupplierController->SupplierDetail Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// SupplierBusinessDetailList.
        /// </summary>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("list")]
        public IActionResult SupplierBusinessDetailList(string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var supplierBusinessDetailList = this.uw.SupplierBusinessDetailsRepository.ListAll(currentTokenUserDetails.CBUniqueId, default, default, sort, orderBy, pageNumber, rowsPerPage);

                    var rec = supplierBusinessDetailList.FirstOrDefault();

                    var supplierBusinessDetailListAPIData = new
                    {
                        total = (rec != null) ? rec.TotalRows : 0,
                        SupplierBusinessDetails = supplierBusinessDetailList,
                    };

                    return this.Ok(supplierBusinessDetailListAPIData);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "SupplierController->SupplierBusinessDetailList Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// SupplierBusinessDetailSearch. ?searchTerm=${searchTerm}&sort=${sortby}&orderBy=${order}&pageNumber=${currentPage}&rowsPerPage=${pageSize}.
        /// </summary>
        /// <param name="searchTerm">dname.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("search")]
        public IActionResult SupplierBusinessDetailSearch(string searchTerm, string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var supplierBusinessDetailList = this.uw.SupplierBusinessDetailsRepository.Search(currentTokenUserDetails.CBUniqueId, default, default, searchTerm, sort, orderBy, pageNumber, rowsPerPage);

                    var rec = supplierBusinessDetailList.FirstOrDefault();

                    var supplierBusinessDetailListAPIData = new
                    {
                        total = (rec != null) ? rec.TotalRows : 0,
                        SupplierBusinessDetails = supplierBusinessDetailList,
                    };

                    return this.Ok(supplierBusinessDetailListAPIData);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "SupplierController->SupplierBusinessDetailSearch Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// SaveSupplier.
        /// </summary>
        /// <param name="supplierDetailModel">SupplierModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("save")]
        public IActionResult SaveSupplier([FromBody] SupplierModel supplierDetailModel)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    if (string.IsNullOrEmpty(supplierDetailModel.SupplierBusinessDetails.BusinessName))
                    {
                        return this.BadRequest("Mandatory fields not entered.");
                    }

                    SupplierBusinessDetails supplierBusinessDetailsRequest = new SupplierBusinessDetails()
                    {
                        UniqueId = supplierDetailModel.SupplierBusinessDetails.UniqueId,
                        BusinessName = supplierDetailModel.SupplierBusinessDetails.BusinessName,
                        FirstName = supplierDetailModel.SupplierBusinessDetails.FirstName,
                        LastName = supplierDetailModel.SupplierBusinessDetails.LastName,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                        ClientReference = supplierDetailModel.SupplierBusinessDetails.ClientReference,
                        Email = supplierDetailModel.SupplierBusinessDetails.Email,
                        Mobile = supplierDetailModel.SupplierBusinessDetails.Mobile,
                        Phone = supplierDetailModel.SupplierBusinessDetails.Phone,
                        RegisteredCountry = supplierDetailModel.SupplierBusinessDetails.RegisteredCountry,
                        RegistrationNumber = supplierDetailModel.SupplierBusinessDetails.RegistrationNumber,
                        TypeOfBusiness = supplierDetailModel.SupplierBusinessDetails.TypeOfBusiness,
                        VatNumber = supplierDetailModel.SupplierBusinessDetails.VatNumber,
                    };

                    SupplierBusinessAddress supplierMainBusinessAddressRequest = new SupplierBusinessAddress()
                    {
                        UniqueId = supplierDetailModel.SupplierMainBusinessAddress.UniqueId,
                        SupplierBusinessDetailsUniqueId = supplierDetailModel.SupplierBusinessDetails.UniqueId,
                        AddressType = SupplierBusinessAddressTypeEnum.Main,
                        Line1 = supplierDetailModel.SupplierMainBusinessAddress.Address1,
                        Line2 = supplierDetailModel.SupplierMainBusinessAddress.Address2,
                        City = supplierDetailModel.SupplierMainBusinessAddress.TownCity,
                        County = supplierDetailModel.SupplierMainBusinessAddress.County,
                        PostCode = supplierDetailModel.SupplierMainBusinessAddress.Postcode,
                        Country = supplierDetailModel.SupplierMainBusinessAddress.Country,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                    };

                    SupplierBusinessPaymentDetails supplierBusinessPaymentDetails = new SupplierBusinessPaymentDetails()
                    {
                        UniqueId = supplierDetailModel.SupplierBusinessPaymentDetails.UniqueId,
                        SupplierBusinessDetailsUniqueId = supplierDetailModel.SupplierBusinessDetails.UniqueId,
                        PaymentType = SupplierPaymentTypeEnum.Bank,
                        AccountName = supplierDetailModel.SupplierBusinessPaymentDetails.AccountName,
                        AccountNumber = supplierDetailModel.SupplierBusinessPaymentDetails.AccountNumber,
                        SortCode = supplierDetailModel.SupplierBusinessPaymentDetails.SortCode,
                        BicSwift = supplierDetailModel.SupplierBusinessPaymentDetails.BicSwift,
                        IBAN = supplierDetailModel.SupplierBusinessPaymentDetails.IBAN,
                        CreditLimit = supplierDetailModel.SupplierBusinessPaymentDetails.CreditLimit,
                        CreditTermInDays = supplierDetailModel.SupplierBusinessPaymentDetails.CreditTermInDays,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                    };

                    SupplierBusinessMisc supplierBusinessMisc = new SupplierBusinessMisc()
                    {
                        UniqueId = supplierDetailModel.SupplierBusinessMisc.UniqueId,
                        SupplierBusinessDetailsUniqueId = supplierDetailModel.SupplierBusinessDetails.UniqueId,
                        Notes = supplierDetailModel.SupplierBusinessMisc.Notes,
                        TermsAndConditions = supplierDetailModel.SupplierBusinessMisc.TermsAndConditions,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                    };

                    this.uw.Begin(System.Data.IsolationLevel.Serializable);

                    if (supplierBusinessDetailsRequest.UniqueId == default)
                    {
                        this.uw.SupplierBusinessDetailsRepository.SaveNewSupplier(supplierBusinessDetailsRequest, supplierMainBusinessAddressRequest, supplierBusinessPaymentDetails, supplierBusinessMisc);
                    }
                    else
                    {
                        this.uw.SupplierBusinessDetailsRepository.SaveSupplier(supplierBusinessDetailsRequest, supplierMainBusinessAddressRequest, supplierBusinessPaymentDetails, supplierBusinessMisc);
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