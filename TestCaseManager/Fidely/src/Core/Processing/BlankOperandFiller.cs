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

using Fidely.Framework.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fidely.Framework.Processing
{
    internal class BlankOperandFiller : ITokenFilter
    {
        public IEnumerable<IToken> Filter(IEnumerable<IToken> input)
        {
            Logger.Info("Filling blank operands.");

            var result = new List<IToken>();

            var tokens = input.ToList();
            tokens.Insert(0, new StartToken());
            tokens.Add(new EndToken());

            for (int i = 0; i < tokens.Count - 1; i++)
            {
                var left = tokens[i];
                var right = tokens[i + 1];

                result.Add(left);

                if ((left is StartToken || left is OperatorToken || left is OpenedParenthesisToken) && (right is EndToken || right is OperatorToken || right is ClosedParenthesisToken))
                {
                    result.Add(new BlankOperandToken());
                    Logger.Verbose("Filled a blank operand between '{0}' and '{1}'.", left, right);
                }
            }

            result.RemoveAll(o => o is StartToken || o is EndToken);

            Logger.Info(result);

            return result;
        }

        private class StartToken : IToken
        {
            public string Value { get { return null; } }
        }

        private class EndToken : IToken
        {
            public string Value { get { return null; } }
        }
    }
}
