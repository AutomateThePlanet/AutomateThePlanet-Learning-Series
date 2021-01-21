﻿// <copyright file="BingMainPageMap.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using PageObjectsThatMakeCodeMoreMaintainable.PageObjectv21.Base;

namespace PageObjectsThatMakeCodeMoreMaintainable.PageObjectv21
{
    public class BingMainPageMap : BaseElementMap
    {
        public BingMainPageMap(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement SearchBox 
        {
            get
            {
                return Driver.FindElement(By.Id("sb_form_q"));
            }
        }

        public IWebElement GoButton 
        {
            get
            {
                return Driver.FindElement(By.Id("sb_form_go"));
            }
        }
       
        public IWebElement ResultsCountDiv
        {
            get
            {
                return Driver.FindElement(By.Id("b_tween"));
            }
        }

        public IWebElement FeelingLuckyButton
        {
            get
            {
                return Driver.FindElement(By.LinkText("I'm Feeling Lucky"));
            }
        }
    }
}