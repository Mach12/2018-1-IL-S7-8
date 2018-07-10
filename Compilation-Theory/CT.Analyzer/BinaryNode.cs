using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public class BinaryNode : Node
    {
        public BinaryNode( TokenType t, Node left, Node right )
        {
            Type = t;
            Left = left;
            Right = right;
        }

        public TokenType Type { get; }

        public Node Left { get; }

        public Node Right { get; }
    }
}
