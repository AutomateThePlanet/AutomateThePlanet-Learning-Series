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

using Fidely.Framework.Compilation.Evaluators;
using Fidely.Framework.Integration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Fidely.Framework.Compilation.Objects.Evaluators
{
    /// <summary>
    /// Provides the basic features for an object evaluator.
    /// </summary>
    public abstract class BaseBuiltInEvaluator : OperandEvaluator
    {
        private IDictionary<string, PropertyInfo> mapping;

        private OperandBuilder builder;


        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        protected BaseBuiltInEvaluator()
        {
            mapping = new Dictionary<string, PropertyInfo>();
            builder = new OperandBuilder();
        }


        /// <summary>
        /// Registers the specified property information for evaluation.
        /// </summary>
        /// <param name="property">The property information.</param>
        protected void Register(PropertyInfo property)
        {
            mapping[property.Name.ToUpperInvariant()] = property;

            var description = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute;
            Register(new AutoCompleteItem(property.Name, (description != null) ? description.Description : ""));

            foreach (AliasAttribute alias in property.GetCustomAttributes(typeof(AliasAttribute), false))
            {
                mapping[alias.Name.ToUpperInvariant()] = mapping[property.Name.ToUpperInvariant()];
                if (String.IsNullOrEmpty(alias.Description) && description != null)
                {
                    Register(new AutoCompleteItem(alias.Name, description.Description));
                }
                else
                {
                    Register(new AutoCompleteItem(alias.Name, alias.Description));
                }
            }
        }

        /// <summary>
        /// Builds up an expression to evaluates the specified value.
        /// </summary>
        /// <param name="current">The expression that represents the current element of the collection.</param>
        /// <param name="value">The evaluatee.</param>
        /// <returns>The operand that wraps the built expression.</returns>
        public override Operand Evaluate(Expression current, string value)
        {
            if (current == null || value == null)
            {
                return null;
            }

            var propertyName = value.ToUpperInvariant();
            if (!mapping.ContainsKey(propertyName))
            {
                return null;
            }

            var property = mapping[propertyName];
            return builder.BuildUp(current, property);
        }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public override OperandEvaluator Clone()
        {
            var instance = CreateInstance();
            instance.builder = builder;
            foreach (var key in mapping.Keys)
            {
                instance.mapping.Add(key, mapping[key]);
            }
            return instance;
        }

        /// <summary>
        /// Creates a new instance of an object evaluator that inherits this class.
        /// </summary>
        /// <returns>The new instance of an object evaluator that inherits this class.</returns>
        protected abstract BaseBuiltInEvaluator CreateInstance();
    }
}
