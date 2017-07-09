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
using System.Globalization;
using System.Linq.Expressions;
using Fidely.Framework.Integration;

namespace Fidely.Framework.Compilation.Evaluators
{
    /// <summary>
    /// Represents the operand evaluator that maps a statically defined variable name to a value generator.
    /// </summary>
    public class StaticVariableEvaluator : OperandEvaluator
    {
        private readonly IOperandBuilder builder;

        private readonly Dictionary<string, Func<object>> evaluators;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="builder">The operand builder to build up a new operand from any object.</param>
        public StaticVariableEvaluator(IOperandBuilder builder)
        {
            this.builder = builder;
            evaluators = new Dictionary<string, Func<object>>();
        }

        /// <summary>
        /// Registers a new mapping between a variable name and a value generator.
        /// </summary>
        /// <param name="name">The variable name.</param>
        /// <param name="procedure">The value generator.</param>
        public void RegisterVariable(string name, Func<object> procedure)
        {
            RegisterVariable(name, procedure, null);
        }

        /// <summary>
        /// Registers a new mapping between a variable name and a value generator.
        /// </summary>
        /// <param name="name">The variable name.</param>
        /// <param name="procedure">The value generator.</param>
        /// <param name="description">The description of the registered variable.</param>
        public void RegisterVariable(string name, Func<object> procedure, string description)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (procedure == null)
            {
                throw new ArgumentNullException("procedure");
            }

            if (evaluators.ContainsKey(name.ToUpperInvariant()))
            {
                var message = String.Format(CultureInfo.CurrentUICulture, "Failed to register the specified variable '{0}' because this variable is already registered.", name);
                throw new ArgumentException(message, "name");
            }

            Logger.Verbose("Registered the specified variable '{0}'.", name);

            evaluators[name.ToUpperInvariant()] = procedure;
            Register(new AutoCompleteItem(name, description));
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

            if (!evaluators.ContainsKey(value.ToUpperInvariant()))
            {
                Logger.Verbose("Ignored the specified value because this didn't match any registered variable names.");
                return null;
            }

            var eval = evaluators[value.ToUpperInvariant()];
            Logger.Verbose("Evaluated as '{0} : {1}'.", eval.ToString(), eval.GetType().FullName);

            return builder.BuildUp(eval());
        }

        /// <summary>
        /// Creates the clone instance.
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public override OperandEvaluator Clone()
        {
            var instance = new StaticVariableEvaluator(builder);
            foreach (var key in evaluators.Keys)
            {
                instance.evaluators.Add(key, evaluators[key]);
            }
            return instance;
        }
    }
}