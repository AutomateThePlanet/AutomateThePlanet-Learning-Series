// <copyright file="HtmlElementExpressionExtensions.cs" company="Automate The Planet Ltd.">
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

namespace HybridTestFramework.UITests.TestingFramework
{
    public static class HtmlElementExpressionExtensions
    {
        public static string GenerateIdEndingWithExpression(this string expression)
        {
            return string.Concat("id=?", expression);
        }

        public static string GenerateValueEndingWithExpression(this string expression)
        {
            return string.Concat("value=?", expression);
        }

        public static string GenerateIdExpression(this string expression)
        {
            return string.Concat("id=", expression);
        }

        public static string GenerateXpathExpression(this string expression)
        {
            return string.Concat("xpath=", expression);
        }

        public static string GenerateXpathContainsExpression(this string id, string expression)
        {
            return string.Concat("xpath=//*[contains(@id, '", id, "')]", expression);
        }

        public static string GenerateIdContainingExpression(this string expression)
        {
            return string.Concat("id=~", expression);
        }

        public static string GenerateForEndingWithExpression(this string expression)
        {
            return string.Concat("for=?", expression);
        }

        public static string GenerateNameContainingExpression(this string expression)
        {
            return string.Concat("name=~", expression);
        }

        public static string GenerateNameEndingWithExpression(this string expression)
        {
            return string.Concat("name=?", expression);
        }

        public static string GenerateClassExpression(this string expression)
        {
            return string.Concat("class=", expression);
        }

        public static string GenerateClassContainingExpression(this string expression)
        {
            return string.Concat("class=~", expression);
        }

        public static string GenerateClassEndingWithExpression(this string expression)
        {
            return string.Concat("class=?", expression);
        }

        public static string GenerateClassStartingWithExpression(this string expression)
        {
            return string.Concat("class=^", expression);
        }

        public static string GenerateTextContentExpression(this string expression)
        {
            return string.Concat("TextContent=", expression);
        }

        public static string GenerateInnerTextContainingExpression(this string expression)
        {
            return string.Concat("InnerText=~", expression);
        }

        public static string GenerateLinkHrefExpression(this string expression)
        {
            return string.Concat("href=", expression);
        }

        public static string GenerateLinkHrefContainingExpression(this string expression)
        {
            return string.Concat("href=~", expression);
        }
    }
}