using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public class EvaluationVisitor : NodeVisitor
    {
        public int Result { get; private set; }

        public override void Visit(ConstantNode n) => Result = n.Value;

        public override void Visit(NegationNode n)
        {
            Visit(n.Operand);
            Result = -Result;
        }

        public override void Visit(BinaryNode n)
        {
            Visit(n.Left);
            var leftValue = Result;
            Visit(n.Right);
            switch ( n.Type )
            {
                case TokenType.Plus: Result = leftValue + Result; break;
                case TokenType.Minus: Result = leftValue - Result; break;
                case TokenType.Mult: Result = leftValue * Result; break;
                case TokenType.Div: Result = leftValue / Result; break;
            }
        }

        public static int Evaluate( Node n )
        {
            var v = new EvaluationVisitor();
            v.Visit(n);
            return v.Result;
        }
    }
}
