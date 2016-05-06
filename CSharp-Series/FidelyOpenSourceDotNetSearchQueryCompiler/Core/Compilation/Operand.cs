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
using System.Globalization;
using System.Linq.Expressions;

namespace Fidely.Framework.Compilation
{
    /// <summary>
    /// Represents the operand.
    /// </summary>
    public class Operand
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="expression">The expression to get value of this operand.</param>
        /// <param name="operandType">The type of the value of this operand.</param>
        public Operand(Expression expression, Type operandType)
        {
            this.Expression = expression;
            this.OperandType = operandType;
        }

        /// <summary>
        /// The expression to get value of this operand.
        /// </summary>
        public Expression Expression { get; private set; }

        /// <summary>
        /// The type of the value of this operand.
        /// </summary>
        public Type OperandType { get; private set; }

        /// <summary>
        /// Returns the string representation of an instance of this class.
        /// </summary>
        /// <returns>The string representation of an instance of this class.</returns>
        public override string ToString()
        {
            string expression = (this.Expression != null) ? this.Expression.ToString() : "null";
            string type = (this.OperandType != null) ? this.OperandType.FullName : "null";
            return String.Format(CultureInfo.CurrentUICulture, "{0}:{1}", expression, type);
        }
    }
}