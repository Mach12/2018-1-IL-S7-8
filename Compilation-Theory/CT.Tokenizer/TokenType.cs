using System;
using System.Collections.Generic;
using System.Text;

namespace CT
{
    public enum TokenType
    {
        Error = -1,
        EOS = 0,
        Integer,
        Mult,
        Div,
        Plus,
        Minus,
        OpenPar,
        ClosePar,
        Identifier,
        QuestionMark,
        Colon,
        GreaterThan,
        LowerThan,
        GreaterOrEqual,
        LowerOrEqual,
        Equal,
        Diff
    }
}
