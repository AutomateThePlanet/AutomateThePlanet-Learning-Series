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
    internal class CalculatorAddTests
    {
        [Test(Description = "Add two integers and returns the sum")]
        [AllureTag("CI")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("8911")]
        [AllureTms("532")]
        [AllureOwner("Anton")]
        [AllureSubSuite("Add")]
        public void Return4_WhenAdd2And2()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Add(2, 2);

            Assert.AreEqual(4, actualResult);
        }

        [Test(Description = "Add two integers and returns the sum")]
        [AllureTag("CI")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSubSuite("Add")]
        public void Return0_WhenAdd0And0()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Add(0, 0);

            Assert.AreEqual(0, actualResult);
        }

        [Test(Description = "Add two integers and returns the sum")]
        [AllureTag("CI")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSubSuite("Add")]
        public void ReturnMinus5_WhenAddMinus3AndMinus2()
        {
            var calculator = new Calculator();

            int actualResult = calculator.Add(0, 0);

            Assert.AreEqual(1, actualResult);
        }
    }
}
