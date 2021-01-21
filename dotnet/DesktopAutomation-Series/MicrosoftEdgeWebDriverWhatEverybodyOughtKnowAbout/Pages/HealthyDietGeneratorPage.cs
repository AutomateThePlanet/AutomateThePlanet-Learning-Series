// <copyright file="HealthyDietGeneratorPage.cs" company="Automate The Planet Ltd.">
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
using SeleniumExtras.PageObjects;

namespace MicrosoftEdgeWebDriverWhatEverybodyOughtKnowAbout.Pages
{
    public class HealthyDietGeneratorPage
    {
        public readonly string Url = @"https://automatetheplanet.com/healthy-diet-menu-generator/";

        public HealthyDietGeneratorPage(IWebDriver browser)
        {
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_18")]
        public IWebElement AddAdditionalSugarCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_19_1")]
        public IWebElement VentiCoffeeRadioButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ninja_forms_field_21']")]
        public IWebElement BurgersDropDown { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_27_2")]
        public IWebElement SmotheredChocolateCakeCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_22")]
        public IWebElement AddSomethingToDietTextArea { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ninja_forms_field_20_div_wrap']/span/div[11]/a")]
        public IWebElement RockStarRating { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_23")]
        public IWebElement FirstNameTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_24")]
        public IWebElement LastNameTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_25")]
        public IWebElement EmailTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_28")]
        public IWebElement AwsomeDietSubmitButton { get; set; }
    }
}