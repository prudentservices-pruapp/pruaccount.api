// <copyright file="CategoryGroup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Entities
{
    using System;

    /// <summary>
    /// CategoryGroup.
    /// </summary>
    public class CategoryGroup
    {
        /// <summary>
        /// Gets or sets CategoryGroupId.
        /// </summary>
        public int CategoryGroupId { get; set; }

        /// <summary>
        /// Gets or sets Category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets Group.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets CreatedDateUTC.
        /// </summary>
        public DateTime CreatedDateUTC { get; set; }

        /// <summary>
        /// Gets or sets UpdatedDateUTC.
        /// </summary>
        public DateTime? UpdatedDateUTC { get; set; }

        /// <summary>
        /// Gets or sets TotalRows.
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// Gets a value indicating whether gets IsNew.
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.CategoryGroupId == default(int);
            }
        }
    }
}
