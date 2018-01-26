using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Rpp
{
    public class ProjectElement : ReaperProjectNode
    {
        public ImmutableArray<ReaperProjectNode> Elements = ImmutableArray<ReaperProjectNode>.Empty;

        public ProjectElement(string header, ImmutableArray<ReaperProjectNode> subelements)
        {
            Header = header;
            Elements = subelements;
            HeaderType = Header.Split()[0];
        }

        public string HeaderType { get; private set; }
        public string Header { get; private set; }
        public override string AsString(int indent)
        {
            var sb = new StringBuilder();
            var indentSpace =IndentString(indent);
            sb.AppendLine(indentSpace+"<" + Header);
            foreach (var el in Elements)
                sb.Append(el.AsString(indent + 1));
            sb.AppendLine(indentSpace + ">");
            return sb.ToString();
        }

        public override ReaperProjectNode Replace(ReaperProjectNode target,
            ReaperProjectNode replacement)
        {
            if (this == target) return replacement;
            var newElements =
                Elements.Select(e => e.Replace(target, replacement)).ToImmutableArray();
            if (newElements.SequenceEqual(Elements))
                return this;
            return NodeFactory.CreateAppropriateProjectElementType(
                Header, newElements);
        }

        public override void WalkTree(Action<ReaperProjectNode> actor)
        {
            actor(this);
            foreach (var e in Elements)
                e.WalkTree(actor);
        }

        public ImmutableArray<ReaperProjectNode>
            SelfAndDescendants()
        {
            var list = new List<ReaperProjectNode>();
                WalkTree(n => list.Add(n));
            return list.ToImmutableArray();
        }

        public ImmutableArray<ReaperProjectNode>
            ElementsAndDescendants()
        {
            return SelfAndDescendants()
                .Skip(1)
                .ToImmutableArray();
        }
    }
}