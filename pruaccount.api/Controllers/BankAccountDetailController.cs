﻿// <copyright file="BankAccountDetailController.cs" company="PlaceholderCompany">
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
    using Pruaccount.Api.MappingConfigurations;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankAccountDetailController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountDetailController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly ILogger<BankAccountDetailController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly BankAccountDetailMapper bankAccountDetailMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountDetailController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public BankAccountDetailController(IUnitOfWork repository, ILogger<BankAccountDetailController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.uw = repository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
            this.bankAccountDetailMapper = new BankAccountDetailMapper();
        }

        /// <summary>
        /// BankAccountDetail.
        /// </summary>
        /// <param name="pid">customerBusinessDetailsUniqueId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("detail/{pid}")]
        public IActionResult BankAccountDetail(Guid pid)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    BankAccountDetails bankAccountDetails = this.uw.BankAccountDetailsRepository.FindByPID(pid);

                    if (bankAccountDetails != null && bankAccountDetails.ClientBusinessDetailsUniqueId == currentTokenUserDetails.CBUniqueId)
                    {
                        BankAccountDetailModel model = new BankAccountDetailModel()
                        {
                            BankAccountDetailsId = bankAccountDetails.BankAccountDetailsId,
                            UniqueId = bankAccountDetails.UniqueId,
                            ClientBusinessDetailsUniqueId = bankAccountDetails.UniqueId,
                            BankAccountTypeId = bankAccountDetails.BankAccountTypeId,
                            BankAccountTypeName = bankAccountDetails.BankAccountTypeName,
                            BankTransactionMethodId = bankAccountDetails.BankTransactionMethodId,
                            BankTransactionMethodName = bankAccountDetails.BankTransactionMethodName,
                            LedgerAccountId = bankAccountDetails.LedgerAccountId,
                            LName = bankAccountDetails.LName,
                            DName = bankAccountDetails.DName,
                            NominalCode = bankAccountDetails.NominalCode,
                            CategoryGroupId = bankAccountDetails.CategoryGroupId,
                            Category = bankAccountDetails.Category,
                            VatRateId = bankAccountDetails.VatRateId,
                            DVatRate = bankAccountDetails.DVatRate,
                            VatRate = bankAccountDetails.VatRate,
                            AccountName = bankAccountDetails.AccountName,
                            SortCode = bankAccountDetails.SortCode,
                            AccountNumber = bankAccountDetails.AccountNumber,
                            BicSwift = bankAccountDetails.BicSwift,
                            IBAN = bankAccountDetails.IBAN,
                            CardLast4Digits = bankAccountDetails.CardLast4Digits,
                        };

                        return this.Ok(model);
                    }

                    return this.NotFound("Could not get any bank account details.");
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankAccountDetailController->BankAccountDetail Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// BankAccountDetailSelectList.
        /// </summary>
        /// <param name="ltype">list type.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("selectlist/{ltype}")]
        public IActionResult BankAccountDetailSelectList(int ltype)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                List<SelectItemModel> bankAccountDetailSelectList = new List<SelectItemModel>();

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var bankAccountDetailList = this.uw.BankAccountDetailsRepository.ListAll(currentTokenUserDetails.CBUniqueId, default, default);

                    foreach (var bankAccount in bankAccountDetailList)
                    {
                        BankAccountDetailModel bankAccountModel = this.bankAccountDetailMapper.PopulateFromEntity(bankAccount);
                        if (bankAccountModel.BankAccountTypeId != ltype)
                        {
                            continue;
                        }

                        bankAccountDetailSelectList.Add(new SelectItemModel() { _id = bankAccount.UniqueId.ToString(), name = bankAccount.AccountName });
                    }

                    return this.Ok(bankAccountDetailSelectList);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankAccountDetailController->BankAccountDetailList Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// BankAccountDetailList.
        /// </summary>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("list")]
        public IActionResult BankAccountDetailList(string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                List<BankAccountDetailModel> bankAccountDetailModels = new List<BankAccountDetailModel>();

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var bankAccountDetailList = this.uw.BankAccountDetailsRepository.ListAll(currentTokenUserDetails.CBUniqueId, default, default, sort, orderBy, pageNumber, rowsPerPage);

                    foreach (var bankAccount in bankAccountDetailList)
                    {
                        bankAccountDetailModels.Add(this.bankAccountDetailMapper.PopulateFromEntity(bankAccount));
                    }

                    var bankAccountDetailListAPIData = new
                    {
                        total = bankAccountDetailModels.Count,
                        bankAccountDetails = bankAccountDetailModels,
                    };

                    return this.Ok(bankAccountDetailListAPIData);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankAccountDetailController->BankAccountDetailList Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// BankAccountDetailSearch. ?searchTerm=${searchTerm}&sort=${sortby}&orderBy=${order}&pageNumber=${currentPage}&rowsPerPage=${pageSize}.
        /// </summary>
        /// <param name="searchTerm">AccountName OR BankAccountTypeName OR BankTransactionMethodName.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("search")]
        public IActionResult BankAccountDetailSearch(string searchTerm, string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                List<BankAccountDetailModel> bankAccountDetailModels = new List<BankAccountDetailModel>();

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var bankAccountDetailList = this.uw.BankAccountDetailsRepository.Search(currentTokenUserDetails.CBUniqueId, default, default, searchTerm, sort, orderBy, pageNumber, rowsPerPage);

                    foreach (var bankAccount in bankAccountDetailList)
                    {
                        bankAccountDetailModels.Add(this.bankAccountDetailMapper.PopulateFromEntity(bankAccount));
                    }

                    var bankAccountDetailListAPIData = new
                    {
                        total = bankAccountDetailModels.Count,
                        bankAccountDetails = bankAccountDetailModels,
                    };

                    return this.Ok(bankAccountDetailListAPIData);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankAccountDetailController->BankAccountDetailSearch Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// SaveBankAccount.
        /// </summary>
        /// <param name="bankAccountDetailModel">BankAccountDetailModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("save")]
        public IActionResult SaveBankAccount([FromBody] BankAccountDetailModel bankAccountDetailModel)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    if (string.IsNullOrEmpty(bankAccountDetailModel.AccountName) && bankAccountDetailModel.BankAccountTypeId <= 0 && bankAccountDetailModel.BankTransactionMethodId <= 0)
                    {
                        return this.BadRequest("Mandatory fields not entered.");
                    }

                    BankAccountDetails bankAccountDetailsRequest = this.bankAccountDetailMapper.PopulateFromModel(bankAccountDetailModel);
                    bankAccountDetailsRequest.ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId;
                    this.uw.Begin(System.Data.IsolationLevel.Serializable);
                    this.uw.BankAccountDetailsRepository.Save(bankAccountDetailsRequest);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankAccountDetailController->SaveBankAccount Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
            finally
            {
                this.uw.Complete();
            }

            return this.Ok();
        }

        /// <summary>
        /// SaveBankAccountMappingLink.
        /// </summary>
        /// <param name="bankAccountMappingLinkSaveModel">BankAccountMappingLinkSaveModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("savemaplink")]
        public IActionResult SaveBankAccountMappingLink([FromBody] BankAccountMappingLinkSaveModel bankAccountMappingLinkSaveModel)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                List<string> brokenRules = new List<string>();

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var currentMappingLinkList = this.uw.BankAccountMappingLinkRepository.ListAll(currentTokenUserDetails.CBUniqueId, bankAccountMappingLinkSaveModel.BankStatementMapDetailUniqueId).ToList();
                    List<BankAccountMappingLink> bankAccountMappingLinkRequests = new List<BankAccountMappingLink>();

                    if (currentMappingLinkList.Count > 0)
                    {
                        foreach (var currentMappingLink in currentMappingLinkList)
                        {
                            if (!bankAccountMappingLinkSaveModel.BankAccountDetailsUniqueIds.Contains(currentMappingLink.BankAccountDetailsUniqueId))
                            {
                                bankAccountMappingLinkRequests.Add(new BankAccountMappingLink()
                                {
                                    UniqueId = currentMappingLink.UniqueId,
                                    ClientBusinessDetailsUniqueId = currentMappingLink.ClientBusinessDetailsUniqueId,
                                    BankAccountDetailsUniqueId = currentMappingLink.BankAccountDetailsUniqueId,
                                    BankStatementMapDetailUniqueId = currentMappingLink.BankStatementMapDetailUniqueId,
                                    IsActive = false,
                                });
                            }
                        }
                    }

                    if (bankAccountMappingLinkSaveModel.BankAccountDetailsUniqueIds.Length > 0)
                    {
                        foreach (var bankAccountDetailsUniqueId in bankAccountMappingLinkSaveModel.BankAccountDetailsUniqueIds)
                        {
                            var currentMappingLink = currentMappingLinkList.Find(x => x.BankAccountDetailsUniqueId == bankAccountDetailsUniqueId);
                            var currentBankAccountMappingLinkList = this.uw.BankAccountMappingLinkRepository.ListAll(currentTokenUserDetails.CBUniqueId, default, bankAccountDetailsUniqueId).ToList();
                            bool isCurrentBankAccountAlreadyMapped = false;
                            string isCurrentBankAccountAlreadyMappedName = string.Empty;

                            if (currentBankAccountMappingLinkList.Count > 0)
                            {
                                var currentBankAccountActiveMappingLinkList = currentBankAccountMappingLinkList.Where(x => x.IsActive && x.BankStatementMapDetailUniqueId != bankAccountMappingLinkSaveModel.BankStatementMapDetailUniqueId).ToList();

                                if (currentBankAccountActiveMappingLinkList.Count >= 1)
                                {
                                    isCurrentBankAccountAlreadyMapped = true;
                                    isCurrentBankAccountAlreadyMappedName = currentBankAccountActiveMappingLinkList[0].MapName;
                                }
                            }

                            if (!isCurrentBankAccountAlreadyMapped)
                            {
                                if (currentMappingLink != null)
                                {
                                    bankAccountMappingLinkRequests.Add(new BankAccountMappingLink()
                                    {
                                        UniqueId = currentMappingLink.UniqueId,
                                        ClientBusinessDetailsUniqueId = currentMappingLink.ClientBusinessDetailsUniqueId,
                                        BankAccountDetailsUniqueId = currentMappingLink.BankAccountDetailsUniqueId,
                                        BankStatementMapDetailUniqueId = currentMappingLink.BankStatementMapDetailUniqueId,
                                        IsActive = true,
                                    });
                                }
                                else
                                {
                                    bankAccountMappingLinkRequests.Add(new BankAccountMappingLink()
                                    {
                                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                                        BankAccountDetailsUniqueId = bankAccountDetailsUniqueId,
                                        BankStatementMapDetailUniqueId = bankAccountMappingLinkSaveModel.BankStatementMapDetailUniqueId,
                                        IsActive = true,
                                    });
                                }
                            }
                            else
                            {
                                var bankaccountDetails = this.uw.BankAccountDetailsRepository.FindByPID(bankAccountDetailsUniqueId);
                                if (bankaccountDetails != null)
                                {
                                    brokenRules.Add($"Please check mappings, could not map bank account {bankaccountDetails.AccountName} as its already mapped to {isCurrentBankAccountAlreadyMappedName}.");
                                }
                                else
                                {
                                    brokenRules.Add($"Please check mappings, could not map bank account. to {isCurrentBankAccountAlreadyMappedName}");
                                }
                            }
                        }
                    }

                    if (bankAccountMappingLinkRequests.Count > 0)
                    {
                        this.uw.Begin(System.Data.IsolationLevel.Serializable);
                        foreach (var bankAccountMappingLinkRequest in bankAccountMappingLinkRequests)
                        {
                            this.uw.BankAccountMappingLinkRepository.Save(bankAccountMappingLinkRequest);
                        }
                    }
                    else
                    {
                        brokenRules.Add($"Please check mappings, did not save any details.");
                    }

                    if (brokenRules.Count > 0)
                    {
                        return this.BadRequest(string.Join(" ", brokenRules));
                    }
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankAccountDetailController->SaveBankAccountMappingLink Exception");
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