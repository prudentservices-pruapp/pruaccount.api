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
        /// BankStatementStatus.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("load/{pid}")]
        public IActionResult BankStatementLoad(Guid pid)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    BankStatementFileImport currentFileImport = this.uw.BankStatementFileImportRepository.FindByPID(pid);

                    if (currentFileImport == null)
                    {
                        this.logger.LogError($"BankStatementUploadController->BankStatementStatus Exception {pid} - Could not load the request file, no records found.");
                        return this.BadRequest("Could not load the request file.");
                    }
                    else if (currentFileImport.ClientBusinessDetailsUniqueId != currentTokenUserDetails.CBUniqueId)
                    {
                        this.logger.LogError($"BankStatementUploadController->BankStatementStatus Exception {pid} - Could not load the request file, due to mismatch - {currentTokenUserDetails.CBUniqueId}.");
                        return this.BadRequest("Could not load the request file due to mismatch.");
                    }

                    string uploadedFilenameWithPath = $"{currentFileImport.UploadedFilePath}\\{currentFileImport.SystemGeneratedFileName}";
                    Domain.BankStatement.BankStatementParser bankStatementParser = new Domain.BankStatement.BankStatementParser(uploadedFilenameWithPath);
                    var bankStatementMapModel = bankStatementParser.GeRowsJson();

                    BankStatementMapDetailModel bankStatementMapDetailModel = new BankStatementMapDetailModel()
                    {
                        MapName = "Lloyds Bank",
                        DatePart1 = "dd",
                        DatePart2 = "MM",
                        DatePart3 = "yyyy",
                        DateSeparator = "/",
                        Dateformat = "dd/MM/yyyy",
                        DateformatValue = "01/09/2009",
                        DateIndex = 0,
                        DebitAmountIndex = 5,
                        CreditAmountIndex = 6,
                        BalanceIndex = 7,
                        DescriptionIndex = 4,
                    };

                    bankStatementMapModel.BankStatementMapDetailModel = bankStatementMapDetailModel;

                    return this.Ok(bankStatementMapModel);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankStatementUploadController->BankStatementStatus Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }

            return this.Ok();
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
    }
}
