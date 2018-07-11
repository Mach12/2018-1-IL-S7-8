using System;
using System.Text;

namespace CT
{
    public class Tokenizer
    {
        readonly string _text;
        int _headPos;
        TokenType _current;
        int _currentInteger;
        string _currentIdentifier;
        readonly StringBuilder _buffer;

        public Tokenizer( string text )
        {
            _buffer = new StringBuilder();
            _text = text;
            Next();
        }


        bool IsEnd => _headPos >= _text.Length;

        char Head => _text[_headPos];

        char Forward() => _text[_headPos++];

        public TokenType Current => _current;

        public int IntegerValue => _currentInteger;

        public string IdentifierValue => _currentIdentifier;

        public TokenType Next()
        {
            // Skipping white space.
            while ( !IsEnd && Char.IsWhiteSpace(Head)) Forward();
            if( IsEnd ) return _current = TokenType.EOS;

            // Handling terminals.
            char c = Forward();
            switch( c )
            {
                case '*': return _current = TokenType.Mult;
                case '/': return _current = TokenType.Div;
                case '-': return _current = TokenType.Minus;
                case '+': return _current = TokenType.Plus;
                case '(': return _current = TokenType.OpenPar;
                case ')': return _current = TokenType.ClosePar;
                case '?': return _current = TokenType.QuestionMark;
                case ':': return _current = TokenType.Colon;
                case '>':
                    if (Head != '=') return _current = TokenType.GreaterThan;
                    Forward();
                    return _current = TokenType.GreaterOrEqual;
                case '<':
                    if (Head != '=') return _current = TokenType.LowerThan;
                    Forward();
                    return _current = TokenType.LowerOrEqual;
                case '=':
                    if (Head != '=') return _current = TokenType.Error;
                    Forward();
                    return _current = TokenType.Equal;
                case '!':
                    if (Head != '=') return _current = TokenType.Error;
                    Forward();
                    return _current = TokenType.Diff;
            }
            // Handling non-terminals.
            _buffer.Clear();
            int v = c - '0';
            if (v > 0 && v <= 9)
            {
               while (!IsEnd)
                {
                    int d = Head - '0';
                    if (d >= 0 && d <= 9)
                    {
                        v = v * 10 + d;
                        Forward();
                    }
                    else break;
                }
                _currentInteger = v;
                return _current = TokenType.Integer;
            }
            if (Char.IsLetter(c))
            {
                _buffer.Append(c);
                while (!IsEnd)
                {
                    if (Char.IsLetterOrDigit(Head))
                    {
                        _buffer.Append(Forward());
                    }
                    else break;
                }
                _currentIdentifier = _buffer.ToString();
                return _current = TokenType.Identifier;
            }
            return _current = TokenType.Error;
        }

        public bool MatchMultiplicative(out TokenType op)
        {
            op = Current;
            if (Current == TokenType.Mult || Current == TokenType.Div)
            {
                Next();
                return true;
            }
            return false;
        }
        public bool MatchAdditive(out TokenType op)
        {
            op = Current;
            if (Current == TokenType.Plus || Current == TokenType.Minus)
            {
                Next();
                return true;
            }
            return false;
        }

        public bool Match( TokenType type )
        {
            if( Current == type )
            {
                Next();
                return true;
            }
            return false;
        }

        public bool Match(out int integer)
        {
            if (Current == TokenType.Integer)
            {
                integer = IntegerValue;
                Next();
                return true;
            }
            integer = 0;
            return false;
        }
        public bool Match(out string identifier)
        {
            if (Current == TokenType.Identifier)
            {
                identifier = IdentifierValue;
                Next();
                return true;
            }
            identifier = null;
            return false;
        }
    }
}
