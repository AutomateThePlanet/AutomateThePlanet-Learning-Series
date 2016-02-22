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
using Fidely.Framework.Compilation.Objects.Operators;
using Fidely.Framework.Integration;
using System;
using System.Text.RegularExpressions;
using Fidely.Framework.Compilation.Operators;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Fidely.Framework.Compilation.Objects
{
    /// <summary>
    /// Provides a set of extension methods to build up a search query compiler.
    /// </summary>
    public static class SearchQueryCompilerBuilderExtension
    {
        /// <summary>
        /// Builds up a search query compiler with default setting that is optimized for LINQ to Object.
        /// </summary>
        /// <typeparam name="T">The type of an elements in a collection that is filtered by a generated expression.</typeparam>
        /// <param name="instance">The search query compiler builder.</param>
        /// <returns>The default search query compiler that is optimized for LINQ to Object.</returns>
        public static SearchQueryCompiler<T> BuildUpDefaultCompilerForObject<T>(this SearchQueryCompilerBuilder instance)
        {
            var setting = instance.BuildUpDefaultObjectCompilerSetting<T>();
            return instance.BuildUpCompiler<T>(setting);
        }

        /// <summary>
        /// Builds up the default compiler setting that is optimized for LINQ to Object.
        /// </summary>
        /// <typeparam name="T">The type of an elements in a collection that is filtered by a generated expression.</typeparam>
        /// <param name="instance">The search query compiler builder.</param>
        /// <returns>The default compiler setting that is optimized for LINQ to Object.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801", Justification = "To make the extension method for SearchQueryCompilerBuilder, the instance parameter is needed.")]
        public static ObjectCompilerSetting BuildUpDefaultObjectCompilerSetting<T>(this SearchQueryCompilerBuilder instance)
        {
            var setting = new ObjectCompilerSetting();

            RegisterDynamicVariableEvaluatorForYearTimeSpan(setting.DynamicVariableEvaluator);
            RegisterDynamicVariableEvaluatorForMonthTimeSpan(setting.DynamicVariableEvaluator);
            RegisterDynamicVariableEvaluatorForDayTimeSpan(setting.DynamicVariableEvaluator);

            setting.StaticVariableEvaluator.RegisterVariable("Now", () => DateTime.Now, "Now");
            setting.StaticVariableEvaluator.RegisterVariable("Today", () => DateTime.Today, "Today");

            setting.Evaluators.Add(new PropertyEvaluator<T>());
            setting.Evaluators.Add(setting.DynamicVariableEvaluator);
            setting.Evaluators.Add(setting.StaticVariableEvaluator);
            setting.Evaluators.Add(new TypeConversionEvaluator());

            setting.Operators.Add(new NotPartialMatch<T>("!:", true, OperatorIndependency.Strong, "Not Partial matching operator"));
            setting.Operators.Add(new PartialMatch<T>(":", true, OperatorIndependency.Strong, "Partial matching operator"));
            setting.Operators.Add(new PrefixSearch<T>("=:", true, OperatorIndependency.Strong, "Prefix search operator"));
            setting.Operators.Add(new SuffixSearch<T>(":=", true, OperatorIndependency.Strong, "Suffix search operator"));
            setting.Operators.Add(new Equal<T>("=", true, OperatorIndependency.Strong, "Equal operator"));
            setting.Operators.Add(new NotEqual<T>("!=", true, OperatorIndependency.Strong, "Not equal operator"));
            setting.Operators.Add(new LessThan<T>("<", OperatorIndependency.Strong, "Less than operator"));
            setting.Operators.Add(new LessThanOrEqual<T>("<=", OperatorIndependency.Strong, "Less than or equal operator"));
            setting.Operators.Add(new GreaterThan<T>(">", OperatorIndependency.Strong, "Greater than operator"));
            setting.Operators.Add(new GreaterThanOrEqual<T>(">=", OperatorIndependency.Strong, "Greater than or equal operator"));
            setting.Operators.Add(new Add("+", 1, OperatorIndependency.Strong, "Add operator"));
            setting.Operators.Add(new Subtract("-", 1, OperatorIndependency.Strong, "Subtract operator"));
            setting.Operators.Add(new Multiply("*", 0, OperatorIndependency.Strong, "Multiply operator"));
            setting.Operators.Add(new Divide("/", 0, OperatorIndependency.Strong, "Divide operator"));

            return setting;
        }

        private static void RegisterDynamicVariableEvaluatorForYearTimeSpan(DynamicVariableEvaluator evaluator)
        {
            var regex = new Regex("^(\\d+)(y|year)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var item = new RegexAutoCompleteItem("NNNyear", "Represents time span (e.g. 1year)", (v, o) => Regex.IsMatch(v, "^\\d+$"), (v, o) => v + "year");
            evaluator.RegisterVariable(regex, o => TimeSpan.FromDays(Int32.Parse(o.Groups[1].Value, CultureInfo.CurrentCulture) * 365), item);
        }

        private static void RegisterDynamicVariableEvaluatorForMonthTimeSpan(DynamicVariableEvaluator evaluator)
        {
            var regex = new Regex("^(\\d+)(m|month)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var item = new RegexAutoCompleteItem("NNNmonth", "Represents time span (e.g. 1month)", (v, o) => Regex.IsMatch(v, "^\\d+$"), (v, o) => v + "month");
            evaluator.RegisterVariable(regex, o => TimeSpan.FromDays(Int32.Parse(o.Groups[1].Value, CultureInfo.CurrentCulture) * 30), item);
        }

        private static void RegisterDynamicVariableEvaluatorForDayTimeSpan(DynamicVariableEvaluator evaluator)
        {
            var regex = new Regex("^(\\d+)(d|day)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var item = new RegexAutoCompleteItem("NNNday", "Represents time span (e.g. 1day)", (v, o) => Regex.IsMatch(v, "^\\d+$"), (v, o) => v + "day");
            evaluator.RegisterVariable(regex, o => TimeSpan.FromDays(Int32.Parse(o.Groups[1].Value, CultureInfo.CurrentCulture)), item);
        }
    }
}
