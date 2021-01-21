// <copyright file="ExceptionAnalyser.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator.Interfaces;
using System;
using System.Collections.Generic;

namespace HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator
{
    public class ExceptionAnalyser : IExceptionAnalyser
    {
        private readonly List<IExceptionAnalysationHandler> _exceptionAnalysationHandlers;

        public ExceptionAnalyser(IEnumerable<IExceptionAnalysationHandler> handlers)
        {
            _exceptionAnalysationHandlers = new List<IExceptionAnalysationHandler>();
            _exceptionAnalysationHandlers.AddRange(handlers);
        }

        public void RemoveFirstExceptionAnalysationHandler()
        {
            if (_exceptionAnalysationHandlers.Count > 0)
            {
                _exceptionAnalysationHandlers.RemoveAt(0);
            }
        }

        public void Analyse(Exception ex = null, params object[] context)
        {
            foreach (var exceptionHandler in _exceptionAnalysationHandlers)
            {
                if (exceptionHandler.IsApplicable(ex, context))
                {
                    throw new AnalyzedTestException(exceptionHandler.DetailedIssueExplanation, ex);
                }
            }
        }

        public void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>(IExceptionAnalysationHandler exceptionAnalysationHandler)
            where TExceptionAnalysationHandler : IExceptionAnalysationHandler
        {
            _exceptionAnalysationHandlers.Insert(0, exceptionAnalysationHandler);
        }

        public void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>()
            where TExceptionAnalysationHandler : IExceptionAnalysationHandler, new()
        {
            _exceptionAnalysationHandlers.Insert(0, new TExceptionAnalysationHandler());
        }
    }
}