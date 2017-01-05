// <copyright file="ArticlesPage.cs" company="Automate The Planet Ltd.">
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

using CodeProjectStatisticsCalculator.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeProjectStatisticsCalculator.Pages.ItemPage
{
    public partial class ArticlesPage : BasePage
    {
        private string viewsRegex = @".*Views: (?<Views>[0-9,]{1,})";
        private string publishDateRegex = @".*Posted: (?<PublishDate>[0-9,A-Za-z ]{1,})";
        private readonly int profileId;
        public ArticlesPage(IWebDriver driver, int profileId) : base(driver)
        {
            this.profileId = profileId;
        }

        public override string Url
        {
            get
            {
                return string.Format("https://www.codeproject.com/script/Articles/MemberArticles.aspx?amid={0}", this.profileId);
            }
        }

        public void Navigate(string part)
        {
            base.Open(part);
        }

        public List<Article> GetArticlesByUrl(string sectionPart)
        {
            this.Navigate(sectionPart);
            var articlesInfos = new List<Article>();
            foreach (var articleRow in this.ArticlesRows.ToList())
            {
                if (!articleRow.Displayed)
                {
                    continue;
                }
                var article = new Article();
                var articleTitleElement = this.GetArticleTitleElement(articleRow);
                article.Title = articleTitleElement.GetAttribute("innerHTML");
                article.Url = articleTitleElement.GetAttribute("href");
                var articleStatisticsElement = this.GetArticleStatisticsElement(articleRow);

                string articleStatisticsElementSource = articleStatisticsElement.GetAttribute("innerHTML");
                if (!string.IsNullOrEmpty(articleStatisticsElementSource))
                {
                    article.Views = this.GetViewsCount(articleStatisticsElementSource);
                    article.PublishDate = this.GetPublishDate(articleStatisticsElementSource);
                }
                articlesInfos.Add(article);
            }

            return articlesInfos;
        }

        private double GetViewsCount(string articleStatisticsElementSource)
        {
            var regexViews = new Regex(viewsRegex, RegexOptions.Singleline);
            Match currentMatch = regexViews.Match(articleStatisticsElementSource);
            if (!currentMatch.Success)
            {
                throw new ArgumentException("No content for the current statistics element.");
            }
            return double.Parse(currentMatch.Groups["Views"].Value);
        }

        private DateTime GetPublishDate(string articleStatisticsElementSource)
        {
            var regexPublishDate = new Regex(publishDateRegex, RegexOptions.IgnorePatternWhitespace);
            Match currentMatch = currentMatch = regexPublishDate.Match(articleStatisticsElementSource);
            if (!currentMatch.Success)
            {
                throw new ArgumentException("No content for the current statistics element.");
            }
            return DateTime.Parse(currentMatch.Groups["PublishDate"].Value);
        }
    }
}