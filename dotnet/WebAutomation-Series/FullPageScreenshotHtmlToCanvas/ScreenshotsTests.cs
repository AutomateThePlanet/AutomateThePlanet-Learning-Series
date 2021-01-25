﻿// <copyright file="ScreenshotsTests.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
// <site>https://automatetheplanet.com/</site>

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace FullPageScreenshotHtmlToCanvas
{
    [TestFixture]
    public class ScreenshotsTests
    {
        [Test]
        public void TakingHTML2CanvasFullPageScreenshot()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
                driver.Navigate().GoToUrl(@"https://automatetheplanet.com");
                IJavaScriptExecutor js = driver;
                var html2canvasJs = File.ReadAllText($"{GetAssemblyDirectory()}\\html2canvas.js");
                js.ExecuteScript(html2canvasJs);
                string generateScreenshotJS =
                    @"function genScreenshot () {
	                        var canvasImgContentDecoded;
                            html2canvas(document.body).then(function(canvas) {
                                    window.canvasImgContentDecoded = canvas.toDataURL(""image/png"");
                            });
                        }
                        genScreenshot();";

                js.ExecuteScript(generateScreenshotJS);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
                wait.Until(
                    wd =>
                    {
                        string response = (string)js.ExecuteScript
                            ("return (typeof canvasImgContentDecoded === 'undefined' || canvasImgContentDecoded === null)");
                        if (string.IsNullOrEmpty(response))
                        {
                            return false;
                        }

                        return bool.Parse(response);
                    });
                wait.Until(wd => !string.IsNullOrEmpty((string)js.ExecuteScript("return canvasImgContentDecoded;")));
                var pngContent = (string)js.ExecuteScript("return canvasImgContentDecoded;");
                pngContent = pngContent.Replace("data:image/png;base64,", string.Empty);
                var tempFilePath = Path.GetTempFileName().Replace(".tmp", ".png");
                File.WriteAllBytes(tempFilePath, Convert.FromBase64String(pngContent));
            }
        }

        private string GetAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
