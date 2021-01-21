﻿// <copyright file="SBingMainPage.Elements.cs" company="Automate The Planet Ltd.">
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
// <site>https://automatetheplanet.com/</site>
using OpenQA.Selenium;

namespace HuddlePageObjectsPartialClassesSingleton.SingletonBingMainPage
{
    public partial class SBingMainPage
    {
        public IWebElement SearchBox => WrappedDriver.FindElement(By.Id("sb_form_q"));

        public IWebElement GoButton => WrappedDriver.FindElement(By.Id("sb_form_go"));

        public IWebElement ResultsCountDiv => WrappedDriver.FindElement(By.Id("b_tween"));
    }
}
