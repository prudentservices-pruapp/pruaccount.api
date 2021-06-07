// <copyright file="BankStatementMapColumnTypeEnum.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Enums
{
    /// <summary>
    /// BankStatementMapColumnTypeEnum.
    /// </summary>
    public static class BankStatementMapColumnTypeEnum
    {
        /// <summary>
        /// Gets Date Column index.
        /// </summary>
        public static int Date { get; } = 1;

        /// <summary>
        /// Gets CreditAmount Column index.
        /// </summary>
        public static int CreditAmount { get; } = 2;

        /// <summary>
        /// Gets DebitAmount Column index.
        /// </summary>
        public static int DebitAmount { get; } = 3;

        /// <summary>
        /// Gets CreditDebitAmount Column index.
        /// </summary>
        public static int CreditDebitAmount { get; } = 4;

        /// <summary>
        /// Gets Balance Column index.
        /// </summary>
        public static int Balance { get; } = 5;

        /// <summary>
        /// Gets Description Column index.
        /// </summary>
        public static int Description { get; } = 6;

        /// <summary>
        /// Gets Ignore Column index.
        /// </summary>
        public static int Ignore { get; } = 7;
    }
}
