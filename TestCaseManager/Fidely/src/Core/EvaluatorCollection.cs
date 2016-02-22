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
using System.Linq;
using System.Text;
using Fidely.Framework.Compilation.Evaluators;
using System.Collections;

namespace Fidely.Framework
{
    /// <summary>
    /// Represents the collection of operand evaluators.
    /// </summary>
    public class EvaluatorCollection : IEnumerable<OperandEvaluator>
    {
        private List<OperandEvaluator> items;


        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public EvaluatorCollection()
        {
            items = new List<OperandEvaluator>();
        }


        /// <summary>
        /// Adds the specified operand evaluator of this collection.
        /// </summary>
        /// <param name="item">The operand evaluator.</param>
        public void Add(OperandEvaluator item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            items.Add(item);
        }

        /// <summary>
        /// Returns the enumerator that iterates through the collection.
        /// </summary>
        /// <returns>The enumerator that iterates through the collection.</returns>
        public IEnumerator<OperandEvaluator> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Returns the enumerator that iterates through the collection.
        /// </summary>
        /// <returns>The enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
