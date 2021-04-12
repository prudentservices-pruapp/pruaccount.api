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
        /// Add.
        /// </summary>
        /// <param name="item">T.</param>
        /// <returns>Returns T.</returns>
        T Add(T item);

        /// <summary>
        /// Remove.
        /// </summary>
        /// <param name="pid">Primary Key.</param>
        void Remove(Guid pid);

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="item">T item.</param>
        /// <returns>Returns T.</returns>
        T Update(T item);

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
        /// FindByFID.
        /// </summary>
        /// <param name="fid">Foreign key.</param>
        /// <returns>IEnumerable T.</returns>
        IEnumerable<T> FindByFID(Guid fid);

        /// <summary>
        /// FindAll.
        /// </summary>
        /// <returns>IEnumerable T.</returns>
        IEnumerable<T> FindAll();

        /// <summary>
        /// FindAll.
        /// </summary>
        /// <param name="sort">Sort.</param>
        /// <param name="orderby">OrderBy.</param>
        /// <param name="pagenumber">PageNumber.</param>
        /// <param name="rowsperpage">RowsPerPage.</param>
        /// <returns>IEnumerable T.</returns>
        IEnumerable<T> FindAll(string sort, string orderby, int pagenumber, int rowsperpage);
    }
}
