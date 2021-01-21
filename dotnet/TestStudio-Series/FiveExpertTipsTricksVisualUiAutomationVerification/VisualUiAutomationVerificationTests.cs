// <copyright file="VisualUiAutomationVerificationTests.cs" company="Automate The Planet Ltd.">
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
using ArtOfTest.Common;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Win32;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace FiveExpertTipsTricksVisualUiAutomationVerification
{
    [TestClass]
    public class VisualUiAutomationVerificationTests
    {
        private Manager manager;

        [TestInitialize]
        public void TestInitialize()
        {
            Settings mySettings = new Settings();
            mySettings.Web.DefaultBrowser = BrowserType.FireFox;
            manager = new Manager(mySettings);
            manager.Start();
            manager.LaunchNewBrowser();
            manager.Settings.Web.RecycleBrowser = true;       
            manager.Settings.Web.KillBrowserProcessOnClose = true;

            manager.Settings.AnnotateExecution = true;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            manager.Dispose();
        }

        // 1. Extract an individual HTML Attribute - http://docs.telerik.com/teststudio/advanced-topics/coded-samples/html/extract-an-attribute
        [TestMethod]
        public void ExtractIndividualHtmlAttribute()
        {
            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");
            HtmlImage healthyBurgerImage = 
                manager.ActiveBrowser.Find.ByXPath<HtmlImage>(
                "/html/body/div[1]/div[3]/section/article/div/div[2]/a/img");
            string altAttributeValue = 
                healthyBurgerImage.Attributes.Single(x => x.Name == "alt").Value;
            Debug.WriteLine(altAttributeValue);
        }

        // 2. Annotator http://docs.telerik.com/teststudio/testing-framework/write-tests-in-code/intermediate-topics-wtc/Annotator
        // Requires reference to System.Drawing
        [TestMethod]
        public void PlayWithAnnotator()
        {
            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");
            HtmlImage healthyBurgerImage = manager.ActiveBrowser.Find.ByXPath<HtmlImage>(
                "/html/body/div[1]/div[3]/section/article/div/div[2]/a/img");
            HtmlInputCheckBox additionalSugarCheckbox = 
                manager.ActiveBrowser.Find.ById<HtmlInputCheckBox>("ninja_forms_field_18");
            additionalSugarCheckbox.Check(
                isChecked: true, 
                invokeOnChange: true, 
                invokeOnClickChanged: true);

            // Add rectangle around element + custom message
            Annotator annotator = new Annotator(manager.ActiveBrowser);
            annotator.Annotate(
                healthyBurgerImage.GetRectangle(), 
                "This is the most healthy meal EVER! Honestly!");
          
        }

        // 3. Visual Capturing http://docs.telerik.com/teststudio/testing-framework/write-tests-in-code/intermediate-topics-wtc/visual-capturing
        [TestMethod]
        public void VisualCapturing()
        {
            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");
            HtmlDiv mainArticleDiv = 
                manager.ActiveBrowser.Find.ByXPath<HtmlDiv>(
                "/html/body/div[1]/div[3]/section/article/div");
            System.Drawing.Bitmap browserImage = manager.ActiveBrowser.Window.GetBitmap();
            System.Drawing.Bitmap divimage = manager.ActiveBrowser.Window.GetBitmap(mainArticleDiv.GetRectangle());
            string browserImagePath =
              Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Concat(Guid.NewGuid().ToString(), @".bmp"));       
            browserImage.Save(browserImagePath);
            string mainDivImagePath =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Concat(Guid.NewGuid().ToString(), @".bmp"));
            divimage.Save(mainDivImagePath);
        }

        // 4. Verify HTML Style - http://docs.telerik.com/teststudio/advanced-topics/coded-samples/html/verify-style
        [TestMethod]
        public void VerifyHtmlStyles()
        {
            manager.ActiveBrowser.NavigateTo(
                  "http://automatetheplanet.com/healthy-diet-menu-generator/");
            HtmlImage healthyBurgerImage = manager.ActiveBrowser.Find.ByXPath<HtmlImage>(
                "/html/body/div[1]/div[3]/section/article/div/div[2]/a/img");
            HtmlControl mainHeadline =
                manager.ActiveBrowser.Find.ByXPath<HtmlControl>("/html/body/div[1]/div[2]/div/h1");
            mainHeadline.AssertStyle().Font(
                ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts.HtmlStyleFont.Family, 
                "Lato,sans-serif", 
                ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts.HtmlStyleType.Computed, 
                ArtOfTest.Common.StringCompareType.Exact);
            mainHeadline.AssertStyle().Font(
                ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts.HtmlStyleFont.Size, 
                "33px", 
                ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts.HtmlStyleType.Computed,
                ArtOfTest.Common.StringCompareType.Exact);
            mainHeadline.AssertStyle().ColorAndBackground(
                ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts.HtmlStyleColorAndBackground.Color, 
                "#000000", 
                ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts.HtmlStyleType.Computed, 
                ArtOfTest.Common.StringCompareType.Exact);
            mainHeadline.AssertStyle().ColorAndBackground(
                ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts.HtmlStyleColorAndBackground.BackgroundColor,
                "#FFFFFF", 
                ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts.HtmlStyleType.Computed, 
                ArtOfTest.Common.StringCompareType.Exact);
        }

        // 5. Image Comparison in Code - http://docs.telerik.com/teststudio/advanced-topics/coded-samples/html/image-comparison
        [TestMethod]
        public void ImageComparison()
        {
            manager.ActiveBrowser.NavigateTo(
                "http://automatetheplanet.com/healthy-diet-menu-generator/");
            HtmlDiv mainArticleDiv =
                manager.ActiveBrowser.Find.ByXPath<HtmlDiv>(
                "/html/body/div[1]/div[3]/section/article/div");
            System.Drawing.Bitmap divimage = manager.ActiveBrowser.Window.GetBitmap(mainArticleDiv.GetRectangle());

            Bitmap expectedbmp = (Bitmap)Image.FromFile("mainArticleDivExpected.bmp", true);
            PixelMap expected = PixelMap.FromBitmap(expectedbmp);
            PixelMap actual = PixelMap.FromBitmap(divimage);

            Assert.IsTrue(expected.Compare(actual, 5.0));
        }
    }
}
