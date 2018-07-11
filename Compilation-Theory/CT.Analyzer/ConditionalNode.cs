using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CT
{
    public class ConditionalNode : Node
    {
        public ConditionalNode( Node condition, Node whenTrue, Node whenFalse )
        {
            Condition = condition;
            WhenTrue = whenTrue;
            WhenFalse = whenFalse;
        }

        public Node Condition { get; }

        public Node WhenTrue { get; }

        public Node WhenFalse { get; }

        public override string ToString() => $"({Condition}?{WhenTrue}:{WhenFalse})";

        internal override void Accept(NodeVisitor v) => v.Visit(this);

    }
}
