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
        public void simple_exprexxion_evaluation(string text, int result)
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
            int term = EvalTerm(t);
            if (t.MatchAdditive( out var op ))
            {
                int expr = EvalExpression(t);
                return op == TokenType.Plus
                                ? term + expr
                                : term - expr;
            }
            return term;
        }

        /// <summary>
        /// terme → facteur  opérateur-multiplicatif  terme  |  facteur 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static int EvalTerm(Tokenizer t)
        {
            int factor = EvalFactor(t);
            if (t.MatchMultiplicative( out var op ))
            {
                int term = EvalTerm(t);
                return op == TokenType.Mult
                                ? factor * term
                                : factor / term;
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