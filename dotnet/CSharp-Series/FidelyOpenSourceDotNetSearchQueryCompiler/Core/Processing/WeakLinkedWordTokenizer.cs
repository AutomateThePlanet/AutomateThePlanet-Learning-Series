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
using Fidely.Framework.Compilation.Operators;
using Fidely.Framework.Tokens;

namespace Fidely.Framework.Processing
{
    internal class WeakLinkedWordTokenizer : BaseTokenizer
    {
        private readonly IDictionary<string, IToken> mappings;

        internal WeakLinkedWordTokenizer(IEnumerable<FidelyOperator> operators)
        {
            mappings = new Dictionary<string, IToken>();

            mappings.Add(LogicalAndOperatorToken.Symbol, new LogicalAndOperatorToken());
            mappings.Add(LogicalOrOperatorToken.Symbol, new LogicalOrOperatorToken());

            foreach (var op in operators)
            {
                if (op is ComparativeOperator)
                {
                    mappings.Add(op.Symbol, new ComparativeOperatorToken((ComparativeOperator)op));
                }
                else
                {
                    mappings.Add(op.Symbol, new CalculatingOperatorToken((CalculatingOperator)op));
                }
            }
        }

        protected override IEnumerable<IToken> Tokenize(UncategorizedToken token)
        {
            Logger.Info("Tokenizing the specified uncategorized token '{0}' with '{1}'.", token.Value, GetType().FullName);

            var result = new List<IToken>();

            var value = token.Value;
            var current = 0;
            var startIndex = 0;

            while (current < value.Length)
            {
                Logger.Verbose("Progressing tokenization (current index = '{0}', start index = '{1}').", current, startIndex);

                if (Char.IsWhiteSpace(value[current]))
                {
                    if (startIndex < current)
                    {
                        var operand = value.Substring(startIndex, current - startIndex);
                        var symbol = mappings.Keys.FirstOrDefault(o => o.Equals(operand, StringComparison.OrdinalIgnoreCase));
                        if (symbol != null)
                        {
                            result.Add(mappings[symbol]);
                            Logger.Verbose("Extracted an operator token '{0}'.", symbol);
                        }
                        else
                        {
                            result.Add(new OperandToken(operand));
                            Logger.Verbose("Extracted an operand token '{0}'.", result.Last().Value);
                        }
                    }
                    startIndex = current + 1;
                }
                current++;
            }

            if (startIndex < current)
            {
                Logger.Verbose("Progressing tokenization (current index = '{0}', start index = '{1}').", current, startIndex);
                result.Add(new OperandToken(value.Substring(startIndex, current - startIndex)));
                Logger.Verbose("Extracted an operand token '{0}'.", result.Last().Value);
            }

            return result;
        }
    }
}