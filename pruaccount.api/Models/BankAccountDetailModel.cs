// <copyright file="BankAccountDetailModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// BankAccountDetailModel.
    /// </summary>
    public class BankAccountDetailModel
    {
        /// <summary>
        /// Gets or sets BankAccountDetailsId.
        /// </summary>
        public int BankAccountDetailsId { get; set; }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets ClientBusinessDetailsUniqueId.
        /// </summary>
        public Guid ClientBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets BankAccountTypeId.
        /// </summary>
        public int BankAccountTypeId { get; set; }

        /// <summary>
        /// Gets or sets BankAccountTypeName.
        /// </summary>
        public string BankAccountTypeName { get; set; }

        /// <summary>
        /// Gets or sets BankTransactionMethodId.
        /// </summary>
        public int BankTransactionMethodId { get; set; }

        /// <summary>
        /// Gets or sets BankTransactionMethodName.
        /// </summary>
        public string BankTransactionMethodName { get; set; }

        /// <summary>
        /// Gets or sets LedgerAccountId.
        /// </summary>
        public int LedgerAccountId { get; set; }

        /// <summary>
        /// Gets or sets LName.
        /// </summary>
        public string LName { get; set; }

        /// <summary>
        /// Gets or sets DName.
        /// </summary>
        public string DName { get; set; }

        /// <summary>
        /// Gets or sets NominalCode.
        /// </summary>
        public int NominalCode { get; set; }

        /// <summary>
        /// Gets or sets CategoryGroupId.
        /// </summary>
        public int CategoryGroupId { get; set; }

        /// <summary>
        /// Gets or sets Category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets VatRateId.
        /// </summary>
        public int VatRateId { get; set; }

        /// <summary>
        /// Gets or sets DVatRate.
        /// </summary>
        public string DVatRate { get; set; }

        /// <summary>
        /// Gets or sets VatRate.
        /// </summary>
        public string VatRate { get; set; }

        /// <summary>
        /// Gets or sets AccountName.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets SortCode.
        /// </summary>
        public string SortCode { get; set; }

        /// <summary>
        /// Gets or sets AccountNumber.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets BicSwift.
        /// </summary>
        public string BicSwift { get; set; }

        /// <summary>
        /// Gets or sets IBAN.
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// Gets or sets CardLast4Digits.
        /// </summary>
        public string CardLast4Digits { get; set; }

        /// <summary>
        /// Gets a value indicating whether CanUploadBankStatement.
        /// </summary>
        public bool CanUploadBankStatement
        {
            get
            {
                if (this.BankAccountTypeId <= 3)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
