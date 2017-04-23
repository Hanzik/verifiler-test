using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest.FormatSpecific {

	[TestClass]
	public class OpenOfficeValidatorTest : Test {

		[TestMethod]
		public void TestCorruptedODTFile() {

			FileCreator.CreateValidFile("document.odt", ".odt");
			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_document.odt", ".odt");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}

		[TestMethod]
		public void TestCorruptedODSFile() {

			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			FileCreator.CreateValidFile("spreadsheet.ods", ".ods");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_spreadsheet.ods", ".ods");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}
	}
}
