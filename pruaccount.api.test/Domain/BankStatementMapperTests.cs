namespace pruaccount.api.test.Domain
{
    using Pruaccount.Api.Domain.BankStatement;
    using Pruaccount.Api.Models;
    using Pruaccount.Api.Validators;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;

    public class BankStatementMapperTests
    {
        private readonly BankStatementMapDetailModel bankStatementMapDetailLloydsModel;
        private readonly BankStatementMapDetailModel bankStatementMapDetailCaterAllenModel;

        private readonly BankStatementMapDetailSaveModel bankStatementMapDetailSaveLloydsModel;
        private readonly BankStatementMapDetailSaveModel bankStatementMapDetailSaveCaterAllenModel;

        public BankStatementMapperTests(ITestOutputHelper output)
        {
            bankStatementMapDetailLloydsModel = new BankStatementMapDetailModel()
            {
                Dateformat = "dd/MM/yyyy",
                DateIndex = 0,
                BalanceIndex = 7,
                CreditAmountIndex = 6,
                DebitAmountIndex = 5,
                DescriptionIndex = 4,
            };

            bankStatementMapDetailCaterAllenModel = new BankStatementMapDetailModel()
            {
                Dateformat = "ddMMMyyyy",
                DateIndex = 0,
                BalanceIndex = 4,
                CreditAmountIndex = 3,
                DebitAmountIndex = 3,
                DescriptionIndex = 1,
            };

            bankStatementMapDetailSaveLloydsModel = new BankStatementMapDetailSaveModel()
            {
                BankAccountDetailsUniqueId = new Guid("AC81EE46-06B3-EB11-A007-00155E179406"),
                BankStatementFileImportUniqueId = new Guid("D077065A-D9C5-EB11-A007-00155E179406"),
                Mapname = "Lloyds Bank",
                Datepart1 = "dd",
                Datepart2 = "MM",
                Datepart3 = "yyyy",
                Dateseparator = "/",
                Dateformat = "dd/MM/yyyy",
                DateformatValue = "01/09/2009",
                Column0 = 1, //Date
                Column4 = 6, //Description
                Column5 = 3, //DebitAmount
                Column6 = 2, //CreditAmount
                Column7 = 5, //Balance
            };

            bankStatementMapDetailSaveCaterAllenModel = new BankStatementMapDetailSaveModel()
            {
                BankAccountDetailsUniqueId = new Guid("AC81EE46-06B3-EB11-A007-00155E179406"),
                BankStatementFileImportUniqueId = new Guid("D077065A-D9C5-EB11-A007-00155E179406"),
                Mapname = "Cater Allen Bank",
                Datepart1 = "dd",
                Datepart2 = "MMM",
                Datepart3 = "yyyy",
                Dateseparator = "",
                Dateformat = "ddMMMyyyy",
                DateformatValue = "01Jun2009",
                Column0 = 1, //Date
                Column1 = 6, //Description
                Column3 = 4, //CreditDebitAmount
                Column4 = 5  //Balance
            };
        }

        [Fact]
        public void PopulateBankStatementMapDetail_For_Lloyds_Bank_Statement()
        {
            var sut = new BankStatementMapper(this.bankStatementMapDetailSaveLloydsModel);
            var result = sut.BankStatementMapDetailModel;

            Assert.Equal(this.bankStatementMapDetailLloydsModel.Dateformat, result.Dateformat);
            Assert.Equal(this.bankStatementMapDetailLloydsModel.DateIndex, result.DateIndex);
            Assert.Equal(this.bankStatementMapDetailLloydsModel.CreditAmountIndex, result.CreditAmountIndex);
            Assert.Equal(this.bankStatementMapDetailLloydsModel.DebitAmountIndex, result.DebitAmountIndex);
            Assert.Equal(this.bankStatementMapDetailLloydsModel.DescriptionIndex, result.DescriptionIndex);
        }

        [Fact]
        public void PopulateBankStatementMapDetail_For_CaterAllen_Bank_Statement()
        {
            var sut = new BankStatementMapper(this.bankStatementMapDetailSaveCaterAllenModel);

            var result = sut.BankStatementMapDetailModel;

            Assert.Equal(this.bankStatementMapDetailCaterAllenModel.Dateformat, result.Dateformat);
            Assert.Equal(this.bankStatementMapDetailCaterAllenModel.DateIndex, result.DateIndex);
            Assert.Equal(this.bankStatementMapDetailCaterAllenModel.CreditAmountIndex, result.CreditAmountIndex);
            Assert.Equal(this.bankStatementMapDetailCaterAllenModel.DebitAmountIndex, result.DebitAmountIndex);
            Assert.Equal(this.bankStatementMapDetailCaterAllenModel.DescriptionIndex, result.DescriptionIndex);
        }
    }
}
