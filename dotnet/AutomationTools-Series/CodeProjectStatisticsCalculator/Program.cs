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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeProjectStatisticsCalculator.Pages.ItemPage;
using CsvHelper;
using Fclp;
using OpenQA.Selenium.Chrome;

namespace CodeProjectStatisticsCalculator
{
    class Program
    {
        private static string _filePath = string.Empty;
        private static string _yearInput = string.Empty;
        private static int _year = -1;
        private static int _profileId = 0;
        private static string _profileIdInput = string.Empty;
        private static List<Article> _articlesInfos;

        static void Main(string[] args)
        {
            var commandLineParser = new FluentCommandLineParser();

            commandLineParser.Setup<string>('p', "path").Callback(s => _filePath = s);
            commandLineParser.Setup<string>('y', "year").Callback(y => _yearInput = y);
            commandLineParser.Setup<string>('i', "profileId").Callback(p => _profileIdInput = p);
            commandLineParser.Parse(args);

            var isProfileIdCorrect = int.TryParse(_profileIdInput, out _profileId);
            if (string.IsNullOrEmpty(_profileIdInput) || !isProfileIdCorrect)
            {
                Console.WriteLine("Please specify a correct profileId.");
                return;
            }
            if (string.IsNullOrEmpty(_filePath))
            {
                Console.WriteLine("Please specify a correct file path.");
                return;
            }
            if (!string.IsNullOrEmpty(_yearInput))
            {
                var isYearCorrect = int.TryParse(_yearInput, out _year);
                if (!isYearCorrect)
                {
                    Console.WriteLine("Please specify a correct year!");
                    return;
                }
            }
            _articlesInfos = GetAllArticlesInfos();
            if (_year == -1)
            {
                CreateReportAllTime();
            }
            else
            {
                CreateReportYear();
            }
           

            Console.WriteLine("Total VIEWS: {0}", _articlesInfos.Sum(x => x.Views));
            Console.ReadLine();
        }

        private static void CreateReportAllTime()
        {
            TextWriter textWriter = new StreamWriter(_filePath);
            var csv = new CsvWriter(textWriter);
            csv.WriteRecords(_articlesInfos.OrderByDescending(x => x.Views));
        }

        private static void CreateReportYear()
        {
            TextWriter currentYearTextWriter = new StreamWriter(_filePath);
            var csv = new CsvWriter(currentYearTextWriter);
            csv.WriteRecords(_articlesInfos.Where(x => x.PublishDate.Year.Equals(_year)).OrderByDescending(x => x.Views));
        }

        private static List<Article> GetAllArticlesInfos()
        {
            var articlesInfos = new List<Article>();
            using (var driver = new ChromeDriver())
            {
                var articlePage = new ArticlesPage(driver, _profileId);
                articlesInfos.AddRange(articlePage.GetArticlesByUrl("#Articles"));
            }
            using (var driver = new ChromeDriver())
            {
                var articlePage = new ArticlesPage(driver, _profileId);
                articlesInfos.AddRange(articlePage.GetArticlesByUrl("#TechnicalBlog"));
            }
            using (var driver = new ChromeDriver())
            {
                var articlePage = new ArticlesPage(driver, _profileId);
                articlesInfos.AddRange(articlePage.GetArticlesByUrl("#Tip"));
            }

            return articlesInfos;
        }
    }
}
