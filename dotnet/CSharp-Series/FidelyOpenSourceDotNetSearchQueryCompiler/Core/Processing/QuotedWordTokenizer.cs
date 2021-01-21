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
using Fidely.Framework.Tokens;

namespace Fidely.Framework.Processing
{
    internal class QuotedWordTokenizer : BaseTokenizer
    {
        protected override IEnumerable<IToken> Tokenize(UncategorizedToken token)
        {
            Logger.Info("Tokenizing the specified uncategorized token '{0}' with '{1}'.", token.Value, GetType().FullName);

            var result = new List<IToken>();

            var value = token.Value;
            var current = 0;
            var startIndex = 0;

            while (current < token.Value.Length)
            {
                Logger.Verbose("Progressing tokenization (current index = '{0}', start index = '{1}').", current, startIndex);

                if (value[current] == '\'' || value[current] == '"')
                {
                    if (startIndex < current)
                    {
                        result.Add(new UncategorizedToken(value.Substring(startIndex, current - startIndex)));
                        Logger.Verbose("Extracted an uncategorized token '{0}'.", result.Last().Value);
                    }

                    var endIndex = value.IndexOf(value[current], current + 1);
                    if (endIndex != -1)
                    {
                        result.Add(BuildUpToken(value[current], value.Substring(current + 1, endIndex - (current + 1))));
                        current = endIndex + 1;
                        startIndex = endIndex + 1;
                        Logger.Verbose("Extracted an uncategorized token '{0}'.", result.Last().Value);
                    }
                    else
                    {
                        result.Add(BuildUpToken(value[current], value.Substring(current + 1)));
                        current = value.Length;
                        startIndex = value.Length;
                        Logger.Verbose("Extracted a quoted token '{0}'.", result.Last().Value);
                    }
                }
                else
                {
                    current++;
                }
            }

            if (startIndex < current)
            {
                Logger.Verbose("Progressing tokenization (current index = '{0}', start index = '{1}').", current, startIndex);
                result.Add(new UncategorizedToken(value.Substring(startIndex, current - startIndex)));
                Logger.Verbose("Extracted an uncategorized token '{0}'.", result.Last().Value);
            }

            return result;
        }

        private static IToken BuildUpToken(char mark, string value)
        {
            return (mark == '\'') ? new QuotedOperandToken(value) : new OperandToken(value);
        }
    }
}