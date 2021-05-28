// <copyright file="BankStatementMapModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// BankStatementMapModel.
    /// </summary>
    public class BankStatementMapModel
    {
        /// <summary>
        /// Gets or sets TotalRows.
        /// </summary>
        public long NoOfRows { get; set; }

        /// <summary>
        ///  Gets or sets NoOfColumns.
        /// </summary>
        public int NoOfColumns { get; set; }

        /// <summary>
        ///  Gets or sets Json.
        /// </summary>
        public string Json { get; set; }
    }
}
