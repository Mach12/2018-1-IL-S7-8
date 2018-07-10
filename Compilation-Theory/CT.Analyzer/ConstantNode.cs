using System;
using System.Collections.Generic;
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


    }
}
