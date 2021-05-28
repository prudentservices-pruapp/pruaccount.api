// <copyright file="BankStatementParser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Domain.BankStatement
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Newtonsoft.Json;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementParser.
    /// </summary>
    public class BankStatementParser
    {

        /// <summary>
        /// GetNoOfRows.
        /// </summary>
        /// <param name="fileNameWithPath">fileNameWithPath.</param>
        /// <returns>long no of rows.</returns>
        public long GetNoOfRows(string fileNameWithPath)
        {
            long count = 0;
            using (StreamReader r = new StreamReader(fileNameWithPath))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// CSVJson.
        /// </summary>
        /// <param name="fileNameWithPath">fileNameWithPath.</param>
        /// <param name="currentPage">currentPage.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>string Json.</returns>
        public BankStatementMapModel GeRowsJson(string fileNameWithPath, int currentPage = 1, int rowsPerPage = 10)
        {
            string jsonData = string.Empty;
            var records = new List<dynamic>();
            int currentRow = 0;
            long noOfRowsProcessed = 0;

            BankStatementMapModel bankStatementMapModel = new BankStatementMapModel();
            bankStatementMapModel.Json = jsonData;

            if (!System.IO.File.Exists(fileNameWithPath))
            {
                throw new Exception("Please check file not found.");
            }

            bankStatementMapModel.NoOfRows = this.GetNoOfRows(fileNameWithPath);
            bankStatementMapModel.NoOfColumns = this.GetNoOfColumns(fileNameWithPath);

            bool hasHeaderRow = this.CheckIfFileHasHeader(fileNameWithPath);
            if (hasHeaderRow)
            {
                bankStatementMapModel.NoOfRows--;
            }

            var totalPages = (int)Math.Ceiling((decimal)bankStatementMapModel.NoOfRows / (decimal)rowsPerPage);

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            var startIndex = (currentPage - 1) * rowsPerPage;
            var endIndex = Math.Min(startIndex + rowsPerPage - 1, bankStatementMapModel.NoOfRows - 1);

            if (hasHeaderRow)
            {
                startIndex++;
                endIndex++;
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IgnoreBlankLines = false,
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader(fileNameWithPath))
            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {
                    var record = csv.GetRecord<dynamic>();

                    if (currentRow == 0)
                    {
                        if (hasHeaderRow)
                        {
                            currentRow++;
                            continue;
                        }
                    }

                    if (currentRow < startIndex)
                    {
                        currentRow++;
                        continue;
                    }

                    if (noOfRowsProcessed >= rowsPerPage)
                    {
                        break;
                    }

                    records.Add(this.CreateRecordWithCustomColumnName(record));
                    currentRow++;
                    noOfRowsProcessed++;
                }

                if (currentRow > 0)
                {
                    bankStatementMapModel.Json = JsonConvert.SerializeObject(records);
                }
            }

            return bankStatementMapModel;
        }

        /// <summary>
        /// CheckIfFileHasHeader.
        /// Check is any columns has number.
        /// If true then its not header.
        /// If false then its a header row.
        /// </summary>
        /// <param name="fileNameWithPath">fileNameWithPath.</param>
        /// <returns>True if header row or false.</returns>
        private bool CheckIfFileHasHeader(string fileNameWithPath)
        {
            bool hasHeaderRow = true;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IgnoreBlankLines = false,
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader(fileNameWithPath))
            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {
                    var record = csv.GetRecord<dynamic>();
                    hasHeaderRow = this.CheckIfFileHasHeader(record);
                    break;
                }
            }

            return hasHeaderRow;
        }

        /// <summary>
        /// CheckIfFileHasHeader.
        /// Check is any columns has number.
        /// If true then its not header.
        /// If false then its a header row.
        /// </summary>
        /// <param name="record">csv first row.</param>
        /// <returns>True if header row or false.</returns>
        private bool CheckIfFileHasHeader(IDictionary<string, object> record)
        {
            bool headerRow = true;
            Dictionary<int, bool> columns = new Dictionary<int, bool>();

            for (int colIndex = 0; colIndex < ((IDictionary<string, object>)record).Count; colIndex++)
            {
                string key = ((IDictionary<string, object>)record).ElementAt(colIndex).Key;
                string val = ((IDictionary<string, object>)record).ElementAt(colIndex).Value as string;
                bool hasDigit = false;

                for (int charIndex = 0; charIndex < val.Length; charIndex++)
                {
                    if (char.IsDigit(val[charIndex]))
                    {
                        hasDigit = true;
                        break;
                    }
                }

                columns.Add(colIndex, hasDigit);
            }

            var colWithDigit = columns.ContainsValue(true);

            if (colWithDigit)
            {
                headerRow = false;
            }

            return headerRow;
        }

        /// <summary>
        /// CreateRecordWithColumnName.
        /// </summary>
        /// <param name="record">csv row.</param>
        /// <returns>csv row with cutom columnnames.</returns>
        private IDictionary<string, object> CreateRecordWithCustomColumnName(IDictionary<string, object> record)
        {
            IDictionary<string, object> recordUpdated = new ExpandoObject();

            for (int i = 0; i < ((IDictionary<string, object>)record).Count; i++)
            {
                string key = ((IDictionary<string, object>)record).ElementAt(i).Key;
                string value = ((IDictionary<string, object>)record).ElementAt(i).Value as string;
                recordUpdated.Add($"Column{i}", value);
            }

            return recordUpdated;
        }

        /// <summary>
        /// GetNoOfColumns.
        /// </summary>
        /// <param name="fileNameWithPath">fileNameWithPath.</param>
        /// <returns>no of columns.</returns>
        private int GetNoOfColumns(string fileNameWithPath)
        {
            int noOfColumns = 0;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IgnoreBlankLines = false,
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader(fileNameWithPath))
            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {
                    var record = csv.GetRecord<dynamic>();
                    noOfColumns = GetNoOfColumns(record);
                    break;
                }
            }

            return noOfColumns;
        }

        /// <summary>
        /// CreateRecordWithColumnName.
        /// </summary>
        /// <param name="record">csv row.</param>
        /// <returns>csv row with custom columnnames.</returns>
        private int GetNoOfColumns(IDictionary<string, object> record)
        {
            return ((IDictionary<string, object>)record).Count;
        }
    }
}
