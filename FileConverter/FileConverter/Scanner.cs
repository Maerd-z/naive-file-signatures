using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace FileChecker
{
    public static class Scanner
    {

        public static List<string> ScanFolders(string currDir, List<string>? scannedDirs = null, List<string>? scannedFiles = null)
        {
            if (scannedDirs == null)
            {
                scannedDirs = new List<string>();
            }

            if (scannedFiles == null)
            {
                scannedFiles = new List<string>();
            }

            if(currDir != "" && currDir != null && !scannedDirs.Contains(currDir))
            {
                scannedDirs.Add(currDir);
            }

            if (FileSystem.DirectoryExists(currDir))
            {
                // Proceed with scanning each file.
                IReadOnlyCollection<string> currFiles = FileSystem.GetFiles(currDir);
                foreach (string currFile in currFiles) 
                {
                    scannedFiles.Add(CheckFileSig(currFile));
                }

                // Get available directories.
                IReadOnlyCollection<string> ithDirs = FileSystem.GetDirectories(currDir);

                // Finding additional directories.
                if (ithDirs.Count > 0)
                {
                    // Continue directory traversal.
                    foreach (string ithDir in ithDirs)
                    {
                        ScanFolders(ithDir, scannedDirs, scannedFiles);
                    }
                }
                return scannedFiles; 
            }
            return scannedFiles; 
        }

        public static string CheckFileSig(string pathToFile)
        {
            Dictionary<string, string> fileSigs = new Dictionary<string, string>()
            {
                {"PDF", "255044462D" },
                {"ZIP/DOCX1", "504B0304" },
                {"ZIP/DOCX2", "504B0506" },
                {"ZIP/DOCX3", "504B0708" },
                {"PNG", "89504E470D0A1A0A" },
                {"JPG", "FFD8FF" },
                {"ELF", "7F454C46" },
                {"RAR > v1.5", "526172211A0700" },
                {"RAR > v5.0", "526172211A070100" },
                {"UTF-8", "EFBBBF" },
                {"UTF-16LE" , "FFFE" },
                {"UTF-16BE", "FEFF" },
                {"UTF-32LE", "FFFE0000" },
                {"UTF-32BE", "0000FEFF" }
            };

            char primSep = Path.DirectorySeparatorChar;
            char altSep = Path.AltDirectorySeparatorChar;

            StringBuilder fileSig = new StringBuilder();

            byte[] rawBytes = new byte[32];

            using (FileStream fs = File.OpenRead(pathToFile))
            {
                fs.Read(rawBytes, 0, rawBytes.Length);
            }

            string hexBytes = Convert.ToHexString(rawBytes);

            for (int i = 0; i < fileSigs.Count; i++)
            {
                for (int j = 0; j < fileSigs.ElementAt(i).Value.Length; j++)
                {
                    fileSig.Append(hexBytes[j]);
                }

                string result = IsCorrectSig(fileSigs.ElementAt(i).Value, fileSig.ToString(), fileSigs.ElementAt(i).Key);

                if (result.Contains("does not"))
                {
                    fileSig.Clear();
                    continue; // Doesn't match currently checked signature.
                }
                else
                {
                    return $"{pathToFile.SplitExt(primSep, altSep)[^1]}'s signature matches {fileSigs.ElementAt(i).Key}";
                }
            }
            return $"{pathToFile.SplitExt(primSep, altSep)[^1]}'s signature matches no known file signature."; // No known signatures matched.
        }
        static string IsCorrectSig(string sig, string fileSig, string format) => sig == fileSig ? $"Signature matches: {format}" : $"Signature does not match: {sig}";
    }
}
