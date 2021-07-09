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
    using Pruaccount.Api.Enums;
    using Pruaccount.Api.MappingConfigurations;
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
        private readonly FinancialSettingMapper financialSettingMapper;

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
            this.financialSettingMapper = new FinancialSettingMapper();
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
                        financialSettingModel = this.financialSettingMapper.PopulateFromEntity(clientFinancialSetting);
                    }

                    return this.Ok(financialSettingModel);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "FinancialSettingController->FinancialSettings Exception");
                return this.BadRequest(BadRequestMessagesTypeEnum.InternalServerErrorsMessage);
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
                    if (financialSettingModel.AccountStartDate == default(DateTime))
                    {
                        return this.BadRequest("Financial account start dates must be set.");
                    }

                    CBFinancialSetting cbFinancialSettingRequest = new CBFinancialSetting();
                    cbFinancialSettingRequest = this.financialSettingMapper.PopulateFromModel(financialSettingModel);
                    cbFinancialSettingRequest.ClientBusinessDetailsUniqueId = currentTokenUserDetails.CBUniqueId;
                    this.uw.Begin(System.Data.IsolationLevel.Serializable);
                    this.uw.CBFinancialSettingRepository.Save(cbFinancialSettingRequest);
                }
                else
                {
                    return this.NotFound(BadRequestMessagesTypeEnum.NotFoundTokenErrorsMessage);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "FinancialSettingController->SaveFinancialSettings Exception");
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
