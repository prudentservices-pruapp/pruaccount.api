// <copyright file="BankStatementMapDetailSaveModelExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Extensions
{
    using System;
    using System.Reflection;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementMapDetailSaveModelExtension.
    /// </summary>
    public static class BankStatementMapDetailSaveModelExtension
    {
        private static readonly string PropertNameStartsWith = "Column";

        /// <summary>
        /// GetBankStatementMapColumnIndex.
        /// </summary>
        /// <param name="bankStatementMapDetailSaveModel">BankStatementMapDetailSaveModel.</param>
        /// <param name="bankStatementMapColumnTypeEnumValue">BankStatementMapColumnTypeEnum.</param>
        /// <returns>-1 if not mapped or valid int.</returns>
        public static int GetBankStatementMapColumnIndex(this BankStatementMapDetailSaveModel bankStatementMapDetailSaveModel, int bankStatementMapColumnTypeEnumValue)
        {
            int mappedIndex = -1;

            for (int colIndex = 0; colIndex <= 15; colIndex++)
            {
                try
                {
                    string propertyName = PropertNameStartsWith + colIndex;
                    PropertyInfo propertyInfo = bankStatementMapDetailSaveModel.GetType().GetProperty(propertyName);
                    int currentColumnValue = -1;

                    if (propertyInfo != null)
                    {
                        var objectValue = propertyInfo.GetValue(bankStatementMapDetailSaveModel, null);

                        if (objectValue != null)
                        {
                            int.TryParse(objectValue.ToString(), out currentColumnValue);

                            if (currentColumnValue == bankStatementMapColumnTypeEnumValue)
                            {
                                mappedIndex = colIndex;
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    mappedIndex = -1;
                }
            }

            return mappedIndex;
        }
    }
}
