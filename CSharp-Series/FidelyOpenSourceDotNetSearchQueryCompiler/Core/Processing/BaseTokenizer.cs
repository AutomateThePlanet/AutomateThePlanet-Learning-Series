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
    internal abstract class BaseTokenizer : ITokenFilter
    {
        public IEnumerable<IToken> Filter(IEnumerable<IToken> tokens)
        {
            Logger.Info("Tokenizing the specified token collection with '{0}'.", this.GetType().FullName);

            if (tokens == null)
            {
                Logger.Error("The specified tokens is null.");
                throw new ArgumentNullException("tokens");
            }

            var result = new List<IToken>();

            foreach (IToken token in tokens)
            {
                if (token == null)
                {
                    Logger.Error("The specified tokens contains null.");
                    throw new ArgumentException("Failed to tokenize because the specified argument contains null.", "tokens");
                }

                if (token is UncategorizedToken)
                {
                    Logger.Verbose("Found uncategorized token '{0}'.", token.Value);
                    result.AddRange(this.Tokenize((UncategorizedToken)token));
                }
                else
                {
                    result.Add(token);
                }
            }

            Logger.Info(result);

            return result;
        }

        protected abstract IEnumerable<IToken> Tokenize(UncategorizedToken token);
    }
}