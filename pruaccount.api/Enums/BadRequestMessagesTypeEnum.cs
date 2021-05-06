// <copyright file="BadRequestMessagesTypeEnum.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Enums
{
    /// <summary>
    /// BadRequestMessagesTypeEnum.
    /// </summary>
    public static class BadRequestMessagesTypeEnum
    {
        /// <summary>
        /// Gets InternalServerErrorsMessage.
        /// </summary>
        public static string InternalServerErrorsMessage { get; } = "Internal Server Error.";

        /// <summary>
        /// Gets MandatoryFieldsErrorsMessage.
        /// </summary>
        public static string MandatoryFieldsErrorsMessage { get; } = "Mandatory fields not entered.";

        /// <summary>
        /// Gets TokenNotFoundErrorsMessage.
        /// </summary>
        public static string NotFoundTokenErrorsMessage { get; } = "Could not get any token details.";

    }
}
