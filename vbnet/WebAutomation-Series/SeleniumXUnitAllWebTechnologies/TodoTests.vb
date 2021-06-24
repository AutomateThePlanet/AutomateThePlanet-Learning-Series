Imports System
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Interactions
Imports OpenQA.Selenium.Support.UI
Imports SE = SeleniumExtras.WaitHelpers
Imports Xunit

Namespace SeleniumXUnitAllWebTechnologies
    Public Class TodoTests
        Implements IDisposable

        Private Const WAIT_FOR_ELEMENT_TIMEOUT As Integer = 30
        Private ReadOnly _driver As IWebDriver
        Private ReadOnly _webDriverWait As WebDriverWait
        Private ReadOnly _actions As Actions

        Public Sub New()
            _driver = New ChromeDriver()
            _driver.Manage().Window.Maximize()
            _webDriverWait = New WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT))
            _actions = New Actions(_driver)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            _driver.Quit()
            GC.SuppressFinalize(Me)
        End Sub

        <Theory>
        <InlineData("Backbone.js")>
        <InlineData("AngularJS")>
        <InlineData("React")>
        <InlineData("Vue.js")>
        <InlineData("CanJS")>
        <InlineData("Ember.js")>
        <InlineData("KnockoutJS")>
        <InlineData("Marionette.js")>
        <InlineData("Polymer")>
        <InlineData("Angular 2.0")>
        <InlineData("Dart")>
        <InlineData("Elm")>
        <InlineData("Closure")>
        <InlineData("Vanilla JS")>
        <InlineData("jQuery")>
        <InlineData("cujoJS")>
        <InlineData("Spine")>
        <InlineData("Dojo")>
        <InlineData("Mithril")>
        <InlineData("Kotlin + React")>
        <InlineData("Firebase + AngularJS")>
        <InlineData("Vanilla ES6")>
        Public Sub VerifyTodoListCreatedSuccessfully(ByVal technology As String)
            _driver.Navigate().GoToUrl("https://todomvc.com/")
            OpenTechnologyApp(technology)
            AddNewToDoItem("Clean the car")
            AddNewToDoItem("Clean the house")
            AddNewToDoItem("Buy Ketchup")
            GetItemCheckBox("Buy Ketchup").Click()
            AssertLeftItems(2)
        End Sub

        <Fact>
        Public Sub VerifyTodoListCreatedSuccessfully_When_jQuery()
            _driver.Navigate().GoToUrl("https://todomvc.com/")
            OpenTechnologyApp("jQuery")
            AddNewToDoItem("Clean the car")
            AddNewToDoItem("Clean the house")
            AddNewToDoItem("Buy Ketchup")
            GetItemCheckBox("Buy Ketchup").Click()
            AssertLeftItems(2)
        End Sub

        Private Sub AssertLeftItems(ByVal expectedCount As Integer)
            Dim resultSpan = WaitAndFindElement(By.XPath("//footer/*/span | //footer/span"))

            If expectedCount <= 0 Then
                Me.ValidateInnerTextIs(resultSpan, $"{expectedCount} item left")
            Else
                Me.ValidateInnerTextIs(resultSpan, $"{expectedCount} items left")
            End If
        End Sub

        Private Sub ValidateInnerTextIs(ByVal resultSpan As IWebElement, ByVal expectedText As String)
            _webDriverWait.Until(SE.ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText))
        End Sub

        Private Function GetItemCheckBox(ByVal todoItem As String) As IWebElement
            Return WaitAndFindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"))
        End Function

        Private Sub AddNewToDoItem(ByVal todoItem As String)
            Dim todoInput = WaitAndFindElement(By.XPath("//input[@placeholder='What needs to be done?']"))
            todoInput.SendKeys(todoItem)
            _actions.Click(todoInput).SendKeys(Keys.Enter).Perform()
        End Sub

        Private Sub OpenTechnologyApp(ByVal name As String)
            Dim technologyLink = WaitAndFindElement(By.LinkText(name))
            technologyLink.Click()
        End Sub

        Private Function WaitAndFindElement(ByVal locator As By) As IWebElement
            Return _webDriverWait.Until(SE.ExpectedConditions.ElementExists(locator))
        End Function
    End Class
End Namespace
