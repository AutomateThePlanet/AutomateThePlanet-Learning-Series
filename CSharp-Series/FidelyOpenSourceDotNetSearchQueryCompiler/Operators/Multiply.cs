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
    /// Represents the multiply operator.
    /// </summary>
    public class Multiply : BaseBuiltInCalculatingOperator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="priority">The priority of this operator.</param>
        public Multiply(string symbol, int priority) : this(symbol, priority, OperatorIndependency.Strong, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="priority">The priority of this operator.</param>
        /// <param name="independency">The independency of this operator.</param>
        /// <param name="description">The description of this operator.</param>
        public Multiply(string symbol, int priority, OperatorIndependency independency, string description) : base(symbol, priority, independency, description)
        {
        }

        /// <summary>
        /// Builds up an expression to compare the specified operators.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The operand that wraps the built expression.</returns>
        public override Operand Calculate(Operand left, Operand right)
        {
            Logger.Info("Multiplying operands (left = '{0}', right = '{1}').", left.OperandType.FullName, right.OperandType.FullName);

            Operand result = null;

            var operands = new OperandPair(left, right);
            if (operands.Are(typeof(decimal)))
            {
                result = new Operand(Expression.MultiplyChecked(left.Expression, right.Expression), typeof(decimal));
            }
            else
            {
                this.Warn("'{0}' doesn't support to multiply '{1}' and '{2}'.", this.GetType().FullName, left.OperandType.FullName, right.OperandType.FullName);

                MethodCallExpression l = Expression.Call(null, typeof(Convert).GetMethod("ToString", new Type[] { typeof(object) }), Expression.Convert(left.Expression, typeof(object)));
                MethodCallExpression r = Expression.Call(null, typeof(Convert).GetMethod("ToString", new Type[] { typeof(object) }), Expression.Convert(right.Expression, typeof(object)));
                MethodInfo method = typeof(string).GetMethod("Concat", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string), typeof(string), typeof(string) }, null);
                result = new Operand(Expression.Call(null, method, l, Expression.Constant(this.Symbol), r), typeof(string));
            }

            Logger.Info("Multiplied operands (result = '{0}').", result.OperandType.FullName);

            return result;
        }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public override FidelyOperator Clone()
        {
            return new Multiply(this.Symbol, this.Priority, this.Independency, this.Description);
        }
    }
}