using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest {

	[TestClass]
	public class VirusTotalTest : Test {

		private const string TestApiKey = "3e53db1820ed376d4f5ba77c54535aaf92608464ea6b1e7ecfd0475be164d2b5";

		[TestMethod]
		public void TestVirusTotal() {

			FileCreator.CreateValidFile("textfile.txt", ".txt");
			Inspector = new Verifiler.Inspector();
			/* File is not flagged by VirusTotal. */
			Result = Inspector.EnableVirusTotal(TestApiKey).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
			FileCreator.CreateEicarFile();
			Inspector = new Verifiler.Inspector();
			/* File is flagged by VirusTotal - EICAR is a sample file flagged by most AV engines. */
			Result = Inspector.EnableVirusTotal(TestApiKey).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Fatal, Result.Code());
		}
	}
}
