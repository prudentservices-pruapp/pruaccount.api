// <copyright file="ISupplierBusinessDetailsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess.Interfaces
{
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Entities;

    /// <summary>
    /// ISupplierBusinessDetailsRepository.
    /// </summary>
    public interface ISupplierBusinessDetailsRepository : IRepositoryBase<SupplierBusinessDetails>
    {
        /// <summary>
        /// SaveNewSupplier.
        /// </summary>
        /// <param name="supplierBusinessDetails">SupplierBusinessDetails.</param>
        /// <param name="supplierMainBusinessAddress">SupplierMainBusinessAddress.</param>
        /// <param name="supplierDeliveryBusinessAddress">SupplierDeliveryBusinessAddress.</param>
        /// <param name="supplierBusinessPaymentDetails">SupplierBusinessPaymentDetails.</param>
        /// <param name="supplierBusinessMisc">SupplierBusinessMisc.</param>
        /// <returns>Supplier BusinessDetails.</returns>
        SupplierBusinessDetails SaveNewSupplier(SupplierBusinessDetails supplierBusinessDetails, SupplierBusinessAddress supplierMainBusinessAddress, SupplierBusinessPaymentDetails supplierBusinessPaymentDetails, SupplierBusinessMisc supplierBusinessMisc);

        /// <summary>
        /// SaveSupplier.
        /// </summary>
        /// <param name="supplierBusinessDetails">SupplierBusinessDetails.</param>
        /// <param name="supplierMainBusinessAddress">SupplierMainBusinessAddress.</param>
        /// <param name="supplierDeliveryBusinessAddress">SupplierDeliveryBusinessAddress.</param>
        /// <param name="supplierBusinessPaymentDetails">SupplierBusinessPaymentDetails.</param>
        /// <param name="supplierBusinessMisc">SupplierBusinessMisc.</param>
        /// <returns>Supplier BusinessDetails.</returns>
        SupplierBusinessDetails SaveSupplier(SupplierBusinessDetails supplierBusinessDetails, SupplierBusinessAddress supplierMainBusinessAddress, SupplierBusinessPaymentDetails supplierBusinessPaymentDetails, SupplierBusinessMisc supplierBusinessMisc);
    }
}