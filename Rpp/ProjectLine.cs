using System;

namespace Rpp
{
    public class ProjectLine : ReaperProjectNode
    {
        public string Line { get; private set; }

        public ProjectLine(string line)
        {
            Line = line;
        }

    

        public override string AsString(int indent)
        {
            return IndentString(indent) + Line + Environment.NewLine;
        }

        public override ReaperProjectNode Replace(ReaperProjectNode target,
            ReaperProjectNode replacement)
        {
            if (this == target)
                return replacement;
            return this;
        }

        public override void WalkTree(Action<ReaperProjectNode> actor)
        {
            actor(this);
        }

        public static ProjectLine Empty { get; } = new ProjectLine(string.Empty);
    }
}