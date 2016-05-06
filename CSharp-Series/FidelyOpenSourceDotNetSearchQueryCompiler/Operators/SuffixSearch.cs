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
using System.Reflection;
using Fidely.Framework.Compilation.Operators;

namespace Fidely.Framework.Compilation.Objects.Operators
{
    /// <summary>
    /// Represents the suffix search operator.
    /// </summary>
    /// <typeparam name="T">The type of an elements in a collection that is filtered by a generated expression.</typeparam>
    public class SuffixSearch<T> : BaseBuiltInComparativeOperator<T>
    {
        private readonly bool ignoreCase;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        public SuffixSearch(string symbol) : this(symbol, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="ignoreCase">Whether or not this operator should ignore case.</param>
        public SuffixSearch(string symbol, bool ignoreCase) : this(symbol, ignoreCase, OperatorIndependency.Strong, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="ignoreCase">Whether or not this operator should ignore case.</param>
        /// <param name="independency">The independency of this operator.</param>
        /// <param name="description">The description of this operator.</param>
        public SuffixSearch(string symbol, bool ignoreCase, OperatorIndependency independency, string description) : base(symbol, independency, description)
        {
            this.ignoreCase = ignoreCase;
        }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public override FidelyOperator Clone()
        {
            return new SuffixSearch<T>(this.Symbol, this.ignoreCase, this.Independency, this.Description);
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

            MethodCallExpression l = Expression.Call(null, typeof(Convert).GetMethod("ToString", new Type[] { typeof(object) }), Expression.Convert(left.Expression, typeof(object)));
            MethodCallExpression r = Expression.Call(null, typeof(Convert).GetMethod("ToString", new Type[] { typeof(object) }), Expression.Convert(right.Expression, typeof(object)));

            if (this.ignoreCase)
            {
                MethodInfo endsWith = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                MethodInfo toLower = typeof(string).GetMethod("ToLower", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { }, null);
                return Expression.Call(Expression.Call(l, toLower), endsWith, Expression.Call(r, toLower));
            }
            else
            {
                MethodInfo endsWith = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                return Expression.Call(l, endsWith, r);
            }
        }
    }
}