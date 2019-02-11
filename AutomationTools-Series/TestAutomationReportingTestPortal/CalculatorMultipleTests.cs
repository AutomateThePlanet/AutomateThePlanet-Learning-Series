using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestAutomationReportingTestPortal
{
    [TestClass]
    public class CalculatorMultipleTests
    {
        [TestMethod]
        public void ThrowException_When_DivisionOn0()
        {
            var calculator = new Calculator();

            float actualResult = calculator.Division(2, 0);

            Assert.AreEqual(4, actualResult);
        }

        [TestMethod]
        public void Return2_When_Division2On1()
        {
            var calculator = new Calculator();

            float actualResult = calculator.Division(2, 1);

            Assert.AreEqual(4, actualResult);
        }
    }
}
