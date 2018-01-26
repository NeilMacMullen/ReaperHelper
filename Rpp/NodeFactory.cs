using System.Collections.Immutable;

namespace Rpp
{
    public class NodeFactory
    {
        public static ProjectElement
            CreateAppropriateProjectElementType(string header,
                ImmutableArray<ReaperProjectNode> elements)
        {
            var typeToken = header.Split()[0];
            switch (typeToken)
            {
                case "SOURCE": return new ProjectSource(header, elements);
                default: return new ProjectElement(header, elements);
            }
        }
    }
}