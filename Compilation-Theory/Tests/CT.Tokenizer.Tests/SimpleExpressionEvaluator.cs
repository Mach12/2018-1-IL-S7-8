using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CT.Tokenizer.Tests
{

    public class SimpleExpressionEvaluator
    {
        
        [Theory]
        [InlineData("3", 3)]
        [InlineData("3 + 7", 3 + 7)]
        [InlineData("(3 + 7)*8", (3 + 7) * 8)]
        public void simple_exprexxion_evaluation( string text, int result)
        {
            Evaluate(text).Should().Be(result);
        }


        static int Evaluate(string text) => EvalExpression(new Tokenizer(text));


        /// <summary>
        /// expression → terme  opérateur-additif  expression  |  terme 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static int EvalExpression(Tokenizer t)
        {
        }

    }
}
