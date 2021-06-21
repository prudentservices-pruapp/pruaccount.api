// <copyright file="BankStatementMapDetailController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Domain.Auth;
    using Pruaccount.Api.Domain.BankStatement;
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Enums;
    using Pruaccount.Api.MappingConfigurations;
    using Pruaccount.Api.Models;
    using Pruaccount.Api.Validators;
    using Pruaccount.Api.Validators.Extensions;

    /// <summary>
    /// BankStatementMapDetailController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BankStatementMapDetailController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly BankStatementMapDetailMapper bankStatementMapDetailMapper;
        private readonly ILogger<BankStatementMapDetailController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementMapDetailController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public BankStatementMapDetailController(IUnitOfWork repository, ILogger<BankStatementMapDetailController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.uw = repository;
            this.logger = logger;
            this.bankStatementMapDetailMapper = new BankStatementMapDetailMapper();
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// BankStatementMapDetailDetail.
        /// </summary>
        /// <param name="pid">bankStatementMapDetailUniqueId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("detail/{pid}")]
        public IActionResult BankStatementMapDetailDetail(Guid pid)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    BankStatementMapDetail bankStatementMapDetail = this.uw.BankStatementMapDetailRepository.FindByPID(pid);

                    if (bankStatementMapDetail != null)
                    {
                        BankStatementMapDetailModel bankStatementMapDetailModel = this.bankStatementMapDetailMapper.PopulateFromEntity(bankStatementMapDetail);

                        return this.Ok(bankStatementMapDetailModel);
                    }

                    return this.NotFound("Could not get statement map details.");
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankStatementMapDetailController->BankStatementMapDetailDetail Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }

        /// <summary>
        /// BankStatementMapDetailList.
        /// </summary>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("list")]
        public IActionResult BankStatementMapDetailList(string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                List<BankStatementMapDetailModel> bankStatementMapDetailModels = new List<BankStatementMapDetailModel>();

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var bankAccountDetailList = this.uw.BankStatementMapDetailRepository.ListAll(default, default, default, sort, orderBy, pageNumber, rowsPerPage);

                    foreach (var bankAccount in bankAccountDetailList)
                    {
                        bankStatementMapDetailModels.Add(this.bankStatementMapDetailMapper.PopulateFromEntity(bankAccount));
                    }

                    var bankStatementMapDetailListAPIData = new
                    {
                        total = bankStatementMapDetailModels.Count,
                        bankStatementMapDetails = bankStatementMapDetailModels,
                    };

                    return this.Ok(bankStatementMapDetailListAPIData);
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
        /// BankStatementMapDetailSearch. ?searchTerm=${searchTerm}&sort=${sortby}&orderBy=${order}&pageNumber=${currentPage}&rowsPerPage=${pageSize}.
        /// </summary>
        /// <param name="searchTerm">MapName.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("search")]
        public IActionResult BankStatementMapDetailSearch(string searchTerm, string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                List<BankStatementMapDetailModel> bankStatementMapDetailModels = new List<BankStatementMapDetailModel>();

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var bankAccountDetailList = this.uw.BankStatementMapDetailRepository.ListAll(default, default, default, sort, orderBy, pageNumber, rowsPerPage);

                    foreach (var bankAccount in bankAccountDetailList)
                    {
                        bankStatementMapDetailModels.Add(this.bankStatementMapDetailMapper.PopulateFromEntity(bankAccount));
                    }

                    var bankStatementMapDetailListAPIData = new
                    {
                        total = bankStatementMapDetailModels.Count,
                        bankStatementMapDetails = bankStatementMapDetailModels,
                    };

                    return this.Ok(bankStatementMapDetailListAPIData);
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
        /// SaveBankStatementMapDetail.
        /// </summary>
        /// <param name="bankStatementMapDetailSaveModel">BankStatementMapDetailSaveModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("save")]
        public IActionResult SaveBankStatementMapDetail([FromBody] BankStatementMapDetailSaveModel bankStatementMapDetailSaveModel)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                List<string> brokenRules = new List<string>();
                List<BankStatementTransactionDetailModel> bankStatementTransactionDetailModels = new List<BankStatementTransactionDetailModel>();

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    if (bankStatementMapDetailSaveModel.ValidateModel(out brokenRules))
                    {
                        BankStatementMapDetailFile currentFileImport = this.uw.BankStatementMapDetailFileRepository.FindByPID(bankStatementMapDetailSaveModel.BankStatementFileUniqueId);
                        BankStatementParser bankStatementParser = new BankStatementParser($"{currentFileImport.UploadedFilePath}\\{currentFileImport.SystemGeneratedFileName}");
                        BankStatementMapper bankStatementMapper = new BankStatementMapper(bankStatementMapDetailSaveModel);

                        BankStatementMapValidator bankStatementMapValidator = new BankStatementMapValidator(bankStatementParser, bankStatementMapper.BankStatementMapDetailModel);

                        brokenRules = bankStatementMapValidator.ValidateStatmentData(out bankStatementTransactionDetailModels);

                        if (brokenRules.Count > 0)
                        {
                            return this.BadRequest(string.Join(" ", brokenRules));
                        }

                        // Save StatementMapDetail
                        BankStatementMapDetail bankStatementMapDetail = this.bankStatementMapDetailMapper.PopulateFromModel(bankStatementMapper.BankStatementMapDetailModel);
                        this.uw.Begin(System.Data.IsolationLevel.Serializable);
                        bankStatementMapDetail = this.uw.BankStatementMapDetailRepository.Save(bankStatementMapDetail);

                        // Update BankStatementMapDetailFile with Mapping process UniqueId
                        currentFileImport.BankStatementMapDetailUniqueId = bankStatementMapDetail.UniqueId;
                        this.uw.BankStatementMapDetailFileRepository.Save(currentFileImport);

                    }
                    else
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
                this.logger.LogError(ex, "BankStatementMapDetailController->SaveBankStatementMapDetail Exception");
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
