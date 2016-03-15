using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFormatter
{
    public class Formatter
    {
        public string OriginalSQL;

        private Token currentToken;
        private Queue<Token> tokens;
        private StringBuilder buffer;

        public Formatter(string originalSql)
        {
            this.OriginalSQL = originalSql;
        }

        public string Format()
        {
            // Split the script into tokens
            var tokenizer = new Tokenizer(this.OriginalSQL);
            tokens = tokenizer.GetTokens();

            buffer = new StringBuilder();

            while(tokens.Count > 0)
            {
                this.NextToken();

                if(currentToken.Value == "SELECT")
                    this.FormatSelect();
            }

            return buffer.ToString();
        }

        private void FormatSelect()
        {
            buffer.Append(currentToken.Value + " ");

            this.NextToken();

            // Format columns
            while(currentToken.Value != "FROM")
            {
                buffer.Append(currentToken.Value);

                if (tokens.Peek().Value != ",")
                    buffer.Append(" ");

                this.NextToken();

                if(currentToken.Value == ",")
                {
                    buffer.Append(",\n       ");
                    this.NextToken();
                }
            }

            buffer.Append("\n");

            // Format FROM
            this.AddExpectedToken(TokenType.Word, "FROM");

            buffer.Append(currentToken.Value);

            if (tokens.Count > 0)
                buffer.Append(" ");

            // Format INNER JOIN
            if(tokens.Count > 0 && tokens.Peek().Value == "INNER")
            {
                this.FormatJoin("INNER");
            }

            if(tokens.Count > 0 && tokens.Peek().Value == "WHERE")
            {
                this.FormatWhere();
            }
        }

        private void FormatJoin(string joinType)
        {
            this.NextToken();

            buffer.Append("\n");

            this.AddExpectedToken(TokenType.Word, joinType);
            this.AddExpectedToken(TokenType.Word, "JOIN");

            buffer.Append(currentToken.Value + " ");
            this.NextToken();

            this.AddExpectedToken(TokenType.Word, "ON");

            buffer.Append(currentToken.Value + " ");

            while (tokens.Count > 0 && currentToken.Value != "WHERE")
            {
                this.NextToken();

                buffer.Append(currentToken.Value);

                if (tokens.Count > 0)
                    buffer.Append(" ");
            }
        }

        private void FormatWhere()
        {
            this.NextToken();

            buffer.Append("\n");

            this.AddExpectedToken(TokenType.Word, "WHERE");

            buffer.Append(currentToken.Value + " ");

            while (tokens.Count > 0 && currentToken.Value != "ORDER BY")
            {
                this.NextToken();

                buffer.Append(currentToken.Value);

                if (tokens.Count > 0)
                {
                    if (tokens.Peek().Value == "AND")
                    {
                        buffer.Append("\n  ");

                        this.NextToken();
                        this.AddExpectedToken(TokenType.Word, "AND");

                        buffer.Append(currentToken.Value + " ");
                    }
                    else
                    {
                        buffer.Append(" ");
                    }

                }
            }
        }

        private void AddExpectedToken(TokenType word, string value)
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

        private void NextToken()
        {
            currentToken = tokens.Dequeue();
        }
    }
}
