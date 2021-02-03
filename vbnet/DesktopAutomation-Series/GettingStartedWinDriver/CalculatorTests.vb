Imports NUnit.Framework
Imports OpenQA.Selenium.Appium
Imports OpenQA.Selenium.Appium.Windows

Namespace GettingStartedWinDriver
    <TestFixture>
    Public Class CalculatorTests
        Private _driver As WindowsDriver(Of WindowsElement)

        <SetUp>
        <Obsolete>
        Public Sub TestInit()
            Dim options = New AppiumOptions()
            options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App")
            options.AddAdditionalCapability("deviceName", "WindowsPC")
            _driver = New WindowsDriver(Of WindowsElement)(New Uri("http://127.0.0.1:4723"), options)
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5)
        End Sub

        <TearDown>
        Public Sub TestCleanup()
            If _driver IsNot Nothing Then
                _driver.Quit()
                _driver = Nothing
            End If
        End Sub

        <Test>
        Public Sub Addition()
            _driver.FindElementByName("Five").Click()
            _driver.FindElementByName("Plus").Click()
            _driver.FindElementByName("Seven").Click()
            _driver.FindElementByName("Equals").Click()
            Dim calculatorResult = GetCalculatorResultText()
            Assert.AreEqual("12", calculatorResult)
        End Sub

        <Test>
        Public Sub Division()
            _driver.FindElementByAccessibilityId("num8Button").Click()
            _driver.FindElementByAccessibilityId("num8Button").Click()
            _driver.FindElementByAccessibilityId("divideButton").Click()
            _driver.FindElementByAccessibilityId("num1Button").Click()
            _driver.FindElementByAccessibilityId("num1Button").Click()
            _driver.FindElementByAccessibilityId("equalButton").Click()
            Call Assert.AreEqual("8", GetCalculatorResultText())
        End Sub

        <Test>
        Public Sub Multiplication()
            _driver.FindElementByXPath("//Button[@Name='Nine']").Click()
            _driver.FindElementByXPath("//Button[@Name='Multiply by']").Click()
            _driver.FindElementByXPath("//Button[@Name='Nine']").Click()
            _driver.FindElementByXPath("//Button[@Name='Equals']").Click()
            Call Assert.AreEqual("81", GetCalculatorResultText())
        End Sub

        <Test>
        Public Sub Subtraction()
            _driver.FindElementByXPath("//Button[@AutomationId=""num9Button""]").Click()
            _driver.FindElementByXPath("//Button[@AutomationId=""minusButton""]").Click()
            _driver.FindElementByXPath("//Button[@AutomationId=""num1Button""]").Click()
            _driver.FindElementByXPath("//Button[@AutomationId=""equalButton""]").Click()
            Call Assert.AreEqual("8", GetCalculatorResultText())
        End Sub

        <Test>
        <TestCase("One", "Plus", "Seven", "8")>
        <TestCase("Nine", "Minus", "One", "8")>
        <TestCase("Eight", "Divide by", "Eight", "1")>
        Public Sub Templatized(ByVal input1 As String, ByVal operation As String, ByVal input2 As String, ByVal expectedResult As String)
            _driver.FindElementByName(input1).Click()
            _driver.FindElementByName(operation).Click()
            _driver.FindElementByName(input2).Click()
            _driver.FindElementByName("Equals").Click()
            Call Assert.AreEqual(expectedResult, GetCalculatorResultText())
        End Sub

        Private Function GetCalculatorResultText() As String
            Return _driver.FindElementByAccessibilityId("CalculatorResults").Text.Replace("Display is", String.Empty).Trim()
        End Function
    End Class
End Namespace
