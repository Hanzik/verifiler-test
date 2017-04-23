using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest.FormatSpecific {

	[TestClass]
	public class LegacyMSOfficeValidatorTest : Test {

		[TestMethod]
		public void TestCorruptedXLSFile() {

			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			FileCreator.CreateValidFile("spreadsheet.xls", ".xls");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_spreadsheet.xls", ".xls");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}
	}
}
