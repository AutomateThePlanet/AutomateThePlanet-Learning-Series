// <copyright file="AnalyzedTestException.cs" company="Automate The Planet Ltd.">
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

namespace HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator
{
    public class AnalyzedTestException : Exception
    {
        public AnalyzedTestException()
        {
        }

        public AnalyzedTestException(string message) : base(FormatExceptionMessage(message))
        {
        }

        public AnalyzedTestException(string message, Exception inner) : base(FormatExceptionMessage(message), inner)
        {
        }

        private static string FormatExceptionMessage(string exceptionMessage)
        {
            return string.Format(
                "\n\n{0}\n\n{1}\n\n{2}\n",
                new string('#', 40),
                exceptionMessage,
                new string('#', 40));
        }
    }
}