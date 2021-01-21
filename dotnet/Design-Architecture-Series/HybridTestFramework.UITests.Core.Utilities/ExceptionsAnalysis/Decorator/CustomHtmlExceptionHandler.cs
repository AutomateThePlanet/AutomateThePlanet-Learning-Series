﻿// <copyright file="CustomHtmlExceptionHandler.cs" company="Automate The Planet Ltd.">
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

namespace HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator
{
    public class CustomHtmlExceptionHandler : HtmlSourceExceptionHandler
    {
        private readonly string _textToSearchInSource;
        private readonly string _detailedIssueExplanation;

        public CustomHtmlExceptionHandler(string textToSearchInSource, string detailedIssueExplanation)
        {
            this._textToSearchInSource = textToSearchInSource;
            this._detailedIssueExplanation = detailedIssueExplanation;
        }

        public CustomHtmlExceptionHandler()
        {
        }

        public override string DetailedIssueExplanation
        {
            get
            {
                return _detailedIssueExplanation;
            }
        }

        public override string TextToSearchInSource
        {
            get
            {
                return _textToSearchInSource;
            }
        }
    }
}