Imports NUnit.Framework

Namespace WinAppDriverPageObjects.Views
    Public Partial Class CalculatorStandardView
        Public Sub AssertResult(ByVal expectedReslt As Decimal)
            Dim strResult As String = GetCalculatorResultText()
            Dim actualResult = Decimal.Parse(strResult)
            Assert.AreEqual(expectedReslt, actualResult)
        End Sub
    End Class
End Namespace
