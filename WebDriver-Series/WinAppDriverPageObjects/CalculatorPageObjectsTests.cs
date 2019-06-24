using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using WinAppDriverPageObjects.Views;

namespace WinAppDriverPageObjects
{
    [TestFixture]
    public class CalculatorPageObjectsTests
    {
        private WindowsDriver<WindowsElement> _driver;
        private CalculatorStandardView _calcStandardView;

        [SetUp]
        public void TestInit()
        {
            var options = new AppiumOptions();
            options.AddAdditionalOption("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            options.AddAdditionalOption("deviceName", "WindowsPC");
            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _calcStandardView = new CalculatorStandardView(_driver);
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
            _calcStandardView.PerformCalculation(5, '+', 7);

            _calcStandardView.AssertResult(12);
        }

        [Test]
        public void Division()
        {
            _calcStandardView.PerformCalculation(8, '/', 1);

            _calcStandardView.AssertResult(8);
        }

        [Test]
        public void Multiplication()
        {
            _calcStandardView.PerformCalculation(9, '*', 9);

            _calcStandardView.AssertResult(81);
        }

        [Test]
        public void Subtraction()
        {
            _calcStandardView.PerformCalculation(9, '-', 1);

            _calcStandardView.AssertResult(8);
        }

        [Test]
        [TestCase(1, '+', 7, 8)]
        [TestCase(9, '-', 7, 2)]
        [TestCase(8, '/', 4, 2)]
        public void Templatized(int num1, char operation, int num2, decimal result)
        {
            _calcStandardView.PerformCalculation(num1, operation, num2);

            _calcStandardView.AssertResult(result);
        }
    }
}
