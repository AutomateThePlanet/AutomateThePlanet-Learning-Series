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
using Fidely.Framework.Tokens;

namespace Fidely.Framework.Parsing
{
    internal class TreeNode
    {
        internal TreeNode()
        {
            Value = new BlankOperandToken();
        }

        internal TreeNode(IToken token)
        {
            Value = token;
        }

        internal IToken Value { get; private set; }

        internal TreeNode Left { get; set; }

        internal TreeNode Right { get; set; }

        internal bool IsFreezed { get; private set; }

        internal void Freeze()
        {
            IsFreezed = true;
        }
    }
}