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
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
