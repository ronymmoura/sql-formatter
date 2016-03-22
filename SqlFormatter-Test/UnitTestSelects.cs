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
            var formatter = new Formatter("SELECT * FROM TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_Distinct()
        {
            var expected = "SELECT DISTINCT * \n" +
                           "FROM TEST";
            var formatter = new Formatter("SELECT DISTINCT * FROM TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        #region Columns

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

        #endregion

        #region Where

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
        public void Select_WhereNumber()
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
        public void Select_WhereVariable()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = @VAR_1\n" +
                           "  AND COLUMN_2 = @VAR_2";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = @VAR_1 AND COLUMN_2 = @VAR_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_WhereComplexExpressions()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE ( COLUMN_1 = @VAR_1 )\n" +
                           "   OR ( COLUMN_2 = @VAR_2 AND COLUMN_3 = @VAR_3 )";
            var formatter = new Formatter("SELECT * FROM TEST WHERE (COLUMN_1 = @VAR_1) OR (COLUMN_2 = @VAR_2 AND COLUMN_3 = @VAR_3)");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_WhereMoreComplexExpressions()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE ( COLUMN_1 = @VAR_1 )\n" +
                           "   OR ( COLUMN_2 = @VAR_2 AND ( COLUMN_3 = @VAR_3 OR COLUMN_4 = @VAR_4 ) )\n" +
                           "  AND ( COLUMN_6 = @VAR_6 )";
            var formatter = new Formatter("SELECT * FROM TEST WHERE (COLUMN_1 = @VAR_1) OR (COLUMN_2 = @VAR_2 AND (COLUMN_3 = @VAR_3 OR COLUMN_4 = @VAR_4)) AND ( COLUMN_6 = @VAR_6 )");
            Assert.AreEqual(expected, formatter.Format());
        }

        #endregion

        #region Joins

        [TestMethod]
        public void Select_InnerJoin()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "INNER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST";
            var formatter = new Formatter("SELECT * FROM TEST INNER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_InnerJoinDouble()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "INNER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST \n" +
                           "INNER JOIN SUBTEST2 ON TEST.ID_TEST = SUBTEST2.ID_TEST";
            var formatter = new Formatter("SELECT * FROM TEST INNER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST INNER JOIN SUBTEST2 ON TEST.ID_TEST = SUBTEST2.ID_TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_LeftJoin()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "LEFT JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST";
            var formatter = new Formatter("SELECT * FROM TEST LEFT JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_LeftOuterJoin()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "LEFT OUTER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST";
            var formatter = new Formatter("SELECT * FROM TEST LEFT OUTER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_RightJoin()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "RIGHT JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST";
            var formatter = new Formatter("SELECT * FROM TEST RIGHT JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_RightOuterJoin()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "RIGHT OUTER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST";
            var formatter = new Formatter("SELECT * FROM TEST RIGHT OUTER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        #endregion

        #region Order By

        [TestMethod]
        public void Select_OrderBy()
        {
            var expected = "SELECT COLUMN_1,\n" +
                           "       COLUMN_2,\n" +
                           "       COLUMN_3,\n" +
                           "       COLUMN_4 \n" +
                           "FROM TEST \n" +
                           "ORDER BY COLUMN_3,\n" + 
                           "         COLUMN_4";
            var formatter = new Formatter("SELECT COLUMN_1, COLUMN_2,COLUMN_3,  COLUMN_4 FROM TEST ORDER BY COLUMN_3, COLUMN_4");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Select_OrderByAfterWhere()
        {
            var expected = "SELECT COLUMN_1,\n" +
                           "       COLUMN_2,\n" +
                           "       COLUMN_3,\n" +
                           "       COLUMN_4 \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = @VAR_1 \n" +
                           "ORDER BY COLUMN_3,\n" +
                           "         COLUMN_4";
            var formatter = new Formatter("SELECT COLUMN_1, COLUMN_2,COLUMN_3,  COLUMN_4 FROM TEST WHERE COLUMN_1 = @VAR_1 ORDER BY COLUMN_3, COLUMN_4");
            Assert.AreEqual(expected, formatter.Format());
        }

        #endregion
    }
}
