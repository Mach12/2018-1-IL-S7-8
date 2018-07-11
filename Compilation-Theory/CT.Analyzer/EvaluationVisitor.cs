using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public class EvaluationVisitor : NodeVisitor
    {
        readonly Func<string, int> _getIdentifierValue;

        public EvaluationVisitor(Func<string, int> getIdentifierValue)
        {
            _getIdentifierValue = getIdentifierValue ?? (name => 0);
        }

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

        public override void Visit(IdentifierNode n)
        {
            Result = _getIdentifierValue(n.Name);
        }

        public static int Evaluate( Node n, Func<string, int> getIdentifierValue = null)
        {
            var v = new EvaluationVisitor(getIdentifierValue);
            v.Visit(n);
            return v.Result;
        }

        public static int Evaluate(Node n, IDictionary<string,int> variables )
        {
            return Evaluate(n, name => variables[name]);
        }
    }
}
