using FileChecker;

namespace FileCheckTest
{
    [TestClass]
    public class Test_CheckFileSig
    {
        static string _baseDir = AppDomain.CurrentDomain.BaseDirectory;

        public static IEnumerable<string[]> GetInvalidFiles()
        {
            string path = Path.Combine(_baseDir, @"..\..\..\TestFiles\InvalidFiles");
            string[] files = Directory.GetFiles(path);


            foreach (string file in files)
            {
                yield return new string[] { file };
            }
        }

        public static IEnumerable<string[]> GetValidFiles()
        {
            string path = Path.Combine(_baseDir, @"..\..\..\TestFiles\ValidFiles");
            string[] files = Directory.GetFiles(path);


            foreach (string file in files)
            {
                yield return new string[] { file };
            }
        }

        /// <summary>
        /// Should only return true if the program CANNOT determine the file signature.
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidFiles), DynamicDataSourceType.Method)]
        public void CheckFileSig_NoMatchFound(string fp)
        {
            string res = Scanner.CheckFileSig(fp);

            Assert.IsTrue(res.Contains("no known file signature"));
        }


        /// <summary>
        /// Should only return true when the program CAN determine the file signature.
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(GetValidFiles), DynamicDataSourceType.Method)]
        public void CheckFileSig_MatchFound(string fp)
        {
            string res = Scanner.CheckFileSig(fp);

            Assert.IsFalse(res.Contains("no known file signature"));
        }

        [TestMethod]
        [DataRow('\\')]
        [DataRow('/')]
        public void CheckFileSig_WindowsOrUnixDelimiter_Succeeds(char d)
        {
            string path = Path.Combine(_baseDir, $"..{d}..{d}..{d}TestFiles{d}ValidFiles{d}testfile2.jpg");
            
            string res = Scanner.CheckFileSig(path);

            Assert.IsFalse(res.Contains("no known file signature"));
        }
    }
}