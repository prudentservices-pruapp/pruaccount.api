// <copyright file="IRepositoryBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.DataAccess.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///  IRepositoryBase.<T>.
    /// </summary>
    /// <typeparam name="T">class of type T.</typeparam>
    public interface IRepositoryBase<T>
        where T : class
    {
        /// <summary>
        /// Remove.
        /// </summary>
        /// <param name="pid">Primary Key.</param>
        void Remove(Guid pid);

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="item">T item.</param>
        /// <returns>Returns T.</returns>
        T Save(T item);

        /// <summary>
        /// FindByPID.
        /// </summary>
        /// <param name="pid">Primary key.</param>
        /// <returns>Returns T.</returns>
        T FindByPID(Guid pid);

        /// <summary>
        /// FindByCID.
        /// </summary>
        /// <param name="pid">UniqueId.</param>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. CustomerBusinessDetailsUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId e.g. InvoiceUniqueId.</param>
        /// <returns>Returns T.</returns>
        T FindByCID(Guid pid, Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId);

        /// <summary>
        /// FindByFID.
        /// </summary>
        /// <param name="fid">Foreign key.</param>
        /// <returns>IEnumerable T.</returns>
        IEnumerable<T> FindByFID(Guid fid);

        /// <summary>
        /// ListAll.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. customerBusinessDetailsUniqueId.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable T.</returns>
        IEnumerable<T> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, string sort, string orderby, int pagenumber, int rowsperpage);

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. customerBusinessDetailsUniqueId.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable T.</returns>
        IEnumerable<T> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage);
    }
}
