using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest {

	[TestClass]
	public class ExtensionTest : Test {

		[TestMethod]
		public void TestNoRestrictions() {

			FileCreator.CreateFile(DefaultFileSize, ".txt");
			Inspector = new Verifiler.Inspector();

			/* No extension restriction is set. */
			Result = Inspector.Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		[TestMethod]
		public void TestAddingRestrictions() {

			FileCreator.CreateFile(DefaultFileSize, ".txt");
			FileCreator.CreateFile(DefaultFileSize, ".html");
			FileCreator.CreateFile(DefaultFileSize, ".jpg");
			Inspector = new Verifiler.Inspector();

			/* Only txt files are allowed, doc and jpg become forbidden. */
			Result = Inspector.AddExtensionRestriction(".txt").Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Extension, Result.Code());

			/* Add doc and jpg to allowed types and expect OK response. */
			string[] moreAllowedTypes = { ".html", ".jpg"};
			Result = Inspector.AddExtensionRestrictions(moreAllowedTypes).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		[TestMethod]
		public void TestRemovingRestrictions() {

			FileCreator.CreateFile(DefaultFileSize, ".txt");
			FileCreator.CreateFile(DefaultFileSize, ".html");
			FileCreator.CreateFile(DefaultFileSize, ".jpg");
			Inspector = new Verifiler.Inspector();

			/* All four allowedTypes are allowed. */
			string[] allowedTypes = { ".txt", ".html", ".jpg", ".png" };
			Result = Inspector.AddExtensionRestrictions(allowedTypes).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* Remove png restriction, expect OK result as there are no png files in test folder. */
			Result = Inspector.RemoveExtensionRestriction(".png").Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* Remove jpg restriction, expect Error as there is jpg file in test folder. */
			Result = Inspector.RemoveExtensionRestriction(".jpg").Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Extension, Result.Code());

			/* Remove all extension restrictions and expect OK result. */
			Result = Inspector.RemoveExtensionRestrictions(allowedTypes).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		[TestMethod]
		public void TestMultipleInputTypes() {

			FileCreator.CreateFile(DefaultFileSize, ".jpg");

			/* Extensions should work both with and without dot at the beggining of the string. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.AddExtensionRestriction(".txt").Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Extension, Result.Code());
			
			Inspector = new Verifiler.Inspector();
			Result = Inspector.AddExtensionRestriction("txt").Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Extension, Result.Code());
			
			Inspector = new Verifiler.Inspector();
			Result = Inspector.AddExtensionRestriction(".jpg").Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
			
			Inspector = new Verifiler.Inspector();
			Result = Inspector.AddExtensionRestriction("jpg").Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}
	}
}
