using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlFormatter;

namespace SqlFormatter_Test
{
    [TestClass]
    public class UnitTestWheres
    {
        [TestMethod]
        public void Where()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = COLUMN_2";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = COLUMN_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_Number()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = 1.5";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = 1.5");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_String()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = 'TEST'";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = 'TEST'");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_And()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = 1.5\n" +
                           "  AND COLUMN_2 = TEST";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = 1.5 AND COLUMN_2 = TEST");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_Variable()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 = @VAR_1\n" +
                           "  AND COLUMN_2 = @VAR_2";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 = @VAR_1 AND COLUMN_2 = @VAR_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_NotEqual()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 <> COLUMN_2";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 <> COLUMN_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_GreaterThan()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 > COLUMN_2";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 > COLUMN_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_GreaterThanOrEqual()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 >= COLUMN_2";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 >= COLUMN_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_LessThan()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 < COLUMN_2";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 < COLUMN_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_LessThanOrEqual()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 <= COLUMN_2";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 <= COLUMN_2");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_Between()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 BETWEEN COLUMN_2 AND COLUMN_3 ";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 BETWEEN COLUMN_2 AND COLUMN_3");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_Like()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 LIKE '%TEST%'";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 LIKE '%TEST%'");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_In()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 IN ( 1 , 2 , 3 )";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 IN (1,2,3)");
            Assert.AreEqual(expected, formatter.Format());
        }

        // TODO: Need a better format for subselects
        [TestMethod]
        public void Where_InSubselect()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE COLUMN_1 IN ( SELECT COLUMN_1 FROM TEST_2 )";
            var formatter = new Formatter("SELECT * FROM TEST WHERE COLUMN_1 IN (SELECT COLUMN_1 FROM TEST_2)");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_ComplexExpressions()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE ( COLUMN_1 = @VAR_1 )\n" +
                           "   OR ( COLUMN_2 = @VAR_2 AND COLUMN_3 = @VAR_3 )";
            var formatter = new Formatter("SELECT * FROM TEST WHERE (COLUMN_1 = @VAR_1) OR (COLUMN_2 = @VAR_2 AND COLUMN_3 = @VAR_3)");
            Assert.AreEqual(expected, formatter.Format());
        }

        [TestMethod]
        public void Where_MoreComplexExpressions()
        {
            var expected = "SELECT * \n" +
                           "FROM TEST \n" +
                           "WHERE ( COLUMN_1 = @VAR_1 )\n" +
                           "   OR ( COLUMN_2 = @VAR_2 AND ( COLUMN_3 = @VAR_3 OR COLUMN_4 = @VAR_4 ) )\n" +
                           "  AND ( COLUMN_6 = @VAR_6 )";
            var formatter = new Formatter("SELECT * FROM TEST WHERE (COLUMN_1 = @VAR_1) OR (COLUMN_2 = @VAR_2 AND (COLUMN_3 = @VAR_3 OR COLUMN_4 = @VAR_4)) AND ( COLUMN_6 = @VAR_6 )");
            Assert.AreEqual(expected, formatter.Format());
        }
    }
}
