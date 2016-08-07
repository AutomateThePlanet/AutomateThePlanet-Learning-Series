// <copyright file="ElementFinderService.cs" company="Automate The Planet Ltd.">
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

using ArtOfTest.WebAii.Core;
using HybridTestFramework.UITests.Core;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace HybridTestFramework.UITests.TestingFramework.Engine
{
    public class ElementFinderService
    {
        private readonly IUnityContainer container;

        public ElementFinderService(IUnityContainer container)
        {
            this.container = container;
        }

        public TElement Find<TElement>(IDriver driver, Find findContext, Core.By by)
            where TElement : class, Core.Controls.IElement
        {
            string testingFrameworkExpression = by.ToTestingFrameworkExpression();
            this.WaitForExists(driver, testingFrameworkExpression);
            var element = findContext.ByExpression(by.ToTestingFrameworkExpression());
            TElement result = this.ResolveElement<TElement>(driver, element);

            return result;
        }

        public IEnumerable<TElement> FindAll<TElement>(IDriver driver, Find findContext, Core.By by)
            where TElement : class, Core.Controls.IElement
        {
            string testingFrameworkExpression = by.ToTestingFrameworkExpression();
            this.WaitForExists(driver, testingFrameworkExpression);
            var elements = findContext.AllByExpression(testingFrameworkExpression);
            List<TElement> resolvedElements = new List<TElement>();
            foreach (var currentElement in elements)
            {
                TElement result = this.ResolveElement<TElement>(driver, currentElement);
                resolvedElements.Add(result);
            }

            return resolvedElements;
        }

        public bool IsElementPresent(Find findContext, Core.By by)
        {
            try
            {
                string controlFindExpression = by.ToTestingFrameworkExpression();
                Manager.Current.ActiveBrowser.RefreshDomTree();
                HtmlFindExpression hfe = new HtmlFindExpression(controlFindExpression);
                Manager.Current.ActiveBrowser.WaitForElement(hfe, 5000, false);
            }
            catch (TimeoutException)
            {
                return false;
            }

            return true;
        }

        private void WaitForExists(IDriver driver, string findExpression)
        {
            try
            {
                driver.WaitUntilReady();
                HtmlFindExpression hfe = new HtmlFindExpression(findExpression);
                Manager.Current.ActiveBrowser.WaitForElement(hfe, 5000, false);
            }
            catch (Exception)
            {
                this.ThrowTimeoutExceptionIfElementIsNull(driver, findExpression);
            }
        }

        private TElement ResolveElement<TElement>(
            IDriver driver,
            ArtOfTest.WebAii.ObjectModel.Element element)
            where TElement : class, Core.Controls.IElement
        {
            TElement result = this.container.Resolve<TElement>(
                new ResolverOverride[]
                {
                    new ParameterOverride("driver", driver),
                    new ParameterOverride("element", element),
                    new ParameterOverride("container", this.container)
                });
            return result;
        }

        private void ThrowTimeoutExceptionIfElementIsNull(IDriver driver, params string[] customExpression)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[3];
            MethodBase method = callingFrame.GetMethod();
            string currentUrl = driver.Url;
            throw new ElementTimeoutException(
                string.Format(
                "TIMED OUT- for element with Find Expression:\n {0}\n Element Name: {1}.{2}\n URL: {3}\nElement Timeout: {4}",
                string.Join(",", customExpression.Select(p => p.ToString()).ToArray()),
                method.ReflectedType.FullName, method.Name, currentUrl, Manager.Current.Settings.ElementWaitTimeout));
        }
    }
}