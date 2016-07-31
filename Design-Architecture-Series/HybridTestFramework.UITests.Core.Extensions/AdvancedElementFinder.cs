// <copyright file="AdvancedElementFinder.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core.Controls;

namespace HybridTestFramework.UITests.Core.Extensions
{
    public static class AdvancedElementFinder
    {
        public static TElement FindByIdEndingWith<TElement>(
            this IElementFinder finder, string idEnding) 
            where TElement : class, IElement
        {
            return finder.Find<TElement>(AdvancedBy.IdEndingWith(idEnding));
        }

        public static TElement FindByIdContaining<TElement>(
            this IElementFinder finder, string idContaining) 
            where TElement : class, IElement
        {
            return finder.Find<TElement>(AdvancedBy.IdContaining(idContaining));
        }

        public static TElement FindByValueEndingWith<TElement>(
            this IElementFinder finder, string valueEnding) 
            where TElement : class, IElement
        {
            return finder.Find<TElement>(AdvancedBy.ValueEndingWith(valueEnding));
        }

        public static TElement FindByXpath<TElement>(
            this IElementFinder finder, string xpath) 
            where TElement : class, IElement
        {
            return finder.Find<TElement>(AdvancedBy.Xpath(xpath));
        }

        public static TElement FindByLinkTextContaining<TElement>(
            this IElementFinder finder, string linkTextContaining) 
            where TElement : class, IElement
        {
            return finder.Find<TElement>(AdvancedBy.LinkTextContaining(linkTextContaining));
        }

        public static TElement FindByClass<TElement>(
            this IElementFinder finder, string cssClass) 
            where TElement : class, IElement
        {
            return finder.Find<TElement>(AdvancedBy.CssClass(cssClass));
        }

        public static TElement FindByClassContaining<TElement>(
            this IElementFinder finder, string cssClassContaining) 
            where TElement : class, IElement
        {
            return finder.Find<TElement>(AdvancedBy.CssClassContaining(cssClassContaining));
        }

        public static TElement FindByInnerTextContaining<TElement>(
            this IElementFinder finder, string innerText) 
            where TElement : class, IElement
        {
            return finder.Find<TElement>(AdvancedBy.InnerTextContains(innerText));
        }

        public static TElement FindByNameEndingWith<TElement>(
            this IElementFinder finder, string name) 
            where TElement : class, IElement
        {
            return finder.Find<TElement>(AdvancedBy.NameEndingWith(name));
        }
    }
}