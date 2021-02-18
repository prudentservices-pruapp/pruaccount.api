// <copyright file="DBInfoConfigSetting.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.AppSettings
{
    using System.Collections.Generic;

    /// <summary>
    /// DB Info Config Setting.
    /// </summary>
    public class DBInfoConfigSetting
    {
        /// <summary>
        /// Gets or sets core Connection.
        /// </summary>
        public string CoreConnection { get; set; }

        /// <summary>
        /// Gets or sets core StorageList.
        /// </summary>
        public List<Storage> StorageList { get; set; }
    }
}
