using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlFormatter;

namespace SqlFormatter_Test
{
    [TestClass]
    public class UnitTestInserts
    {
        [TestMethod]
        public void Insert()
        {
            var expected = "INSERT INTO TEST \n" +
                           "VALUES (COLUMN_1)";
            var formatter = new Formatter("INSERT INTO TEST VALUES(COLUMN_1)");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Insert_Columns()
        {
            var expected = "INSERT INTO TEST (COLUMN_1, COLUMN_2, COLUMN_3)\n" +
                           "VALUES ('VAL 1', 'VAL 2', 3)";
            var formatter = new Formatter("INSERT INTO TEST(COLUMN_1, COLUMN_2, COLUMN_3) VALUES('VAL 1', 'VAL 2', 3)");
            Assert.AreEqual(expected, formatter.Format());
        }

        #region Values

        [TestMethod]
        public void Insert_Number()
        {
            var expected = "INSERT INTO TEST \n" +
                           "VALUES (123.45)";
            var formatter = new Formatter("INSERT INTO TEST VALUES(123.45)");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Insert_String()
        {
            var expected = "INSERT INTO TEST \n" +
                           "VALUES ('TEST')";
            var formatter = new Formatter("INSERT INTO TEST VALUES('TEST')");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Insert_Variable()
        {
            var expected = "INSERT INTO TEST \n" +
                           "VALUES (@VAR_1)";
            var formatter = new Formatter("INSERT INTO TEST VALUES(@VAR_1)");
            Assert.AreEqual(expected, formatter.Format());
        }

        #endregion

        #region Where

        [TestMethod]
        public void Insert_Where()
        {
            var expected = "INSERT INTO TEST (COLUMN_1, COLUMN_2, COLUMN_3)\n" +
                           "VALUES ('VAL 1', 'VAL 2', 3)\n" +
                           "WHERE COLUMN_1 = COLUMN_2";
            var formatter = new Formatter("INSERT INTO TEST(COLUMN_1, COLUMN_2, COLUMN_3) VALUES('VAL 1', 'VAL 2', 3) WHERE COLUMN_1 = COLUMN_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Insert_WhereNumber()
        {
            var expected = "INSERT INTO TEST (COLUMN_1, COLUMN_2, COLUMN_3)\n" +
                           "VALUES ('VAL 1', 'VAL 2', 3)\n" +
                           "WHERE COLUMN_1 = 123.45";
            var formatter = new Formatter("INSERT INTO TEST(COLUMN_1, COLUMN_2, COLUMN_3) VALUES('VAL 1', 'VAL 2', 3) WHERE COLUMN_1 = 123.45");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Insert_WhereString()
        {
            var expected = "INSERT INTO TEST (COLUMN_1, COLUMN_2, COLUMN_3)\n" +
                           "VALUES ('VAL 1', 'VAL 2', 3)\n" +
                           "WHERE COLUMN_1 = 'TEST'";
            var formatter = new Formatter("INSERT INTO TEST(COLUMN_1, COLUMN_2, COLUMN_3) VALUES('VAL 1', 'VAL 2', 3) WHERE COLUMN_1 = 'TEST'");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Insert_WhereVariable()
        {
            var expected = "INSERT INTO TEST (COLUMN_1, COLUMN_2, COLUMN_3)\n" +
                           "VALUES ('VAL 1', 'VAL 2', 3)\n" +
                           "WHERE COLUMN_1 = @VAR_1";
            var formatter = new Formatter("INSERT INTO TEST(COLUMN_1, COLUMN_2, COLUMN_3) VALUES('VAL 1', 'VAL 2', 3) WHERE COLUMN_1 = @VAR_1");
            Assert.AreEqual(expected, formatter.Format());
        }

        #endregion
    }
}
