﻿// <copyright file="CustomerBusinessDetails.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// CustomerBusinessDetails.
    /// </summary>
    public class CustomerBusinessDetails
    {
        /// <summary>
        /// Gets or sets CustomerBusinessDetailsId.
        /// </summary>
        public int CustomerBusinessDetailsId { get; set; }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets ClientBusinessDetailsUniqueId.
        /// </summary>
        public Guid ClientBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets BusinessName.
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// Gets or sets FirstName.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets LastName.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets ClientReference.
        /// </summary>
        public string ClientReference { get; set; }

        /// <summary>
        /// Gets or sets Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets Phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets Mobile.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets TypeOfBusiness.
        /// </summary>
        public string TypeOfBusiness { get; set; }

        /// <summary>
        /// Gets or sets RegistrationNumber.
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or sets RegisteredCountry.
        /// </summary>
        public string RegisteredCountry { get; set; }

        /// <summary>
        /// Gets or sets VatNumber.
        /// </summary>
        public string VatNumber { get; set; }

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
