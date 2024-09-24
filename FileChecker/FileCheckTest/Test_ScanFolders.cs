using FileChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheckTest
{
    [TestClass]
    public class Test_ScanFolders
    {
        static string _baseDir = AppDomain.CurrentDomain.BaseDirectory;
        [TestMethod]
        public void ScanFolders_NestedFolders_FilesFound()
        {
            string startFolder = Path.Combine(_baseDir, @"..\..\..\TestFolders");
            Scanner scanner = new Scanner(new FileWrapper());

            List<string> scannedFiles = scanner.ScanFolders(startFolder);
            Assert.IsTrue(scannedFiles.Contains("testfile2.jpg's signature matches JPG"));
        }
    }
}
