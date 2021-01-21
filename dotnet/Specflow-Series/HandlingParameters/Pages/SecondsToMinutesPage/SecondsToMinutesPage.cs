﻿// <copyright file="KilowattHoursPage.cs" company="Automate The Planet Ltd.">
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

using HandlingParameters.Base;
using OpenQA.Selenium;

namespace HandlingParameters.Pages.SecondsToMinutesPage
{
    public partial class SecondsToMinutesPage : BasePage
    {
        public SecondsToMinutesPage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url
        {
            get
            {
                return "http://www.metric-conversions.org/time/seconds-to-minutes.htm";
            }
        }

        public void ConvertSecondsToMintes(double seconds)
        {
            SecondsInput.SendKeys(seconds.ToString());
            DriverWait.Until(drv => Answer != null);
        }
    }
}