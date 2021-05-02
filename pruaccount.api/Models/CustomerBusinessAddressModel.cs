// <copyright file="CustomerBusinessAddressModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// CustomerBusinessAddressModel.
    /// </summary>
    public class CustomerBusinessAddressModel : AddressModel
    {
        /// <summary>
        /// Gets or sets CustomerBusinessDetailsUniqueId.
        /// </summary>
        public Guid CustomerBusinessDetailsUniqueId { get; set; }
    }
}
