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
using System.Collections.Generic;
using System.Linq.Expressions;
using Fidely.Framework.Integration;

namespace Fidely.Framework.Compilation.Evaluators
{
    /// <summary>
    /// Represents the operand evaluator.
    /// </summary>
    public abstract class OperandEvaluator
    {
        private readonly ICollection<IAutoCompleteItem> autocompleteItems;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        protected OperandEvaluator()
        {
            this.autocompleteItems = new List<IAutoCompleteItem>();
        }

        internal IEnumerable<IAutoCompleteItem> AutocompleteItems
        {
            get
            {
                return this.autocompleteItems;
            }
        }

        internal IWarningNotifier WarningNotifier { get; set; }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public abstract OperandEvaluator Clone();

        /// <summary>
        /// Builds up an expression to evaluates the specified value.
        /// </summary>
        /// <param name="current">The expression that represents the current element of the collection.</param>
        /// <param name="value">The evaluatee.</param>
        /// <returns>The operand that wraps the built expression.</returns>
        public abstract Operand Evaluate(Expression current, string value);

        /// <summary>
        /// Registers the specified autocomplete item.
        /// </summary>
        /// <param name="item">The autocomplete item.</param>
        protected void Register(IAutoCompleteItem item)
        {
            this.autocompleteItems.Add(item);
        }

        /// <summary>
        /// Notifies warning message.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The object array that contains zero or more objects to format.</param>
        protected void Warn(string format, params object[] args)
        {
            Logger.Warn(format, args);
            if (this.WarningNotifier != null)
            {
                this.WarningNotifier.Notify(this.GetType(), "", format, args);
            }
        }
    }
}