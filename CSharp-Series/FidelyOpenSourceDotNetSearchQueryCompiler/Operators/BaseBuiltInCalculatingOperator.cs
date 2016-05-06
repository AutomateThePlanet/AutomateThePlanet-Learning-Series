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
using System.ComponentModel;
using Fidely.Framework.Compilation.Operators;

namespace Fidely.Framework.Compilation.Objects.Operators
{
    /// <summary>
    /// Provides the basic features for a calculating operator.
    /// </summary>
    public abstract class BaseBuiltInCalculatingOperator : CalculatingOperator, IDescribable
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="priority">The priority of this operator.</param>
        protected BaseBuiltInCalculatingOperator(string symbol, int priority) : this(symbol, priority, OperatorIndependency.Strong, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="symbol">The symbol of this operator.</param>
        /// <param name="priority">The priority of this operator.</param>
        /// <param name="independency">The independency of this operator.</param>
        /// <param name="description">The description of this operator.</param>
        protected BaseBuiltInCalculatingOperator(string symbol, int priority, OperatorIndependency independency, string description) : base(symbol, priority, independency)
        {
            if (description == null)
            {
                var attribute = Attribute.GetCustomAttribute(this.GetType(), typeof(DescriptionAttribute)) as DescriptionAttribute;
                this.Description = (attribute != null) ? attribute.Description : "";
            }
            else
            {
                this.Description = description;
            }
        }

        /// <summary>
        /// The description of this operator.
        /// </summary>
        public string Description { get; private set; }
    }
}