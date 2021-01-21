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
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Fidely.Framework.Compilation.Objects.Evaluators
{
    /// <summary>
    /// Provides the feature to evaluate an operand as the property of the elements of the collection.
    /// </summary>
    /// <typeparam name="T">The type of an elements in a collection that is filtered by a generated expression.</typeparam>
    public class PropertyEvaluator<T> : BaseBuiltInEvaluator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public PropertyEvaluator()
        {
            var attr = Attribute.GetCustomAttribute(typeof(T), typeof(MetadataTypeAttribute)) as MetadataTypeAttribute;
            var type = (attr != null) ? attr.MetadataClassType : typeof(T);

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.CanRead && Attribute.GetCustomAttribute(property, typeof(NotEvaluateAttribute)) == null)
                {
                    Register(typeof(T).GetProperty(property.Name, BindingFlags.Instance | BindingFlags.Public));
                }
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA1801", Justification = "The parameter 'dummy' doesn't have to use, but it can't be removed to overload constructor.")]
        private PropertyEvaluator(string dummy)
        {
        }

        /// <summary>
        /// Creates a new instance of an object evaluator that inherits this class.
        /// </summary>
        /// <returns>The new instance of an object evaluator that inherits this class.</returns>
        protected override BaseBuiltInEvaluator CreateInstance()
        {
            return new PropertyEvaluator<T>("");
        }
    }
}