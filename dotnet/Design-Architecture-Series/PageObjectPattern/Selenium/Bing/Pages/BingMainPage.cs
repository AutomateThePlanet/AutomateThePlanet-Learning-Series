﻿// <copyright file="BingMainPage.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectPattern.Selenium.Bing.Pages;

public class BingMainPage
{
    private readonly IWebDriver _driver;
    private readonly string _url = @"http://www.bing.com/";

    public BingMainPage(IWebDriver browser)
    {
        _driver = browser;
        PageFactory.InitElements(browser, this);
    }

    [FindsBy(How = How.Id, Using = "sb_form_q")]
    public IWebElement SearchBox { get; set; }

    [FindsBy(How = How.XPath, Using = "//label[@for='sb_form_go']")]
    public IWebElement GoButton { get; set; }

    [FindsBy(How = How.Id, Using = "b_tween")]
    public IWebElement ResultsCountDiv { get; set; }

    public void Navigate()
    {
        _driver.Navigate().GoToUrl(_url);
    }

    public void Search(string textToType)
    {
        SearchBox.Clear();
        SearchBox.SendKeys(textToType);
        GoButton.Click();
    }

    public void ValidateResultsCount(string expectedCount)
    {
        Assert.IsTrue(ResultsCountDiv.Text.Contains(expectedCount), "The results DIV doesn't contain the specified text.");
    }
}