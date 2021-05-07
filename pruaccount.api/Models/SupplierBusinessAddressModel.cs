// <copyright file="SupplierBusinessAddressModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// SupplierBusinessAddressModel.
    /// </summary>
    public class SupplierBusinessAddressModel : AddressModel
    {
        /// <summary>
        /// Gets or sets SupplierBusinessDetailsUniqueId.
        /// </summary>
        public Guid SupplierBusinessDetailsUniqueId { get; set; }
    }
}