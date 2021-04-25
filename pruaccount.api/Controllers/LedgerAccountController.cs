// <copyright file="LedgerAccountController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Domain.Auth;
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// LedgerAccountController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerAccountController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        private readonly ILogger<LedgerAccountController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="LedgerAccountController"/> class.
        /// </summary>
        /// <param name="repository">IUnitOfWork.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        public LedgerAccountController(IUnitOfWork repository, ILogger<LedgerAccountController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.uw = repository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// LedgerAccountDetail.
        /// </summary>
        /// <param name="pid">LedgerAccountId</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("detail/{pid}")]
        public IActionResult LedgerAccountDetail(int pid)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null)
                {
                    var ledgerAccount = this.uw.LedgerAccountRepository.FindByPID(pid);

                    if (ledgerAccount != null)
                    {
                        LedgerAccountModel model = new LedgerAccountModel()
                        {
                            LedgerAccountId = ledgerAccount.LedgerAccountId,
                            Group = ledgerAccount.Group,
                            Category = ledgerAccount.Category,
                            CategoryGroupId = ledgerAccount.CategoryGroupId,
                            DName = ledgerAccount.DName,
                            LName = ledgerAccount.LName,
                            DVatRate = ledgerAccount.DVatRate,
                            NominalCode = ledgerAccount.NominalCode,
                            VatRate = ledgerAccount.VatRate,
                            VatRateId = ledgerAccount.VatRateId,
                            IncludeInChart = ledgerAccount.IncludeInChart,
                            M_Bank = ledgerAccount.M_Bank,
                            M_Journals = ledgerAccount.M_Journals,
                            M_Other_Payment = ledgerAccount.M_Other_Payment,
                            M_Other_Receipt = ledgerAccount.M_Other_Receipt,
                            M_Purchase = ledgerAccount.M_Purchase,
                            M_Reports = ledgerAccount.M_Reports,
                            M_Sales = ledgerAccount.M_Sales,
                        };

                        return this.Ok(ledgerAccount);
                    }

                    return this.NotFound("Could not get any ledgerAccount details.");
                }
                else
                {
                    return this.NotFound("Could not get any token details.");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "LedgerAccountController->LedgerAccountList Exception");
                return this.BadRequest("Internal Server Error");
            }
        }

        /// <summary>
        /// LedgerAccountList.
        /// </summary>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("list")]
        public IActionResult LedgerAccountList(string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null)
                {
                    var ledgerAccountList = this.uw.LedgerAccountRepository.FindAll(sort, orderBy, pageNumber, rowsPerPage);

                    var rec = ledgerAccountList.FirstOrDefault();

                    var ledgerAccountListAPIData = new
                    {
                        total = (rec != null) ? rec.TotalRows : 0,
                        ledgerAccounts = ledgerAccountList,
                    };

                    return this.Ok(ledgerAccountListAPIData);
                }
                else
                {
                    return this.NotFound("Could not get any token details.");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "LedgerAccountController->LedgerAccountList Exception");
                return this.BadRequest("Internal Server Error");
            }
        }

        /// <summary>
        /// LedgerAccountSearch. ?userId=${userId}&searchTerm=${searchTerm}&sort=${sortby}&orderBy=${order}&pageNumber=${currentPage}&rowsPerPage=${pageSize}
        /// </summary>
        /// <param name="searchTerm">dname.</param>
        /// <param name="categoryGroupId">categoryGroupId.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("search")]
        public IActionResult LedgerAccountSearch(string searchTerm, int categoryGroupId, string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null)
                {
                    var ledgerAccountList = this.uw.LedgerAccountRepository.FindLedgerAccounts(searchTerm, categoryGroupId, sort, orderBy, pageNumber, rowsPerPage);

                    var rec = ledgerAccountList.FirstOrDefault();

                    var ledgerAccountListAPIData = new
                    {
                        total = (rec != null) ? rec.TotalRows : 0,
                        ledgerAccounts = ledgerAccountList,
                    };

                    return this.Ok(ledgerAccountListAPIData);
                }
                else
                {
                    return this.NotFound("Could not get any token details.");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "LedgerAccountController->LedgerAccountSearch Exception");
                return this.BadRequest("Internal Server Error");
            }
        }

        /// <summary>
        /// SaveLedgerAccount.
        /// </summary>
        /// <param name="ledgeraccountModel">LedgerAccountModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("save")]
        public IActionResult SaveLedgerAccount([FromBody] LedgerAccountModel ledgeraccountModel)
        {
            try
            {
                TokenUserDetails currentTokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;

                if (currentTokenUserDetails != null)
                {
                    if (string.IsNullOrEmpty(ledgeraccountModel.LName) || string.IsNullOrEmpty(ledgeraccountModel.DName) || ledgeraccountModel.NominalCode == default(int)
                        || ledgeraccountModel.CategoryGroupId == default(int) || ledgeraccountModel.VatRateId == default(int))
                    {
                        return this.BadRequest("Mandatory fields not entered.");
                    }

                    if (ledgeraccountModel.LedgerAccountId != default(int))
                    {
                        LedgerAccount ledgerAccount = this.uw.LedgerAccountRepository.FindByPID(ledgeraccountModel.LedgerAccountId);

                        // To be removed when frontend shows input fields to updates these values.
                        if (ledgerAccount != null)
                        {
                            ledgeraccountModel.IncludeInChart = ledgerAccount.IncludeInChart;
                            ledgeraccountModel.M_Bank = ledgerAccount.M_Bank;
                            ledgeraccountModel.M_Sales = ledgerAccount.M_Sales;
                            ledgeraccountModel.M_Purchase = ledgerAccount.M_Purchase;
                            ledgeraccountModel.M_Other_Payment = ledgerAccount.M_Other_Payment;
                            ledgeraccountModel.M_Other_Receipt = ledgerAccount.M_Other_Receipt;
                            ledgeraccountModel.M_Journals = ledgerAccount.M_Journals;
                            ledgeraccountModel.M_Reports = ledgerAccount.M_Reports;
                        }

                    }

                    LedgerAccount ledgerAccountRequest = new LedgerAccount()
                    {
                        LedgerAccountId = ledgeraccountModel.LedgerAccountId,
                        LName = ledgeraccountModel.LName,
                        DName = ledgeraccountModel.DName,
                        NominalCode = ledgeraccountModel.NominalCode,
                        CategoryGroupId = ledgeraccountModel.CategoryGroupId,
                        VatRateId = ledgeraccountModel.VatRateId,
                        IncludeInChart = ledgeraccountModel.IncludeInChart,
                        M_Bank = ledgeraccountModel.M_Bank,
                        M_Sales = ledgeraccountModel.M_Sales,
                        M_Purchase = ledgeraccountModel.M_Purchase,
                        M_Other_Payment = ledgeraccountModel.M_Other_Payment,
                        M_Other_Receipt = ledgeraccountModel.M_Other_Receipt,
                        M_Journals = ledgeraccountModel.M_Journals,
                        M_Reports = ledgeraccountModel.M_Reports,
                    };

                    this.uw.Begin(System.Data.IsolationLevel.Serializable);

                    this.uw.LedgerAccountRepository.Save(ledgerAccountRequest);
                }
                else
                {
                    return this.NotFound("Could not get any token details.");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "FinancialSettingController->SaveLedgerAccount Exception");
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
