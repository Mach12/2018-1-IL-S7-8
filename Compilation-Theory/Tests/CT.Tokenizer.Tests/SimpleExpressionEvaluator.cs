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
            if (t.Current == TokenType.Plus || t.Current == TokenType.Minus)
            {
                var op = t.Current;
                t.Next();
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
            if (t.Current == TokenType.Mult || t.Current == TokenType.Div)
            {
                var op = t.Current;
                t.Next();
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
            if( t.Current == TokenType.Integer )
            {
                int v = t.IntegerValue;
                t.Next();
                return v;
            }
            if( t.Current == TokenType.OpenPar )
            {
                t.Next();
                int expr = EvalExpression(t);
                if (t.Current != TokenType.ClosePar) throw new Exception("Expected ).");
                t.Next();
                return expr;
            }
            throw new Exception("Syntax error.");
        }
    }
}