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
    internal class LogicalAndFiller : ITokenFilter
    {
        public IEnumerable<IToken> Filter(IEnumerable<IToken> input)
        {
            Logger.Info("Filling logical and operators.");

            if (input.Count() == 0)
            {
                return input;
            }

            var result = new List<IToken>();

            var tokens = input.ToList();
            for (var i = 0; i < tokens.Count - 1; i++)
            {
                var left = tokens[i];
                var right = tokens[i + 1];

                result.Add(left);
                if ((left is OperandToken || left is ClosedParenthesisToken) && (right is OperandToken || right is OpenedParenthesisToken))
                {
                    result.Add(new LogicalAndOperatorToken());
                    Logger.Verbose("Filled a logical and operator between '{0}' and '{1}'.", left, right);
                }
            }

            result.Add(tokens.Last());

            Logger.Info(result);

            return result;
        }
    }
}