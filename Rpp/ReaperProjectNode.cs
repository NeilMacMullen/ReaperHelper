using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rpp
{
    public abstract class ReaperProjectNode
    {
        public abstract string AsString(int indent);

        protected string IndentString(int indent)
        {
            return "".PadRight(indent * 2);
        }

        public abstract ReaperProjectNode Replace(ReaperProjectNode target,
            ReaperProjectNode replacement);

        public abstract void WalkTree(Action<ReaperProjectNode> actor);
    }
}
