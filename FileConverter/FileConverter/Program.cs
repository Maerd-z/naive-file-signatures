using Microsoft.VisualBasic;
using System.Text;

namespace FileChecker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inp = MainMenu();

            while (inp != "0")
            {
                switch (inp)
                {
                    case "0":
                        Environment.Exit(0);
                        break;


                    case "1":
                        Console.Clear();
                        Console.Write("Paste or type the path to the file...");
                        string res = Scanner.CheckFileSig(Console.ReadLine());

                        Console.WriteLine("\n" + res);
                        Console.ReadKey();
                        inp = MainMenu();
                        break;


                    case "2":
                        Console.Clear();
                        Console.WriteLine("Choose a directory to act as the root of the search.");
                        string dir = Console.ReadLine();

                        List<string> scannedFiles = Scanner.ScanFolders(dir);
                        foreach (string file in scannedFiles)
                        {
                            Console.WriteLine(file);
                        }

                        Console.ReadKey();
                        inp = MainMenu();
                        break;
                }
            }
        }

        public static string MainMenu()
        {
            Console.Clear();
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("0: Exit the application.");
            Console.WriteLine("1: Check file signature.");
            Console.WriteLine("2: Recursively scan folders.");

            return Console.ReadLine();
        }
    }
}