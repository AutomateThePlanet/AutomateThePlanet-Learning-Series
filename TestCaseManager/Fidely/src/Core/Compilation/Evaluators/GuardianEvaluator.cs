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
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Fidely.Framework.Compilation.Evaluators
{
    /// <summary>
    /// Represents the guardian operand evaluator.
    /// </summary>
    public class GuardianEvaluator : OperandEvaluator
    {
        internal GuardianEvaluator()
        {
        }


        /// <summary>
        /// Builds up a constant expression that consists of the specified value.
        /// </summary>
        /// <param name="current">The expression that represents the current element of the collection.</param>
        /// <param name="value">The evaluatee.</param>
        /// <returns>The constant expression that consists of the specified value.</returns>
        public override Operand Evaluate(Expression current, string value)
        {
            Logger.Info("Evaluating the specified value '{0}'.", value ?? "null");
            Logger.Verbose("Evaluated the specified value as a string operand.");
            return new Operand(Expression.Constant((value != null) ? value.ToString() : ""), typeof(string));
        }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public override OperandEvaluator Clone()
        {
            return new GuardianEvaluator();
        }
    }
}
