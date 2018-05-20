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
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Fidely.Framework.Compilation.Evaluators
{
    /// <summary>
    /// Represents the operand evaluator that maps a dynamically changing variable name to a value generator.
    /// </summary>
    public class DynamicVariableEvaluator : OperandEvaluator
    {
        private IOperandBuilder builder;

        private Dictionary<Regex, Func<Match, object>> evaluators;


        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="builder">The operand builder to build up a new operand from any object.</param>
        public DynamicVariableEvaluator(IOperandBuilder builder)
        {
            this.builder = builder;
            evaluators = new Dictionary<Regex, Func<Match, object>>();
        }


        /// <summary>
        /// Registers a new mapping between a pattern of regular expression and a value generator.
        /// </summary>
        /// <param name="pattern">The pattern of regular expression to find out a variable.</param>
        /// <param name="procedure">The value generator.</param>
        /// <param name="item">The autocomplete item.</param>
        public void RegisterVariable(string pattern, Func<Match, object> procedure, RegexAutoCompleteItem item)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException("pattern");
            }

            RegisterVariable(new Regex(pattern, RegexOptions.Compiled), procedure, item);
        }

        /// <summary>
        /// Registers a new mapping between a regular expression and a value generator.
        /// </summary>
        /// <param name="regex">The regular expression to find out a variable.</param>
        /// <param name="procedure">The value generator.</param>
        /// <param name="item">The autocomplete item.</param>
        public void RegisterVariable(Regex regex, Func<Match, object> procedure, RegexAutoCompleteItem item)
        {
            if (regex == null)
            {
                throw new ArgumentNullException("regex");
            }

            if (procedure == null)
            {
                throw new ArgumentNullException("procedure");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Logger.Verbose("Registered the specified pattern '{0}'.", regex.ToString());

            evaluators[regex] = procedure;
            Register(item);
        }

        /// <summary>
        /// Builds up a constant expression that consists of the value that is mapped to the specified value.
        /// </summary>
        /// <param name="current">The expression that represents the current element of the collection.</param>
        /// <param name="value">The evaluatee.</param>
        /// <returns>The constant expression that consists of the value that is mapped to the specified value.</returns>
        public override Operand Evaluate(Expression current, string value)
        {
            Logger.Info("Evaluating the specified value '{0}'.", value ?? "null");

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            foreach (var eval in evaluators)
            {
                var match = eval.Key.Match(value);
                if (match.Success)
                {
                    var result = eval.Value(match);
                    Logger.Verbose("Evaluated as '{0} : {1}'.", result.ToString(), result.GetType().FullName);
                    return builder.BuildUp(result);
                }
            }

            Logger.Verbose("Ignored the specified value because this didn't match any registered patterns.");
            return null;
        }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public override OperandEvaluator Clone()
        {
            var instance = new DynamicVariableEvaluator(builder);
            foreach (var key in evaluators.Keys)
            {
                instance.evaluators.Add(key, evaluators[key]);
            }
            return instance;
        }
    }
}
