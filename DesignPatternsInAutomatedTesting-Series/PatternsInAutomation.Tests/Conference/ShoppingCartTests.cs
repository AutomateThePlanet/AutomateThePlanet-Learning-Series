using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PatternsInAutomation.Tests.Conference.Base;
using PatternsInAutomation.Tests.Conference.Data;

namespace PatternsInAutomation.Tests.Conference
{
    [TestClass]
    public class ShoppingCartTests
    {
        private IFactory<ShoppingCart> shoppingCartFactory;
        private ShoppingCart shoppingCart;
        private IWebDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            shoppingCartFactory = new ShoppingCartFactory(driver);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            driver.Quit();
        }

        [TestMethod]
        public void Purchase_Book_Discounts()
        {
         
            shoppingCart = shoppingCartFactory.Create();
            shoppingCart.PurchaseItem("The Hitchhiker's Guide to the Galaxy", 22.2, new ClientInfo());
        }
    }
}

// the facade design pattern no facade in the name because you don't want to know that this class is hiding the complexity
// You can use the page objects directly. Because the page objects can be used easily in tests.
/*
 *  hard instanciation to facade
 *  no need to factory to the pages, 
 *  you can change it.
 *  pages are created easily
 */
