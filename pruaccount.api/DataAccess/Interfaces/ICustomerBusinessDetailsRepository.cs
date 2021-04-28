// <copyright file="ICustomerBusinessDetailsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess.Interfaces
{
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Entities;

    /// <summary>
    /// ICustomerBusinessDetailsRepository.
    /// </summary>
    public interface ICustomerBusinessDetailsRepository : IRepositoryBase<CustomerBusinessDetails>
    {
        /// <summary>
        /// SaveNewCustomer.
        /// </summary>
        /// <param name="customerBusinessDetails">customerBusinessDetails.</param>
        /// <param name="customerMainBusinessAddress">customerMainBusinessAddress.</param>
        /// <param name="customerDeliveryBusinessAddress">customerDeliveryBusinessAddress.</param>
        /// <param name="customerBusinessPaymentDetails">customerBusinessPaymentDetails.</param>
        /// <param name="customerBusinessMisc">customerBusinessMisc.</param>
        /// <returns>CustomerBusinessDetails.</returns>
        CustomerBusinessDetails SaveNewCustomer(CustomerBusinessDetails customerBusinessDetails, CustomerBusinessAddress customerMainBusinessAddress, CustomerBusinessAddress customerDeliveryBusinessAddress, CustomerBusinessPaymentDetails customerBusinessPaymentDetails, CustomerBusinessMisc customerBusinessMisc);

        /// <summary>
        /// SaveCustomer.
        /// </summary>
        /// <param name="customerBusinessDetails">customerBusinessDetails.</param>
        /// <param name="customerMainBusinessAddress">customerMainBusinessAddress.</param>
        /// <param name="customerDeliveryBusinessAddress">customerDeliveryBusinessAddress.</param>
        /// <param name="customerBusinessPaymentDetails">customerBusinessPaymentDetails.</param>
        /// <param name="customerBusinessMisc">customerBusinessMisc.</param>
        /// <returns>CustomerBusinessDetails.</returns>
        CustomerBusinessDetails SaveCustomer(CustomerBusinessDetails customerBusinessDetails, CustomerBusinessAddress customerMainBusinessAddress, CustomerBusinessAddress customerDeliveryBusinessAddress, CustomerBusinessPaymentDetails customerBusinessPaymentDetails, CustomerBusinessMisc customerBusinessMisc);
    }
}
