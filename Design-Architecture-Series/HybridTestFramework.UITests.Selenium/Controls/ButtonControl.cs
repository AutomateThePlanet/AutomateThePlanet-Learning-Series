using HybridTestFramework.UITests.Core.Controls;
using Microsoft.Practices.Unity;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace HybridTestFramework.UITests.Selenium.Controls
{
    public class ButtonControl : Element, IButton
    {
        public ButtonControl(IWebDriver driver, IWebElement webElement, IUnityContainer container) 
            : base(driver, webElement, container)
        {
        }

        public new string Content
        {
            get
            {
                return this.webElement.Text;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.webElement.Enabled;
            }
        }

        public void Hover()
        {
            Actions action = new Actions(driver);
            action.MoveToElement(this.webElement).Perform();
        }
    }
}
