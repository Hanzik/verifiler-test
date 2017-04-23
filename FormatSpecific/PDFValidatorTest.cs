using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest.FormatSpecific {

	[TestClass]
	public class PDFValidatorTest : Test {

		[TestMethod]
		public void TestCorruptedPDFFile() {

			FileCreator.CreateValidFile("document.pdf", ".pdf");
			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_document.pdf", ".pdf");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}
	}
}
