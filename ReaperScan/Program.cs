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

                var enclosingFolder =
                    Path.GetFileName(Path.GetDirectoryName(r));
                var projName = Path.GetFileName(r);


                var txt = File.ReadAllText(r);
                var source = new LineSource(txt);
                var project = ReaperProjectParser.ParseProjectFromLines(source);
                var sources = project.ElementsAndDescendants()
                    .OfType<ProjectSource>(
                    )
                    .Where(src =>
                        src.Source == ProjectSource.SourceType.File)
                    .ToArray();
                foreach (var projectSource in sources)
                {
                    Console.WriteLine($"   src:{projectSource.File}");
                    if (projectSource.File.Contains("Neil"))
                    {
                        var fileName =
                            Path.GetFileName(projectSource.File);
                        var newSource =
                            projectSource.WithFile(fileName);
                        project = (ProjectElement) project.Replace(projectSource,
                            newSource);
                    }
                }

                var destFolder =
                    Path.Combine(args[1], enclosingFolder);
                var destFile = Path.Combine(destFolder, projName);
                Console.WriteLine($"creating {destFolder} for {destFile}");
                Directory.CreateDirectory(destFolder);
                File.WriteAllText(destFile,project.AsString(0));


            }
        }
    }
}
