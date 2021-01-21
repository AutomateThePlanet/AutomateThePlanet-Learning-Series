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
using Fidely.Framework.Compilation.Evaluators;

namespace Fidely.Framework.Compilation.Objects.Evaluators
{
    /// <summary>
    /// Provides the feature to evaluate an operand as the numerical value, date time or string.
    /// </summary>
    public class TypeConversionEvaluator : OperandEvaluator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public TypeConversionEvaluator()
        {
        }

        /// <summary>
        /// Builds up an expression to evaluates the specified value.
        /// </summary>
        /// <param name="current">The expression that represents the current element of the collection.</param>
        /// <param name="value">The evaluatee.</param>
        /// <returns>The operand that wraps the built expression.</returns>
        public override Operand Evaluate(Expression current, string value)
        {
            Logger.Info("Evaluating the specified value '{0}'.", value ?? "null");
            if (value == null)
            {
                return new Operand(Expression.Constant(""), typeof(string));
            }

            var decimalValue = 0m;
            if (Decimal.TryParse(value.ToString(), out decimalValue))
            {
                Logger.Verbose("Evaluated the specified value as a number operand.");
                return new Operand(Expression.Constant(decimalValue), typeof(decimal));
            }

            var dateTimeValue = new DateTime();
            if (DateTime.TryParse(value.ToString(), out dateTimeValue))
            {
                Logger.Verbose("Evaluated the specified value as a date operand.");
                return new Operand(Expression.Constant(dateTimeValue), typeof(DateTime));
            }

            return new Operand(Expression.Constant(value), typeof(string));
        }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public override OperandEvaluator Clone()
        {
            return new TypeConversionEvaluator();
        }
    }
}