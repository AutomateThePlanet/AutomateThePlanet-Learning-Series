﻿using HybridTestFramework.UITests.Core.Events;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator.Interfaces;
// <copyright file="ExceptionAnalyzedNavigationService.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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

namespace HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator
{
    public class ExceptionAnalyzedNavigationService : INavigationService
    {
        public ExceptionAnalyzedNavigationService(INavigationService navigationService, IUiExceptionAnalyser exceptionAnalyser)
        {
            NavigationService = navigationService;
            UiExceptionAnalyser = exceptionAnalyser;
        }

        public ExceptionAnalyzedNavigationService(ExceptionAnalyzedNavigationService exceptionAnalyzedNavigationService)
        {
            UiExceptionAnalyser = exceptionAnalyzedNavigationService.UiExceptionAnalyser;
            NavigationService = exceptionAnalyzedNavigationService.NavigationService;
        }

        public ExceptionAnalyzedNavigationService(ExceptionAnalyzedNavigationService exceptionAnalyzedNavigationService, IUiExceptionAnalyser exceptionAnalyser)
        {
            UiExceptionAnalyser = exceptionAnalyser;
            NavigationService = exceptionAnalyzedNavigationService.NavigationService;
        }

        public IUiExceptionAnalyser UiExceptionAnalyser { get; private set; }

        public INavigationService NavigationService { get; private set; }

        public event EventHandler<PageEventArgs> Navigated;

        public string Url
        {
            get
            {
                return NavigationService.Url;
            }
        }

        public string Title
        {
            get
            {
                return NavigationService.Title;
            }
        }

        public void Navigate(string relativeUrl, string currentLocation, bool sslEnabled = false)
        {
            NavigationService.Navigate(relativeUrl, currentLocation, sslEnabled);
        }

        public void NavigateByAbsoluteUrl(string absoluteUrl, bool useDecodedUrl = true)
        {
            NavigationService.NavigateByAbsoluteUrl(absoluteUrl, useDecodedUrl);
        }

        public void Navigate(string currentLocation, bool sslEnabled = false)
        {
            NavigationService.Navigate(currentLocation, sslEnabled);
        }

        public void WaitForUrl(string url)
        {
            try
            {
                NavigationService.WaitForUrl(url);
            }
            catch (Exception ex)
            {
                UiExceptionAnalyser.Analyse(ex, NavigationService);
                throw;
            }
        }

        public void WaitForPartialUrl(string url)
        {
            try
            {
                NavigationService.WaitForPartialUrl(url);
            }
            catch (Exception ex)
            {
                UiExceptionAnalyser.Analyse(ex, NavigationService);
                throw;
            }
        }
    }
}