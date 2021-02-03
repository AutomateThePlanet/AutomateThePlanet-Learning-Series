Imports OpenQA.Selenium.Appium.Windows

Namespace WinAppDriverPageObjects.Views
    Public Partial Class CalculatorStandardView
        Public ReadOnly Property ZeroButton As WindowsElement
            Get
                Return _driver.FindElementByName("Zero")
            End Get
        End Property

        Public ReadOnly Property OneButton As WindowsElement
            Get
                Return _driver.FindElementByName("One")
            End Get
        End Property

        Public ReadOnly Property TwoButton As WindowsElement
            Get
                Return _driver.FindElementByName("Two")
            End Get
        End Property

        Public ReadOnly Property ThreeButton As WindowsElement
            Get
                Return _driver.FindElementByName("Three")
            End Get
        End Property

        Public ReadOnly Property FourButton As WindowsElement
            Get
                Return _driver.FindElementByName("Four")
            End Get
        End Property

        Public ReadOnly Property FiveButton As WindowsElement
            Get
                Return _driver.FindElementByName("Five")
            End Get
        End Property

        Public ReadOnly Property SixButton As WindowsElement
            Get
                Return _driver.FindElementByName("Six")
            End Get
        End Property

        Public ReadOnly Property SevenButton As WindowsElement
            Get
                Return _driver.FindElementByName("Seven")
            End Get
        End Property

        Public ReadOnly Property EightButton As WindowsElement
            Get
                Return _driver.FindElementByName("Eight")
            End Get
        End Property

        Public ReadOnly Property NineButton As WindowsElement
            Get
                Return _driver.FindElementByName("Nine")
            End Get
        End Property

        Public ReadOnly Property PlusButton As WindowsElement
            Get
                Return _driver.FindElementByName("Plus")
            End Get
        End Property

        Public ReadOnly Property MinusButton As WindowsElement
            Get
                Return _driver.FindElementByName("Minus")
            End Get
        End Property

        Public ReadOnly Property EqualsButton As WindowsElement
            Get
                Return _driver.FindElementByName("Equals")
            End Get
        End Property

        Public ReadOnly Property DivideButton As WindowsElement
            Get
                Return _driver.FindElementByName("Divide by")
            End Get
        End Property

        Public ReadOnly Property MultiplyByButton As WindowsElement
            Get
                Return _driver.FindElementByName("Multiply by")
            End Get
        End Property

        Public ReadOnly Property ResultsInput As WindowsElement
            Get
                Return _driver.FindElementByAccessibilityId("CalculatorResults")
            End Get
        End Property
    End Class
End Namespace
