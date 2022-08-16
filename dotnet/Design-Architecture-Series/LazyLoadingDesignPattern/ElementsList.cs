// <copyright file="ElementsList.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace LazyLoadingDesignPattern;

public class ElementsList : IElementsList
{
    private readonly By _by;
    private readonly ElementFinderService _elementFinder;
    private readonly IWebDriver _driver;

    public ElementsList(IWebDriver driver, By by)
    {
        _by = by;
        _elementFinder = new ElementFinderService(driver);
        _driver = driver;
    }

    public IElement this[int i] => GetAndWaitWebDriverElements().ElementAt(i);

    public IEnumerator<IElement> GetEnumerator() => GetAndWaitWebDriverElements().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count()
    {
        return _elementFinder.FindAll(_by).Count();
    }

    public void ForEach(Action<IElement> action)
    {
        foreach (var element in this)
        {
            action(element);
        }
    }

    private IEnumerable<IElement> GetAndWaitWebDriverElements()
    {
        var nativeElements = _elementFinder.FindAll(_by);
        foreach (var nativeElement in nativeElements)
        {
            IElement element = new ElementAdapter(_driver, _by);
            yield return element;
        }
    }
}
