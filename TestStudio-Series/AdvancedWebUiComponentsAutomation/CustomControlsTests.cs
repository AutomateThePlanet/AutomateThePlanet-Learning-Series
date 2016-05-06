// <copyright file="CustomControlsTests.cs" company="Automate The Planet Ltd.">
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
using AdvancedWebUiComponentsAutomation.ControlsExtensions;
using AdvancedWebUiComponentsAutomation.CustomControls;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Telerik.TestingFramework.Controls.KendoUI;

namespace AdvancedWebUiComponentsAutomation
{
    [TestClass]
    public class CustomControlsTests
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
        }

        [TestCleanup]
        public void TestCleanup()
        {
            manager.Dispose();
        }

        [TestMethod]
        public void SimulateRealTextTypingHtmlInputExtension()
        {
            manager.ActiveBrowser.NavigateTo("http://automatetheplanet.com/healthy-diet-menu-generator/");
            HtmlInputText firstNameTextInput = manager.ActiveBrowser.Find.ByXPath<HtmlInputText>("//*[@id='ninja_forms_field_23']");

            firstNameTextInput.SimulateRealTextTyping("Anton");
        }

        [TestMethod]
        public void SelectItemKendoComboxWithJavaScriptExtensionMethod()
        {
            manager.ActiveBrowser.NavigateTo("http://demos.telerik.com/kendo-ui/combobox/events");
            HtmlInputText htmlInputText = manager.ActiveBrowser.Find.ByXPath<HtmlInputText>("//*[@id='example']/div[1]/span/span/input");
            KendoComboBox kendoComboBox = new KendoComboBox(htmlInputText);
            kendoComboBox.SelectItemByText("Item 2");
        }

        [TestMethod]
        public void SelectItemKendoComboxWithDefaultTelerikTestFrameworkMethod()
        {
            manager.ActiveBrowser.NavigateTo("http://demos.telerik.com/kendo-ui/combobox/events");
            HtmlSpan kendoInput = manager.ActiveBrowser.Find.ByXPath<HtmlSpan>("//*[@id='example']/div[1]/span/span/span/span");
            kendoInput.Click();
            KendoListBox kendoListBox = manager.ActiveBrowser.Find.ById<KendoListBox>("combobox_listbox");
            kendoListBox.SelectItem("Item 2");
        }

        [TestMethod]
        public void SetDateKendoDatePickerCustomControl()
        {
            manager.ActiveBrowser.NavigateTo("http://demos.telerik.com/kendo-ui/datepicker/index");
            KendoDatePicker datePicket = new KendoDatePicker("datepicker");
            datePicket.SetDate(DateTime.Now);
        }

        [TestMethod]
        public void SetValueGaugeNeedleCustomControl()
        {
            manager.ActiveBrowser.NavigateTo("http://www.igniteui.com/radial-gauge/gauge-needle");
            GaugeNeedle gaugeNeedle = new GaugeNeedle("radialgauge");
            gaugeNeedle.SetValue(44);
        }

        [TestMethod]
        public void TestMethodsFullCalendarCustomControl()
        {
            manager.ActiveBrowser.NavigateTo("http://fullcalendar.io/");
            FullCalendar fullCalendar = new FullCalendar("calendar");
            fullCalendar.ClickNextButton();
            fullCalendar.ClickPreviousButton();
            fullCalendar.GoToDate(new DateTime(2012, 11, 28));
            fullCalendar.GoToToday();
        }

        [TestMethod]
        public void SetColorKendoColorPickerCustomControl()
        {
            manager.ActiveBrowser.NavigateTo("http://demos.telerik.com/kendo-ui/colorpicker/index");
            KendoColorPicker kendoColorPicker = new KendoColorPicker("colorpicker");
            kendoColorPicker.SetColor("ccc");
        }
    }
}