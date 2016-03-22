using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFormatter
{
    public class Formatter : FormatterBase
    {
        public string OriginalSQL;

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

                if (currentToken.Value == "SELECT")
                    this.FormatSelect();

                else if (currentToken.Value == "UPDATE")
                    this.FormatUpdate();

                else if (currentToken.Value == "INSERT")
                    this.FormatInsert();

                else if (currentToken.Value == "DELETE")
                    this.FormatDelete();
            }

            return buffer.ToString();
        }

        #region Select specific formatters

        private void FormatSelect()
        {
            this.AddExpectedToken(TokenType.Word, "SELECT");

            // Format columns
            while (currentToken.Value != "FROM")
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

            // Format JOIN
            if (tokens.Count > 0 && (tokens.Peek().Value == "INNER" || tokens.Peek().Value == "LEFT" || tokens.Peek().Value == "RIGHT"))
            {
                this.NextToken();
                this.FormatJoin(currentToken.Value);
            }

            // Format WHERE
            if(tokens.Count > 0 && tokens.Peek().Value == "WHERE")
            {
                this.FormatWhere();
            }

            // Format ORDER BY
            if (tokens.Count > 0 && (currentToken.Value == "ORDER"|| tokens.Peek().Value == "ORDER"))
            {
                if (tokens.Peek().Value == "ORDER")
                    this.NextToken();

                this.FormatOrderBy();
            }
        }

        private void FormatJoin(string joinType)
        {
            buffer.Append("\n");

            this.AddExpectedToken(TokenType.Word, joinType);

            if(currentToken.Value == "OUTER")
            {
                buffer.Append(currentToken.Value + " ");
                this.NextToken();
            }

            this.AddExpectedToken(TokenType.Word, "JOIN");

            buffer.Append(currentToken.Value + " ");
            this.NextToken();

            if(currentToken.Value == "AS")
            {
                buffer.Append(currentToken.Value + " ");
                this.NextToken();
                buffer.Append(currentToken.Value + " ");
                this.NextToken();
            }

            this.AddExpectedToken(TokenType.Word, "ON");

            buffer.Append(currentToken.Value + " ");

            while (tokens.Count > 0 && (tokens.Peek().Value != "WHERE" || (currentToken.Value == "INNER" || currentToken.Value == "LEFT" || currentToken.Value == "RIGHT")))
            {
                this.NextToken();

                if (tokens.Count > 0 && (currentToken.Value == "INNER" || currentToken.Value == "LEFT" || currentToken.Value == "RIGHT"))
                {
                    this.FormatJoin(currentToken.Value);
                }
                else
                {
                    buffer.Append(currentToken.Value);

                    if (tokens.Count > 0)
                        buffer.Append(" ");
                }
            }
        }

        private void FormatOrderBy()
        {
            if (currentToken.Value == "ORDER")
            {
                buffer.Append("\n");
                this.AddExpectedToken(TokenType.Word, "ORDER");
            }

            this.AddExpectedToken(TokenType.Word, "BY");

            while (tokens.Count > 0)
            {
                buffer.Append(currentToken.Value);

                if (tokens.Peek().Value != ",")
                    buffer.Append(" ");

                this.NextToken();

                if (currentToken.Value == ",")
                {
                    buffer.Append(",\n         ");
                    this.NextToken();
                }
            }

            // Append the last item from order by
            buffer.Append(currentToken.Value);
        }

        #endregion

        #region Update specific formatters

        private void FormatUpdate()
        {
            this.AddExpectedToken(TokenType.Word, "UPDATE");

            // Append the table name
            this.buffer.Append(currentToken.Value + " ");
            this.NextToken();

            this.FormatSet();

            if (tokens.Count > 0 && tokens.Peek().Value == "WHERE")
            {
                this.FormatWhere();
            }
        }

        private void FormatSet()
        {
            buffer.Append("\n");

            this.AddExpectedToken(TokenType.Word, "SET");

            while (tokens.Count > 0 && tokens.Peek().Value != "WHERE")
            {
                buffer.Append(currentToken.Value + " ");

                this.NextToken();
            }

            buffer.Append(currentToken.Value);
        }

        #endregion

        #region Insert specific formatters

        private void FormatInsert()
        {
            this.AddExpectedToken(TokenType.Word, "INSERT");
            this.AddExpectedToken(TokenType.Word, "INTO");

            // Append the table name
            this.buffer.Append(currentToken.Value + " ");
            this.NextToken();

            if (currentToken.Value == "(")
                this.FormatColumnList();

            buffer.Append("\n");

            this.FormatValues();

            if(tokens.Count > 0 && tokens.Peek().Value == "WHERE")
            {
                this.FormatWhere();
            }
        }

        private void FormatColumnList()
        {
            while(tokens.Count > 0 && currentToken.Value != ")")
            {
                buffer.Append(currentToken.Value);

                if (currentToken.Value == ",")
                    buffer.Append(" ");

                this.NextToken();
            }

            buffer.Append(currentToken.Value);
            this.NextToken();
        }

        private void FormatValues()
        {
            this.AddExpectedToken(TokenType.Word, "VALUES");

            while (tokens.Count > 0 && tokens.Peek().Value != "WHERE")
            {
                buffer.Append(currentToken.Value);

                if (currentToken.Value == ",")
                    buffer.Append(" ");

                this.NextToken();
            }

            buffer.Append(currentToken.Value);
        }

        #endregion

        #region Delete specific formatters

        private void FormatDelete()
        {
            this.AddExpectedToken(TokenType.Word, "DELETE");
            this.AddExpectedToken(TokenType.Word, "FROM");

            // Append the table name
            this.buffer.Append(currentToken.Value);

            if (tokens.Count > 0 && tokens.Peek().Value == "WHERE")
            {
                this.FormatWhere();
            }
        }

        #endregion

        #region General formatters

        private void FormatWhere()
        {
            this.NextToken();

            buffer.Append("\n");

            this.AddExpectedToken(TokenType.Word, "WHERE");

            buffer.Append(currentToken.Value + " ");

            var complexityLevel = 0;

            while (tokens.Count > 0 && currentToken.Value != "ORDER")
            {
                if (currentToken.Value == "(")
                    complexityLevel++;

                this.NextToken();

                if (currentToken.Value == ")")
                    complexityLevel--;

                if (currentToken.Value != "ORDER")
                    buffer.Append(currentToken.Value);
                else
                    break;

                if (tokens.Count > 0)
                {
                    if (tokens.Peek().Value == "AND")
                    {
                        if (complexityLevel == 0)
                            buffer.Append("\n  ");
                        else
                            buffer.Append(" ");

                        this.NextToken();
                        this.AddExpectedToken(TokenType.Word, "AND");

                        buffer.Append(currentToken.Value + " ");
                    }
                    else if (tokens.Peek().Value == "OR")
                    {
                        if (complexityLevel == 0)
                            buffer.Append("\n   ");
                        else
                            buffer.Append(" ");

                        this.NextToken();
                        this.AddExpectedToken(TokenType.Word, "OR");

                        buffer.Append(currentToken.Value + " ");
                    }
                    else
                    {
                        buffer.Append(" ");
                    }
                }
            }
        }

        #endregion
    }
}
