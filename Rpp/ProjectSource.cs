using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rpp
{
    public class ProjectSource : ProjectElement
    {
        private readonly ProjectLine _lineElement = ProjectLine.Empty;
        public SourceType Source { get; private set; }
        public ProjectSource(string header,
            ImmutableArray<ReaperProjectNode> subelements) : base(
            header, subelements)
        {
            var headerTokens = header.Split();
            if (headerTokens.Contains("MIDI"))
            {
                Source = SourceType.Midi;
                return;
            }

            if (headerTokens.Contains("CLICK"))
            {
                Source = SourceType.Click;
                return;
            }


            Source = SourceType.File;
            _lineElement = subelements
                .OfType<ProjectLine>()
                .Single(p => p.Line.StartsWith("FILE"));

            var filedescription = _lineElement.Line;
            var m = Regex.Match(filedescription,
                @"FILE\s+\""([^\""]+)\""\s*(.*)");
            if (!m.Success)
                throw new NotImplementedException(
                    $"Couldn't parse '{filedescription}'");
            File = m.Groups[1]
                .Value;
            Trailing = m.Groups[2]
                .Value;
        }

        public string File { get; } = string.Empty;
        public string Trailing { get; } = string.Empty;

        public ProjectSource WithFile(string replacementfile)
        {
            if(Source!=SourceType.File)
                throw new InvalidOperationException();
            var pl =
                new ProjectLine(
                    $"FILE \"{replacementfile}\" {Trailing}");
            var newElements = Elements.Replace(_lineElement, pl);
            return new ProjectSource(Header, newElements);
        }

        public enum SourceType
        {
            File,
            Midi,
            Click,
            Unknown,
        }
    }
}