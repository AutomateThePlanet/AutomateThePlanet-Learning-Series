// <copyright file="ItemPage.cs" company="Automate The Planet Ltd.">
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
using System;
using OpenQA.Selenium;
using PerfectSystemTestsDesign.Base;

namespace PerfectSystemTestsDesign.Pages.ItemPage;

public partial class ItemPage : BasePage
{
    public ItemPage(IWebDriver driver) : base(driver)
    {
    }

    public override string Url
    {
        get
        {
            return "http://www.amazon.com/";
        }
    }

    public void ClickBuyNowButton()
    {
        AddToCartButton.Click();
    }

    public void Navigate(string part)
    {
        ///Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743
        Open(part);
    }
}