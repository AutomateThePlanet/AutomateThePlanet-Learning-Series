using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Telerik.TestingFramework.Controls.KendoUI;
using TestStudio.Series.Tests.ControlsExtensions;
using TestStudio.Series.Tests.CustomControls;

namespace TestStudio.Series.Tests
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