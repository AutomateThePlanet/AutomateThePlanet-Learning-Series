using OpenQA.Selenium.Appium.Windows;
using System;

namespace WinAppDriverPageObjects.Views
{
    public partial class CalculatorStandardView
    {
        private readonly WindowsDriver<WindowsElement> _driver;

        public CalculatorStandardView(WindowsDriver<WindowsElement> driver) => _driver = driver;

        public void PerformCalculation(int num1, char operation, int num2)
        {
            ClickByDigit(num1);
            PerformOperations(operation);
            ClickByDigit(num2);
            EqualsButton.Click();
        }
        
        private void ClickByDigit(int digit)
        {
            switch (digit)
            {
                case 1:
                    OneButton.Click();
                    break;
                case 2:
                    TwoButton.Click();
                    break;
                case 3:
                    ThreeButton.Click();
                    break;
                case 4:
                    FourButton.Click();
                    break;
                case 5:
                    FiveButton.Click();
                    break;
                case 6:
                    SixButton.Click();
                    break;
                case 7:
                    SevenButton.Click();
                    break;
                case 8:
                    EightButton.Click();
                    break;
                case 9:
                    NineButton.Click();
                    break;
                default:
                    throw new NotSupportedException($"Not Supported digit = {digit}");
            }
        }

        private void PerformOperations(char operation)
        {
            switch (operation)
            {
                case '+':
                    PlusButton.Click();
                    break;
                case '-':
                    MinusButton.Click();
                    break;
                case '=':
                    EqualsButton.Click();
                    break;
                case '*':
                    MultiplyByButton.Click();
                    break;
                case '/':
                    DivideButton.Click();
                    break;
                default:
                    throw new NotSupportedException($"Not Supported operation = {operation}");
            }
        }

        private string GetCalculatorResultText() => ResultsInput.Text.Replace("Display is", string.Empty).Trim();
    }
}
