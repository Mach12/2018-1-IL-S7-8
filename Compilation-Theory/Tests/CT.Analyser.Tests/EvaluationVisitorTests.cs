using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CT.Tests
{

    public class EvaluationVisitorTests
    {

        [Theory]
        [InlineData("3", 3)]
        [InlineData("3 + 7", 3 + 7)]
        [InlineData("(3 + 7)*8", (3 + 7) * 8)]
        [InlineData("(3 + 7)*(8-9)", (3 + 7) * (8 - 9))]
        [InlineData("3 * 1 + 2", 3 * 1 + 2)]
        [InlineData("1 - 1 + 2", 1 - 1 + 2)]
        [InlineData("2 * 3 * 4 + 1", 2 * 3 * 4 + 1)]
        [InlineData("4 / 2 * 3", 4 / 2 * 3)]
        [InlineData("3 + 5 * 125 / 7 - 6 + 10 ", 3 + 5 * 125 / 7 - 6 + 10)]

        [InlineData("-3 + 5 *-6", -3 + 5 * -6)]
        [InlineData("1*-3+-7*5", 1 * -3 + -7 * 5)]
        [InlineData("-5*-4+-2", -5 * -4 + -2)]
        [InlineData("-5*-(4+8*-(2+5))+-2", -5 * -(4 + 8 * -(2 + 5)) + -2)]
        public void evaluating_constants_only(string text, int result)
        {
            var ast = Analyzer.Parse(text);
            
            ValidationVisitor.Check(ast).SyntaxErrorNodeCount.Should().Be(0, "No syntax error");

            EvaluationVisitor.Evaluate( ast ).Should().Be(result);
        }


        [Theory]
        [InlineData("x + y", 3, 4, 7)]
        [InlineData("x / (y*2)", 10, 2, 10 / (2 * 2))]
        [InlineData("-x * (y*2)", 89, 34, -89 * (34 * 2))]
        public void evaluating_expression_only(string text, int x, int y, int result)
        {
            var ast = Analyzer.Parse(text);

            ValidationVisitor.Check(ast).SyntaxErrorNodeCount.Should().Be(0, "No syntax error");

            EvaluationVisitor.Evaluate(ast, name => name == "x" ? x : y).Should().Be(result);
        }
    }
}