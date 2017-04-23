using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest {

	[TestClass]
	public class SizeTests : Test {

		[TestMethod]
		public void TestMinSize() {
			
			var testFile = FileCreator.CreateFile(DefaultFileSize, ".txt");

			/* MinSize requirement is met. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.MinSize(DefaultFileSize - 1).Scan(testFile);
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* MinSize requirement is not met - file is smaller. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.MinSize(DefaultFileSize + 1).Scan(testFile);
			Assert.AreEqual(VerifilerCore.Error.Size, Result.Code());
		}

		[TestMethod]
		public void TestMaxSize() {
			
			var testFile = FileCreator.CreateFile(DefaultFileSize, ".txt");

			/* MaxSize requirement is met. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.MaxSize(DefaultFileSize + 1).Scan(testFile);
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* MaxSize requirement is not met - file is bigger. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.MaxSize(DefaultFileSize - 1).Scan(testFile);
			Assert.AreEqual(VerifilerCore.Error.Size, Result.Code());
		}

		[TestMethod]
		public void TestBothSizeConstraints() {
		
			var testFile = FileCreator.CreateFile(DefaultFileSize, ".txt");

			/* Both requirements are met. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.MaxSize(DefaultFileSize + 1).MinSize(DefaultFileSize - 1).Scan(testFile);
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* Both requirements are met - both constraints are equal to test file size. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.MaxSize(DefaultFileSize).MinSize(DefaultFileSize).Scan(testFile);
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* One requirements is not met - MaxSize is broken. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.MaxSize(DefaultFileSize - 1).MinSize(DefaultFileSize).Scan(testFile);
			Assert.AreEqual(VerifilerCore.Error.Size, Result.Code());

			/* One requirements is not met - MinSize is broken. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.MaxSize(DefaultFileSize - 1).MinSize(DefaultFileSize).Scan(testFile);
			Assert.AreEqual(VerifilerCore.Error.Size, Result.Code());

			/* Both requirements are not met. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector.MaxSize(DefaultFileSize - 1).MinSize(DefaultFileSize + 1).Scan(testFile);
			Assert.AreEqual(VerifilerCore.Error.Size, Result.Code());
		}
	}
}
