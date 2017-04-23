using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest.FormatSpecific {

	[TestClass]
	public class OpenXMLValidatorTest : Test {

		[TestMethod]
		public void TestCorruptedDOCXFile() {

			FileCreator.CreateValidFile("document.docx", ".docx");
			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_document.docx", ".docx");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}

		[TestMethod]
		public void TestCorruptedXLSXFile() {

			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			FileCreator.CreateValidFile("spreadsheet.xlsx", ".xlsx");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_spreadsheet.xlsx", ".xlsx");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}

		[TestMethod]
		public void TestCorruptedPPTXFile() {

			FileCreator.CreateValidFile("presentation.pptx", ".pptx");
			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_presentation.pptx", ".pptx");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}
	}
}
