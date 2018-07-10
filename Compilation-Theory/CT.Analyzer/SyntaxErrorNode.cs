using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public class SyntaxErrorNode : Node
    {
        public SyntaxErrorNode( string message )
        {
            Message = message;
        }

        public string Message { get; }

        internal override void Accept(NodeVisitor v) => v.Visit(this);

    }
}
