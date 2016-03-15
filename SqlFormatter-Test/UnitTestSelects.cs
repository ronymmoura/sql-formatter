using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlFormatter;

namespace SqlFormatter_Test
{
    [TestClass]
    public class UnitTestSelects
    {
        [TestMethod]
        public void Select()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST";
            var formatter = new Formatter("SELECT * FROM TEST\0");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_Columns()
        {
            var expected = "SELECT COLUMN_1,\n" +
                           "       COLUMN_2,\n" +
                           "       COLUMN_3,\n" +
                           "       COLUMN_4 \n" +
                           "FROM TEST";
            var formatter = new Formatter("SELECT COLUMN_1, COLUMN_2,COLUMN_3,  COLUMN_4 FROM TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_ColumnsAndAliases()
        {
            var expected = "SELECT COLUMN_1 AS C1,\n" +
                           "       COLUMN_2 AS C2,\n" + 
                           "       COLUMN_3 AS C3,\n" +
                           "       COLUMN_4 AS C4 \n" +
                           "FROM TEST";
            var formatter = new Formatter("SELECT COLUMN_1 AS C1, COLUMN_2 AS C2,COLUMN_3 AS C3,  COLUMN_4 AS C4 FROM TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_Where()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = COLUMN_2";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = COLUMN_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_WhereNumbered()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = 1.5";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = 1.5");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_WhereString()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = 'TEST'";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = 'TEST'");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_WhereAnd()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = 1.5\n" +
                           "  AND COLUMN_2 = TEST";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = 1.5 AND COLUMN_2 = TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_InnerJoin()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "INNER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST";
            var formatter = new Formatter("SELECT * FROM TEST INNER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST");
            Assert.AreEqual(expected, formatter.Format());
        }
    }
}
