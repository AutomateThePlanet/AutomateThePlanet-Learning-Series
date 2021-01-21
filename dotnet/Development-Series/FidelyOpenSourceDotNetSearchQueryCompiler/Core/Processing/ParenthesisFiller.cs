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
using Fidely.Framework.Tokens;

namespace Fidely.Framework.Processing
{
    internal class ParenthesisFiller : ITokenFilter
    {
        public IEnumerable<IToken> Filter(IEnumerable<IToken> tokens)
        {
            Logger.Info("Filling unclosed parenthesis tokens.");

            var stack = new Stack<IToken>();

            foreach (var token in tokens)
            {
                if (token is OpenedParenthesisToken)
                {
                    stack.Push(token);
                }
                else if (token is ClosedParenthesisToken)
                {
                    if (stack.Count > 0 && stack.Peek() is OpenedParenthesisToken)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(token);
                    }
                }
            }

            var result = new List<IToken>(tokens);
            foreach (var parenthesis in stack)
            {
                if (parenthesis is OpenedParenthesisToken)
                {
                    result.Add(new ClosedParenthesisToken());
                    Logger.Verbose("Filled close parenthesis token.");
                }
                else
                {
                    result.Insert(0, new OpenedParenthesisToken());
                    Logger.Verbose("Filled open parenthesis token.");
                }
            }

            Logger.Info(result);

            return result;
        }
    }
}