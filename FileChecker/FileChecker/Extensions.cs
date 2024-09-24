using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChecker
{
    public static class Extensions
    {
        public static List<string> SplitExt(this String str, char sep1, char sep2)
        {
            List<string> splitStr = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == sep1 || str[i] == sep2)
                {
                    splitStr.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                else
                {
                    stringBuilder.Append(str[i]);
                }
            }
            splitStr.Add(stringBuilder.ToString());
            return splitStr;
        }
    }
}
