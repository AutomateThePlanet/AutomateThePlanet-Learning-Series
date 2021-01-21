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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Fidely.Framework.Caching;
using Fidely.Framework.Compilation;
using Fidely.Framework.Compilation.Evaluators;
using Fidely.Framework.Compilation.Operators;
using Fidely.Framework.Parsing;
using Fidely.Framework.Processing;
using Fidely.Framework.Tokens;

namespace Fidely.Framework
{
    /// <summary>
    /// Provides the feature to compile a search query string into an expression tree to filter a collection object.
    /// </summary>
    /// <typeparam name="T">The type of an elements in a collection that is filtered by a generated expression.</typeparam>
    public class SearchQueryCompiler<T> : IWarningNotifier
    {
        private readonly EvaluatorCollection _evaluators;

        private readonly OperatorCollection _operators;

        private ComparativeOperator _defaultComparativeOperator;

        internal SearchQueryCompiler()
        {
            _evaluators = new EvaluatorCollection();
            _operators = new OperatorCollection();
        }

        /// <summary>
        /// Occurs when a warning message is notified on tokenizing, parsing or compiling process.
        /// </summary>
        public event EventHandler<WarningNotifiedEventArgs> WarningNotified;

        internal Cache<string, Expression<Func<T, bool>>> Cache { get; set; }

        /// <summary>
        /// Compiles the specified query string into an expression tree to filter a collection object.
        /// </summary>
        /// <param name="query">The query string that is compiled.</param>
        /// <returns>The expression tree that is generated from the specified query string.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006", Justification = "The type of the return value of this method should be the strongly typed expression tree.")]
        public Expression<Func<T, bool>> Compile(string query)
        {
            Logger.Info("Compiling the specified search query '{0}'.", query);

            query = (query != null) ? query.Trim() : "";

            if (string.IsNullOrWhiteSpace(query))
            {
                var current = Expression.Parameter(typeof(T), "current");
                Logger.Info("Generated the constant expression that represents 'true' because the specified query string is empty.", query);
                return Expression.Lambda<Func<T, bool>>(Expression.Constant(true), current);
            }

            var start = DateTime.Now;

            var result = Cache.GetValue(query);
            if (result == null)
            {
                result = Compile(new UncategorizedToken(query));
                Cache.SetValue(query, result);
            }

            Logger.Info("Compilation completed ({0} ms).", (DateTime.Now - start).TotalMilliseconds);

            return result;
        }

        internal void RegisterEvaluator(OperandEvaluator evaluator)
        {
            var instance = evaluator.Clone();
            instance.WarningNotifier = this;
            _evaluators.Add(instance);
            Logger.Verbose("Registered an operand evaluator '{0}'.", instance.GetType().FullName);
        }

        internal void RegisterOperator(FidelyOperator op)
        {
            var instance = op.Clone();
            instance.WarningNotifier = this;
            _operators.Add(instance);
            Logger.Verbose("Registered an operator '{0}' (symbol = {1}, independency = {2}).", instance.GetType().FullName, instance.Symbol, instance.Independency);

            if (_defaultComparativeOperator == null && instance is ComparativeOperator)
            {
                _defaultComparativeOperator = (ComparativeOperator)instance;
                Logger.Verbose("Registered an operator '{0}' as the default comparative operator.", instance.Symbol);
            }
        }

        private Expression<Func<T, bool>> Compile(IToken token)
        {
            if (_defaultComparativeOperator == null)
            {
                Logger.Error("The defualt comparative operator isn't registered.");
                throw new InvalidOperationException("Failed to compile the secified search query because this compiler doesn't have comparative operator. You have to register a comparative operator at least one.");
            }

            Logger.Info("STAGE 1: Tokenizing the specified query string.");

            var filters = BuildUpTokenFilters();
            IEnumerable<IToken> tokens = new List<IToken> { token };
            filters.ToList().ForEach(o => tokens = o.Filter(tokens));

            Logger.Info("STAGE 2: Building an abstract syntax tree.");

            var analyzer = new SyntacticAnalyzer<T>(_defaultComparativeOperator);
            var root = analyzer.Parse(tokens);

            Logger.Info("STAGE 3: Compiling an abstract syntax tree.");

            var current = Expression.Parameter(typeof(T), "current");
            return Expression.Lambda<Func<T, bool>>(Compile(current, root).Expression, current);
        }

