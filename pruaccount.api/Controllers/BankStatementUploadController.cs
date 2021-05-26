// <copyright file="BankStatementUploadController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
    using Pruaccount.Api.Validators.Extensions;

    /// <summary>
    /// BankStatementUploadController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BankStatementUploadController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly ILogger<BankAccountDetailController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("BankStatements", "Uploaded"));

        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementUploadController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public BankStatementUploadController(IUnitOfWork repository, ILogger<BankAccountDetailController> logger, IHttpContextAccessor httpContextAccessor)
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
        [HttpGet("status/{pid}")]
        public IActionResult BankStatementStatus(Guid pid)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    List<BankStatementFileImport> currentImports = this.uw.BankStatementFileImportRepository.ListAll(currentTokenUserDetails.CBUniqueId, pid, default, "BankStatementFileImportId", "desc", 1, 10).ToList();
                    BankStatementFileImportModel lastProcessStatus = new BankStatementFileImportModel();
                    lastProcessStatus.CurrentProcessStatus = string.Empty;

                    if (currentImports.Count > 0)
                    {
                        lastProcessStatus.PopulatePartialBankStatementFileImportModelFromEntity(currentImports[0]);
                    }

                    return Ok(lastProcessStatus);
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

                    BankStatementFileImportModel bankStatementFileImportModel = new BankStatementFileImportModel();
                    bankStatementFileImportModel.ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId;

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

                            List<BankStatementFileImport> currentImports = this.uw.BankStatementFileImportRepository.ListAll(currentTokenUserDetails.CBUniqueId, bankAccountDetailsUniqueId, default, "BankStatementFileImportId", "desc", 1, 10).ToList();

                            BankStatementFileImport lastImport = currentImports.OrderByDescending(x => x.BankStatementFileImportId).FirstOrDefault();

                            if (lastImport != null)
                            {
                                if (lastImport.CurrentProcessStatus != BankStatementFileProcessStatusTypeEnum.Processed || lastImport.CurrentProcessStatus != BankStatementFileProcessStatusTypeEnum.Rejected)
                                {
                                    return this.BadRequest($"Please process the previous uploaded bank statement - {lastImport.UploadedFileName}.");
                                }
                            }

                            bankStatementFileImportModel.FileExtenstion = ext;
                            bankStatementFileImportModel.FileLengthInBytes = formFile.Length;
                            bankStatementFileImportModel.UploadedFileName = uploadedFileName.Substring(uploadedFileNameStartIndex + 1);
                            bankStatementFileImportModel.UploadedFilePath = this.pathToSave;
                            bankStatementFileImportModel.SystemGeneratedFileName = fileNameToSave;
                            bankStatementFileImportModel.BankAccountDetailsUniqueId = bankAccountDetailsUniqueId;
                            bankStatementFileImportModel.CurrentProcessStatus = BankStatementFileProcessStatusTypeEnum.Uploaded;

                            if (bankStatementFileImportModel.ValidateModel(out brokenRules))
                            {
                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    formFile.CopyTo(stream);
                                }

                                try
                                {
                                    this.uw.Begin(System.Data.IsolationLevel.Serializable);
                                    this.uw.BankStatementFileImportRepository.Save(new BankStatementFileImport().PopulateBankStatementFileImportFromModel(bankStatementFileImportModel));
                                }
                                catch (Exception ex)
                                {
                                    this.logger.LogError(ex, "BankStatementUploadController->UploadBankStatement Database Exception");
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
                this.logger.LogError(ex, "BankStatementUploadController->UploadBankStatement Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
            }

            return this.Ok();
        }
    }
}
