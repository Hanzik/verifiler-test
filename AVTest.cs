using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest {

	[TestClass]
	public class AVTest : Test {

		/* Individual to everyone's own AV engine. */ 
		private string AVLocation = "\"C:\\Program Files\\AVAST Software\\Avast\\ashCmd.exe\"";

		[TestMethod]
		public void TestEmptyFolder() {

			Inspector = new Verifiler.Inspector();
			string AVParams = GetTestFolderPath() + " /_";
			Result = Inspector.EnableAV(AVLocation, AVParams).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		[TestMethod]
		public void TestCleanFiles() {

			FileCreator.CreateFile(DefaultFileSize, ".txt");
			Inspector = new Verifiler.Inspector();
			string AVParams = GetTestFolderPath() + " /_";
			Result = Inspector.EnableAV(AVLocation, AVParams).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		[TestMethod]
		public void TestInfectedFiles() {
			
			FileCreator.CreateFile(DefaultFileSize, ".txt");
			FileCreator.CreateEicarFile();
			Inspector = new Verifiler.Inspector();
			string AVParams = GetTestFolderPath() + " /_";
			Result = Inspector.EnableAV(AVLocation, AVParams).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Fatal, Result.Code());
		}

		[TestMethod]
		public void TestInvalidAVLocation() {

		}
	}
}
