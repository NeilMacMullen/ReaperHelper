using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rpp;

namespace RppTests
{
    [TestClass]
    public class RppParsingTests
    {
        [TestMethod]
        public void BasicElementIsRead()
        {
            var input = @"<element
>";
            var source = new LineSource(input);
            var elements = ReaperProjectParser.ParseFromLines(source);
            elements.Length.Should()
                .Be(1);
            var projectElement = elements.First() as ProjectElement;
            projectElement.Should()
                .NotBeNull();
            projectElement.Header.Should()
                .Be("element");
        }

        [TestMethod]
        public void SimpleProjectCanBeRoundTripped()
        {
            var input =
                @"<A
  line 1
  line 2
  <B header
    line B1
    line B2
  >
  line3
  line4
>
";
            var source = new LineSource(input);
            var project = ReaperProjectParser.ParseProjectFromLines(source);
            var rendered = ReaperProjectParser.Render(project);
            rendered.Should()
                .Be(input);
        }
    }
}