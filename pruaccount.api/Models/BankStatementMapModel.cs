// <copyright file="BankStatementMapModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// BankStatementMapModel.
    /// </summary>
    public class BankStatementMapModel
    {
        /// <summary>
        /// Gets or sets TotalRows.
        /// </summary>
        public long TotalNoOfRowsInCSV { get; set; }

        /// <summary>
        ///  Gets or sets NoOfColumns.
        /// </summary>
        public int NoOfColumnsInCSV { get; set; }

        /// <summary>
        ///  Gets or sets Json.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        ///  Gets bankStatementCSVDataModels.
        /// </summary>
        public List<BankStatementCSVDataModel> BankStatementCSVDataList
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Json))
                {
                   return JsonConvert.DeserializeObject<List<BankStatementCSVDataModel>>(this.Json);
                }

                return new List<BankStatementCSVDataModel>();
            }
        }
    }
}
