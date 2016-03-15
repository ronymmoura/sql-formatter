using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFormatter
{
    public enum TokenType
    {
        None = 0,
        Word,
        Keyword,
        Number,
        String,
        Symbol,
        Variable
    }
}
