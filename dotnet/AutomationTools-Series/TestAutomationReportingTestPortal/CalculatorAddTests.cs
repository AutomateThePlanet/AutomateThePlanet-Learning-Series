using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestAutomationReportingReportPortal
{
    [TestClass]
    internal class CalculatorAddTests
    {
        [TestMethod]
        public void Return4_WhenAdd2And2()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Add(2, 2);

            Assert.AreEqual(4, actualResult);
        }

        [TestMethod]
        public void Return0_WhenAdd0And0()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Add(0, 0);

            Assert.AreEqual(0, actualResult);
        }

        [TestMethod]
        public void ReturnMinus5_WhenAddMinus3AndMinus2()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Add(0, 0);

            Assert.AreEqual(1, actualResult);
        }
    }
}
