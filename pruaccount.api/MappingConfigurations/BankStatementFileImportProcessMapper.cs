// <copyright file="BankStatementFileImportProcessMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.MappingConfigurations
{
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;

    /// <summary>
    /// BankStatementFileImportProcessMapper.
    /// </summary>
    public static class BankStatementFileImportProcessMapper
    {
        /// <summary>
        /// PopulateBankStatementFileImportProcessFromModel.
        /// </summary>
        /// <param name="bankStatementFileImportProcess">bankStatementFileImportProcess.</param>
        /// <param name="bankStatementFileImportProcessModel">bankStatementFileImportProcessModel.</param>
        /// <returns>BankStatementFileImportProcess.</returns>
        public static BankStatementFileImportProcess PopulateBankStatementFileImportProcessFromModel(this BankStatementFileImportProcess bankStatementFileImportProcess, BankStatementFileImportProcessModel bankStatementFileImportProcessModel)
        {
            if (bankStatementFileImportProcess != null)
            {
                bankStatementFileImportProcess.UniqueId = bankStatementFileImportProcessModel.UniqueId;
                bankStatementFileImportProcess.ClientBusinessDetailsUniqueId = bankStatementFileImportProcessModel.ClientBusinessDetailsUniqueId;
                bankStatementFileImportProcess.BankAccountDetailsUniqueId = bankStatementFileImportProcessModel.BankAccountDetailsUniqueId;
                bankStatementFileImportProcess.BankStatementFileImportUniqueId = bankStatementFileImportProcessModel.BankStatementFileImportUniqueId;
                bankStatementFileImportProcess.ProcessStatus = bankStatementFileImportProcessModel.ProcessStatus;
                bankStatementFileImportProcess.PreviousUniqueId = bankStatementFileImportProcessModel.PreviousUniqueId;
            }

            return bankStatementFileImportProcess;
        }

        /// <summary>
        /// PopulateBankStatementFileImportProcessModelFromEntity.
        /// </summary>
        /// <param name="bankStatementFileImportProcessModel">bankStatementFileImportProcessModel.</param>
        /// <param name="bankStatementFileImportProcess">bankStatementFileImportProcess.</param>
        /// <returns>BankStatementFileImportProcessModel.</returns>
        public static BankStatementFileImportProcessModel PopulateBankStatementFileImportProcessModelFromEntity(this BankStatementFileImportProcessModel bankStatementFileImportProcessModel, BankStatementFileImportProcess bankStatementFileImportProcess)
        {
            if (bankStatementFileImportProcessModel != null)
            {
                bankStatementFileImportProcessModel.UniqueId = bankStatementFileImportProcess.UniqueId;
                bankStatementFileImportProcessModel.ClientBusinessDetailsUniqueId = bankStatementFileImportProcess.ClientBusinessDetailsUniqueId;
                bankStatementFileImportProcessModel.BankAccountDetailsUniqueId = bankStatementFileImportProcess.BankAccountDetailsUniqueId;
                bankStatementFileImportProcessModel.BankStatementFileImportUniqueId = bankStatementFileImportProcess.BankStatementFileImportUniqueId;
                bankStatementFileImportProcessModel.ProcessStatus = bankStatementFileImportProcess.ProcessStatus;
                bankStatementFileImportProcessModel.PreviousUniqueId = bankStatementFileImportProcess.PreviousUniqueId;
            }

            return bankStatementFileImportProcessModel;
        }
    }
}
