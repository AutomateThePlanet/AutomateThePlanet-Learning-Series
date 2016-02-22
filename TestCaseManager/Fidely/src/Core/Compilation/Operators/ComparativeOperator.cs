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
using System.Linq.Expressions;

namespace Fidely.Framework.Compilation.Operators
{
    /// <summary>
    /// Represents the comparative operator.
    /// </summary>
    public abstract class ComparativeOperator : FidelyOperator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        protected ComparativeOperator(string symbol)
            : this(symbol, OperatorIndependency.Strong)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="independency">The independency of this operator.</param>
        protected ComparativeOperator(string symbol, OperatorIndependency independency)
            : base(symbol, independency)
        {
        }


        /// <summary>
        /// Builds up an expression to compare the specified operators.
        /// </summary>
        /// <param name="current">The expression that represents the current element of the collection.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The operand that wraps the built expression.</returns>
        public abstract Operand Compare(Expression current, Operand left, Operand right);
    }
}
