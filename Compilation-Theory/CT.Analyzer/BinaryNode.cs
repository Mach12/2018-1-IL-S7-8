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

        public string DisplayType
        {
            get
            {
                switch (Type)
                {
                    case TokenType.Div: return "/";
                    case TokenType.Mult: return "*";
                    case TokenType.Minus: return "-";
                    case TokenType.Plus: return "+";
                }
                throw new NotSupportedException("Can not be here!");
            }
        }

        public override string ToString() => $"({Left}{DisplayType}{Right})";

        internal override void Accept(NodeVisitor v) => v.Visit(this);
    }
}
