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
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Enums;
    using Pruaccount.Api.MappingConfigurations;
    using Pruaccount.Api.Models;
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
                    Domain.BankStatement.BankStatementParser bankStatementParser = new Domain.BankStatement.BankStatementParser();
                    var bankStatementMapModel = bankStatementParser.GeRowsJson(uploadedFilenameWithPath);

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
    }
}
