using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public class EvaluationVisitor : NodeVisitor
    {
        public int Result { get; private set; }

    }
}
