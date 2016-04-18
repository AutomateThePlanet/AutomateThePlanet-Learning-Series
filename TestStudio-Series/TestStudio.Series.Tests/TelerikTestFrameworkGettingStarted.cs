// <copyright file="TelerikTestFrameworkGettingStarted.cs" company="Automate The Planet Ltd.">
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
using ArtOfTest.WebAii.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestStudio.Series.Tests
{
    [TestClass]
    public class TelerikTestFrameworkGettingStarted
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

            ////////When set to true, one instance of the browser will be launched and recycled through out the entire test class and tests until Manager. Dispose is called.	
            ////manager.Settings.Web.RecycleBrowser = true;
            ////////Controls whether or not to highlight/annotate the target elements that the requested action is being executed against. e.g. Actions.Click(goButton);
            ////manager.Settings.AnnotateExecution = true;

            ////////Sets the base URL to use for all NavigateTo() commands. When set NavigateTo() should use a relative URL (e.g. "~/default.aspx").
            ////manager.Settings.Web.BaseUrl = "http://automatetheplanet.com/";

            ////////Gets or sets whether to make sure the browser process is killed when closing the browser. Note: Firefox is a single process browser. If you are using multiple browser instances and this setting is on, it will kill all open instances of Firefox.
            ////manager.Settings.Web.KillBrowserProcessOnClose = true;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            manager.Dispose();
        }

        [TestMethod]
        public void NavigateToAutomateThePlanet()
        {
            manager.ActiveBrowser.NavigateTo("http://automatetheplanet.com/");
        }

        [TestMethod]
        public void FindElementsAutomateThePlanet()
        {
            manager.ActiveBrowser.NavigateTo("http://automatetheplanet.com/");
            HtmlAnchor anchorById = manager.ActiveBrowser.Find.ById<HtmlAnchor>("uniqueId");
            HtmlInputText textInputByName = manager.ActiveBrowser.Find.ByName<HtmlInputText>("textInputName");
            HtmlDiv divByClass = manager.ActiveBrowser.Find.ByAttributes<HtmlDiv>("class=myclass");
            IList<HtmlDiv> divsByClass = manager.ActiveBrowser.Find.AllByAttributes<HtmlDiv>("class=myclass");
            // Find element with TextContent has literal value: Automate The Planet
            // l: signifies literal
            HtmlSpan spanByContent = manager.ActiveBrowser.Find.ByContent<HtmlSpan>("l:Automate The Planet", FindContentType.InnerText);

            // Find element with TextContent has patrial value: Automate
            // p: signifies partial
            spanByContent = manager.ActiveBrowser.Find.ByContent<HtmlSpan>("p:Automate");

            HtmlSelect selectByXpath = manager.ActiveBrowser.Find.ByXPath<HtmlSelect>("/html/body/select");

            IList<Element> allImages = manager.ActiveBrowser.Find.AllByTagName("img");

            // Return all elements matching HTMLFindExpression
            var allSams = manager.ActiveBrowser.Find.AllByExpression("class=myclass", "textcontent=!Planet");
        }

        [TestMethod]
        public void TelerikTestStudioFrameworkBasicActions()
        {
            manager.ActiveBrowser.NavigateTo("http://automatetheplanet.com/healthy-diet-menu-generator/");
            HtmlInputSubmit generateButton = manager.ActiveBrowser.Find.ByName<HtmlInputSubmit>("_ninja_forms_field_28");
            HtmlInputCheckBox additionalSugarCheckbox = manager.ActiveBrowser.Find.ById<HtmlInputCheckBox>("ninja_forms_field_18");
            HtmlInputText firstNameTextInput = manager.ActiveBrowser.Find.ByXPath<HtmlInputText>("//*[@id='ninja_forms_field_23']");
            HtmlSelect burgersSelect = manager.ActiveBrowser.Find.ByName<HtmlSelect>("ninja_forms_field_21");
            HtmlInputRadioButton coffeeRadioButton = manager.ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("value=^1 x Trenta");


            coffeeRadioButton.Check(isChecked: true, invokeOnChange: true, invokeOnClickChanged: true);
            burgersSelect.SelectByText("10 x Double Cheeseburgers");
            firstNameTextInput.Text = "Anton";
            additionalSugarCheckbox.Check(isChecked: true, invokeOnChange: true, invokeOnClickChanged: true);
            generateButton.Click();
        }

        [TestMethod]
        public void AssertRequiredFieldValidation()
        {
            manager.ActiveBrowser.NavigateTo("http://automatetheplanet.com/healthy-diet-menu-generator/");
            HtmlInputSubmit generateButton = manager.ActiveBrowser.Find.ByName<HtmlInputSubmit>("_ninja_forms_field_28");
            generateButton.Click();
            var requiredField = manager.ActiveBrowser.Find.ByXPath<HtmlContainerControl>("//*[@id='ninja_forms_field_18_error']/p");

            requiredField.AssertContent().InnerText(ArtOfTest.Common.StringCompareType.Exact, "This is a required field");
        }
    }
}