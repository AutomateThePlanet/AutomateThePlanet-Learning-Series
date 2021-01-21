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
    /// Represents the add operator.
    /// </summary>
    public class Add : BaseBuiltInCalculatingOperator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="priority">The priority of this operator.</param>
        public Add(string symbol, int priority) : this(symbol, priority, OperatorIndependency.Strong, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="priority">The priority of this operator.</param>
        /// <param name="independency">The independency of this operator.</param>
        /// <param name="description">The description of this operator.</param>
        public Add(string symbol, int priority, OperatorIndependency independency, string description) : base(symbol, priority, independency, description)
        {
        }

        /// <summary>
        /// Builds up an expression to add the right operand to the left operand.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The operand that consists of the expression to add the right operand to the left operand.</returns>
        public override Operand Calculate(Operand left, Operand right)
        {
            Logger.Info("Adding operands (left = '{0}', right = '{1}').", left.OperandType.FullName, right.OperandType.FullName);

            Operand result = null;

            var operands = new OperandPair(left, right);
            if (operands.Are(typeof(decimal)) || operands.Are(typeof(TimeSpan)))
            {
                result = new Operand(Expression.AddChecked(left.Expression, right.Expression), left.OperandType);
            }
            else if (operands.Are(typeof(DateTime), typeof(TimeSpan)) || operands.Are(typeof(DateTimeOffset), typeof(TimeSpan)))
            {
                if (left.OperandType == typeof(TimeSpan))
                {
                    result = new Operand(Expression.AddChecked(right.Expression, left.Expression), right.OperandType);
                }
                else
                {
                    result = new Operand(Expression.AddChecked(left.Expression, right.Expression), left.OperandType);
                }
            }
            else
            {
                Warn("'{0}' doesn't support to add '{1}' and '{2}'.", GetType().FullName, left.OperandType.FullName, right.OperandType.FullName);

                var l = Expression.Call(null, typeof(Convert).GetMethod("ToString", new Type[] { typeof(object) }), Expression.Convert(left.Expression, typeof(object)));
                var r = Expression.Call(null, typeof(Convert).GetMethod("ToString", new Type[] { typeof(object) }), Expression.Convert(right.Expression, typeof(object)));
                var method = typeof(string).GetMethod("Concat", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string), typeof(string) }, null);
                result = new Operand(Expression.Call(null, method, l, r), typeof(string));
            }

            Logger.Info("Added operands (result = '{0}').", result.OperandType.FullName);

            return result;
        }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public override FidelyOperator Clone()
        {
            return new Add(Symbol, Priority, Independency, Description);
        }
    }
}