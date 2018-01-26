using System;
using System.Collections.Immutable;

namespace Rpp
{
    public class LineSource
    {
        private readonly ImmutableArray<string> _lines;
        private int _currentLine;
        public string AdvanceLine()
        {
            return _lines[_currentLine++];
        }

        public bool IsFinished()
        {
            return _currentLine >= _lines.Length;
        }

        public LineSource(string str)
        {
            if (str.Length==0)
                _lines = ImmutableArray<string>.Empty;
            else 
            _lines= str
                .Replace("\r\n", "\r")
                .Replace("\n", "\r")
                .Split('\r')
                .ToImmutableArray();
        }

        public int TotalLines => _lines.Length;
        public int RemainingLines => TotalLines - _currentLine;
    }
}