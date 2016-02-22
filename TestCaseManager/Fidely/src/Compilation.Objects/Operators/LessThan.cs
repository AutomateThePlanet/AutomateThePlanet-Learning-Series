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

using Fidely.Framework.Compilation.Operators;
using System;
using System.Linq.Expressions;

namespace Fidely.Framework.Compilation.Objects.Operators
{
    /// <summary>
    /// Represents the less than operator.
    /// </summary>
    /// <typeparam name="T">The type of an elements in a collection that is filtered by a generated expression.</typeparam>
    public class LessThan<T> : BaseBuiltInComparativeOperator<T>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        public LessThan(string symbol)
            : this(symbol, OperatorIndependency.Strong, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="independency">The independency of this operator.</param>
        /// <param name="description">The description of this operator.</param>
        public LessThan(string symbol, OperatorIndependency independency, string description)
            : base(symbol, independency, description)
        {
        }


        /// <summary>
        /// Builds up an expression to compare the specified operators.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The operand that wraps the built expression.</returns>
        protected internal override Expression Compare(Operand left, Operand right)
        {
            Logger.Info("Comparing operands (left = '{0}', right = '{1}').", left.OperandType.FullName, right.OperandType.FullName);

            Expression result = null;

            var operands = new OperandPair(left, right);
            if (operands.Are(typeof(decimal)) || operands.Are(typeof(DateTime)) || operands.Are(typeof(DateTimeOffset)) || operands.Are(typeof(TimeSpan)))
            {
                result = Expression.LessThan(left.Expression, right.Expression);
            }
            else
            {
                Warn("'{0}' doesn't support to compare '{1}' and '{2}'.", GetType().FullName, left.OperandType.FullName, right.OperandType.FullName);
                result = Expression.Constant(false);
            }

            return result;
        }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public override FidelyOperator Clone()
        {
            return new LessThan<T>(Symbol, Independency, Description);
        }
    }
}
