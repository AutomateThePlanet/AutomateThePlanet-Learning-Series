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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Fidely.Framework.Compilation.Objects.Evaluators;
using Fidely.Framework.Compilation.Operators;
using System.ComponentModel.DataAnnotations;

namespace Fidely.Framework.Compilation.Objects.Operators
{
    /// <summary>
    /// Provides the basic features for a comparative operator.
    /// </summary>
    /// <typeparam name="T">The type of an elements in a collection that is filtered by a generated expression.</typeparam>
    public abstract class BaseBuiltInComparativeOperator<T> : ComparativeOperator, IDescribable
    {
        private readonly OperandBuilder _builder;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        protected BaseBuiltInComparativeOperator(string symbol) : this(symbol, OperatorIndependency.Strong, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="independency">The independency of this operator.</param>
        /// <param name="description">The description of this operator.</param>
        protected BaseBuiltInComparativeOperator(string symbol, OperatorIndependency independency, string description) : base(symbol, independency)
        {
            _builder = new OperandBuilder();

            if (description == null)
            {
                var attribute = Attribute.GetCustomAttribute(GetType(), typeof(DescriptionAttribute)) as DescriptionAttribute;
                Description = (attribute != null) ? attribute.Description : "";
            }
            else
            {
                Description = description;
            }
        }

        /// <summary>
        /// The description of this operator.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Builds up an expression to compare the specified operators.
        /// </summary>
        /// <param name="current">The expression that represents the current element of the collection.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The operand that wraps the built expression.</returns>
        public override Operand Compare(Expression current, Operand left, Operand right)
        {
            Logger.Info("Comparing operands (left = '{0}', right = '{1}').", left.OperandType.FullName, right.OperandType.FullName);

            Expression result = null;

            if (left is BlankOperand)
            {
                var operands = new List<Operand>();

                var attr = Attribute.GetCustomAttribute(typeof(T), typeof(MetadataTypeAttribute)) as MetadataTypeAttribute;
                var type = (attr != null) ? attr.MetadataClassType : typeof(T);
                foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (property.CanRead && Attribute.GetCustomAttribute(property, typeof(NotEvaluateAttribute)) == null)
                    {
                        operands.Add(_builder.BuildUp(current, typeof(T).GetProperty(property.Name, BindingFlags.Instance | BindingFlags.Public)));
                        Logger.Verbose("Generated an operand with '{0}' property.", property.Name);
                    }
                    else
                    {
                        Logger.Verbose("Ignored '{0}' property.", property.Name);
                    }
                }

                if (operands.Count == 0)
                {
                    Logger.Verbose("Generated a constant true operand.");
                    return new Operand(Expression.Constant(true), typeof(bool));
                }

                result = Compare(operands[0], right);
                for (var i = 1; i < operands.Count; i++)
                {
                    result = Expression.Or(result, Compare(operands[i], right));
                }
            }
            else
            {
                result = Compare(left, right);
            }

            return new Operand(result, typeof(bool));
        }

        /// <summary>
        /// Builds up an expression to compare the specified operators.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The operand that wraps the built expression.</returns>
        protected internal abstract Expression Compare(Operand left, Operand right);
    }
}