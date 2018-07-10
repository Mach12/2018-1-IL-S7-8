using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public class NegationNode : Node
    {
        public NegationNode( Node operand )
        {
            Operand = operand;
        }

        public Node Operand { get; }

        public override string ToString() => $"NEG[{Operand}]";

        internal override void Accept(NodeVisitor v) => v.Visit(this);

    }
}
