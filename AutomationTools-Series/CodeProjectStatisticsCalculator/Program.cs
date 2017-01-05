// <copyright file="Program.cs" company="Automate The Planet Ltd.">
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

using CodeProjectStatisticsCalculator.Pages.ItemPage;
using CsvHelper;
using Fclp;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeProjectStatisticsCalculator
{
    class Program
    {
        private static string filePath = string.Empty;
        private static string yearInput = string.Empty;
        private static int year = -1;
        private static int profileId = 0;
        private static string profileIdInput = string.Empty;
        private static List<Article> articlesInfos;
        static void Main(string[] args)
        {
            var commandLineParser = new FluentCommandLineParser();

            commandLineParser.Setup<string>('p', "path").Callback(s => filePath = s);
            commandLineParser.Setup<string>('y', "year").Callback(y => yearInput = y);
            commandLineParser.Setup<string>('i', "profileId").Callback(p => profileIdInput = p);
            commandLineParser.Parse(args);

            bool isProfileIdCorrect = int.TryParse(profileIdInput, out profileId);
            if (string.IsNullOrEmpty(profileIdInput) || !isProfileIdCorrect)
            {
                Console.WriteLine("Please specify a correct profileId.");
                return;
            }
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Please specify a correct file path.");
                return;
            }
            if (!string.IsNullOrEmpty(yearInput))
            {
                bool isYearCorrect = int.TryParse(yearInput, out year);
                if (!isYearCorrect)
                {
                    Console.WriteLine("Please specify a correct year!");
                    return;
                }
            }
            articlesInfos = GetAllArticlesInfos();
            if (year == -1)
            {
                CreateReportAllTime();
            }
            else
            {
                CreateReportYear();
            }
           

            Console.WriteLine("Total VIEWS: {0}", articlesInfos.Sum(x => x.Views));
            Console.ReadLine();
        }

        private static void CreateReportAllTime()
        {
            TextWriter textWriter = new StreamWriter(filePath);
            var csv = new CsvWriter(textWriter);
            csv.WriteRecords(articlesInfos.OrderByDescending(x => x.Views));
        }

        private static void CreateReportYear()
        {
            TextWriter currentYearTextWriter = new StreamWriter(filePath);
            var csv = new CsvWriter(currentYearTextWriter);
            csv.WriteRecords(articlesInfos.Where(x => x.PublishDate.Year.Equals(year)).OrderByDescending(x => x.Views));
        }

        private static List<Article> GetAllArticlesInfos()
        {
            var articlesInfos = new List<Article>();
            using (var driver = new ChromeDriver())
            {
                var articlePage = new ArticlesPage(driver, profileId);
                articlesInfos.AddRange(articlePage.GetArticlesByUrl("#Articles"));
            }
            using (var driver = new ChromeDriver())
            {
                var articlePage = new ArticlesPage(driver, profileId);
                articlesInfos.AddRange(articlePage.GetArticlesByUrl("#TechnicalBlog"));
            }
            using (var driver = new ChromeDriver())
            {
                var articlePage = new ArticlesPage(driver, profileId);
                articlesInfos.AddRange(articlePage.GetArticlesByUrl("#Tip"));
            }

            return articlesInfos;
        }
    }
}
