using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChecker
{
    public class FileWrapper : IFileWrapper
    {
        public Stream OpenRead(string path)
        {
            return File.OpenRead(path);
        }
    }
}
