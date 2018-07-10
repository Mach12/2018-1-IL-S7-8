using FluentAssertions;
using System;
using Xunit;

namespace CT
{
    public class AnalyserTests
    {
        [Theory]
        [InlineData("3", "3")]
        [InlineData("1+2+3", "((1+2)+3)")]
        [InlineData("1*2+3", "((1*2)+3)")]
        [InlineData("1+2*3", "(1+(2*3))")]
        [InlineData("1+2*-3", "(1+(2*NEG[3]))")]
        [InlineData("1*-(2+3)", "(1*NEG[(2+3)])")]
        [InlineData("4/2*3", "((4/2)*3)")]
        public void simple_rewriting( string text, string result )
        {
            Analyzer.Parse(text).ToString().Should().Be(result);
        }
    }
}
