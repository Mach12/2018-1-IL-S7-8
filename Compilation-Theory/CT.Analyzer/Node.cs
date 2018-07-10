using System;

namespace CT
{
    public abstract class Node
    {
        internal abstract void Accept(NodeVisitor nodeVisitor);
    }

}
