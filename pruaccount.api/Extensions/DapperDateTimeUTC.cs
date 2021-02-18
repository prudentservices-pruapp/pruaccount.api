// <copyright file="DapperDateTimeUTC.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Extensions
{
    using System;
    using System.Data;
    using Dapper;

    /// <summary>
    /// DapperDateTimeUTC.
    /// </summary>
    public class DapperDateTimeUTC : SqlMapper.TypeHandler<DateTime>
    {
        /// <summary>
        /// SetValue.
        /// </summary>
        /// <param name="parameter">IDbDataParameter.</param>
        /// <param name="value">DateTime.</param>
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.Value = value;
        }

        /// <summary>
        /// DateTime Parse.
        /// </summary>
        /// <param name="value">Object.</param>
        /// <returns>DateTimeKind.Utc DateTime.</returns>
        public override DateTime Parse(object value)
        {
            return DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
        }
    }
}
