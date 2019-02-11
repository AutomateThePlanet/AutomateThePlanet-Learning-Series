using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TestAutomationReportingAllure
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("CalculatorTests")]
    [AllureDisplayIgnored]
    class CalculatorMultipleTests
    {
        [Test(Description = "Performing Division on two float variables. ")]
        [AllureTag("CI")]
        [AllureOwner("Anton")]
        [AllureSubSuite("Division")]
        public void ThrowException_When_DivisionOn0()
        {
            var calculator = new Calculator();

            float actualResult = calculator.Division(2, 0);

            Assert.AreEqual(4, actualResult);
        }

        [Test(Description = "Performing Division on two float variables. ")]
        [AllureTag("CI")]
        [AllureOwner("Anton")]
        [AllureSubSuite("Division")]
        public void Return2_When_Division2On1()
        {
            var calculator = new Calculator();

            float actualResult = calculator.Division(2, 1);

            Assert.AreEqual(4, actualResult);
        }
    }
}
