using System;
using System.IO;
using NLog;

namespace VerifilerTest.Toolbox {

	public class TestFileCreator {

		private static readonly string TempFolderPath = Path.GetTempPath();
		private static string testFolder = Path.Combine(TempFolderPath, Guid.NewGuid().ToString());

		public static int DefaultFileNameLength = 16;

		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public void PrepareDirectory() {
			testFolder = Path.Combine(TempFolderPath, Guid.NewGuid().ToString());
			
			logger.Info("Creating directory " + testFolder);
			Directory.CreateDirectory(testFolder);
		}

		public void Cleanup() {
			DeleteTestDirectory();
		}

		private void DeleteTestDirectory() {
			if (Directory.Exists(testFolder)) {
				logger.Info("Deleting directory " + testFolder);
				try {
					Directory.Delete(testFolder, true);
				} catch (IOException e) {
					// log this
				}
			}
		}

		public string CreateFile(int kilobytes) {
			return CreateFile(kilobytes, null);
		}

		public string CreateFile(int kilobytes, string extension) {
			return CreateFile(kilobytes, extension, null);
		}

		public string CreateFile(int kilobytes, string extension, string name) {

			if (extension == null) {
				extension = "";
			}

			if (name == null) {
				name = Guid.NewGuid().ToString().Substring(0, DefaultFileNameLength);
			}

			var filePath = testFolder + Path.DirectorySeparatorChar + name + extension;
			
			if (File.Exists(filePath)) {
				logger.Debug("Deleting file " + filePath);
				File.Delete(filePath);
			}

			logger.Debug("Creating file " + filePath);
			File.WriteAllBytes(filePath, new byte[kilobytes * 1024]);

			return filePath;
		}

		public string CreateEicarFile() {

			string path = testFolder + Path.DirectorySeparatorChar + Guid.NewGuid().ToString().Substring(0, DefaultFileNameLength);
			using (StreamWriter sw = File.CreateText(path)) {
				sw.WriteLine("X5O!P%@AP[4\\PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-FILE!$H+H*");
			}

			return path;
		}

		public string CreateValidFile(string filename, string templateExtension) {
			return CopyFromFolder("valid", filename, templateExtension);
		}

		public string CreateCorruptedFile(string filename, string templateExtension) {
			return CopyFromFolder("corrupted", filename, templateExtension);
		}

		public string CopyFromFolder(string folder, string filename, string extension) {
			extension = extension.TrimStart('.');
			var originalFileName = extension + "." + extension;
			var copiedFileName = testFolder + Path.DirectorySeparatorChar + filename;
			File.Copy(@"Resources\templates\" + folder + '\\' + originalFileName, copiedFileName);
			return copiedFileName;
		}

		public static string GetTestFolderPath() {
			return testFolder;
		}
	}
}
