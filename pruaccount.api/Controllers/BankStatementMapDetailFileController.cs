// <copyright file="BankStatementMapDetailFileController.cs" company="PlaceholderCompany">
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
    /// BankStatementMapDetailFileController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BankStatementMapDetailFileController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly ILogger<BankStatementMapDetailFileController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly BankStatementMapDetailFileMapper bankStatementMapDetailFileMapper;
        private readonly string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("BankStatements", "SampleMapFile"));

        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementMapDetailFileController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public BankStatementMapDetailFileController(IUnitOfWork repository, ILogger<BankStatementMapDetailFileController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.uw = repository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
            this.bankStatementMapDetailFileMapper = new BankStatementMapDetailFileMapper();
        }

        /// <summary>
        /// UploadBankStatement.
        /// </summary>
        /// <param name="files">files uploaded.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("upload")]
        public IActionResult UploadBankStatement()
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                List<string> brokenRules = new List<string>();

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    BankStatementMapDetailFileModel bankStatementMapDetailFileModel = new BankStatementMapDetailFileModel();
                    bankStatementMapDetailFileModel.ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId;

                    foreach (var formFile in this.httpContextAccessor.HttpContext.Request.Form.Files)
                    {
                        if (formFile.Length > 0)
                        {
                            string ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                            string uploadedFileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                            int uploadedFileNameStartIndex = uploadedFileName.IndexOf('_');
                            Guid bankAccountDetailsUniqueId = Guid.Empty;

                            DateTime timestamp = DateTime.UtcNow;
                            string fileNameToSave = Path.GetFileNameWithoutExtension(uploadedFileName) + $"_{timestamp.Year}_{timestamp.Month}_{timestamp.Day}_{timestamp.Hour}{timestamp.Minute}{timestamp.Second}{ext}";
                            string fullPath = Path.Combine(this.pathToSave, fileNameToSave);

                            if (uploadedFileNameStartIndex == -1)
                            {
                                uploadedFileNameStartIndex = 0;
                            }
                            else
                            {
                                Guid.TryParse(uploadedFileName.Substring(0, 36), out bankAccountDetailsUniqueId);
                            }

                            bankStatementMapDetailFileModel.FileExtenstion = ext;
                            bankStatementMapDetailFileModel.FileLengthInBytes = formFile.Length;
                            bankStatementMapDetailFileModel.UploadedFileName = uploadedFileName.Substring(uploadedFileNameStartIndex + 1);
                            bankStatementMapDetailFileModel.UploadedFilePath = this.pathToSave;
                            bankStatementMapDetailFileModel.SystemGeneratedFileName = fileNameToSave;
                            bankStatementMapDetailFileModel.BankAccountDetailsUniqueId = bankAccountDetailsUniqueId;

                            if (bankStatementMapDetailFileModel.ValidateModel(out brokenRules))
                            {
                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    formFile.CopyTo(stream);
                                }

                                try
                                {
                                    this.uw.Begin(System.Data.IsolationLevel.Serializable);
                                    this.uw.BankStatementMapDetailFileRepository.Save(this.bankStatementMapDetailFileMapper.PopulateFromModel(bankStatementMapDetailFileModel));
                                }
                                catch (Exception ex)
                                {
                                    this.logger.LogError(ex, "BankStatementMapDetailFileController->UploadBankStatement Database Exception");
                                    return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
                                }
                                finally
                                {
                                    this.uw.Complete();
                                }
                            }
                            else
                            {
                                return this.BadRequest(string.Join(" ", brokenRules));
                            }
                        }
                        else
                        {
                            return this.BadRequest("Please upload valid bank statement.");
                        }
                    }
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "BankStatementMapDetailFileController->UploadBankStatement Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }

            return this.Ok();
        }
    }
}