        private Operand Compile(Expression current, TreeNode node)
        {
            if (node.Value is LogicalAndOperatorToken)
            {
                Logger.Info("Compiling the specified AND node.");
                var left = Compile(current, node.Left);
                var right = Compile(current, node.Right);
                return new Operand(Expression.And(left.Expression, right.Expression), typeof(bool));
            }
            else if (node.Value is LogicalOrOperatorToken)
            {
                Logger.Info("Compiling the specified OR node.");
                var left = Compile(current, node.Left);
                var right = Compile(current, node.Right);
                return new Operand(Expression.Or(left.Expression, right.Expression), typeof(bool));
            }
            else if (node.Value is ComparativeOperatorToken)
            {
                Logger.Info("Compiling the specified comparative operator node '{0}'.", node.Value.Value);
                var left = Evaluate(current, node.Left);
                var right = Evaluate(current, node.Right);
                var token = (ComparativeOperatorToken)node.Value;
                return token.Operator.Compare(current, left, right);
            }

            Logger.Error("Failed to compile because the specified node has invalid token '{0}'.", node.Value);
            throw new InvalidOperationException("Failed to compile because the specified node has invalid token.");
        }

        private Operand Evaluate(Expression current, TreeNode node)
        {
            if (node.Value is CalculatingOperatorToken)
            {
                Logger.Info("Evaluating the specified calculating operator node '{0}'.", node.Value.Value);
                var left = Evaluate(current, node.Left);
                var right = Evaluate(current, node.Right);
                var token = (CalculatingOperatorToken)node.Value;
                return token.Operator.Calculate(left, right);
            }
            else if (node.Value is BlankOperandToken)
            {
                Logger.Info("Evaluated the specified node as blank operand.");
                return new BlankOperand();
            }
            else if (node.Value is QuotedOperandToken)
            {
                Logger.Info("Evaluated the specified node as quoted string operand '{0}'.", node.Value.Value);
                return new Operand(Expression.Constant(node.Value.Value), typeof(string));
            }
            else if (node.Value is OperandToken)
            {
                Logger.Info("Evaluating the specified operand node '{0}'.", node.Value.Value);
                foreach (var evaluator in _evaluators)
                {
                    var result = evaluator.Evaluate(current, node.Value.Value);
                    if (result != null)
                    {
                        Logger.Info("Evaluated the value of token ('{0}') by the evaluator '{1}'.", node.Value, evaluator.GetType().FullName);
                        return result;
                    }
                }
            }

            Logger.Error("Failed to evaluate because the specified node has invalid token '{0}'.", node.Value);
            throw new InvalidOperationException("Failed to evaluate because the specified node has invalid token.");
        }

        private IEnumerable<ITokenFilter> BuildUpTokenFilters()
        {
            Logger.Info("Building up token filters.");

            var filters = new List<ITokenFilter>();

            filters.Add(new QuotedWordTokenizer());
            filters.Add(new StrongLinkedWordTokenizer(_operators.Where(o => o.Independency == OperatorIndependency.Strong)));
            filters.Add(new WeakLinkedWordTokenizer(_operators.Where(o => o.Independency == OperatorIndependency.Weak)));
            filters.Add(new ParenthesisFiller());
            filters.Add(new BlankOperandFiller());
            filters.Add(new LogicalAndFiller());

            return filters;
        }

        void IWarningNotifier.Notify(Type notifiedBy, string symbol, string format, params object[] args)
        {
            if (WarningNotified != null)
            {
                WarningNotified(this, new WarningNotifiedEventArgs(notifiedBy, symbol, string.Format(CultureInfo.CurrentCulture, format, args)));
            }
        }
    }
}