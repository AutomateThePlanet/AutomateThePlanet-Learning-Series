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

namespace Fidely.Framework.Compilation.Objects.Operators
{
    internal class OperandPair
    {
        internal Operand Left { get; private set; }

        internal Operand Right { get; private set; }


        internal OperandPair(Operand left, Operand right)
        {
            Left = left;
            Right = right;
        }


        internal bool Are(Type type)
        {
            return Left.OperandType == type && Right.OperandType == type;
        }

        internal bool Are(Type type1, Type type2)
        {
            return (Left.OperandType == type1 && Right.OperandType == type2) || (Left.OperandType == type2 && Right.OperandType == type1);
        }

        internal bool AreStrictly(Type type1, Type type2)
        {
            return Left.OperandType == type1 && Right.OperandType == type2;
        }
    }
}
