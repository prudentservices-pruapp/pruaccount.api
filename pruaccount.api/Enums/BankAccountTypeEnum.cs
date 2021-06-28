// <copyright file="BankAccountTypeEnum.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Enums
{
    /// <summary>
    /// BankAccountTypeEnum.
    /// </summary>
    public class BankAccountTypeEnum
    {
        /// <summary>
        /// Gets Current.
        /// </summary>
        public static int Current { get; } = 1;

        /// <summary>
        /// Gets Savings.
        /// </summary>
        public static int Savings { get; } = 2;

        /// <summary>
        /// Gets CreditCard.
        /// </summary>
        public static int CreditCard { get; } = 3;

        /// <summary>
        /// Gets CashinHand.
        /// </summary>
        public static int CashinHand { get; } = 4;

        /// <summary>
        /// Gets Loan.
        /// </summary>
        public static int Loan { get; } = 5;

        /// <summary>
        /// Gets Other.
        /// </summary>
        public static int Other { get; } = 6;

        /// <summary>
        /// Gets NotKnown.
        /// </summary>
        public static int NotKnown { get; } = 7;
    }
}
