using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rpp;

namespace RppTests
{
    [TestClass]
    public class LineSourceTests
    {
        [TestMethod]
        public void LineSourceSplitsCorrectly()
        {
            var input = "line1\rline2";
            var source = new LineSource(input);
            source.TotalLines.Should()
                .Be(2);
        }

        [TestMethod]
        public void LineSourceIsInsensitiveToLineEndingType()
        {
            new LineSource("a\rb\nc").TotalLines.Should().Be(3);
            new LineSource("a \r\n b \n c \r d").TotalLines.Should().Be(4);

        }

        [TestMethod]
        public void EmptyLineSourceIsEmpty()
        {
            var source = new LineSource(String.Empty);
            source.IsFinished()
                .Should()
                .BeTrue();
        }


        [TestMethod]
        public void LineSourceCorrectlyAdvances()
        {
            var source =new LineSource("a\rb");
            source.IsFinished()
                .Should()
                .BeFalse();
            source.AdvanceLine()
                .Should()
                .Be("a");
            source.IsFinished()
                .Should()
                .BeFalse();
            source.AdvanceLine()
                .Should()
                .Be("b");
            source.IsFinished()
                .Should()
                .BeTrue();

        }


    }
}