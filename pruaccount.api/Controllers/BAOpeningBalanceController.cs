﻿// <copyright file="BAOpeningBalanceController.cs" company="PlaceholderCompany">
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
    /// BAOpeningBalanceController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BAOpeningBalanceController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly ILogger<BAOpeningBalanceController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BAOpeningBalanceController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public BAOpeningBalanceController(IUnitOfWork repository, ILogger<BAOpeningBalanceController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.uw = repository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// BAOpeningBalanceDetail.
        /// </summary>
        /// <param name="pid">customerBusinessDetailsUniqueId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("detail/{pid}")]
        public IActionResult BAOpeningBalanceDetail(Guid pid)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    BAOpeningBalance baOpeningBalance = this.uw.BAOpeningBalanceRepository.FindByPID(pid);

                    if (baOpeningBalance != null && baOpeningBalance.ClientBusinessDetailsUniqueId == currentTokenUserDetails.CBUniqueId)
                    {
                        BAOpeningBalanceModel model = new BAOpeningBalanceModel()
                        {
                            BAOpeningBalanceId = baOpeningBalance.BAOpeningBalanceId,
                            UniqueId = baOpeningBalance.UniqueId,
                            ClientBusinessDetailsUniqueId = baOpeningBalance.UniqueId,
                            BankAccountDetailsUniqueId = baOpeningBalance.BankAccountDetailsUniqueId,
                            LedgerAccountId = baOpeningBalance.LedgerAccountId,
                            AccountName = baOpeningBalance.AccountName,
                            AccountNumber = baOpeningBalance.AccountNumber,
                            SortCode = baOpeningBalance.SortCode,
                            BalanceDate = baOpeningBalance.BalanceDate,
                            BAOpeningBalanceTypeId = baOpeningBalance.BAOpeningBalanceTypeId,
                            BAOpeningBalanceTypeName = baOpeningBalance.BAOpeningBalanceTypeName,
                            BalanceAmount = baOpeningBalance.BalanceAmount,
                        };

                        return this.Ok(model);
                    }

                    return this.NotFound("Could not get any bank account opening balance details.");
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BAOpeningBalanceController->BAOpeningBalance Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// BAOpeningBalanceList.
        /// </summary>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("list")]
        public IActionResult BAOpeningBalanceList(string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var bapOpeningBalanceList = this.uw.BAOpeningBalanceRepository.ListAll(currentTokenUserDetails.CBUniqueId, default, default, sort, orderBy, pageNumber, rowsPerPage);

                    var rec = bapOpeningBalanceList.FirstOrDefault();

                    var baOpeningBalanceListAPIData = new
                    {
                        total = (rec != null) ? rec.TotalRows : 0,
                        baOpeningBalances = bapOpeningBalanceList,
                    };

                    return this.Ok(baOpeningBalanceListAPIData);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BAOpeningBalanceController->BAOpeningBalanceList Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// BAOpeningBalanceSearch. ?searchTerm=${searchTerm}&sort=${sortby}&orderBy=${order}&pageNumber=${currentPage}&rowsPerPage=${pageSize}.
        /// </summary>
        /// <param name="searchTerm">AccountName OR AccountNumber OR SortCode OR BAOpeningBalanceTypeName.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("search")]
        public IActionResult BAOpeningBalanceSearch(string searchTerm, string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var bapOpeningBalanceList = this.uw.BAOpeningBalanceRepository.Search(currentTokenUserDetails.CBUniqueId, default, default, searchTerm, sort, orderBy, pageNumber, rowsPerPage);

                    var rec = bapOpeningBalanceList.FirstOrDefault();

                    var baOpeningBalanceListAPIData = new
                    {
                        total = (rec != null) ? rec.TotalRows : 0,
                        bankAccountDetails = bapOpeningBalanceList,
                    };

                    return this.Ok(baOpeningBalanceListAPIData);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BAOpeningBalanceController->BAOpeningBalanceSearch Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// SaveBAOpeningBalance.
        /// </summary>
        /// <param name="baOpeningBalanceModel">BAOpeningBalanceModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("save")]
        public IActionResult SaveBAOpeningBalance([FromBody] BAOpeningBalanceModel baOpeningBalanceModel)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    if (baOpeningBalanceModel.BankAccountDetailsUniqueId == default && baOpeningBalanceModel.BAOpeningBalanceTypeId <= 0 && baOpeningBalanceModel.BalanceDate != default)
                    {
                        return this.BadRequest("Mandatory fields not entered.");
                    }

                    var clientFinancialSettings = this.uw.CBFinancialSettingRepository.FindByFID(currentTokenUserDetails.CBUniqueId).ToList();

                    if (clientFinancialSettings != null && clientFinancialSettings.Count > 0)
                    {
                        CBFinancialSetting clientFinancialSetting = clientFinancialSettings[0];

                        if (baOpeningBalanceModel.BalanceDate == default)
                        {
                            return this.BadRequest("Accounts Start date is not set.");
                        }

                        if (baOpeningBalanceModel.BalanceDate >= clientFinancialSetting.YearStartDate)
                        {
                            return this.BadRequest("Bank Opening Balance Date should be less than Accounts Start Date.");
                        }
                    }
                    else
                    {
                        return this.BadRequest("Accounts Start date is not set.");
                    }

                    BAOpeningBalance bankAccountDetailsRequest = new BAOpeningBalance()
                    {
                        BAOpeningBalanceId = baOpeningBalanceModel.BAOpeningBalanceId,
                        UniqueId = baOpeningBalanceModel.UniqueId,
                        ClientBusinessDetailsUniqueId = baOpeningBalanceModel.ClientBusinessDetailsUniqueId,
                        BankAccountDetailsUniqueId = baOpeningBalanceModel.BankAccountDetailsUniqueId,
                        LedgerAccountId = baOpeningBalanceModel.LedgerAccountId,
                        BalanceDate = baOpeningBalanceModel.BalanceDate,
                        BAOpeningBalanceTypeId = baOpeningBalanceModel.BAOpeningBalanceTypeId,
                        BalanceAmount = baOpeningBalanceModel.BalanceAmount,
                    };

                    this.uw.Begin(System.Data.IsolationLevel.Serializable);
                    this.uw.BAOpeningBalanceRepository.Save(bankAccountDetailsRequest);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BAOpeningBalanceController->SaveBAOpeningBalance Exception");
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