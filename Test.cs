using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using VerifilerCore;
using VerifilerTest.Toolbox;

namespace VerifilerTest {

	public class Test {

		protected TestFileCreator FileCreator = new TestFileCreator();
		protected Verifiler.Inspector Inspector;
		protected Result Result;

		private static Logger logger = LogManager.GetCurrentClassLogger();

		protected const int DefaultFileSize = 100;

		[TestInitialize]
		public void BeforeTest() {
			FileCreator.PrepareDirectory();
		}

		[TestCleanup]
		public void AfterTest() {
			FileCreator.Cleanup();
		}

		public string GetTestFolderPath() {
			return TestFileCreator.GetTestFolderPath();
		}
	}
}
