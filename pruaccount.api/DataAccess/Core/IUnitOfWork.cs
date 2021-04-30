// <copyright file="IUnitOfWork.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess.Core
{
    using System;
    using System.Data;
    using Pruaccount.Api.DataAccess.Interfaces;

    /// <summary>
    /// IUnitOfWork.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets connection.
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Gets transaction.
        /// </summary>
        IDbTransaction Transaction { get; }

        /// <summary>
        /// Gets CBFinancialSettingRepository.
        /// </summary>
        ICBFinancialSettingRepository CBFinancialSettingRepository { get; }

        /// <summary>
        /// Gets CustomerBusinessAddressRepository.
        /// </summary>
        ICustomerBusinessAddressRepository CustomerBusinessAddressRepository { get; }

        /// <summary>
        /// Gets CustomerBusinessDetailsRepository.
        /// </summary>
        ICustomerBusinessDetailsRepository CustomerBusinessDetailsRepository { get; }

        /// <summary>
        /// Gets CustomerBusinessPaymentDetailsRepository.
        /// </summary>
        ICustomerBusinessPaymentDetailsRepository CustomerBusinessPaymentDetailsRepository { get; }

        /// <summary>
        /// Gets CustomerBusinessMiscRepository.
        /// </summary>
        ICustomerBusinessMiscRepository CustomerBusinessMiscRepository { get; }

        /// <summary>
        /// Gets LedgerAccountRepository.
        /// </summary>
        ILedgerAccountRepository LedgerAccountRepository { get; }

        /// <summary>
        /// Begin Transaction IsolationLevel.
        /// </summary>
        /// <param name="isolationLevel">IsolationLevel.</param>
        void Begin(IsolationLevel isolationLevel);

        /// <summary>
        /// Complete.
        /// </summary>
        void Complete();
    }
}
