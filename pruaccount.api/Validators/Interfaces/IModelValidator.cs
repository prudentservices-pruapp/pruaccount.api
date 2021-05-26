// <copyright file="IModelValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Validators.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// IValidator.
    /// </summary>
    /// <typeparam name="T">Model Class Object.</typeparam>
    public interface IModelValidator<T>
    {
        /// <summary>
        /// IsValid.
        /// </summary>
        /// <param name="model">Model e.g. BankAccountDetailModel.</param>
        /// <returns>True or False.</returns>
        bool IsValid(T model);

        /// <summary>
        /// BrokenRules.
        /// </summary>
        /// <param name="model">Model e.g. BankAccountDetailModel.</param>
        /// <returns>True or False.</returns>
        List<string> BrokenRules(T model);
    }
}
