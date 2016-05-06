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
    /// Represents the calculating operator.
    /// </summary>
    public abstract class CalculatingOperator : FidelyOperator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="priority">The priority of this operator.</param>
        protected CalculatingOperator(string symbol, int priority) : this(symbol, priority, OperatorIndependency.Strong)
        {
            this.Priority = priority;
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="priority">The priority of this operator.</param>
        /// <param name="independency">The independency of this operator.</param>
        protected CalculatingOperator(string symbol, int priority, OperatorIndependency independency) : base(symbol, independency)
        {
            this.Priority = priority;
        }

        /// <summary>
        /// The priority of this operator.
        /// </summary>
        public int Priority { get; private set; }

        /// <summary>
        /// Builds up an expression to calculate with the specified oprands.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The operand that wraps the built expression.</returns>
        public abstract Operand Calculate(Operand left, Operand right);
    }
}