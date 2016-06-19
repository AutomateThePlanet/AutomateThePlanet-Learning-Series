// <copyright file="EnhancedBrowserAutomationTests.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Messaging.Http;
using ArtOfTest.WebAii.Silverlight.UI;
using ArtOfTest.WebAii.Win32.Dialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EnhancedBrowserAutomationTricksYouNeedToKnow
{
    [TestClass]
    public class EnhancedBrowserAutomationTests
    {
        private Manager manager;

        [TestInitialize]
        public void TestInitialize()
        {
            Settings mySettings = new Settings();
            mySettings.Web.DefaultBrowser = BrowserType.FireFox;
            mySettings.Web.UseHttpProxy = true;
            manager = new Manager(mySettings);
            manager.Settings.Web.RecycleBrowser = true;
            manager.Settings.Web.KillBrowserProcessOnClose = true;
            manager.Settings.AnnotateExecution = true;
            manager.Start();
            manager.LaunchNewBrowser();        
        }

        [TestCleanup]
        public void TestCleanup()
        {
            manager.Dispose();
        }

        // 1. Logon Dialogs - http://docs.telerik.com/teststudio/testing-framework/write-tests-in-code/advanced-topics-wtc/html-popups-and-dialogs-wtc/win32-dialogs
        [TestMethod]
        public void LogonDialog()
        {
            // add using to ArtOfTest.WebAii.Win32.Dialogs
            manager.DialogMonitor.AddDialog(new LogonDialog(manager.ActiveBrowser, "<username>", "<password>", DialogButton.OK));
            manager.DialogMonitor.Start();

            // Navigate to a page that need a logon
            manager.ActiveBrowser.NavigateTo("<Exchange with the URL to LogOn>");
        }

        // 2. Using HTTP Proxy- intercept raw HTTP traffic http://docs.telerik.com/teststudio/testing-framework/write-tests-in-code/advanced-topics-wtc/using-the-http-proxy
        [TestMethod]
        public void InterceptRawHttpTraffic()
        {
            ResponseListenerInfo responseListner = new ResponseListenerInfo(AssertJavaScriptIsGZiped);
            manager.Http.AddBeforeResponseListener(responseListner);

            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");

            manager.Http.RemoveBeforeResponseListener(responseListner);
        }

        // 3. Browser History + Refresh http://docs.telerik.com/teststudio/testing-framework/automate-browser-actions
        [TestMethod]
        public void BrowserHistory()
        {
            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");
            manager.ActiveBrowser.Refresh();

            Assert.AreEqual(
                "Healthy Diet Menu Generator - Automate The Planet",
                manager.ActiveBrowser.PageTitle);

            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/");
            manager.ActiveBrowser.GoBack();

            Assert.AreEqual(
                "Healthy Diet Menu Generator - Automate The Planet",
                manager.ActiveBrowser.PageTitle);

            manager.ActiveBrowser.GoForward();

            Assert.AreEqual(
                "Automate The Planet",
                manager.ActiveBrowser.PageTitle);
        }

        // 4. Manage Cookies http://docs.telerik.com/teststudio/testing-framework/write-tests-in-code/intermediate-topics-wtc/cookie-support
        [TestMethod]
        public void ManageCookies()
        {
            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");
          
            // Create/Set new Cookie
            var newCookie = new System.Net.Cookie("AutomateThePlanet", "Rocks", "/", "http://automatetheplanet.com");
            manager.ActiveBrowser.Cookies.SetCookie(newCookie);

            // Get All Cookies
            System.Net.CookieCollection siteCookies =
                manager.ActiveBrowser.Cookies.GetCookies("http://automatetheplanet.com");

            // Deleting Cookies
            manager.ActiveBrowser.Cookies.DeleteCookie(newCookie);

            // Purge any cookies associated with specified URL
            foreach (System.Net.Cookie cookie in siteCookies)
            {
                manager.ActiveBrowser.Cookies.DeleteCookie(cookie);
            }
        }

        // 5. Scroll to Visible http://docs.telerik.com/teststudio/testing-framework/automate-browser-actions
        [TestMethod]
        public void ScrollToVisible()
        {
            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");
            HtmlInputRadioButton coffeeRadioButton = 
                manager.ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("value=^1 x Trenta");
            coffeeRadioButton.ScrollToVisible(ScrollToVisibleType.ElementTopAtWindowTop);
        }

        // 6. Multi-Browser Instance Support - http://docs.telerik.com/teststudio/testing-framework/write-tests-in-code/advanced-topics-wtc/multi-browser-support
        [TestMethod]
        public void MultiBrowserInstanceSuppprt()
        {
            Browser firefox = manager.ActiveBrowser;
            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");
            
            manager.LaunchNewBrowser(BrowserType.InternetExplorer, true);            
            Browser internetExplorer = manager.ActiveBrowser;
            internetExplorer.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");
            
            HtmlInputRadioButton coffeeRadioButton = firefox.Find.ByExpression<HtmlInputRadioButton>("value=^1 x Trenta");
            coffeeRadioButton.ScrollToVisible(ScrollToVisibleType.ElementTopAtWindowTop);
            
            Browser firefox2 = manager.Browsers[0];
            firefox2.Window.SetFocus();

            internetExplorer.Window.SetFocus();

            firefox.Close(40);
        }

        // 7. Download Files - http://docs.telerik.com/teststudio/testing-framework/write-tests-in-code/intermediate-topics-wtc/html-control-suite-wtc/transfer-files
        [TestMethod]
        public void DownloadFiles()
        {
            string fileToDownload =
                System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    @"myw3schoolsimage.jpg");
            if (File.Exists(fileToDownload))
            {
                File.Delete(fileToDownload);
            }

            manager.ActiveBrowser.NavigateTo(
                "http://www.w3schools.com/tags/tryit.asp?filename=tryhtml5_a_download");
            Browser myFrame = manager.ActiveBrowser.Frames.ById("iframeResult");
            HtmlImage image = myFrame.Find.AllByTagName<HtmlImage>("img")[0];
            image.Download(false, DownloadOption.Save, fileToDownload, 50000);
        }

        private void AssertJavaScriptIsGZiped(object sender, HttpResponseEventArgs e)
        {
            Debug.WriteLine(String.Format("Request for {0}", e.Response.Request.RequestUri));
            if (e.Response.Headers["Content-Type"] != null && e.Response.Headers["Content-Type"].Equals("application/javascript"))
            {
                Assert.AreEqual("gzip", e.Response.Headers["Content-Encoding"]);
            }
        }
    }
}