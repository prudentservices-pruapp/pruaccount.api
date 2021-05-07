// <copyright file="SupplierModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    /// <summary>
    /// SupplierModel.
    /// </summary>
    public class SupplierModel
    {
        /// <summary>
        /// Gets or sets SupplierBusinessDetails.
        /// </summary>
        public SupplierBusinessDetailsModel SupplierBusinessDetails { get; set; }

        /// <summary>
        /// Gets or sets SupplierMainBusinessAddress.
        /// </summary>
        public SupplierBusinessAddressModel SupplierMainBusinessAddress { get; set; }

        /// <summary>
        /// Gets or sets SupplierBusinessPaymentDetailsModel.
        /// </summary>
        public SupplierBusinessPaymentDetailsModel SupplierBusinessPaymentDetails { get; set; }

        /// <summary>
        /// Gets or sets SupplierBusinessMisc.
        /// </summary>
        public SupplierBusinessMiscModel SupplierBusinessMisc { get; set; }
    }
}