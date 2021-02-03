Imports System
Imports NUnit.Framework
Imports OpenQA.Selenium.Appium
Imports OpenQA.Selenium.Appium.Windows
Imports WinAppDriverPageObjects.WinAppDriverPageObjects.Views

Namespace WinAppDriverPageObjects
    <TestFixture>
    Public Class CalculatorPageObjectsTests
        Private _driver As WindowsDriver(Of WindowsElement)
        Private _calcStandardView As CalculatorStandardView

        <SetUp>
        Public Sub TestInit()
            Dim options = New AppiumOptions()
            options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App")
            options.AddAdditionalCapability("deviceName", "WindowsPC")
            _driver = New WindowsDriver(Of WindowsElement)(New Uri("http://127.0.0.1:4723"), options)
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5)
            _calcStandardView = New CalculatorStandardView(_driver)
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
            _calcStandardView.PerformCalculation(5, "+"c, 7)
            _calcStandardView.AssertResult(12)
        End Sub

        <Test>
        Public Sub Division()
            _calcStandardView.PerformCalculation(8, "/"c, 1)
            _calcStandardView.AssertResult(8)
        End Sub

        <Test>
        Public Sub Multiplication()
            _calcStandardView.PerformCalculation(9, "*"c, 9)
            _calcStandardView.AssertResult(81)
        End Sub

        <Test>
        Public Sub Subtraction()
            _calcStandardView.PerformCalculation(9, "-"c, 1)
            _calcStandardView.AssertResult(8)
        End Sub

        <Test>
        <TestCase(1, "+"c, 7, 8)>
        <TestCase(9, "-"c, 7, 2)>
        <TestCase(8, "/"c, 4, 2)>
        Public Sub Templatized(ByVal num1 As Integer, ByVal operation As Char, ByVal num2 As Integer, ByVal result As Decimal)
            _calcStandardView.PerformCalculation(num1, operation, num2)
            _calcStandardView.AssertResult(result)
        End Sub
    End Class
End Namespace
