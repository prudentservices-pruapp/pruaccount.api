// <copyright file="SupplierBusinessDetails.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// SupplierBusinessDetails.
    /// </summary>
    public class SupplierBusinessDetails
    {
        /// <summary>
        /// Gets or sets SupplierBusinessDetailsId.
        /// </summary>
        public int SupplierBusinessDetailsId { get; set; }

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
        /// Gets or sets a value indicating whether gets or sets ImportAgentVat.
        /// This supplier is an import agent.
        /// Set a supplier as an import agent to deal with import VAT.
        /// </summary>
        public bool ImportAgentVat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets VATReverseCharge.
        /// VAT Reverse Charge.
        /// Allows you to apply VAT reverse charge to invoices for this contact.
        /// </summary>
        public bool VATReverseCharge { get; set; }

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