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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Fidely.Framework.Compilation.Operators;
using Fidely.Framework.Tokens;

namespace Fidely.Framework
{
    /// <summary>
    /// Represents the collection of operators.
    /// </summary>
    public class OperatorCollection : IEnumerable<FidelyOperator>
    {
        private static readonly string[] BuiltInSymbols = new string[]
        {
            LogicalAndOperatorToken.Symbol,
            LogicalOrOperatorToken.Symbol,
            OpenedParenthesisToken.Symbol,
            ClosedParenthesisToken.Symbol,
        };

        private readonly List<FidelyOperator> _items;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public OperatorCollection()
        {
            _items = new List<FidelyOperator>();
        }

        /// <summary>
        /// Adds the specified operator of this collection.
        /// </summary>
        /// <param name="item">The operator.</param>
        public void Add(FidelyOperator item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (string.IsNullOrWhiteSpace(item.Symbol))
            {
                throw new ArgumentException("Failed to register the specified operator because its symbol can't be null, empty or white space.", "item");
            }

            if (BuiltInSymbols.Any(o => o.Equals(item.Symbol, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, "Failed to register the specified operator because its symbol '{0}' is the reserved symbol.", item.Symbol), "item");
            }

            if (_items.Any(o => o.Symbol.Equals(item.Symbol, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, "Failed to register the specified operator because its symbol '{0}' is already registered.", item.Symbol), "item");
            }

            _items.Add(item);
        }

        /// <summary>
        /// Returns the enumerator that iterates through the collection.
        /// </summary>
        /// <returns>The enumerator that iterates through the collection.</returns>
        public IEnumerator<FidelyOperator> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Returns the enumerator that iterates through the collection.
        /// </summary>
        /// <returns>The enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}