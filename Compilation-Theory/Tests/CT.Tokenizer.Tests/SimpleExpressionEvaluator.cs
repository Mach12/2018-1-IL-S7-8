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
        [InlineData("(3 + 7)*(8-9)", (3 + 7) * (8 - 9))]
        [InlineData("3 * 1 + 2", 3 * 1 + 2)]
        [InlineData("1 - 1 + 2", 1 - 1 + 2)]
        [InlineData("2 * 3 * 4 + 1", 2 * 3 * 4 + 1)]
        [InlineData("4 / 2 * 3", 4 / 2 * 3)]
        [InlineData("3 + 5 * 125 / 7 - 6 + 10 ", 3 + 5 * 125 / 7 - 6 + 10)]

        [InlineData("-3 + 5 *-6", -3 + 5 * -6)]
        [InlineData("1*-3+-7*5", 1 * -3 + -7 * 5)]
        [InlineData("-5*-4+-2", -5 * -4 + -2)]
        public void simple_exprexxion_evaluation(string text, int result)
        {
            Evaluate(text).Should().Be(result);
        }

        static int Evaluate(string text) => EvalExpression(new Tokenizer(text));


        /// <summary>
        /// Buggy: expression → terme  opérateur-additif  expression  |  terme 
        /// Fixed: expression → terme  (opérateur-additif  terme)* 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static int EvalExpression(Tokenizer t)
        {
            int term = EvalTerm(t);
            while (t.MatchAdditive(out var op))
            {
                int nextTerm = EvalTerm(t);
                if (op == TokenType.Plus)
                    term += nextTerm;
                else term -= nextTerm;
            }
            return term;
        }

        /// <summary>
        /// Buggy: terme → facteur  opérateur-multiplicatif  terme  |  facteur 
        /// Fixed: terme → facteur  (opérateur-multiplicatif  facteur)*
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static int EvalTerm(Tokenizer t)
        {
            int factor = EvalFactor(t);
            while (t.MatchMultiplicative(out var op))
            {
                int nextFactor = EvalFactor(t);
                if (op == TokenType.Mult)
                    factor *= nextFactor;
                else factor /= nextFactor;
            }
            return factor;
        }

        /// <summary>
        /// facteur → nombre  |  ‘(’  expression  ‘)’ 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static int EvalFactor(Tokenizer t)
        {
            if (t.Match(out int number)) return number;

            if( t.Match( TokenType.OpenPar ) )
            {
                int expr = EvalExpression(t);
                if (!t.Match( TokenType.ClosePar)) throw new Exception("Expected ).");
                return expr;
            }
            throw new Exception("Syntax error.");
        }
    }
}