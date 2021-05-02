// <copyright file="CustomerBusinessPaymentDetailsModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// CustomerBusinessPaymentDetailsModel.
    /// </summary>
    public class CustomerBusinessPaymentDetailsModel
    {
        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets CustomerBusinessDetailsUniqueId.
        /// </summary>
        public Guid CustomerBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets PaymentType.
        /// </summary>
        public string PaymentType { get; set; }

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
        /// Gets or sets CreditLimit.
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// Gets or sets CreditTermInDays.
        /// </summary>
        public int CreditTermInDays { get; set; }
    }
}
