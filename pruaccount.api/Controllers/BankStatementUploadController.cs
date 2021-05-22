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
    using Pruaccount.Api.Models;

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
        private readonly string uploadFolder = Path.Combine("BankStatements", "Uploaded");
        private readonly string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("BankStatements", "Uploaded"));
        private readonly IDictionary<string, string> allowedExtensions = new Dictionary<string, string>()
        {
            { ".csv", "text/csv" },
        };

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

                if (currentTokenUserDetails != null && currentTokenUserDetails.CBUniqueId != default)
                {
                    if (this.httpContextAccessor.HttpContext.Request.Form.Files.Count == 0)
                    {
                        return this.BadRequest("Please upload bank statement.");
                    }
                    else if (this.httpContextAccessor.HttpContext.Request.Form.Files.Count > 1)
                    {
                        return this.BadRequest("Please upload one bank statement.");
                    }

                    foreach (var formFile in this.httpContextAccessor.HttpContext.Request.Form.Files)
                    {
                        string ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                        if (!this.allowedExtensions.Keys.Contains(ext))
                        {
                            return this.BadRequest("Please upload .csv bank statement.");
                        }

                        if (formFile.Length > 3000000)
                        {
                            return this.BadRequest("Please upload a file less than 3MB.");
                        }

                        if (formFile.Length > 0)
                        {
                            string uploadedFileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                            var timestamp = DateTime.UtcNow;
                            var fileNameToSave = Path.GetFileNameWithoutExtension(uploadedFileName) + $"_{timestamp.Year}_{timestamp.Month}_{timestamp.Day}_{timestamp.Hour}{timestamp.Minute}{timestamp.Second}{ext}";
                            var fullPath = Path.Combine(this.pathToSave, fileNameToSave);

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                formFile.CopyTo(stream);
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
            finally
            {
                // this.uw.Complete();
            }

            return this.Ok();
        }
    }
}
