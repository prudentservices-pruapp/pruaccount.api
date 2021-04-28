// <copyright file="CustomerBusinessAddressRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.DataAccess.Interfaces;
    using Pruaccount.Api.Entities;

    /// <summary>
    /// CustomerBusinessAddressRepository.
    /// </summary>
    public class CustomerBusinessAddressRepository : RepositoryBase, ICustomerBusinessAddressRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBusinessAddressRepository"/> class.
        /// </summary>
        /// <param name="uw">IUnitOfWork.</param>
        public CustomerBusinessAddressRepository(IUnitOfWork uw)
            : base(uw)
        {
        }

        /// <summary>
        /// FindByCID.
        /// </summary>
        /// <param name="pid">pid.</param>
        /// <param name="businessDetailsUniqueId">businessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId.</param>
        /// <returns>CustomerBusinessAddress.</returns>
        public CustomerBusinessAddress FindByCID(Guid pid, Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerBusinessAddress> FindByFID(Guid fid)
        {
            throw new NotImplementedException();
        }

        public CustomerBusinessAddress FindByPID(Guid pid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerBusinessAddress> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid pid)
        {
            throw new NotImplementedException();
        }

        public CustomerBusinessAddress Save(CustomerBusinessAddress item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerBusinessAddress> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage)
        {
            throw new NotImplementedException();
        }
    }
}
