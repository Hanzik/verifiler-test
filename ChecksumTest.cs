using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifilerTest {

	[TestClass]
	public class ChecksumTest : Test {

		[TestMethod]
		public void TestAddChecksumRestriction() {

			var file1 = FileCreator.CreateValidFile("image1.jpg", ".jpg");
			var file2 = FileCreator.CreateValidFile("image2.png", ".png");
			var file3 = FileCreator.CreateValidFile("text.txt", ".txt");
			Inspector = new Verifiler.Inspector();

			/* Only the checksum of first file was placed on the whitelist and so the test should fail. */
			Result = Inspector.AddAllowedChecksum(GetMD5FromFile(file1)).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Checksum, Result.Code());

			/* All file's checksums are on the whitelist, the test should pass. */
			string[] checksums = {GetMD5FromFile(file2), GetMD5FromFile(file3)};
			Result = Inspector.AddAllowedChecksums(checksums).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		[TestMethod]
		public void TestRemoveChecksumRestriction() {

			var file1 = FileCreator.CreateValidFile("image1.jpg", ".jpg");
			var file2 = FileCreator.CreateValidFile("image2.png", ".png");
			var file3 = FileCreator.CreateValidFile("text.txt", ".txt");
			Inspector = new Verifiler.Inspector();

			/* All file's checksums are on the whitelist, the test should pass. */
			string[] checksums = { GetMD5FromFile(file1), GetMD5FromFile(file2), GetMD5FromFile(file3) };
			Result = Inspector.AddAllowedChecksums(checksums).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());

			/* We remove one checksum from the whitelist, the test should now fail. */
			Result = Inspector.RemoveAllowedChecksum(checksums[0]).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Error.Checksum, Result.Code());

			/* We remove all checksums from the whitelist, the test should now pass as this step
			   is skipped. */
			Result = Inspector.RemoveAllowedChecksums(checksums).Scan(GetTestFolderPath());
			Assert.AreEqual(VerifilerCore.Result.Ok, Result.Code());
		}

		private string GetMD5FromFile(string file) {
			using (var md5 = MD5.Create()) {
				using (var stream = File.OpenRead(file)) {
					return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "‌​").ToLower();
				}
			}
		}
	}
}
