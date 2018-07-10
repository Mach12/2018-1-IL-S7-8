using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public class ValidationVisitor : NodeVisitor
    {
        public int TotalNodeCount { get; private set; }

        public int SyntaxtErrorNodeCount { get; private set; }

        public void ResetCounts()
        {
            TotalNodeCount = SyntaxtErrorNodeCount = 0;
        }

        public override void Visit(Node n)
        {
            ++TotalNodeCount;
            base.Visit(n);
        }

        public override void Visit(SyntaxErrorNode n)
        {
            ++SyntaxtErrorNodeCount;
            base.Visit(n);
        }

    }
}
