using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rpp
{
    public class ProjectSource : ProjectElement
    {
        public ProjectSource(string header, ImmutableArray<ReaperProjectNode> subelements) : base(header, subelements)
        {
            if (subelements.Length!=1) 
                throw new NotImplementedException($"Can't cope with SOURCE {string.Join(Environment.NewLine,subelements.Select(e=>e.AsString(0)).ToArray())}");
            var file = subelements
                .OfType<ProjectLine>()
                .Single(p => p.Line.StartsWith("FILE"))
                .Line;
           var m = Regex.Match(file,@"FILE\s+\""([^\""]+)\""\s*(.*)");
            if (!m.Success)
                throw  new NotImplementedException($"Couldn't parse '{file}'");
            File = m.Groups[1].Value;
            Trailing = m.Groups[2].Value;
        }

        public string File { get; }
        public string Trailing { get; }

        public ProjectSource WithFile(string replacementfile)
        {
            var newLine = $"FILE \"{replacementfile}\" {Trailing}";
            var pl = new ProjectLine(newLine);
           return  new ProjectSource(Header, ImmutableArray.Create((ReaperProjectNode) pl));
        }
    }
}