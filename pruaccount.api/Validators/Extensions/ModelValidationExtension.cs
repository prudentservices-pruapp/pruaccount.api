﻿// <copyright file="ModelValidationExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Validators.Extensions
{
    using System.Collections.Generic;
    using Pruaccount.Api.Models;
    using Pruaccount.Api.Validators.Interfaces;

    /// <summary>
    /// ModelValidationExtension.
    /// </summary>
    public static class ModelValidationExtension
    {
        /// <summary>
        /// ValidateModel.
        /// </summary>
        /// <typeparam name="T">T is model object that implements IModelValidation.</typeparam>
        /// <param name="model">model.</param>
        /// <param name="brokenRules">list of errors.</param>
        /// <returns>True valid and False for invalid.</returns>
        public static bool ValidateModel<T>(this T model, out List<string> brokenRules)
            where T : IModelValidation<T>
        {
            IModelValidator<T> validator = ModelValidatorFactory.GetValidatorFor(model);
            brokenRules = new List<string>();

            return model.ValidateModel(validator, out brokenRules);
        }
    }
}
