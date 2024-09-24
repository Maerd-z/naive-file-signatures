using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChecker
{
    public interface IFileWrapper
    {
        Stream OpenRead(string path);
    }
}
