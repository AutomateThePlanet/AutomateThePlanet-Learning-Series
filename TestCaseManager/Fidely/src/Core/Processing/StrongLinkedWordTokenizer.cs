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

using Fidely.Framework.Compilation;
using Fidely.Framework.Compilation.Operators;
using Fidely.Framework.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fidely.Framework.Processing
{
    internal class StrongLinkedWordTokenizer : BaseTokenizer
    {
        private IDictionary<string, IToken> mappings;


        internal StrongLinkedWordTokenizer(IEnumerable<FidelyOperator> operators)
        {
            mappings = new Dictionary<string, IToken>();

            mappings.Add(OpenedParenthesisToken.Symbol, new OpenedParenthesisToken());
            mappings.Add(ClosedParenthesisToken.Symbol, new ClosedParenthesisToken());

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

                foreach (var symbol in mappings.Keys.Where(o => o.Length <= value.Length - current).OrderByDescending(o => o.Length))
                {
                    if (value.Substring(current, symbol.Length).Equals(symbol, StringComparison.OrdinalIgnoreCase))
                    {
                        if (startIndex < current)
                        {
                            result.Add(new UncategorizedToken(value.Substring(startIndex, current - startIndex)));
                            Logger.Verbose("Extracted an uncategorized token '{0}'.", result.Last().Value);
                        }
                        current += symbol.Length - 1;
                        startIndex = current + 1;
                        result.Add(mappings[symbol]);
                        Logger.Verbose("Extracted an operand token '{0}'.", result.Last().Value);
                        break;
                    }
                }
                current++;
            }

            if (startIndex < current)
            {
                Logger.Verbose("Progressing tokenization (current index = '{0}', start index = '{1}').", current, startIndex);
                result.Add(new UncategorizedToken(value.Substring(startIndex, current - startIndex)));
                Logger.Verbose("Extracted an uncategorized token '{0}'.", result.Last().Value);
            }

            return result;
        }
    }
}
