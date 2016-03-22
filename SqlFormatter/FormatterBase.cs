using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFormatter
{
    public abstract class FormatterBase
    {
        protected Token currentToken;
        protected Queue<Token> tokens;
        protected StringBuilder buffer;

        protected void AddExpectedToken(TokenType word, string value)
        {
            if (currentToken.Equals(TokenType.Word, value))
            {
                buffer.Append(currentToken.Value + " ");
                this.NextToken();
            }
            else
            {
                throw new Exception("Unexpected token: " + currentToken.Value);
            }
        }

        protected void NextToken()
        {
            currentToken = tokens.Dequeue();
        }
    }
}
