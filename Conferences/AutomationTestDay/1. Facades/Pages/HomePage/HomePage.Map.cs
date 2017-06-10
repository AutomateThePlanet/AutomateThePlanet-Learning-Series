// <copyright file="HomePage.Map.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
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
using AutomationTestDay.Facades.Pages.Enums;
using OpenQA.Selenium;

namespace AutomationTestDay.Facades.Pages
{
    public partial class HomePage
    {
        public IWebElement GoToBlogButton
        {
            get
            {
                return _driver.FindElement(By.LinkText("Go to the blog"));
            }
        }

        public IWebElement GetFindHowButtonByText(string text)
        {
            string xpathLocator = string.Format("//div[contains(text(), '{0}')]/following-sibling::a", text);
            var findHow = _driver.FindElement(By.XPath(xpathLocator));
            return findHow;
        }

        public IWebElement GetFindHowButtonByTopic(Topic topic)
        {
            string xpathLocator = string.Format("//div[contains(text(), '{0}')]/following-sibling::a", GetTopicText(topic));
            var findHow = _driver.FindElement(By.XPath(xpathLocator));
            return findHow;
        }

        public IWebElement GetFindHowButtonByFramework(Framework framework)
        {
            string xpathLocator = string.Format("//div[contains(text(), '{0}')]/following-sibling::a", GetFrameworkText(framework));
            var findHow = _driver.FindElement(By.XPath(xpathLocator));
            return findHow;
        }

        private string GetFrameworkText(Framework framework)
        {
            string topicText = string.Empty;
            switch(framework)
            {
                case Framework.DotNet:
                    topicText = ".NET";
                    break;
                case Framework.WebDriver:
                    topicText = "WebDriver";
                    break;
                case Framework.Jenkins:
                    topicText = "Jenkins";
                    break;
                case Framework.SpecFlow:
                    topicText = "SpecFlow";
                    break;
                case Framework.TestingFramework:
                    topicText = "Testing Framework";
                    break;
                case Framework.TfsTestApi:
                    topicText = "TFS Test API";
                    break;
            }

            return topicText;
        }


        private string GetTopicText(Topic topic)
        {
            string topicText = string.Empty;
            switch(topic)
            {
                case Topic.DesignPattern:
                    topicText = "Design Patterns";
                    break;
                case Topic.DevOpsCi:
                    topicText = "DevOps and CI";
                    break;
                case Topic.DesignAndArchitecture:
                    topicText = "Design And Architecture";
                    break;
                case Topic.AutomationTools:
                    topicText = "Automation Tools";
                    break;
            }

            return topicText;
        }
    }
}
