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
    public class BankStatementFileImportProcessMapper
    {
        /// <summary>
        /// PopulateFromModel.
        /// BankStatementFileImportProcess populated From Model.
        /// </summary>
        /// <param name="bankStatementFileImportProcessModel">bankStatementFileImportProcessModel.</param>
        /// <returns>BankStatementFileImportProcess.</returns>
        public BankStatementFileImportProcess PopulateFromModel(BankStatementFileImportProcessModel bankStatementFileImportProcessModel)
        {
            BankStatementFileImportProcess bankStatementFileImportProcess = new BankStatementFileImportProcess();
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
        /// PopulateFromEntity.
        /// BankStatementFileImportProcessModel populated From Entity.
        /// </summary>
        /// <param name="bankStatementFileImportProcess">bankStatementFileImportProcess.</param>
        /// <returns>BankStatementFileImportProcessModel.</returns>
        public BankStatementFileImportProcessModel PopulateFromEntity(BankStatementFileImportProcess bankStatementFileImportProcess)
        {
            BankStatementFileImportProcessModel bankStatementFileImportProcessModel = new BankStatementFileImportProcessModel();
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
