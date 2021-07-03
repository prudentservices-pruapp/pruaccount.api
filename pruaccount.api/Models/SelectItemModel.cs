// <copyright file="SelectItemModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Models
{
    using System;

    /// <summary>
    /// SelectItemModel.
    /// </summary>
    public class SelectItemModel
    {
        /// <summary>
        /// Gets or sets UniqueItemValue.
        /// </summary>
#pragma warning disable SA1300 // Element should begin with upper-case letter
        public string _id { get; set; }
#pragma warning restore SA1300 // Element should begin with upper-case letter

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
#pragma warning disable SA1300 // Element should begin with upper-case letter
        public string name { get; set; }
#pragma warning restore SA1300 // Element should begin with upper-case letter

    }
}
