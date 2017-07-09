/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace Fidely.Framework.Compilation.Operators
{
    /// <summary>
    /// Represents the operator.
    /// </summary>
    public abstract class FidelyOperator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this class.</param>
        /// <param name="independency">The independency of this operator.</param>
        protected FidelyOperator(string symbol, OperatorIndependency independency)
        {
            Symbol = symbol;
            Independency = independency;
        }

        /// <summary>
        /// The symbol of this operator.
        /// </summary>
        public string Symbol { get; private set; }

        /// <summary>
        /// The independency of this operator.
        /// </summary>
        public OperatorIndependency Independency { get; private set; }

        internal IWarningNotifier WarningNotifier { get; set; }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public abstract FidelyOperator Clone();

        /// <summary>
        /// Notifies warning message.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The object array that contains zero or more objects to format.</param>
        protected void Warn(string format, params object[] args)
        {
            Logger.Warn(format, args);
            if (WarningNotifier != null)
            {
                WarningNotifier.Notify(GetType(), Symbol, format, args);
            }
        }
    }
}