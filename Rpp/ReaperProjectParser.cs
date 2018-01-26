using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rpp
{
    public class ReaperProjectParser
    {

      

        public static ImmutableArray<ReaperProjectNode> ParseFromLines(LineSource lines)
        {
            var nodes = new List<ReaperProjectNode>();
            while (!lines.IsFinished())
            {
                var line = lines.AdvanceLine().Trim();
                if (line.StartsWith("<"))
                {
                    var header = line.Substring(1);
                    var subelements = ParseFromLines(lines);
                    nodes.Add( NodeFactory.CreateAppropriateProjectElementType(header,subelements));
                }
                else if (line.StartsWith(">"))
                {
                    break;
                }
                else 
                    nodes.Add(new ProjectLine(line));
            }

            return nodes.ToImmutableArray();
        }

        public static ProjectElement ParseProjectFromLines(
            LineSource lines)
        {
            var elements = ParseFromLines(lines);
            if (elements.Length != 2)
            {
                throw new ArgumentException();
            }

            var trailingLine = elements[1] as ProjectLine;
            if (trailingLine ==null)
                throw new ArgumentException();
            if (trailingLine.Line.Trim().Length!=0)
                throw new ArgumentException();
            return elements[0] as ProjectElement;
        }

        public static string Render(ReaperProjectNode node)
        {
            return Render(ImmutableArray.Create(node));
        }

        public static string Render(
            ImmutableArray<ReaperProjectNode> nodes)
        {
            var sb = new StringBuilder();
            foreach (var reaperProjectNode in nodes)
            {
                sb.Append(reaperProjectNode.AsString(0));
            }

            return sb.ToString();
        }
    }
}