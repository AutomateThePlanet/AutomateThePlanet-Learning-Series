// <copyright file="ElementHighlighter.cs" company="Automate The Planet Ltd.">
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
using System;
using System.ComponentModel;
using System.Threading;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Utilities;
using OpenQA.Selenium;
using Unity;

namespace HybridTestFramework.UITests.Selenium.Plugins
{
    public static class ElementHighlighter
    {
        private static readonly IJavaScriptInvoker JavaScriptExecutor;

        static ElementHighlighter()
        {
            JavaScriptExecutor = UnityContainerFactory.GetContainer().Resolve<IJavaScriptInvoker>();
        }

        public static void Highlight(this IWebElement nativeElement, int waitBeforeUnhighlightMiliSeconds = 100, string color = "yellow")
        {
            try
            {
                var originalElementBorder = (string)JavaScriptExecutor.ExecuteScript("return arguments[0].style.background", nativeElement);
                JavaScriptExecutor.ExecuteScript($"arguments[0].style.background='{color}'; return;", nativeElement);
                if (waitBeforeUnhighlightMiliSeconds >= 0)
                {
                    if (waitBeforeUnhighlightMiliSeconds > 1000)
                    {
                        var backgroundWorker = new BackgroundWorker();
                        backgroundWorker.DoWork += (obj, e) => Unhighlight(nativeElement, originalElementBorder, waitBeforeUnhighlightMiliSeconds);
                        backgroundWorker.RunWorkerAsync();
                    }
                    else
                    {
                        Unhighlight(nativeElement, originalElementBorder, waitBeforeUnhighlightMiliSeconds);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void Unhighlight(IWebElement nativeElement, string border, int waitBeforeUnhighlightMiliSeconds)
        {
            try
            {
                Thread.Sleep(waitBeforeUnhighlightMiliSeconds);
                JavaScriptExecutor.ExecuteScript("arguments[0].style.background='" + border + "'; return;", nativeElement);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
