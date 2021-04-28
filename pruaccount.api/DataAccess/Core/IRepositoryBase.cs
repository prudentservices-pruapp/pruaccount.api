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
        /// ListAll.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. customerBusinessDetailsUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId e.g. InvoiceUniqueId.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable T.</returns>
        IEnumerable<T> ListAll(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId = default, string sort = "Unknown", string orderby = "asc", int pagenumber = 1, int rowsperpage = 10);

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="businessDetailsUniqueId">e.g. clientBusinessDetailsUniqueId.</param>
        /// <param name="masterUniqueId">masterUniqueId e.g. customerBusinessDetailsUniqueId.</param>
        /// <param name="parentUniqueId">parentUniqueId e.g. InvoiceUniqueId.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable T.</returns>
        IEnumerable<T> Search(Guid businessDetailsUniqueId, Guid masterUniqueId, Guid parentUniqueId, string searchTerm, string sort, string orderby, int pagenumber, int rowsperpage);
    }
}
