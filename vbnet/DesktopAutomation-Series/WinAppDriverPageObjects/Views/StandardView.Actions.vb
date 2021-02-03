Imports OpenQA.Selenium.Appium.Windows
Imports System

Namespace WinAppDriverPageObjects.Views
    Public Partial Class CalculatorStandardView
        Private ReadOnly _driver As WindowsDriver(Of WindowsElement)

        Public Sub New(driver As WindowsDriver(Of WindowsElement))
            _driver = driver
        End Sub

        Public Sub PerformCalculation(ByVal num1 As Integer, ByVal operation As Char, ByVal num2 As Integer)
            ClickByDigit(num1)
            PerformOperations(operation)
            ClickByDigit(num2)
            EqualsButton.Click()
        End Sub

        Private Sub ClickByDigit(ByVal digit As Integer)
            Select Case digit
                Case 1
                    OneButton.Click()
                Case 2
                    TwoButton.Click()
                Case 3
                    ThreeButton.Click()
                Case 4
                    FourButton.Click()
                Case 5
                    FiveButton.Click()
                Case 6
                    SixButton.Click()
                Case 7
                    SevenButton.Click()
                Case 8
                    EightButton.Click()
                Case 9
                    NineButton.Click()
                Case Else
                    Throw New NotSupportedException($"Not Supported digit = {digit}")
            End Select
        End Sub

        Private Sub PerformOperations(ByVal operation As Char)
            Select Case operation
                Case "+"c
                    PlusButton.Click()
                Case "-"c
                    MinusButton.Click()
                Case "="c
                    EqualsButton.Click()
                Case "*"c
                    MultiplyByButton.Click()
                Case "/"c
                    DivideButton.Click()
                Case Else
                    Throw New NotSupportedException($"Not Supported operation = {operation}")
            End Select
        End Sub

        Private Function GetCalculatorResultText() As String
            Return ResultsInput.Text.Replace("Display is", String.Empty).Trim()
        End Function
    End Class
End Namespace
