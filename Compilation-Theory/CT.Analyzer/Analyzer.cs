using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public class Analyzer
    {
        public static Node Parse(string text) => ParseExpression(new Tokenizer(text));

        public static Node ParseExpression(Tokenizer t )
        {
            Node term = ParseTerm(t);
            while (t.MatchAdditive(out var op))
            {
                term = new BinaryNode( op, term, ParseTerm(t));
            }
            return term;
        }

        /// <summary>
        /// Buggy: terme → facteur  opérateur-multiplicatif  terme  |  facteur 
        /// Fixed: terme → facteur  (opérateur-multiplicatif  facteur)*
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static Node ParseTerm(Tokenizer t)
        {
            Node factor = ParseFactor(t);
            while (t.MatchMultiplicative(out var op))
            {
                factor = new BinaryNode(op, factor, ParseFactor(t));
            }
            return factor;
        }

        /// <summary>
        /// facteur → ‘-’ facteur positif | facteur positif 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static Node ParseFactor(Tokenizer t)
        {
            bool isNegative = t.Match(TokenType.Minus);
            return isNegative ? new NegationNode( ParsePositiveFactor(t) ) : ParsePositiveFactor(t);
        }

        /// <summary>
        /// facteur positif → nombre  |  ‘(’  expression  ‘)’ 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static Node ParsePositiveFactor(Tokenizer t)
        {
            if (t.Match(out int number)) return new ConstantNode(number);
            if (t.Match(out string identifier)) return new IdentifierNode(identifier);
            if (t.Match(TokenType.OpenPar))
            {
                Node expr = ParseExpression(t);
                if (!t.Match(TokenType.ClosePar)) return new SyntaxErrorNode("Expected ).");
                return expr;
            }
            return new SyntaxErrorNode("Syntax error.");
        }
    }
}
