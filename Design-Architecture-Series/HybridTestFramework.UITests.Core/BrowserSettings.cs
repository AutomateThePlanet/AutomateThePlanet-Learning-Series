// <copyright file="BrowserSettings.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.UITests.Core.Enums;

namespace HybridTestFramework.UITests.Core
{
    public sealed class BrowserSettings
    {
        private BrowserSettings(Browsers type)
        {
            this.Type = type;
        }

        public static BrowserSettings DefaultChomeSettings
        {
            get
            {
                return new BrowserSettings(Browsers.Chrome)
                {
                    BrowserExeDirectory = string.Empty,
                    PageLoadTimeout = 60,
                    ScriptTimeout = 60,
                    ElementsWaitTimeout = 60
                };
            }
        }

        public static BrowserSettings DefaultFirefoxSettings
        {
            get
            {
                return new BrowserSettings(Browsers.Firefox)
                {
                    BrowserExeDirectory = Environment.CurrentDirectory,
                    PageLoadTimeout = 60,
                    ScriptTimeout = 60,
                    ElementsWaitTimeout = 60
                };
            }
        }

        public static BrowserSettings DefaultInternetExplorerSettings
        {
            get
            {
                return new BrowserSettings(Browsers.InternetExplorer)
                {
                    BrowserExeDirectory = Environment.CurrentDirectory,
                    PageLoadTimeout = 60,
                    ScriptTimeout = 60,
                    ElementsWaitTimeout = 60
                };
            }
        }

        public Browsers Type { get; private set; }

        public int ScriptTimeout { get; set; }

        public int PageLoadTimeout { get; set; }

        public int ElementsWaitTimeout { get; set; }

        public string BrowserExeDirectory { get; set; }
    }
}