// <copyright file="HtmlSourceExceptionHandler.cs" company="Automate The Planet Ltd.">
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
using System.Linq;

namespace HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.ChainOfResponsibility
{
    public abstract class HtmlSourceExceptionHandler : Handler
    {
        public HtmlSourceExceptionHandler()
        {
        }

        protected abstract string TextToSearchInSource { get; }

        protected override bool IsApplicable(params object[] context)
        {
            var browser = (IBrowser)context.FirstOrDefault();
            if (browser == null)
            {
                throw new ArgumentNullException("The browser cannot be null!");
            }
            var result = browser.SourceString.Contains(TextToSearchInSource);
            return result;
        }
    }
}