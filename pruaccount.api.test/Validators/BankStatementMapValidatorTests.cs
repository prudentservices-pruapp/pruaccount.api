namespace pruaccount.api.test.Validators
{
    using Pruaccount.Api.Domain.BankStatement;
    using Pruaccount.Api.Enums;
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

    public class BankStatementMapValidatorTests
    {
        private readonly string LloydsCSV = "Lloyds_01082020_31102020.csv";
        private readonly string CaterAllenCSV = "CaterAllen_01082020_31102020.csv";

        private readonly BankStatementMapDetailSaveModel bankStatementMapDetailSaveLloydsModel;
        private readonly BankStatementMapDetailSaveModel bankStatementMapDetailSaveCaterAllenModel;

        private readonly BankStatementMapDetailSaveModel bankStatementMapDetailSaveLloydsModelWithIncorrectDebitCredit;

        public BankStatementMapValidatorTests(ITestOutputHelper output)
        {
            bankStatementMapDetailSaveLloydsModel = new BankStatementMapDetailSaveModel()
            {
                BankAccountDetailsUniqueId = new Guid("AC81EE46-06B3-EB11-A007-00155E179406"),
                BankStatementFileUniqueId = new Guid("D077065A-D9C5-EB11-A007-00155E179406"),
                BankAccountTypeId = BankAccountTypeEnum.Current,
                MapName = "Lloyds Bank",
                DatePart1 = "dd",
                DatePart2 = "MM",
                DatePart3 = "yyyy",
                DateSeparator = "/",
                Dateformat = "dd/MM/yyyy",
                DateformatValue = "01/09/2009",
                Column0 = 1, //Date
                Column4 = 6, //Description
                Column5 = 3, //DebitAmount
                Column6 = 2, //CreditAmount
                Column7 = 5, //Balance
            };

            bankStatementMapDetailSaveLloydsModelWithIncorrectDebitCredit = new BankStatementMapDetailSaveModel()
            {
                BankAccountDetailsUniqueId = new Guid("AC81EE46-06B3-EB11-A007-00155E179406"),
                BankStatementFileUniqueId = new Guid("D077065A-D9C5-EB11-A007-00155E179406"),
                BankAccountTypeId = BankAccountTypeEnum.Current,
                MapName = "Lloyds Bank",
                DatePart1 = "dd",
                DatePart2 = "MM",
                DatePart3 = "yyyy",
                DateSeparator = "/",
                Dateformat = "dd/MM/yyyy",
                DateformatValue = "01/09/2009",
                Column0 = 1, //Date
                Column4 = 6, //Description
                Column5 = 2, //DebitAmount
                Column6 = 3, //CreditAmount
                Column7 = 5, //Balance
            };

            bankStatementMapDetailSaveCaterAllenModel = new BankStatementMapDetailSaveModel()
            {
                BankAccountDetailsUniqueId = new Guid("AC81EE46-06B3-EB11-A007-00155E179406"),
                BankStatementFileUniqueId = new Guid("D077065A-D9C5-EB11-A007-00155E179406"),
                BankAccountTypeId = BankAccountTypeEnum.Current,
                MapName = "Cater Allen Bank",
                DatePart1 = "dd",
                DatePart2 = "MMM",
                DatePart3 = "yyyy",
                DateSeparator = "",
                Dateformat = "ddMMMyyyy",
                DateformatValue = "01Jun2009",
                Column0 = 1, //Date
                Column1 = 6, //Description
                Column3 = 4, //CreditDebitAmount
                Column4 = 5  //Balance
            };
        }

        [Fact]
        public void ValidateStatmentData_For_Lloyds_Bank_Statement()
        {
            List<BankStatementTransactionDetailModel> bankStatementTransactionDetailModels;
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data";
            var fileNameWithPath = string.Format("{0}\\{1}", directory, this.LloydsCSV);
            var bankStatmentParser = new BankStatementParser(fileNameWithPath);
            var bankStatmentMapper = new BankStatementMapper(this.bankStatementMapDetailSaveLloydsModel);

            var sut = new BankStatementMapValidator(bankStatmentParser, bankStatmentMapper.BankStatementMapDetailModel);
            var result = sut.ValidateStatmentData(out bankStatementTransactionDetailModels);
            Assert.Empty(result);
            Assert.Equal(10, bankStatementTransactionDetailModels.Count);
        }

        [Fact]
        public void ValidateStatmentData_For_CaterAllen_Bank_Statement()
        {
            List<BankStatementTransactionDetailModel> bankStatementTransactionDetailModels;
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data";
            var fileNameWithPath = string.Format("{0}\\{1}", directory, this.CaterAllenCSV);
            var bankStatmentParser = new BankStatementParser(fileNameWithPath);
            var bankStatmentMapper = new BankStatementMapper(this.bankStatementMapDetailSaveCaterAllenModel);

            var sut = new BankStatementMapValidator(bankStatmentParser, bankStatmentMapper.BankStatementMapDetailModel);
            var result = sut.ValidateStatmentData(out bankStatementTransactionDetailModels);
            Assert.Empty(result);
            Assert.Equal(10, bankStatementTransactionDetailModels.Count);
        }

        [Fact]
        public void ValidateStatmentData_For_Lloyds_Bank_Statement_For_Errors()
        {
            List<BankStatementTransactionDetailModel> bankStatementTransactionDetailModels;
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data";
            var fileNameWithPath = string.Format("{0}\\{1}", directory, this.LloydsCSV);
            var bankStatmentParser = new BankStatementParser(fileNameWithPath);
            var bankStatmentMapper = new BankStatementMapper(this.bankStatementMapDetailSaveLloydsModelWithIncorrectDebitCredit);

            var sut = new BankStatementMapValidator(bankStatmentParser, bankStatmentMapper.BankStatementMapDetailModel);
            var result = sut.ValidateStatmentData(out bankStatementTransactionDetailModels);
            Assert.Single(result);
            Assert.Equal(10, bankStatementTransactionDetailModels.Count);
        }
    }
}
