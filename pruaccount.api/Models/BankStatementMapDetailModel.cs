// <copyright file="BankStatementMapDetailModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pruaccount.Api.Models
{
    using Pruaccount.Api.Enums;

    /// <summary>
    /// BankStatementMapDetailModel.
    /// </summary>
    public class BankStatementMapDetailModel
    {
        private readonly string columnStartsWith = "Column";

        /// <summary>
        /// Gets or sets MapName.
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets DatePart1.
        /// </summary>
        public string DatePart1 { get; set; }

        /// <summary>
        /// Gets or sets DatePart2.
        /// </summary>
        public string DatePart2 { get; set; }

        /// <summary>
        /// Gets or sets DatePart3.
        /// </summary>
        public string DatePart3 { get; set; }

        /// <summary>
        /// Gets or sets DateSeparator.
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
        /// Gets or sets DateIndex.
        /// </summary>
        public int DateIndex { get; set; } = -1;

        /// <summary>
        /// Gets a value indicating select list CreditAmountIndexSelectedColumn.
        /// </summary>
        public string DateIndexSelectedColumn
        {
            get
            {
                if (this.CreditAmountIndex > -1)
                {
                    return $"{this.columnStartsWith}{this.DateIndex}";
                }

                return this.columnStartsWith;
            }
        }

        /// <summary>
        /// Gets a value indicating select list DateIndexSelectedValue.
        /// </summary>
        public int DateIndexSelectedValue { get; } = BankStatementMapColumnTypeEnum.Date;

        /// <summary>
        /// Gets or sets CreditAmountIndex.
        /// </summary>
        public int CreditAmountIndex { get; set; } = -1;

        /// <summary>
        /// Gets a value indicating select list CreditAmountIndexSelectedColumn.
        /// </summary>
        public string CreditAmountIndexSelectedColumn
        {
            get
            {
                if (this.CreditAmountIndex > -1)
                {
                    return $"{this.columnStartsWith}{this.CreditAmountIndex}";
                }

                return this.columnStartsWith;
            }
        }

        /// <summary>
        /// Gets a value indicating select list CreditAmountIndexSelectedValue.
        /// </summary>
        public int CreditAmountIndexSelectedValue { get; } = BankStatementMapColumnTypeEnum.CreditAmount;

        /// <summary>
        /// Gets or sets DebitAmountIndex.
        /// </summary>
        public int DebitAmountIndex { get; set; } = -1;

        /// <summary>
        /// Gets a value indicating select list DebitAmountIndexSelectedColumn.
        /// </summary>
        public string DebitAmountIndexSelectedColumn
        {
            get
            {
                if (this.DebitAmountIndex > -1)
                {
                    return $"{this.columnStartsWith}{this.DebitAmountIndex}";
                }

                return this.columnStartsWith;
            }
        }

        /// <summary>
        /// Gets a value indicating select list DebitAmountIndexSelectedValue.
        /// </summary>
        public int DebitAmountIndexSelectedValue { get; } = BankStatementMapColumnTypeEnum.DebitAmount;

        /// <summary>
        /// Gets a value indicating whether IsCreditDebitAmountIndexSame.
        /// </summary>
        public bool IsCreditDebitAmountIndexSame
        {
            get
            {
                if (this.CreditAmountIndex == this.DebitAmountIndex)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating select list CreditDebitAmountIndexSelectedValue.
        /// </summary>
        public int CreditDebitAmountIndexSelectedValue { get; } = BankStatementMapColumnTypeEnum.CreditDebitAmount;

        /// <summary>
        /// Gets or sets DescriptionIndex.
        /// </summary>
        public int DescriptionIndex { get; set; } = -1;

        /// <summary>
        /// Gets a value indicating select list DescriptionIndexSelectedColumn.
        /// </summary>
        public string DescriptionIndexSelectedColumn
        {
            get
            {
                if (this.DescriptionIndex > -1)
                {
                    return $"{this.columnStartsWith}{this.DescriptionIndex}";
                }

                return this.columnStartsWith;
            }
        }

        /// <summary>
        /// Gets a value indicating select list DescriptionIndexSelectedValue.
        /// </summary>
        public int DescriptionIndexSelectedValue { get; } = BankStatementMapColumnTypeEnum.Description;

        /// <summary>
        /// Gets or sets BalanceIndex.
        /// </summary>
        public int BalanceIndex { get; set; } = -1;

        /// <summary>
        /// Gets a value indicating select list BalanceIndexSelectedColumn.
        /// </summary>
        public string BalanceIndexSelectedColumn
        {
            get
            {
                if (this.BalanceIndex > -1)
                {
                    return $"{this.columnStartsWith}{this.BalanceIndex}";
                }

                return this.columnStartsWith;
            }
        }

        /// <summary>
        /// Gets a value indicating select list BalanceIndexSelectedValue.
        /// </summary>
        public int BalanceIndexSelectedValue { get; } = BankStatementMapColumnTypeEnum.Balance;
    }
}
