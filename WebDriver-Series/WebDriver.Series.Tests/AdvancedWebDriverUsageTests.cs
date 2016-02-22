using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace WebDriver.Series.Tests
{
    [TestClass]
    public class AdvancedWebDriverUsageTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            // 7. Use specific Firefox Profile
            ////FirefoxProfileManager profileManager = new FirefoxProfileManager();
            ////FirefoxProfile profile = profileManager.GetProfile("HARDDISKUSER");
            ////this.driver = new FirefoxDriver(profile);

            // 7.1. Set Chrome Options.
            ////ChromeOptions options = new ChromeOptions();
            ////// set some options
            ////DesiredCapabilities dc = DesiredCapabilities.Chrome();
            ////dc.SetCapability(ChromeOptions.Capability, options);
            ////IWebDriver driver = new RemoteWebDriver(dc);

            // 8. Turn off Java Script
            ////FirefoxProfileManager profileManager = new FirefoxProfileManager();
            ////FirefoxProfile profile = profileManager.GetProfile("HARDDISKUSER");
            ////profile.SetPreference("javascript.enabled", false);
            ////this.driver = new FirefoxDriver(profile);

            ////this.driver = new FirefoxDriver();
            ////var options = new InternetExplorerOptions();
            ////options.EnsureCleanSession = true;
            ////options.IgnoreZoomLevel = true;
            ////options.EnableNativeEvents = true;
            ////options.PageLoadStrategy = InternetExplorerPageLoadStrategy.Eager;
            ////this.driver = new InternetExplorerDriver(@"D:\Projects\PatternsInAutomation.Tests\WebDriver.Series.Tests\Drivers", options);
            this.driver = new FirefoxDriver();
            this.driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));

            // 10 Advanced WebDriver Tips and Tricks Part 2
            // 6. Change Firefox user agent
            ////FirefoxProfileManager profileManager = new FirefoxProfileManager();
            ////FirefoxProfile profile = new FirefoxProfile();
            ////profile.SetPreference(
            ////"general.useragent.override",
            ////"Mozilla/5.0 (BlackBerry; U; BlackBerry 9900; en) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.1.0.346 Mobile Safari/534.11+");
            ////this.driver = new FirefoxDriver(profile);

            // 7. Set HTTP proxy for browser
            ////FirefoxProfile firefoxProfile = new FirefoxProfile();
            ////firefoxProfile.SetPreference("network.proxy.type", 1);
            ////firefoxProfile.SetPreference("network.proxy.http", "myproxy.com");
            ////firefoxProfile.SetPreference("network.proxy.http_port", 3239);
            ////driver = new FirefoxDriver(firefoxProfile);

            // 8.1. How to handle SSL certificate error Firefox Driver
            ////FirefoxProfile firefoxProfile = new FirefoxProfile();
            ////firefoxProfile.AcceptUntrustedCertificates = true;
            ////firefoxProfile.AssumeUntrustedCertificateIssuer = false;
            ////driver = new FirefoxDriver(firefoxProfile);

            // 8.2. Accept all certificates Chrome Driver
            ////DesiredCapabilities capability = DesiredCapabilities.Chrome();
            ////Environment.SetEnvironmentVariable("webdriver.ie.driver", "C:\\Path\\To\\ChromeDriver.exe");
            ////capability.SetCapability(CapabilityType.AcceptSslCertificates, true);
            ////driver = new RemoteWebDriver(capability);

            // 8.3. Accept all certificates IE Driver
            ////DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();
            ////Environment.SetEnvironmentVariable("webdriver.ie.driver", "C:\\Path\\To\\IEDriver.exe");
            ////capability.SetCapability(CapabilityType.AcceptSslCertificates, true);
            ////driver = new RemoteWebDriver(capability);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        // 1.1. Taking Screenshot. Full Screen.
        [TestMethod]
        public void WebDriverAdvancedUsage_TakingFullScrenenScreenshot()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            this.WaitUntilLoaded();
            string tempFilePath = Path.GetTempFileName().Replace(".tmp", ".png");
            this.TakeFullScreenshot(this.driver, tempFilePath);
        }

        // 1.2. Taking Screenshot. Full Screen.
        [TestMethod]
        public void WebDriverAdvancedUsage_TakingElementScreenshot()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            this.WaitUntilLoaded();
            string tempFilePath = Path.GetTempFileName().Replace(".tmp", ".png");
            this.TakeScreenshotOfElement(this.driver, By.XPath("//*[@id='tve_editor']/div[2]/div[2]/div/div"), tempFilePath);
        }

        // 2. How to set Page Load Timeout
        [TestMethod]
        public void SetPageLoadTimeout()
        {
            // 2.1. Set Default Page Load Timeout
            this.driver.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 0, 10));

            // 2.2. Wait Until Page is Fully Loaded via JS
            this.WaitUntilLoaded();

            // 2.3. Wait For Visibility of element
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='tve_editor']/div[2]/div[2]/div/div")));
        }

        // 3. Get HTML Source of WebElement
        [TestMethod]
        public void GetHtmlSourceOfWebElement()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            this.WaitUntilLoaded();
            var element = this.driver.FindElement(By.XPath("//*[@id='tve_editor']/div[2]/div[3]/div/div"));
            string sourceHtml = element.GetAttribute("innerHTML");
            Debug.WriteLine(sourceHtml);
        }

        // 4. Execute JS C# 
        [TestMethod]
        public void ExecuteJavaScript()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            this.WaitUntilLoaded();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string title = (string)js.ExecuteScript("return document.title");
            Debug.WriteLine(title);
        }

        // 5. Execute in headless browser
        // Download binaries- http://phantomjs.org/download.html
        [TestMethod]
        public void ExecuteInHeadlessBrowser()
        {
            this.driver = new PhantomJSDriver(@"D:\Projects\PatternsInAutomation.Tests\WebDriver.Series.Tests\Drivers");
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            this.WaitUntilLoaded();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string title = (string)js.ExecuteScript("return document.title");
            Debug.WriteLine(title);
        }

        // 6. How to check if an element is visible
        [TestMethod]
        public void CheckIfElementIsVisible()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            Assert.IsTrue(driver.FindElement(By.XPath("//*[@id='tve_editor']/div[2]/div[2]/div/div")).Displayed);
        }

        // 9. Manage Cookies
        [TestMethod]
        public void ManageCookies()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com");

            // 9.1. Add new cookie
            Cookie cookie = new Cookie("key", "value");
            this.driver.Manage().Cookies.AddCookie(cookie);

            // 9.2. Get All Cookies
            var cookies = this.driver.Manage().Cookies.AllCookies;
            foreach (var currentCookie in cookies)
            {
                Debug.WriteLine(currentCookie.Value);
            }

            // 9.3. Delete Cookie by name
            this.driver.Manage().Cookies.DeleteCookieNamed("CookieName");

            // 9.4. Delete All Cookies
            this.driver.Manage().Cookies.DeleteAllCookies();

            // 9.5. Get Cookie by name
            var myCookie = this.driver.Manage().Cookies.GetCookieNamed("CookieName");
            Debug.WriteLine(myCookie.Value);
        }

        // 10. Maximize Window
        [TestMethod]
        public void MaximizeWindow()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            this.driver.Manage().Window.Maximize();
        }

        // 1. Drag and Drop
        [TestMethod]
        public void DragAndDrop()
        {
            this.driver.Navigate().GoToUrl(@"http://loopj.com/jquery-simple-slider/");
            IWebElement element = driver.FindElement(By.XPath("//*[@id='project']/p[1]/div/div[2]"));
            Actions move = new Actions(driver);
            move.DragAndDropToOffset(element, 30, 0).Perform();
        }

        // 2. File Upload
        [TestMethod]
        public void FileUpload()
        {
            this.driver.Navigate().GoToUrl(@"https://demos.telerik.com/aspnet-ajax/ajaxpanel/application-scenarios/file-upload/defaultcs.aspx");
            IWebElement element = driver.FindElement(By.Id("ctl00_ContentPlaceholder1_RadUpload1file0"));
            String filePath = @"D:\Projects\PatternsInAutomation.Tests\WebDriver.Series.Tests\bin\Debug\WebDriver.xml";
            element.SendKeys(filePath);
        }

        // 3. JavaScript pop-ups
        [TestMethod]
        public void JavaScripPopUps()
        {
            this.driver.Navigate().GoToUrl(@"http://www.w3schools.com/js/tryit.asp?filename=tryjs_confirm");
            this.driver.SwitchTo().Frame("iframeResult");
            IWebElement button = driver.FindElement(By.XPath("/html/body/button"));
            button.Click();
            IAlert a = driver.SwitchTo().Alert();
            if (a.Text.Equals("Press a button!"))
            {
                a.Accept();
            }
            else
            {
                a.Dismiss();
            }
        }

        // 4. Switch between browser windows or tabs
        [TestMethod]
        public void MovingBetweenTabs()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com/compelling-sunday-14022016/");
            driver.FindElement(By.LinkText("10 Advanced WebDriver Tips and Tricks Part 1")).Click();
            driver.FindElement(By.LinkText("The Ultimate Guide To Unit Testing in ASP.NET MVC")).Click();
            ReadOnlyCollection<String> windowHandles = driver.WindowHandles;
            String firstTab = windowHandles.First();
            String lastTab = windowHandles.Last();
            driver.SwitchTo().Window(lastTab);
            Assert.AreEqual<string>("The Ultimate Guide To Unit Testing in ASP.NET MVC", driver.Title);
            driver.SwitchTo().Window(firstTab);
            Assert.AreEqual<string>("Compelling Sunday – 19 Posts on Programming and Quality Assurance", driver.Title);
        }

        // 5. Navigation History
        [TestMethod]
        public void NavigationHistory()
        {
            this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/1078541/Advanced-WebDriver-Tips-and-Tricks-Part");
            this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/1017816/Speed-up-Selenium-Tests-through-RAM-Facts-and-Myth");
            driver.Navigate().Back();
            Assert.AreEqual<string>("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", driver.Title);
            driver.Navigate().Refresh();
            Assert.AreEqual<string>("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", driver.Title);
            driver.Navigate().Forward();
            Assert.AreEqual<string>("Speed up Selenium Tests through RAM Facts and Myths - CodeProject", driver.Title);
        }

        // 9. Scroll focus to control
        [TestMethod]
        public void ScrollFocusToControl()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com/compelling-sunday-14022016/");
            IWebElement link = driver.FindElement(By.PartialLinkText("Previous post"));
            string jsToBeExecuted = string.Format("window.scroll(0, {0});", link.Location.Y);
            ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
            link.Click();
            Assert.AreEqual<string>("10 Advanced WebDriver Tips and Tricks - Part 1", driver.Title);
        }

        // 10. Focus on a Control
        [TestMethod]
        public void FocusOnControl()
        {
            this.driver.Navigate().GoToUrl(@"http://automatetheplanet.com/compelling-sunday-14022016/");
            IWebElement link = driver.FindElement(By.PartialLinkText("Previous post"));

            // 9.1. Option 1.
            link.SendKeys(string.Empty);

            // 9.1. Option 2.
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].focus();", link);
        }

        // 1. Set and assert the value of a hidden field
        [TestMethod]
        public void SetHiddenField()
        {
             ////<input type="hidden" name="country" value="Bulgaria"/>
            IWebElement theHiddenElem = driver.FindElement(By.Name("country"));
            string hiddenFieldValue = theHiddenElem.GetAttribute("value");
            Assert.AreEqual("Bulgaria", hiddenFieldValue);
            ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].value='Germany';",
            theHiddenElem);
            hiddenFieldValue = theHiddenElem.GetAttribute("value");
            Assert.AreEqual("Germany", hiddenFieldValue);
        }

        private void WaitUntilLoaded()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until((x) =>
            {
                return ((IJavaScriptExecutor)this.driver).ExecuteScript("return document.readyState").Equals("complete");
            });
        }

        public void TakeFullScreenshot(IWebDriver driver, String filename)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filename, ImageFormat.Png);
        }

        public void TakeScreenshotOfElement(IWebDriver driver, By by, string fileName)
        {
            // 1. Make screenshot of all screen
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var bmpScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));

            // 2. Get screenshot of specific element
            IWebElement element = driver.FindElement(by);
            var cropArea = new Rectangle(element.Location, element.Size);
            var bitmap = bmpScreen.Clone(cropArea, bmpScreen.PixelFormat);
            bitmap.Save(fileName);
        }
    }
}