namespace Pruaccount.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// BankStatementTransactionDetailModel.
    /// </summary>
    public class BankStatementTransactionDetailModel
    {

        /// <summary>
        /// Gets or sets RowId.
        /// </summary>
        public int RowId { get; set; }

        /// <summary>
        /// Gets or sets BankStatementTransactionDetailId.
        /// </summary>
        public int BankStatementTransactionDetailId { get; set; }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets ClientBusinessDetailsUniqueId.
        /// </summary>
        public Guid ClientBusinessDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets BankAccountDetailsUniqueId.
        /// </summary>
        public Guid BankAccountDetailsUniqueId { get; set; }

        /// <summary>
        /// Gets or sets TransactionDate.
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets Reference.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets CreditAmount.
        /// </summary>
        public decimal CreditAmount { get; set; }

        /// <summary>
        /// Gets or sets DebitAmount.
        /// </summary>
        public decimal DebitAmount { get; set; }

        /// <summary>
        /// Gets or sets Balance.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
