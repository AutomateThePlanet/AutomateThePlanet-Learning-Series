// <copyright file="TestsExceptionsAnalyzerContext.cs" company="Automate The Planet Ltd.">
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
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.AmbientContext
{
    public class TestsExceptionsAnalyzerContext<THandler> : IDisposable
        where THandler : Handler, new()
    {
        private static readonly Stack<Handler> ScopeStack = new Stack<Handler>();

        public TestsExceptionsAnalyzerContext()
        {
            AddHandlerInfrontOfChain<THandler>();
        }

        public void Dispose()
        {
            MakeSuccessorMainHandler();
        }

        protected void AddHandlerInfrontOfChain<TNewHandler>()
            where TNewHandler : Handler, new()
        {
            var mainApplicationHandler = UnityContainerFactory.GetContainer().Resolve<Handler>(ExceptionAnalyzerConstants.MainApplicationHandlerName);
            var newHandler = UnityContainerFactory.GetContainer().Resolve<TNewHandler>();
            newHandler.SetSuccessor(mainApplicationHandler);
            UnityContainerFactory.GetContainer().RegisterInstance<Handler>(ExceptionAnalyzerConstants.MainApplicationHandlerName, newHandler);
            ScopeStack.Push(newHandler);
        }

        private void MakeSuccessorMainHandler()
        {
            for (var i = 0; i < GetType().GetGenericArguments().Length; i++)
            {
                var handler = ScopeStack.Pop();
                UnityContainerFactory.GetContainer().RegisterInstance(ExceptionAnalyzerConstants.MainApplicationHandlerName, handler.Successor);
                handler.ClearSuccessor();
            }
        }
    }
}