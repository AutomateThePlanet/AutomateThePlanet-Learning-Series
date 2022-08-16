// <copyright file="BingMainPage.cs" company="Automate The Planet Ltd.">
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
// <site>http://automatetheplanet.com/</site>
using OpenQA.Selenium;

namespace ObserverDesignPatternClassicImplementation.Pages;

public class BingMainPage
{
    private readonly IWebDriver _browser;
    private readonly string _url = @"http://www.bing.com/";

    public BingMainPage(IWebDriver browser)
    {
        _browser = browser;
    }

    protected BingMainPageElementMap Map
    {
        get
        {
            return new BingMainPageElementMap(_browser);
        }
    }

    public BingMainPageValidator Validate()
    {
        return new BingMainPageValidator(_browser);
    }

    public void Navigate()
    {
        _browser.Navigate().GoToUrl(_url);
    }

    public void Search(string textToType)
    {
        Map.SearchBox.Clear();
        Map.SearchBox.SendKeys(textToType);
        Map.GoButton.Click();
    }
}