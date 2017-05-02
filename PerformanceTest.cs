using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest {

	[TestClass]
	public class PerformanceTest : Test {
		
		[TestMethod]
		public void TestPerformanceStandard10() {
			testFiles(10);
		}
		[TestMethod]
		public void TestPerformanceStandard100() {
			testFiles(100);
		}
		[TestMethod]
		public void TestPerformanceStandard1000() {
			testFiles(1000);
		}
		[TestMethod]
		public void TestPerformanceStandard10000() {
			testFiles(10000);
		}
		[TestMethod]
		public void TestPerformanceStandard50000() {
			testFiles(50000);
		}
		[TestMethod]
		public void TestPerformanceStandard100000() {
			testFiles(100000);
		}

		private void testFiles(int n) {
			for (var i = 0; i < n; i++) {
				FileCreator.CreateValidFile("test-" + i, ".txt");
				/*
				var mod = i % 4;
				if (mod == 0) {
					FileCreator.CreateValidFile("test-" + i, ".odt");
				} else if (mod == 1) {
					FileCreator.CreateValidFile("test-" + i, ".pdf");
				} else if (mod == 2) {
					FileCreator.CreateValidFile("test-" + i, ".jpg");
				} else if (mod == 3) {
					FileCreator.CreateValidFile("test-" + i, ".xlsx");
				}*/
			}

			/* Preheat */
			Inspector = new Verifiler.Inspector();
			Result = Inspector
				.MinSize(1)
				.MaxSize(2000) 
				.AddExtensionRestriction(".txt")
				.AddExtensionRestriction(".jpg")
				.AddExtensionRestriction(".pdf")
				.EnableSignatureTest()
				.Scan(GetTestFolderPath());

			var sw = new Stopwatch();
			sw.Start();

			/* MinSize requirement is met. */
			Inspector = new Verifiler.Inspector();
			Result = Inspector
				.MinSize(1)
				.MaxSize(2000)
				.AddExtensionRestriction(".txt")
				.AddExtensionRestriction(".jpg")
				.AddExtensionRestriction(".pdf")
				.EnableSignatureTest()
				.Scan(GetTestFolderPath());

			sw.Stop();
			Console.WriteLine("Time elapsed for " + n + " files: " + sw.ElapsedMilliseconds + "ms");
		}
	}
}
