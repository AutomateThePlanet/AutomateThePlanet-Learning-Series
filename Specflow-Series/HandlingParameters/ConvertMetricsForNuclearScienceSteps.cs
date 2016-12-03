// <copyright file="ConvertMetricsForNuclearScienceSteps.cs" company="Automate The Planet Ltd.">
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

using HandlingParameters.Pages.CelsiusFahrenheitPage;
using HandlingParameters.Pages.HomePage;
using HandlingParameters.Pages.SecondsToMinutesPage;
using Microsoft.Practices.Unity;
using System;
using TechTalk.SpecFlow;

namespace HandlingParameters
{
    [Binding]
    public class ConvertMetricsForNuclearScienceSteps
    {
        private readonly HomePage homePage;
        private readonly KilowattHoursPage kilowattHoursPage;
        private readonly SecondsToMinutesPage secondsToMinutesPage;

        public ConvertMetricsForNuclearScienceSteps()
        {
            this.homePage =
                HandlingParameters.Base.UnityContainerFactory.GetContainer().Resolve<HomePage>();
            this.kilowattHoursPage =
                HandlingParameters.Base.UnityContainerFactory.GetContainer().Resolve<KilowattHoursPage>();
            this.secondsToMinutesPage =
                HandlingParameters.Base.UnityContainerFactory.GetContainer().Resolve<SecondsToMinutesPage>();
        }

        [When(@"I navigate to Metric Conversions")]
        public void WhenINavigateToMetricConversions_()
        {
            this.homePage.Open();
        }

        [When(@"navigate to Energy and power section")]
        public void WhenNavigateToEnergyAndPowerSection()
        {
            this.homePage.EnergyAndPowerAnchor.Click();
        }

        [When(@"I navigate to Seconds to Minutes Page")]
        public void WhenINavigateToSecondsToMinutesPage()
        {
            this.secondsToMinutesPage.Open();
        }

        [When(@"navigate to Kilowatt-hours")]
        public void WhenNavigateToKilowatt_Hours()
        {
            this.homePage.KilowattHours.Click();
        }

        [When(@"choose conversions to Newton-meters")]
        public void WhenChooseConversionsToNewton_Meters()
        {
            this.kilowattHoursPage.KilowatHoursToNewtonMetersAnchor.Click();
        }

        [When(@"type (.*) kWh")]
        public void WhenTypeKWh(double kWh)
        {
            this.kilowattHoursPage.ConvertKilowattHoursToNewtonMeters(kWh);
        }

        [When(@"type (.*) kWh in (.*) format")]
        public void WhenTypeKWhInFormat(double kWh, Format format)
        {
            this.kilowattHoursPage.ConvertKilowattHoursToNewtonMeters(kWh, format);
        }

        [Then(@"assert that (.*) Nm are displayed as answer")]
        public void ThenAssertThatENmAreDisplayedAsAnswer(string expectedNewtonMeters)
        {
            this.kilowattHoursPage.AssertFahrenheit(expectedNewtonMeters);
        }

        [When(@"type seconds for (.*)")]
        public void WhenTypeSeconds(TimeSpan seconds)
        {
            this.secondsToMinutesPage.ConvertSecondsToMintes(seconds.TotalSeconds);
        }

        [Then(@"assert that (.*) minutes are displayed as answer")]
        public void ThenAssertThatSecondsAreDisplayedAsAnswer(int expectedMinutes)
        {
            this.secondsToMinutesPage.AssertMinutes(expectedMinutes.ToString());
        }

        [StepArgumentTransformation(@"(?:(\d*) day(?:s)?(?:, )?)?(?:(\d*) hour(?:s)?(?:, )?)?(?:(\d*) minute(?:s)?(?:, )?)?(?:(\d*) second(?:s)?(?:, )?)?")]
        public TimeSpan TimeSpanTransform(string days, string hours, string minutes, string seconds)
        {
            int daysParsed;
            int hoursParsed;
            int minutesParsed;
            int secondsParsed;

            int.TryParse(days, out daysParsed);
            int.TryParse(hours, out hoursParsed);
            int.TryParse(minutes, out minutesParsed);
            int.TryParse(seconds, out secondsParsed);

            return new TimeSpan(daysParsed, hoursParsed, minutesParsed, secondsParsed);
        }
    }
}