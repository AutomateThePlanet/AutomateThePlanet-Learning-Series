// <copyright file="ExceptionAnalyzer.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.ChainOfResponsibility;
using Unity;
using System;

namespace HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.AmbientContext
{
    public static class ExceptionAnalyzer
    {
        public static void AnalyzeFor<TExceptionHander>(Action action)
            where TExceptionHander : Handler, new()
        {
            using (UnityContainerFactory.GetContainer().Resolve<TestsExceptionsAnalyzerContext<TExceptionHander>>())
            {
                action();
            }
        }

        public static void AnalyzeFor<TExceptionHander1, TExceptionHander2>(Action action)
            where TExceptionHander1 : Handler, new()
            where TExceptionHander2 : Handler, new()
        {
            using (UnityContainerFactory.GetContainer().Resolve<TestsExceptionsAnalyzerContext<TExceptionHander1, TExceptionHander2>>())
            {
                action();
            }
        }

        public static void AnalyzeFor<TExceptionHander1, TExceptionHander2, TExceptionHander3>(Action action)
            where TExceptionHander1 : Handler, new()
            where TExceptionHander2 : Handler, new()
            where TExceptionHander3 : Handler, new()
        {
            using (UnityContainerFactory.GetContainer().Resolve<TestsExceptionsAnalyzerContext<TExceptionHander1, TExceptionHander2, TExceptionHander3>>())
            {
                action();
            }
        }
    }
}