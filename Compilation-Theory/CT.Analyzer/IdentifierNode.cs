using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CT
{
    public class IdentifierNode : Node
    {
        public IdentifierNode( string name )
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString() => Name;
    }
}
