// <copyright file="ModelValidatorFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Validators
{
    using System;
    using Pruaccount.Api.Models;
    using Pruaccount.Api.Validators.Interfaces;

    /// <summary>
    /// ModelValidatorFactory.
    /// https://lostechies.com/jimmybogard/2007/10/24/entity-validation-with-visitors-and-extension-methods.
    /// </summary>
    public static class ModelValidatorFactory
    {
        /// <summary>
        /// GetValidatorFor.
        /// </summary>
        /// <typeparam name="T">T is model object that implements IModelValidation.</typeparam>
        /// <param name="model">model.</param>
        /// <returns>instance of IModelValidator.</returns>
        public static IModelValidator<T> GetValidatorFor<T>(T model)
        where T : IModelValidation<T>
        {
            string validatorClassName = $"Pruaccount.Api.Validators.{typeof(T).Name}Validator";

            Type obj = Type.GetType(validatorClassName);

            if (obj != null)
            {
                return (IModelValidator<T>)Activator.CreateInstance(obj);
            }

            throw new InvalidOperationException($"Could not find type of {validatorClassName}");
        }
    }
}
