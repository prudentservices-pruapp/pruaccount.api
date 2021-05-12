// <copyright file="UnitOfWork.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess.Core
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Pruaccount.Api.AppSettings;
    using Pruaccount.Api.DataAccess.Interfaces;
    using Pruaccount.Api.Domain.Auth;

    /// <summary>
    /// UnitOfWork.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBInfoConfigSetting dbinfoconfigsettings;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<UnitOfWork> logger;

        private IDbConnection connection;
        private IDbTransaction transaction;
        private bool disposed;

        private IBankAccountDetailsRepository bankAccountDetailsRepository;
        private ICBFinancialSettingRepository cbFinancialSettingRepository;
        private ICustomerBusinessAddressRepository customerBusinessAddressRepository;
        private ICustomerBusinessDetailsRepository customerBusinessDetailsRepository;
        private ICustomerBusinessPaymentDetailsRepository customerBusinessPaymentDetailsRepository;
        private ICustomerBusinessMiscRepository customerBusinessMiscRepository;
        private ILedgerAccountRepository ledgerAccountRepository;
        private ISupplierBusinessAddressRepository supplierBusinessAddressRepository;
        private ISupplierBusinessDetailsRepository supplierBusinessDetailsRepository;
        private ISupplierBusinessPaymentDetailsRepository supplierBusinessPaymentDetailsRepository;
        private ISupplierBusinessMiscRepository supplierBusinessMiscRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dbinfoconfigsettings">IOptions DBInfoConfigSetting.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor.</param>
        /// <param name="logger">logger.</param>
        public UnitOfWork(IOptions<DBInfoConfigSetting> dbinfoconfigsettings, IHttpContextAccessor httpContextAccessor, ILogger<UnitOfWork> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbinfoconfigsettings = dbinfoconfigsettings.Value;
            this.logger = logger;
            var hostname = this.httpContextAccessor.HttpContext.Request.Scheme + "://" + this.httpContextAccessor.HttpContext.Request.Host.Value;
            if (this.connection == null && this.dbinfoconfigsettings != null)
            {
                // Change the logic later to use it from maybe token.
                var tokenUserDetails = this.httpContextAccessor.HttpContext.Items["CurrentTokenUserDetails"] as TokenUserDetails;
                var productConnection = this.dbinfoconfigsettings.StorageList.Find(x => x.Product == tokenUserDetails.Products[0]);

                if (productConnection != null)
                {
                    this.connection = new SqlConnection(productConnection.DataConnection);
                    this.connection.Open();
                }
                else
                {
                    this.logger.LogError("UnitOfWork->UnitOfWork Constructor Could not get any Product from Token.");
                }
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        ~UnitOfWork()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets connection.
        /// </summary>
        public IDbConnection Connection
        {
            get { return this.connection; }
        }

        /// <summary>
        /// Gets transaction.
        /// </summary>
        public IDbTransaction Transaction
        {
            get { return this.transaction; }
        }

        /// <summary>
        /// Gets BankAccountDetailsRepository.
        /// </summary>
        public IBankAccountDetailsRepository BankAccountDetailsRepository
        {
            get { return this.bankAccountDetailsRepository ??= new BankAccountDetailsRepository(this); }
        }

        /// <summary>
        /// Gets CBFinancialSettingRepository.
        /// </summary>
        public ICBFinancialSettingRepository CBFinancialSettingRepository
        {
            get { return this.cbFinancialSettingRepository ??= new CBFinancialSettingRepository(this); }
        }

        /// <summary>
        /// Gets CustomerBusinessAddressRepository.
        /// </summary>
        public ICustomerBusinessAddressRepository CustomerBusinessAddressRepository
        {
            get { return this.customerBusinessAddressRepository ?? new CustomerBusinessAddressRepository(this);  }
        }

        /// <summary>
        /// Gets CustomerBusinessDetailsRepository.
        /// </summary>
        public ICustomerBusinessDetailsRepository CustomerBusinessDetailsRepository
        {
            get { return this.customerBusinessDetailsRepository ?? new CustomerBusinessDetailsRepository(this); }
        }

        /// <summary>
        /// Gets CustomerBusinessPaymentDetailsRepository.
        /// </summary>
        public ICustomerBusinessPaymentDetailsRepository CustomerBusinessPaymentDetailsRepository
        {
            get { return this.customerBusinessPaymentDetailsRepository ?? new CustomerBusinessPaymentDetailsRepository(this); }
        }

        /// <summary>
        /// Gets CustomerBusinessMiscRepository.
        /// </summary>
        public ICustomerBusinessMiscRepository CustomerBusinessMiscRepository
        {
            get { return this.customerBusinessMiscRepository ?? new CustomerBusinessMiscRepository(this); }
        }

        /// <summary>
        /// Gets SupplierBusinessAddressRepository.
        /// </summary>
        public ISupplierBusinessAddressRepository SupplierBusinessAddressRepository
        {
            get { return this.supplierBusinessAddressRepository ?? new SupplierBusinessAddressRepository(this); }
        }

        /// <summary>
        /// Gets SupplierBusinessDetailsRepository.
        /// </summary>
        public ISupplierBusinessDetailsRepository SupplierBusinessDetailsRepository
        {
            get { return this.supplierBusinessDetailsRepository ?? new SupplierBusinessDetailsRepository(this); }
        }

        /// <summary>
        /// Gets SupplierBusinessPaymentDetailsRepository.
        /// </summary>
        public ISupplierBusinessPaymentDetailsRepository SupplierBusinessPaymentDetailsRepository
        {
            get { return this.supplierBusinessPaymentDetailsRepository ?? new SupplierBusinessPaymentDetailsRepository(this); }
        }

        /// <summary>
        /// Gets SupplierBusinessMiscRepository.
        /// </summary>
        public ISupplierBusinessMiscRepository SupplierBusinessMiscRepository
        {
            get { return this.supplierBusinessMiscRepository ?? new SupplierBusinessMiscRepository(this); }
        }

        /// <summary>
        /// Gets LedgerAccountRepository.
        /// </summary>
        public ILedgerAccountRepository LedgerAccountRepository
        {
            get { return this.ledgerAccountRepository ??= new LedgerAccountRepository(this); }
        }

        /// <summary>
        /// Begin.
        /// </summary>
        /// <param name="isolationLevel">IsolationLevel.</param>
        public void Begin(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            try
            {
                this.transaction = this.connection.BeginTransaction(isolationLevel);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Complete.
        /// </summary>
        public void Complete()
        {
            try
            {
                this.transaction?.Commit();
            }
            catch
            {
                this.transaction?.Rollback();
            }
            finally
            {
                this.transaction?.Dispose();
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">Disposing.</param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.transaction != null)
                    {
                        this.transaction.Dispose();
                        this.transaction = null;
                    }

                    if (this.connection != null)
                    {
                        this.connection.Dispose();
                        this.connection = null;
                    }
                }

                this.disposed = true;
            }
        }
    }
}
