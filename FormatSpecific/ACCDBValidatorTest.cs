using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest.FormatSpecific {

	[TestClass]
	public class ACCDBValidatorTest : Test {

		[TestMethod]
		public void TestCorruptedACCDBFile() {

			FileCreator.CreateValidFile("database.accdb", ".accdb");
			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
			
			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_database.accdb", ".accdb");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}
	}
}
