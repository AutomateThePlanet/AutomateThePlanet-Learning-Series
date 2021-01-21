﻿// <copyright file="BingMainPage.Map.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;

namespace ImprovedConfigureExecutionEngine.Pages.BingMain
{
    public partial class BingMainPage
    {
        public ISearch SearchBox
        {
            get
            {
                return ElementFinder.Find<ISearch>(By.Id("sb_form_q"));
            }
        }

        public IInputSubmit GoButton
        {
            get
            {
                return ElementFinder.Find<IInputSubmit>(By.Id("sb_form_go"));
            }
        }

        public IDiv ResultsCountDiv
        {
            get
            {
                return ElementFinder.Find<IDiv>(By.Id("b_tween"));
            }
        }
    }
}