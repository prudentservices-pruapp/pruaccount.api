// <copyright file="BankStatementFileProcessStatusTypeEnum.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Enums
{

    /// <summary>
    /// BankStatement File Process Status TypeEnum.
    /// </summary>
    public static class BankStatementFileProcessStatusTypeEnum
    {
        /// <summary>
        /// Gets Uploaded.
        /// </summary>
        public static string Uploaded { get; } = "Uploaded";

        /// <summary>
        /// Gets Mapped.
        /// </summary>
        public static string Mapped { get; } = "Mapping";

        /// <summary>
        /// Gets InProcess.
        /// </summary>
        public static string InProcess { get; } = "InProcess";

        /// <summary>
        /// Gets Processed.
        /// </summary>
        public static string Processed { get; } = "Processed";

        /// <summary>
        /// Gets Rejected.
        /// </summary>
        public static string Rejected { get; } = "Rejected";

        /// <summary>
        /// Gets FileError.
        /// </summary>
        public static string FileError { get; } = "FileError";
    }
}
