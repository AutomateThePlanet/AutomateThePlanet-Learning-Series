// <copyright file="By.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Core.Enums;

namespace HybridTestFramework.UITests.Core
{
    public class By
    {
        public By(SearchType type, string value) : this(type, value, null)
        {
        }

        public By(SearchType type, string value, IElement parent)
        {
            Type = type;
            Value = value;
            Parent = parent;
        }

        public SearchType Type { get; private set; }

        public string Value { get; private set; }

        public IElement Parent { get; private set; }

        public static By Id(string id)
        {
            return new By(SearchType.Id, id);
        }
        
        public static By Id(string id, IElement parentElement)
        {
            return new By(SearchType.Id, id, parentElement);
        }

        public static By LinkText(string linkText)
        {
            return new By(SearchType.LinkText, linkText);
        }

        public static By CssClass(string cssClass, IElement parentElement)
        {
            return new By(SearchType.CssClass, cssClass, parentElement);
        }

        public static By Tag(string tagName)
        {
            return new By(SearchType.Tag, tagName);
        }

        public static By Tag(string tagName, IElement parentElement)
        {
            return new By(SearchType.Tag, tagName, parentElement);
        }

        public static By CssSelector(string cssSelector)
        {
            return new By(SearchType.CssSelector, cssSelector);
        }

        public static By CssSelector(string cssSelector, IElement parentElement)
        {
            return new By(SearchType.CssSelector, cssSelector, parentElement);
        }

        public static By Name(string name)
        {
            return new By(SearchType.Name, name);
        }

        public static By Name(string name, IElement parentElement)
        {
            return new By(SearchType.Name, name, parentElement);
        }
    }
}