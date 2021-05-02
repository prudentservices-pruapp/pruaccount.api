// <copyright file="AddressModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// AddressModel.
    /// </summary>
    public class AddressModel
    {
        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets AddressType.
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// Gets or sets Address1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets Address2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets TownCity.
        /// </summary>
        public string TownCity { get; set; }

        /// <summary>
        /// Gets or sets County.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets Postcode.
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets Country.
        /// </summary>
        public string Country { get; set; }
    }
}
