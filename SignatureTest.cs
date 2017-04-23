using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest {

	[TestClass]
	public class SignatureTest : Test {

		[TestMethod]
		public void TestSignatureOfValidFile() {

			FileCreator.CreateValidFile("image.jpg", ".jpg");
			Inspector = new Verifiler.Inspector();

			/* Image is in fact an image, so the test should pass. */
			Result = Inspector.EnableSignatureTest().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		[TestMethod]
		public void TestSignatureOfInvalidFile() {

			FileCreator.CreateValidFile("notimage.jpg", ".txt");
			Inspector = new Verifiler.Inspector();

			/* Text file pretends it's an image, so the test should fail. */
			Result = Inspector.EnableSignatureTest().Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Signature, Result.Code());
		}
	}
}
