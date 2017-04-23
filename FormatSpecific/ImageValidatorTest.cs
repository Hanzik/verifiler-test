using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest.FormatSpecific {

	[TestClass]
	public class ImageValidatorTest : Test {

		[TestMethod]
		public void TestCorruptedJPGFile() {

			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			FileCreator.CreateValidFile("image.jpg", ".jpg");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_image.jpg", ".jpg");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}

		[TestMethod]
		public void TestCorruptedPNGFile() {

			Inspector = new Verifiler.Inspector();

			/* A real valid PDF file is scanned. */
			FileCreator.CreateValidFile("image.png", ".png");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* A corrupted PDF file is added to the scanned folder. */
			FileCreator.CreateCorruptedFile("corrupted_image.png", ".png");
			Result = Inspector.EnableFormatVerification().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Corrupted, Result.Code());
		}
	}
}
