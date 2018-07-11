using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public class NodeVisitor
    {
        public virtual void Visit(Node n) => n.Accept(this);

        public virtual void Visit( BinaryNode n )
        {
            Visit(n.Left);
            Visit(n.Right);
        }

        public virtual void Visit(ConstantNode n)
        {
        }

        public virtual void Visit(IdentifierNode n)
        {
        }

        public virtual void Visit( NegationNode n )
        {
            Visit(n.Operand);
        }

        public virtual void Visit( SyntaxErrorNode n )
        {
        }

        public virtual void Visit(ConditionalNode n)
        {
            Visit(n.Condition);
            Visit(n.WhenTrue);
            Visit(n.WhenFalse);
        }

    }
}
