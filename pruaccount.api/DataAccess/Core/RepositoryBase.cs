// <copyright file="RepositoryBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess.Core
{
    using System.Data;

    /// <summary>
    /// RepositoryBase.
    /// </summary>
    public abstract class RepositoryBase
    {
        private IUnitOfWork uw;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public RepositoryBase(IUnitOfWork uw)
        {
            this.uw = uw;
        }

        /// <summary>
        /// Gets connection.
        /// </summary>
        protected IDbConnection Connection
        {
            get { return this.uw.Connection; }
        }

        /// <summary>
        /// Gets transaction.
        /// </summary>
        protected IDbTransaction Transaction
        {
            get { return this.uw.Transaction; }
        }
    }
}
