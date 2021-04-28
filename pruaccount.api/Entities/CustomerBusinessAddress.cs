// <copyright file="CustomerBusinessAddress.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// CustomerBusinessAddress.
    /// </summary>
    public class CustomerBusinessAddress
    {
        /// <summary>
        /// Gets or sets CustomerBusinessAddressId.
        /// </summary>
        public int CustomerBusinessAddressId { get; set; }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets ClientBusinessDetailsUniqueId.
        /// </summary>
        public Guid ClientBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets CustomerBusinessDetailsUniqueId.
        /// </summary>
        public Guid CustomerBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets AddressType.
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// Gets or sets Line1.
        /// </summary>
        public string Line1 { get; set; }

        /// <summary>
        /// Gets or sets Line2.
        /// </summary>
        public string Line2 { get; set; }

        /// <summary>
        /// Gets or sets City.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets County.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets PostCode.
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// Gets or sets Country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets CreatedDateUTC.
        /// </summary>
        public DateTime CreatedDateUTC { get; set; }

        /// <summary>
        /// Gets or sets UpdatedDateUTC.
        /// </summary>
        public DateTime? UpdatedDateUTC { get; set; }

        /// <summary>
        /// Gets or sets TotalRows.
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// Gets a value indicating whether gets IsNew.
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.UniqueId == default(Guid);
            }
        }
    }
}
