// <copyright file="CustomControlsTests.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using System.ComponentModel;
using AdvancedWebUiComponentsAutomation;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
namespace AdvancedWebUIComponentsAutomation;

[TestFixture]
public class CustomControlsTests
{
    private IWebDriver _driver;

    [SetUp]
    public void TestInit()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TestCleanup()
    {
        _driver.Quit();
    }

    [Test]
    public void SetDateKendoDatePickerCustomControl()
    {
        _driver.Navigate().GoToUrl("http://demos.telerik.com/kendo-ui/datepicker/index");
        var datePicket = new KendoDatePicker(_driver, "datepicker");
        datePicket.SetDate(DateTime.Now);
    }

    [Test]
    public void SetValueGaugeNeedleCustomControl()
    {
        _driver.Navigate().GoToUrl("http://www.igniteui.com/radial-gauge/gauge-needle");
        var gaugeNeedle = new GaugeNeedle(_driver, "radialgauge");
        gaugeNeedle.SetValue(44);
    }

    [Test]
    public void TestMethodsFullCalendarCustomControl()
    {
        _driver.Navigate().GoToUrl("https://fullcalendar.io/docs/v3/month-view-demo");
        var fullCalendar = new FullCalendar(_driver, "calendar");
        fullCalendar.ClickNextButton();
        fullCalendar.ClickPreviousButton();
        fullCalendar.GoToDate(new DateTime(2012, 11, 28));
        fullCalendar.GoToToday();
    }

    [Test]
    public void SetColorKendoColorPickerCustomControl()
    {
        _driver.Navigate().GoToUrl("http://demos.telerik.com/kendo-ui/colorpicker/index");
        var kendoColorPicker = new KendoColorPicker(_driver, "picker");
        kendoColorPicker.SetColor("ccc");
    }
}
