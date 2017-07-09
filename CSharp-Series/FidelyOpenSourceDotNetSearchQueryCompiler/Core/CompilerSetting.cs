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
using Fidely.Framework.Compilation.Evaluators;
using Fidely.Framework.Compilation.Operators;
using Fidely.Framework.Integration;

namespace Fidely.Framework
{
    /// <summary>
    /// Represents the compiler setting.
    /// </summary>
    public class CompilerSetting
    {
        private const int DefaultCacheSize = 1024;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public CompilerSetting()
        {
            CacheSize = DefaultCacheSize;
            Operators = new List<FidelyOperator>();
            Evaluators = new List<OperandEvaluator>();
        }

        /// <summary>
        /// The cache size.
        /// </summary>
        public int CacheSize { get; set; }

        /// <summary>
        /// The operators colleciton.
        /// </summary>
        public ICollection<FidelyOperator> Operators { get; private set; }

        /// <summary>
        /// The operand evaluators collection.
        /// </summary>
        public ICollection<OperandEvaluator> Evaluators { get; private set; }

        /// <summary>
        /// Extracts autocomplete items.
        /// </summary>
        /// <returns>The collection of the extracted autocomplete items.</returns>
        public virtual IEnumerable<IAutoCompleteItem> ExtractAutoCompleteItems()
        {
            var items = new List<IAutoCompleteItem>();

            foreach (var evaluator in Evaluators)
            {
                items.AddRange(evaluator.AutocompleteItems);
            }

            foreach (var op in Operators)
            {
                items.Add(new AutoCompleteItem(op.Symbol, GetOperatorDescription(op)));
            }

            return items;
        }

        /// <summary>
        /// Gets the description from the specified operator.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <returns>The description of the specified operator.</returns>
        protected virtual string GetOperatorDescription(FidelyOperator op)
        {
            var describable = op as IDescribable;
            if (describable != null)
            {
                return describable.Description;
            }
            
            var attribute = Attribute.GetCustomAttribute(op.GetType(), typeof(DescriptionAttribute)) as DescriptionAttribute;
            return (attribute != null) ? attribute.Description : "";
        }
    }
}