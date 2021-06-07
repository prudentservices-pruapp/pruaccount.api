namespace pruaccount.api.test.Domain
{
    using Pruaccount.Api.Domain.BankStatement;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;

    public class BankStatementParserTests
    {
        private readonly string LloydsCSV = "Lloyds_01082020_31102020.csv";
        private readonly string CaterAllenCSV = "CaterAllen_01082020_31102020.csv";

        public BankStatementParserTests(ITestOutputHelper output)
        {
        }

        [Fact]
        public void Get_Rows_For_Lloyds_Bank_Statement()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data";
            var fileNameWithPath = string.Format("{0}\\{1}", directory, this.LloydsCSV);

            var sut = new BankStatementParser(fileNameWithPath);

            var result = sut.GeRowsJson();

            Assert.Equal(8, result.NoOfColumnsInCSV);
            Assert.Equal(13, result.TotalNoOfRowsInCSV);
            Assert.Equal(10, result.BankStatementCSVDataList.Count);
            Assert.NotNull(result.Json);
        }

        [Fact]
        public void Get_Rows_For_CaterAllen_Bank_Statement()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data";
            var fileNameWithPath = string.Format("{0}\\{1}", directory, this.CaterAllenCSV);

            var sut = new BankStatementParser(fileNameWithPath);

            var result = sut.GeRowsJson();

            Assert.Equal(5, result.NoOfColumnsInCSV);
            Assert.Equal(41, result.TotalNoOfRowsInCSV);
            Assert.Equal(10, result.BankStatementCSVDataList.Count);
            Assert.NotNull(result.Json);
        }
    }
}
