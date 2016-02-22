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
using Fidely.Framework.Compilation.Operators;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fidely.Framework
{
    /// <summary>
    /// Provides the feature to build up a search query compiler.
    /// </summary>
    public class SearchQueryCompilerBuilder
    {
        private static SearchQueryCompilerBuilder instance;


        /// <summary>
        /// The instance of search query compiler builder.
        /// </summary>
        public static SearchQueryCompilerBuilder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchQueryCompilerBuilder();
                }
                return instance;
            }
        }


        private SearchQueryCompilerBuilder()
        {
        }


        /// <summary>
        /// Builds up a new search query compiler with the specified compiler setting.
        /// </summary>
        /// <typeparam name="T">The type of an elements in a collection that is filtered by a generated expression.</typeparam>
        /// <param name="setting">The compiler setting.</param>
        /// <returns>The built up search query compiler.</returns>
        public SearchQueryCompiler<T> BuildUpCompiler<T>(CompilerSetting setting)
        {
            var compiler = new SearchQueryCompiler<T>();

            Logger.Info("Creating a compiler for '{0}'.", typeof(T).FullName);

            compiler.Cache = new Caching.Cache<string, Expression<Func<T, bool>>>(setting.CacheSize);

            setting.Evaluators.ToList().ForEach(o => compiler.RegisterEvaluator(o));
            compiler.RegisterEvaluator(new GuardianEvaluator());

            setting.Operators.ToList().ForEach(o => compiler.RegisterOperator(o));

            return compiler;
        }
    }
}
