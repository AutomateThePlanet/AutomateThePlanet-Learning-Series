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

namespace Fidely.Framework.Parsing
{
    internal class SyntacticAnalyzer<T>
    {
        private readonly ComparativeOperator defaultOperator;

        internal SyntacticAnalyzer(ComparativeOperator defaultOperator)
        {
            this.defaultOperator = defaultOperator;
        }

        public TreeNode Parse(IEnumerable<IToken> tokens)
        {
            Logger.Info("Parsing tokens.");

            if (tokens.Count() == 0)
            {
                return this.BuildUpDefaultComparativeNode(new TreeNode());
            }

            var nodes = new Stack<TreeNode>();
            var stack = new Stack<IToken>();

            foreach (IToken token in tokens)
            {
                if (token is OperandToken)
                {
                    nodes.Push(new TreeNode(token));
                }
                else if (token is ClosedParenthesisToken)
                {
                    while (!(stack.Peek() is OpenedParenthesisToken))
                    {
                        TreeNode right = nodes.Pop();
                        TreeNode left = nodes.Pop();
                        TreeNode node = this.BuildUpTreeNode((OperatorToken)stack.Pop(), left, right);
                        nodes.Push(node);
                    }
                    nodes.Peek().Freeze();
                    stack.Pop();
                }
                else if (token is OpenedParenthesisToken)
                {
                    stack.Push(token);
                }
                else
                {
                    while (stack.Count > 0)
                    {
                        if (stack.Peek() is OpenedParenthesisToken)
                        {
                            break;
                        }
                        else
                        {
                            var left = stack.Peek() as OperatorToken;
                            var right = token as OperatorToken;
                            if (right.TypePriority < left.TypePriority || (right.TypePriority == left.TypePriority && right.Priority < left.Priority))
                            {
                                break;
                            }
                        }

                        TreeNode r = nodes.Pop();
                        TreeNode l = nodes.Pop();
                        TreeNode node = this.BuildUpTreeNode((OperatorToken)stack.Pop(), l, r);
                        nodes.Push(node);
                    }
                    stack.Push(token);
                }
            }

            while (stack.Count > 0)
            {
                TreeNode r = nodes.Pop();
                TreeNode l = nodes.Pop();
                TreeNode node = this.BuildUpTreeNode((OperatorToken)stack.Pop(), l, r);
                nodes.Push(node);
            }

            if (nodes.Peek().Value is OperandToken || nodes.Peek().Value is CalculatingOperatorToken)
            {
                nodes.Push(this.BuildUpDefaultComparativeNode(nodes.Pop()));
            }

            return nodes.Pop();
        }

        private TreeNode BuildUpTreeNode(OperatorToken token, TreeNode left, TreeNode right)
        {
            TreeNode root = null;
            var node = new TreeNode(token);

            if (token is LogicalOperatorToken)
            {
                if (left.Value is CalculatingOperatorToken || left.Value is OperandToken)
                {
                    node.Left = this.BuildUpDefaultComparativeNode(left);
                }
                else
                {
                    node.Left = left;
                }

                if (right.Value is CalculatingOperatorToken || right.Value is OperandToken)
                {
                    node.Right = this.BuildUpDefaultComparativeNode(right);
                }
                else
                {
                    node.Right = right;
                }

                root = node;
            }
            else if (token is ComparativeOperatorToken)
            {
                if (left.Value is LogicalOperatorToken || left.Value is ComparativeOperatorToken)
                {
                    var and = new TreeNode(new LogicalAndOperatorToken());
                    and.Left = left;
                    and.Right = node;
                    if (left.IsFreezed)
                    {
                        node.Left = new TreeNode();
                    }
                    else
                    {
                        TreeNode r = and.Left.Right;
                        while (r.Value is LogicalOperatorToken || r.Value is ComparativeOperatorToken)
                        {
                            r = r.Right;
                        }
                        node.Left = r;
                    }
                    root = and;
                }
                else
                {
                    node.Left = left;
                }

                if (right.Value is LogicalOperatorToken || right.Value is ComparativeOperatorToken)
                {
                    var and = new TreeNode(new LogicalAndOperatorToken());
                    and.Left = node;
                    and.Right = right;
                    if (right.IsFreezed)
                    {
                        node.Right = new TreeNode();
                    }
                    else
                    {
                        TreeNode l = and.Right.Left;
                        while (l.Value is LogicalOperatorToken || l.Value is ComparativeOperatorToken)
                        {
                            l = l.Left;
                        }
                        node.Right = l;
                    }

                    if (root != null)
                    {
                        root.Right = and;
                    }
                    else
                    {
                        root = and;
                    }
                }
                else
                {
                    node.Right = right;
                }

                if (root == null)
                {
                    root = node;
                }
            }
            else if (token is CalculatingOperatorToken)
            {
                if (left.Value is LogicalOperatorToken || left.Value is ComparativeOperatorToken)
                {
                    var and = new TreeNode(new LogicalAndOperatorToken());
                    and.Left = left;
                    and.Right = this.BuildUpDefaultComparativeNode(node);
                    node.Left = new TreeNode();
                    root = and;
                }
                else
                {
                    node.Left = left;
                }

                if (right.Value is LogicalOperatorToken || right.Value is ComparativeOperatorToken)
                {
                    var and = new TreeNode(new LogicalAndOperatorToken());
                    and.Left = this.BuildUpDefaultComparativeNode(node);
                    and.Right = right;
                    node.Right = new TreeNode();
                    if (root != null)
                    {
                        root.Right = and;
                    }
                    else
                    {
                        root = and;
                    }
                }
                else
                {
                    node.Right = right;
                }

                if (root == null)
                {
                    root = node;
                }
            }

            return root;
        }

        private TreeNode BuildUpDefaultComparativeNode(TreeNode node)
        {
            var comparative = new TreeNode(new ComparativeOperatorToken(this.defaultOperator));
            comparative.Left = new TreeNode();
            comparative.Right = node;
            return comparative;
        }
    }
}