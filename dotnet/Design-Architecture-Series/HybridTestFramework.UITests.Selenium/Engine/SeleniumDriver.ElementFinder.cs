﻿// <copyright file="seleniumdriver.elementfinder.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : IElementFinder
    {
        public TElement Find<TElement>(By by) where TElement : class, Core.Controls.IElement
        {
            return _elementFinderService.Find<TElement>(_driver, by);
        }

        public IEnumerable<TElement> FindAll<TElement>(By by) where TElement : class, Core.Controls.IElement
        {
            return _elementFinderService.FindAll<TElement>(_driver, by);
        }

        public bool IsElementPresent(By by)
        {
            return _elementFinderService.IsElementPresent(_driver, by);
        }
    }
}