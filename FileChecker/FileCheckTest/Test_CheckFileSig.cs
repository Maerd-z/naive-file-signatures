using FileChecker;
using Moq;
using System.Diagnostics;

namespace FileCheckTest
{
    [TestClass]
    public class Test_CheckFileSig
    {
        public static IEnumerable<object[]> GetKnownSignatures
        {
            get
            {
                return new[]
                {
                    new object[] { new byte[] { 0xFF, 0xD8, 0xFF } },
                    new object[] { new byte[] { 0x50, 0x4B, 0x05, 0x06 } },
                    new object[] { new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00 } },
                };
            }
        }

        public static IEnumerable<object[]> GetUnknownSignatures
        {
            get
            {
                return new[]
                {
                    new object[] {new byte[] { 0x52, 0x49, 0x46, 0x46, 0xDA, 0x0F, 0x57, 0x45, 0x42, 0x50 } },
                    new object[] {new byte[] { 0x54, 0x68, 0x69, 0x73 } },
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(GetUnknownSignatures), DynamicDataSourceType.Property)]
        public void CheckFileSig_UnknownSignature_NoMatchFound(byte[] bytes)
        {
            var mockFileWrapper = new Mock<IFileWrapper>();
            string fp = "";

            mockFileWrapper.Setup(fw => fw.OpenRead(fp)).Returns(new MemoryStream(bytes));
            Scanner scanner = new Scanner(mockFileWrapper.Object);

            string res = scanner.CheckFileSig(fp);

            Assert.IsTrue(res.Contains("no known file signature"));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetKnownSignatures), DynamicDataSourceType.Property)]
        public void CheckFileSig_KnownSignature_MatchFound(byte[] bytes)
        {
            string fp = "";
            var mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(fw => fw.OpenRead(fp)).Returns(new MemoryStream(bytes));

            Scanner scanner = new Scanner(mockFileWrapper.Object);
            string res = scanner.CheckFileSig(fp);

            Assert.IsFalse(res.Contains("no known file signature"));
        }
        
        [TestMethod]
        [DataRow('\\')]
        [DataRow('/')]
        public void CheckFileSig_WindowsOrUnixDelimiter_Succeeds(char d)
        {
            string _baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(_baseDir, $"..{d}..{d}..{d}TestFiles{d}ValidFiles{d}testfile2.jpg");
            Scanner scanner = new Scanner(new FileWrapper());
            
            string res = scanner.CheckFileSig(path);

            Assert.IsFalse(res.Contains("no known file signature"));
        }
    }
}