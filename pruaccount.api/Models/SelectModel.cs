// <copyright file="SelectModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// SelectModel.
    /// </summary>
    public class SelectModel
    {
        /// <summary>
        /// Gets or sets UniqueItemValue.
        /// </summary>
        public Guid UniqueItemValue { get; set; }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public int ItemValue{ get; set; }

        /// <summary>
        /// Gets or sets ItemText.
        /// </summary>
        public string ItemText { get; set; }
    }
}
