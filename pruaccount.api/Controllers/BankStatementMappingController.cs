// <copyright file="BankStatementMappingController.cs" company="PlaceholderCompany">
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
    /// BankStatementMappingController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BankStatementMappingController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly ILogger<BankStatementMappingController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementMappingController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public BankStatementMappingController(IUnitOfWork repository, ILogger<BankStatementMappingController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.uw = repository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// SaveMapping.
        /// </summary>
        /// <param name="bankStatementMapDetailSaveModel">BankStatementMapDetailSaveModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("save")]
        public IActionResult SaveMapping([FromBody] BankStatementMapDetailSaveModel bankStatementMapDetailSaveModel)
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
                        BankStatementFileImport currentFileImport = this.uw.BankStatementFileImportRepository.FindByPID(bankStatementMapDetailSaveModel.BankStatementFileUniqueId);
                        BankStatementParser bankStatementParser = new BankStatementParser($"{currentFileImport.UploadedFilePath}\\{currentFileImport.SystemGeneratedFileName}");
                        BankStatementMapper bankStatementMapper = new BankStatementMapper(bankStatementMapDetailSaveModel);

                        BankStatementMapValidator bankStatementMapValidator = new BankStatementMapValidator(bankStatementParser, bankStatementMapper.BankStatementMapDetailModel);

                        brokenRules = bankStatementMapValidator.ValidateStatmentData(out bankStatementTransactionDetailModels);

                        if (brokenRules.Count > 0)
                        {
                            return this.BadRequest(string.Join(" ", brokenRules));
                        }

                        // Now save to DB
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
                this.logger.LogError(ex, "BankStatementMappingController->SaveMapping Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
            finally
            {
                this.uw.Complete();
            }

            return this.Ok();
        }

        /// <summary>
        /// GetMappedBankAccounts.
        /// </summary>
        /// <param name="pid">bankStatementMapDetailUniqueId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("mappedbankaccounts/{pid}")]
        public IActionResult GetMappedBankAccounts(Guid pid)
        {
            try
            {
                BankAccountsMappedModel bankAccountsMappedModel = new BankAccountsMappedModel();

                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    var bankAccountMappingLinkList = this.uw.BankAccountMappingLinkRepository.ListAll(currentTokenUserDetails.CBUniqueId, pid).ToList();

                    if (bankAccountMappingLinkList.Count > 0)
                    {
                        foreach (var bankAccountMappingLink in bankAccountMappingLinkList)
                        {
                            if (bankAccountMappingLink.IsActive)
                            {
                                bankAccountsMappedModel.BankAccountDetailsUniqueIds.Add(bankAccountMappingLink.BankAccountDetailsUniqueId);
                                bankAccountsMappedModel.BankAccountTypeId = bankAccountMappingLink.BankAccountTypeId;
                                bankAccountsMappedModel.BankAccountTypeName = bankAccountMappingLink.BankAccountTypeName;
                                bankAccountsMappedModel.BankStatementMapDetailUniqueId = bankAccountMappingLink.BankStatementMapDetailUniqueId;
                                bankAccountsMappedModel.MapName = bankAccountMappingLink.MapName;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(bankAccountsMappedModel.MapName))
                    {
                        BankStatementMapDetail bankStatementMapDetail = this.uw.BankStatementMapDetailRepository.FindByPID(pid);

                        if (bankStatementMapDetail == null)
                        {
                            return this.NotFound("Could not get statement map details.");
                        }

                        bankAccountsMappedModel.BankAccountTypeId = bankStatementMapDetail.BankAccountTypeId;
                        bankAccountsMappedModel.BankAccountTypeName = bankStatementMapDetail.BankAccountTypeName;
                        bankAccountsMappedModel.BankStatementMapDetailUniqueId = bankStatementMapDetail.UniqueId;
                        bankAccountsMappedModel.MapName = bankStatementMapDetail.MapName;
                    }
                }

                return this.Ok(bankAccountsMappedModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankStatementUploadController->GetMappedBankAccounts Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }
        }
    }
}
