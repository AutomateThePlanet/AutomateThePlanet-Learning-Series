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

namespace Fidely.Framework.Compilation.Evaluators
{
    /// <summary>
    /// Provides the feature to build up a new operand from any object.
    /// </summary>
    public interface IOperandBuilder
    {
        /// <summary>
        /// Builds up a new operand from the specified object.
        /// </summary>
        /// <param name="value">The object that is used to build up a new operand.</param>
        /// <returns>The new operand that is built up from the specified object.</returns>
        Operand BuildUp(object value);
    }
}