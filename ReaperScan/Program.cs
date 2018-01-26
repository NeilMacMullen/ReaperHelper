using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpp;

namespace ReaperScan
{
    class Program
    {
        static void Main(string[] args)
        {
            var rppFiles = Directory.EnumerateFiles(args[0], "*.rpp",
                SearchOption.AllDirectories);
            foreach (var r in rppFiles)
            {
                Console.WriteLine($"Trying to read {r}");
                var txt = File.ReadAllText(r);
                var source = new LineSource(txt);
                ReaperProjectParser.ParseProjectFromLines(source);
            }
        }
    }
}
