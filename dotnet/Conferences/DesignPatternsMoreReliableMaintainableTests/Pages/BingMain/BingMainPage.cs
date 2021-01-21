﻿// <copyright file="BingMainPage.cs" company="Automate The Planet Ltd.">
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

using System;
using DesignPatternsMoreReliableMaintainableTests.Base;
using OpenQA.Selenium;

namespace DesignPatternsMoreReliableMaintainableTests.Pages.BingMain
{
    public class BingMainPage : BasePage<BingMainPageMap>, IBingMainPage
    {
        public BingMainPage(IWebDriver driver) : base(driver, new BingMainPageMap(driver))
        {
        }

        public override string Url
        {
            get
            {
                return @"http://www.bing.com/";
            }
        }

        public void Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.GoButton.Click();
        }

        public int GetResultsCount()
        {
            int resultsCount = default(int);
            resultsCount = int.Parse(this.Map.ResultsCountDiv.Text);
            return resultsCount;
        }
    }
}