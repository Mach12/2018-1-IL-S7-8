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
    }
}
