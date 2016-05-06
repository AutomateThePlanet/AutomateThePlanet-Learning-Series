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
using Fidely.Framework.Compilation.Objects.Evaluators;

namespace Fidely.Framework.Compilation.Objects
{
    /// <summary>
    /// Represents the compiler setting for LINQ to Object.
    /// </summary>
    public class ObjectCompilerSetting : CompilerSetting
    {
        internal ObjectCompilerSetting()
        {
            var builder = new OperandBuilder();
            this.DynamicVariableEvaluator = new DynamicVariableEvaluator(builder);
            this.StaticVariableEvaluator = new StaticVariableEvaluator(builder);
        }

        /// <summary>
        /// The dynamic variable evaluator.
        /// </summary>
        public DynamicVariableEvaluator DynamicVariableEvaluator { get; private set; }

        /// <summary>
        /// The static variable evaluator.
        /// </summary>
        public StaticVariableEvaluator StaticVariableEvaluator { get; private set; }
    }
}