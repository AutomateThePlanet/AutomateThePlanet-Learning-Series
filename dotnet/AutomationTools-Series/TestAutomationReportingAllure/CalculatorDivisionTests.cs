using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TestAutomationReportingAllure
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("CalculatorTests")]
    [AllureDisplayIgnored]
    internal class CalculatorDivisionTests
    {
        [Test(Description = "Multiply two integers and returns the result")]
        [AllureTag("CI")]
        [AllureOwner("Anton")]
        [AllureSubSuite("Multiply")]
        public void Return4_WhenMultiply2And2()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Multiply(2, 2);

            Assert.AreEqual(4, actualResult);
        }

        [Test(Description = "Multiply two integers and returns the result")]
        [AllureTag("CI")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSubSuite("Multiply")]
        public void Return0_WhenMultiply0And0()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Multiply(0, 0);

            Assert.AreEqual(0, actualResult);
        }

        [Test(Description = "Multiply two integers and returns the result")]
        [AllureTag("CI")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSubSuite("Multiply")]
        public void ReturnMinus5_WhenMultiply5AndMinus1()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Multiply(5, -1);

            Assert.AreEqual(0, actualResult);
        }
    }
}
