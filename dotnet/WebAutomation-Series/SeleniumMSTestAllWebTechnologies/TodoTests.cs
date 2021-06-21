using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumMSTestAllWebTechnologies
{
    [TestClass]
    public class TodoTests
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        private IWebDriver _driver;
        private WebDriverWait _webDriverWait;
        private Actions _actions;

        [TestInitialize]
        public void TestInit()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
            _actions = new Actions(_driver);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Quit();
        }

        [DataRow("Backbone.js")]
        [DataRow("AngularJS")]
        [DataRow("React")]
        [DataRow("Vue.js")]
        [DataRow("CanJS")]
        [DataRow("Ember.js")]
        [DataRow("KnockoutJS")]
        [DataRow("Marionette.js")]
        [DataRow("Polymer")]
        [DataRow("Angular 2.0")]
        [DataRow("Dart")]
        [DataRow("Elm")]
        [DataRow("Closure")]
        [DataRow("Vanilla JS")]
        [DataRow("jQuery")]
        [DataRow("cujoJS")]
        [DataRow("Spine")]
        [DataRow("Dojo")]
        [DataRow("Mithril")]
        [DataRow("Kotlin + React")]
        [DataRow("Firebase + AngularJS")]
        [DataRow("Vanilla ES6")]
        [DataTestMethod]
        public void VerifyTodoListCreatedSuccessfully(string technology)
        {
            _driver.Navigate().GoToUrl("https://todomvc.com/");
            OpenTechnologyApp(technology);
            AddNewToDoItem("Clean the car");
            AddNewToDoItem("Clean the house");
            AddNewToDoItem("Buy Ketchup");
            GetItemCheckBox("Buy Ketchup").Click();
            AssertLeftItems(2);
        }

        private void AssertLeftItems(int expectedCount)
        {
            var resultSpan = WaitAndFindElement(By.XPath("//footer/*/span | //footer/span"));
            if (expectedCount <= 0)
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
            else
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} items left");
            }
        }

        private void ValidateInnerTextIs(IWebElement resultSpan, string expectedText)
        {
            _webDriverWait.Until(ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText));
        }

        private IWebElement GetItemCheckBox(string todoItem)
        {
            return WaitAndFindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"));
        }

        private void AddNewToDoItem(string todoItem)
        {
            var todoInput = WaitAndFindElement(By.XPath("//input[@placeholder='What needs to be done?']"));
            todoInput.SendKeys(todoItem);
            _actions.Click(todoInput).SendKeys(Keys.Enter).Perform();
        }

        private void OpenTechnologyApp(string name)
        {
            var technologyLink = WaitAndFindElement(By.LinkText(name));
            technologyLink.Click();
        }

        private IWebElement WaitAndFindElement(By locator)
        {
            return _webDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }
    }
}
