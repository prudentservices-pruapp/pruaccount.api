// <copyright file="BaseModelValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Validators
{
    using System;

    /// <summary>
    /// BaseModelValidator.
    /// </summary>
    public class BaseModelValidator
    {
        /// <summary>
        /// GetObject.
        /// Create Object to Generic or Unknown Class.
        /// </summary>
        /// <typeparam name="T">type of object.</typeparam>
        /// <param name="lstArgument">lstArgument arguments where constuctor needs params.</param>
        /// <returns>instance of object.</returns>
        public T GetObject<T>(params object[] lstArgument)
        {
            // Create object with default parameter.
            // Employee emp = GetObject<Employee>();
            // Create object with argumented parameter
            // Student student = GetObject<Student>("Name", "MCA"); ;
            return (T)Activator.CreateInstance(typeof(T), lstArgument);
        }
    }
}
