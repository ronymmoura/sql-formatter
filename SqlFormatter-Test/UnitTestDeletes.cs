using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlFormatter;

namespace SqlFormatter_Test
{
    [TestClass]
    public class UnitTestDeletes
    {
        [TestMethod]
        public void Delete()
        {
            var expected = "DELETE FROM TEST";
            var formatter = new Formatter("DELETE FROM TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        #region Where

        [TestMethod]
        public void Delete_Where()
        {
            var expected = "DELETE FROM TEST\n" +
                           "WHERE COLUMN_1 = COLUMN_1\n" + 
                           "  AND COLUMN_2 = COLUMN_2";
            var formatter = new Formatter("DELETE FROM TEST WHERE COLUMN_1 = COLUMN_1 AND COLUMN_2 = COLUMN_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Delete_WhereNumber()
        {
            var expected = "DELETE FROM TEST\n" +
                           "WHERE COLUMN_1 = 123.45";
            var formatter = new Formatter("DELETE FROM TEST WHERE COLUMN_1 = 123.45");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Delete_WhereString()
        {
            var expected = "DELETE FROM TEST\n" +
                           "WHERE COLUMN_1 = 'TEST'";
            var formatter = new Formatter("DELETE FROM TEST WHERE COLUMN_1 = 'TEST'");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Delete_WhereVariable()
        {
            var expected = "DELETE FROM TEST\n" +
                           "WHERE COLUMN_1 = @VAR_1";
            var formatter = new Formatter("DELETE FROM TEST WHERE COLUMN_1 = @VAR_1");
            Assert.AreEqual(expected, formatter.Format());
        }

        #endregion
    }
}
