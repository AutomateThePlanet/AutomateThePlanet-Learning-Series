using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace GettingStartedWinDriver
{
    [TestFixture]
    public class CalculatorTests
    {
        private WindowsDriver<WindowsElement> _driver;

        [SetUp]
        public void TestInit()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void TestCleanup()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }

        [Test]
        public void Addition()
        {
            _driver.FindElementByName("Five").Click();
            _driver.FindElementByName("Plus").Click();
            _driver.FindElementByName("Seven").Click();
            _driver.FindElementByName("Equals").Click();

            var calculatorResult = GetCalculatorResultText();
            Assert.AreEqual("12", calculatorResult);
        }

        [Test]
        public void Division()
        {
            _driver.FindElementByAccessibilityId("num8Button").Click();
            _driver.FindElementByAccessibilityId("num8Button").Click();
            _driver.FindElementByAccessibilityId("divideButton").Click();
            _driver.FindElementByAccessibilityId("num1Button").Click();
            _driver.FindElementByAccessibilityId("num1Button").Click();
            _driver.FindElementByAccessibilityId("equalButton").Click();

            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [Test]
        public void Multiplication()
        {
            _driver.FindElementByXPath("//Button[@Name='Nine']").Click();
            _driver.FindElementByXPath("//Button[@Name='Multiply by']").Click();
            _driver.FindElementByXPath("//Button[@Name='Nine']").Click();
            _driver.FindElementByXPath("//Button[@Name='Equals']").Click();

            Assert.AreEqual("81", GetCalculatorResultText());
        }

        [Test]
        public void Subtraction()
        {
            _driver.FindElementByXPath("//Button[@AutomationId=\"num9Button\"]").Click();
            _driver.FindElementByXPath("//Button[@AutomationId=\"minusButton\"]").Click();
            _driver.FindElementByXPath("//Button[@AutomationId=\"num1Button\"]").Click();
            _driver.FindElementByXPath("//Button[@AutomationId=\"equalButton\"]").Click();

            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [Test]
        [TestCase("One", "Plus", "Seven", "8")]
        [TestCase("Nine", "Minus", "One", "8")]
        [TestCase("Eight", "Divide by", "Eight", "1")]
        public void Templatized(string input1, string operation, string input2, string expectedResult)
        {
            _driver.FindElementByName(input1).Click();
            _driver.FindElementByName(operation).Click();
            _driver.FindElementByName(input2).Click();
            _driver.FindElementByName("Equals").Click();

            Assert.AreEqual(expectedResult, GetCalculatorResultText());
        }

        private string GetCalculatorResultText()
        {
            return _driver.FindElementByAccessibilityId("CalculatorResults").Text.Replace("Display is", string.Empty).Trim();
        }
    }
}
