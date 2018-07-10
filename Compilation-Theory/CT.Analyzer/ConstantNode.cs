using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CT
{
    public class ConstantNode : Node
    {
        public ConstantNode( int value )
        {
            Value = value;
        }

        public int Value { get; }


        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

        internal override void Accept(NodeVisitor v) => v.Visit(this);

    }
}
