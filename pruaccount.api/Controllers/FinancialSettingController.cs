// <copyright file="FinancialSettingController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Domain.Auth;
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;

    /// <summary>
    /// FinancialSettingController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialSettingController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly ILogger<FinancialSettingController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialSettingController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public FinancialSettingController(IUnitOfWork repository, ILogger<FinancialSettingController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.uw = repository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// FinancialSettings.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet("settings")]
        public IActionResult FinancialSettings()
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                FinancialSettingModel financialSettingModel = new FinancialSettingModel();

                if (currentTokenUserDetails != null)
                {
                    var clientFinancialSettings = this.uw.CBFinancialSettingRepository.FindByFID(currentTokenUserDetails.CBUniqueId).ToList();

                    if (clientFinancialSettings != null && clientFinancialSettings.Count > 0)
                    {
                        CBFinancialSetting clientFinancialSetting = clientFinancialSettings[0];

                        financialSettingModel = new FinancialSettingModel()
                        {
                            UniqueId = clientFinancialSetting.UniqueId,
                            ClientBusinessDetailsUniqueId = clientFinancialSetting.ClientBusinessDetailsUniqueId,
                            HMRCUserId = clientFinancialSetting.HMRCUserId,
                            RetentionPeriod = clientFinancialSetting.RetentionPeriod,
                            VatFlatRate = clientFinancialSetting.VatFlatRate,
                            VatNumber = clientFinancialSetting.VatNumber,
                            VatScheme = clientFinancialSetting.VatScheme,
                            VatSubmissionRequency = clientFinancialSetting.VatSubmissionRequency,
                            YearEndDate = clientFinancialSetting.YearEndDate,
                            YearEndLockdownDate = clientFinancialSetting.YearEndLockdownDate,
                            YearEndTaxMonth = clientFinancialSetting.YearEndTaxMonth,
                            YearStartDate = clientFinancialSetting.YearStartDate,
                        };
                    }

                    return this.Ok(financialSettingModel);
                }
                else
                {
                    return this.NotFound("Could not get any token details.");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "FinancialSettingController->FinancialSettings Exception");
                return this.BadRequest("Internal Server Error");
            }
        }

        /// <summary>
        /// SaveFinancialSettings.
        /// </summary>
        /// <param name="financialSettingModel">FinancialSettingModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("savesettings")]
        public IActionResult SaveFinancialSettings([FromBody] FinancialSettingModel financialSettingModel)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null)
                {
                    if (financialSettingModel.YearStartDate != default(DateTime) && financialSettingModel.YearEndDate != default(DateTime))
                    {
                        if (financialSettingModel.YearStartDate > financialSettingModel.YearEndDate)
                        {
                            return this.BadRequest("Financial Start and End dates are not correct.");
                        }
                    }

                    CBFinancialSetting cbFinancialSettingRequest = new CBFinancialSetting()
                    {
                        UniqueId = financialSettingModel.UniqueId,
                        ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId,
                        HMRCUserId = financialSettingModel.HMRCUserId,
                        RetentionPeriod = financialSettingModel.RetentionPeriod,
                        VatFlatRate = financialSettingModel.VatFlatRate,
                        VatNumber = financialSettingModel.VatNumber,
                        VatScheme = financialSettingModel.VatScheme,
                        VatSubmissionRequency = financialSettingModel.VatSubmissionRequency,
                        YearEndDate = financialSettingModel.YearEndDate,
                        YearEndLockdownDate = financialSettingModel.YearEndLockdownDate,
                        YearEndTaxMonth = financialSettingModel.YearEndTaxMonth,
                        YearStartDate = financialSettingModel.YearStartDate,
                    };

                    this.uw.Begin(System.Data.IsolationLevel.Serializable);

                    this.uw.CBFinancialSettingRepository.Save(cbFinancialSettingRequest);
                }
                else
                {
                    return this.NotFound("Could not get any token details.");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "FinancialSettingController->SaveFinancialSettings Exception");
                return this.BadRequest("Internal Server Error");
            }
            finally
            {
                this.uw.Complete();
            }

            return this.Ok();
        }
    }
}
