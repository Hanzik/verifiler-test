using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest {

	[TestClass]
	public class InputTests : Test {
		
		[TestMethod]
		public void TestPathFormat() {
			
			var testFile = FileCreator.CreateFile(DefaultFileSize, ".txt");
			Inspector = new Verifiler.Inspector();

			/* No path is given. */
			Result = Inspector.Scan("");
			Assert.AreEqual(VerifilerCore.Error.ScanPathInvalid, Result.Code());

			/* Broken string is given. */
			Result = Inspector.Scan("lorem ipsum");
			Assert.AreEqual(VerifilerCore.Error.ScanPathInvalid, Result.Code());
			Result = Inspector.Scan(",.-ů§¨ú)0%´=_");
			Assert.AreEqual(VerifilerCore.Error.ScanPathInvalid, Result.Code());
			Result = Inspector.Scan("&/%\\.exe");
			Assert.AreEqual(VerifilerCore.Error.ScanPathInvalid, Result.Code());

			/* Valid path is given. */
			Result = Inspector.Scan(testFile);
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		[TestMethod]
		public void TestPermissions() {

			//TODO
			
			var testFile = FileCreator.CreateFile(DefaultFileSize, ".txt");

			/* Both requirements are met. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.Scan(testFile);
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		[TestMethod]
		public void TestFilesExistance() {
			
			var testFile = FileCreator.CreateFile(DefaultFileSize, ".txt");

			/* File exists. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.Scan(testFile);
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* Delete the file and run the same test again. */
			System.IO.File.Delete(testFile);

			/* File does not exist. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.Scan(testFile);
			Assert.AreEqual(VerifilerCore.Error.ScanPathInvalid, Result.Code());
		}
	}
}
