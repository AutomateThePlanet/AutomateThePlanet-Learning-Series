using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestAutomationReportingReportPortal
{
    [TestClass]
    class CalculatorDivisionTests
    {
        [TestMethod]
        public void Return4_WhenMultiply2And2()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Multiply(2, 2);

            Assert.AreEqual(4, actualResult);
        }

        [TestMethod]
        public void Return0_WhenMultiply0And0()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Multiply(0, 0);

            Assert.AreEqual(0, actualResult);
        }

        [TestMethod]
        public void ReturnMinus5_WhenMultiply5AndMinus1()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Multiply(5, -1);

            Assert.AreEqual(0, actualResult);
        }
    }
}
