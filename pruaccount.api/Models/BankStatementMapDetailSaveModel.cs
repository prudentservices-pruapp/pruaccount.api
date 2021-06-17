// <copyright file="BankStatementMapDetailSaveModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;
    using System.Collections.Generic;
    using Pruaccount.Api.Enums;
    using Pruaccount.Api.Validators.Interfaces;

    /// <summary>
    /// BankStatementMapDetailSaveModel.
    /// </summary>
    public class BankStatementMapDetailSaveModel : IModelValidation<BankStatementMapDetailSaveModel>
    {
        /// <summary>
        /// Gets or sets BankAccountDetailsUniqueId.
        /// It can UniqueId of.
        /// BankStatementMapDetailFile Or BankStatementFileImport.
        /// </summary>
        public Guid BankAccountDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets BankStatementFileUniqueId.
        /// </summary>
        public Guid BankStatementFileUniqueId { get; set; }

        /// <summary>
        /// Gets or sets BankStatementMapDetailUniqueId.
        /// </summary>
        public Guid BankStatementMapDetailUniqueId { get; set; }

        /// <summary>
        /// Gets or sets Mapname.
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets Datepart1.
        /// </summary>
        public string DatePart1 { get; set; }

        /// <summary>
        /// Gets or sets Datepart1.
        /// </summary>
        public string DatePart2 { get; set; }

        /// <summary>
        /// Gets or sets Datepart3.
        /// </summary>
        public string DatePart3 { get; set; }

        /// <summary>
        /// Gets or sets Dateseparator.
        /// </summary>
        public string DateSeparator { get; set; }

        /// <summary>
        /// Gets or sets Dateformat.
        /// </summary>
        public string Dateformat { get; set; }

        /// <summary>
        /// Gets or sets DateformatValue.
        /// </summary>
        public string DateformatValue { get; set; }

        /// <summary>
        /// Gets or sets Column0.
        /// </summary>
        public int Column0 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column1.
        /// </summary>
        public int Column1 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column2.
        /// </summary>
        public int Column2 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column3.
        /// </summary>
        public int Column3 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column4.
        /// </summary>
        public int Column4 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column5.
        /// </summary>
        public int Column5 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column6.
        /// </summary>
        public int Column6 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column7.
        /// </summary>
        public int Column7 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column8.
        /// </summary>
        public int Column8 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column9.
        /// </summary>
        public int Column9 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column10.
        /// </summary>
        public int Column10 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column11.
        /// </summary>
        public int Column11 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column12.
        /// </summary>
        public int Column12 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column13.
        /// </summary>
        public int Column13 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column14.
        /// </summary>
        public int Column14 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// Gets or sets Column15.
        /// </summary>
        public int Column15 { get; set; } = BankStatementMapColumnTypeEnum.Ignore;

        /// <summary>
        /// ValidateModel.
        /// </summary>
        /// <param name="validator">BankStatementMapDetailSaveModelValidator.</param>
        /// <param name="brokenRules">list of errors.</param>
        /// <returns>True valid and False for invalid.</returns>
        public bool ValidateModel(IModelValidator<BankStatementMapDetailSaveModel> validator, out List<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}
