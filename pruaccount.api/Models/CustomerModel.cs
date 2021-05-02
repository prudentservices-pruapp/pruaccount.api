// <copyright file="CustomerModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    /// <summary>
    /// CustomerModel.
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// Gets or sets CustomerBusinessDetails.
        /// </summary>
        public CustomerBusinessDetailsModel CustomerBusinessDetails { get; set; }

        /// <summary>
        /// Gets or sets CustomerMainBusinessAddress.
        /// </summary>
        public CustomerBusinessAddressModel CustomerMainBusinessAddress { get; set; }

        /// <summary>
        /// Gets or sets CustomerDeliverBusinessAddress.
        /// </summary>
        public CustomerBusinessAddressModel CustomerDeliverBusinessAddress { get; set; }

        /// <summary>
        /// Gets or sets CustomerBusinessPaymentDetailsModel.
        /// </summary>
        public CustomerBusinessPaymentDetailsModel CustomerBusinessPaymentDetails { get; set; }

        /// <summary>
        /// Gets or sets CustomerBusinessMisc.
        /// </summary>
        public CustomerBusinessMiscModel CustomerBusinessMisc { get; set; }
    }
}
