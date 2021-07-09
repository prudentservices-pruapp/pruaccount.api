// <copyright file="FinancialSettingMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.MappingConfigurations
{
    using Pruaccount.Api.Entities;
    using Pruaccount.Api.Models;

    /// <summary>
    /// FinancialSettingMapper.
    /// </summary>
    public class FinancialSettingMapper
    {
        /// <summary>
        /// PopulateFromModel
        /// CBFinancialSetting populated From Model.
        /// </summary>
        /// <param name="financialSettingModel">FinancialSettingModel.</param>
        /// <returns>CBFinancialSetting.</returns>
        public CBFinancialSetting PopulateFromModel(FinancialSettingModel financialSettingModel)
        {
            CBFinancialSetting cBFinancialSetting = new CBFinancialSetting();
            cBFinancialSetting.UniqueId = financialSettingModel.UniqueId;
            cBFinancialSetting.AccountStartDate = financialSettingModel.AccountStartDate;
            cBFinancialSetting.HMRCUserId = financialSettingModel.HMRCUserId;
            cBFinancialSetting.RetentionPeriod = financialSettingModel.RetentionPeriod;
            cBFinancialSetting.VatFlatRate = financialSettingModel.VatFlatRate;
            cBFinancialSetting.VatNumber = financialSettingModel.VatNumber;
            cBFinancialSetting.VatScheme = financialSettingModel.VatScheme;
            cBFinancialSetting.VatSubmissionRequency = financialSettingModel.VatSubmissionRequency;
            cBFinancialSetting.YearEndDate = financialSettingModel.YearEndDate;
            cBFinancialSetting.YearEndLockdownDate = financialSettingModel.YearEndLockdownDate;
            cBFinancialSetting.YearEndTaxMonth = financialSettingModel.YearEndTaxMonth;
            cBFinancialSetting.YearStartDate = financialSettingModel.YearStartDate;

            return cBFinancialSetting;
        }

        /// <summary>
        /// PopulateFromEntity
        /// FinancialSettingModel populated From Entity.
        /// </summary>
        /// <param name="cBFinancialSetting">CBFinancialSetting.</param>
        /// <returns>FinancialSettingModel.</returns>
        public FinancialSettingModel PopulateFromEntity(CBFinancialSetting cBFinancialSetting)
        {

            FinancialSettingModel financialSettingModel = new FinancialSettingModel();
            financialSettingModel.UniqueId = cBFinancialSetting.UniqueId;
            financialSettingModel.AccountStartDate = cBFinancialSetting.AccountStartDate;
            financialSettingModel.HMRCUserId = cBFinancialSetting.HMRCUserId;
            financialSettingModel.RetentionPeriod = cBFinancialSetting.RetentionPeriod;
            financialSettingModel.VatFlatRate = cBFinancialSetting.VatFlatRate;
            financialSettingModel.VatNumber = cBFinancialSetting.VatNumber;
            financialSettingModel.VatScheme = cBFinancialSetting.VatScheme;
            financialSettingModel.VatSubmissionRequency = cBFinancialSetting.VatSubmissionRequency;
            financialSettingModel.YearEndDate = cBFinancialSetting.YearEndDate;
            financialSettingModel.YearEndLockdownDate = cBFinancialSetting.YearEndLockdownDate;
            financialSettingModel.YearEndTaxMonth = cBFinancialSetting.YearEndTaxMonth;
            financialSettingModel.YearStartDate = cBFinancialSetting.YearStartDate;

            return financialSettingModel;
        }
    }
}
