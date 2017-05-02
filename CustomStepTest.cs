using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest {

	[TestClass]
	public class CustomStepTest : Test {

		[TestMethod]
		public void TestSimpleCustomStep() {

			FileCreator.CreateFile(DefaultFileSize, ".txt", "testfile");
			Inspector = new Verifiler.Inspector();

			Result = Inspector.AddCustomStep(new StartsWithStep("test")).Scan(GetTestFolderPath());
			/* Custom step checks if all file's name start with 'test'. This should pass. */
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* Custom step checks if all file's name start with 'failtest'. This should fail. */
			Result = Inspector.RemoveAllCustomSteps().AddCustomStep(new StartsWithStep("failtest")).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Generic, Result.Code());
		}
	}

	class StartsWithStep : VerifilerCore.Step {

		private readonly string startString;

		public override int ErrorCode { get; set; } = VerifilerCore.Error.Generic;

		public StartsWithStep(string startString) {
			this.startString = startString;
			Enable();
		}

		public override void Run() {
			foreach (var file in GetListOfFiles()) {
				string name = Path.GetFileName(file);
				if (name.StartsWith(startString)) {
					ReportAsValid(file);
				} else {
					ReportAsError(file, name + " doesn't start with " + startString);
				}
			}
		}
	}
}
