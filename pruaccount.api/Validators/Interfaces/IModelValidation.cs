// <copyright file="IModelValidation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Validators.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// IModelValidation.
    /// </summary>
    /// <typeparam name="T">Model Class Object.</typeparam>
    public interface IModelValidation<T>
    {
        /// <summary>
        /// ValidateModel.
        /// </summary>
        /// <param name="validator">e.g. IValidator AddressModel.</param>
        /// <param name="brokenRules">list of errors.</param>
        /// <returns>True or False.</returns>
        bool ValidateModel(IModelValidator<T> validator, out List<string> brokenRules);
    }
}
