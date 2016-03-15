using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFormatter
{
    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public bool Equals(TokenType type, string value)
        {
            return Type == type && Value == value;
        }
    }
}
