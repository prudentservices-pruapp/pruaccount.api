// <copyright file="BankStatementCSVDataModelExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementCSVDataModelExtension.
    /// </summary>
    public static class BankStatementCSVDataModelExtension
    {
        /// <summary>
        /// GetValueForPropertyEndingWithIndex.
        /// </summary>
        /// <param name="bankStatementCSVDataModel">BankStatementCSVDataModel.</param>
        /// <param name="index">ending number for property.</param>
        /// <returns>string value.</returns>
        public static string GetValueForPropertyEndingWithIndex(this BankStatementCSVDataModel bankStatementCSVDataModel, int index)
        {
            string currentPropertyValue = string.Empty;

            switch (index)
            {
                case 0:
                    currentPropertyValue = bankStatementCSVDataModel.Column0;
                    break;
                case 1:
                    currentPropertyValue = bankStatementCSVDataModel.Column1;
                    break;
                case 2:
                    currentPropertyValue = bankStatementCSVDataModel.Column2;
                    break;
                case 3:
                    currentPropertyValue = bankStatementCSVDataModel.Column3;
                    break;
                case 4:
                    currentPropertyValue = bankStatementCSVDataModel.Column4;
                    break;
                case 5:
                    currentPropertyValue = bankStatementCSVDataModel.Column5;
                    break;
                case 6:
                    currentPropertyValue = bankStatementCSVDataModel.Column6;
                    break;
                case 7:
                    currentPropertyValue = bankStatementCSVDataModel.Column7;
                    break;
                case 8:
                    currentPropertyValue = bankStatementCSVDataModel.Column8;
                    break;
                case 9:
                    currentPropertyValue = bankStatementCSVDataModel.Column9;
                    break;
                case 10:
                    currentPropertyValue = bankStatementCSVDataModel.Column10;
                    break;
                case 11:
                    currentPropertyValue = bankStatementCSVDataModel.Column11;
                    break;
                case 12:
                    currentPropertyValue = bankStatementCSVDataModel.Column12;
                    break;
                case 13:
                    currentPropertyValue = bankStatementCSVDataModel.Column13;
                    break;
                case 14:
                    currentPropertyValue = bankStatementCSVDataModel.Column14;
                    break;
                case 15:
                    currentPropertyValue = bankStatementCSVDataModel.Column15;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            return currentPropertyValue;
        }
    }
}
