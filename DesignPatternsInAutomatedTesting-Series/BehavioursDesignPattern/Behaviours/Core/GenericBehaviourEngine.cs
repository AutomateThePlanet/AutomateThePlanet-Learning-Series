// <copyright file="GenericBehaviourEngine.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>

using System;

namespace BehavioursDesignPattern.Behaviours.Core
{
    public static class GenericBehaviourEngine
    {
        public static void Execute(params Type[] pageBehaviours)
        {
            foreach (var pageBehaviour in pageBehaviours)
            {
                var currentbehaviour = Activator.CreateInstance(pageBehaviour) as Behaviour;
                currentbehaviour.PerformPreActAsserts();
                currentbehaviour.PerformAct();
                currentbehaviour.PerformPostActAsserts();
                currentbehaviour.PerformPostAct();
            }
        }

        public static void Execute<T1>()
            where T1 : Behaviour
        {
            Execute(typeof(T1));
        }

        public static void Execute<T1, T2>()
            where T1 : Behaviour
            where T2 : Behaviour
        {
            Execute(typeof(T1), typeof(T2));
        }

        public static void Execute<T1, T2, T3>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
        {
            Execute(typeof(T1), typeof(T2), typeof(T3));
        }

        public static void Execute<T1, T2, T3, T4>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
        {
            Execute(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static void Execute<T1, T2, T3, T4, T5>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
        {
            Execute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
        {
            Execute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
        {
            Execute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
        {
            Execute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
        {
            Execute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T11));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
            where T12 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T12),
                typeof(T11));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
            where T12 : Behaviour
            where T13 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T12),
                typeof(T13),
                typeof(T11));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
            where T12 : Behaviour
            where T13 : Behaviour
            where T14 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T12),
                typeof(T13),
                typeof(T14),
                typeof(T11));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
            where T12 : Behaviour
            where T13 : Behaviour
            where T14 : Behaviour
            where T15 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T12),
                typeof(T13),
                typeof(T14),
                typeof(T15),
                typeof(T11));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
            where T12 : Behaviour
            where T13 : Behaviour
            where T14 : Behaviour
            where T15 : Behaviour
            where T16 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T12),
                typeof(T13),
                typeof(T14),
                typeof(T15),
                typeof(T16),
                typeof(T11));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
            where T12 : Behaviour
            where T13 : Behaviour
            where T14 : Behaviour
            where T15 : Behaviour
            where T16 : Behaviour
            where T17 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T12),
                typeof(T13),
                typeof(T14),
                typeof(T15),
                typeof(T16),
                typeof(T17),
                typeof(T11));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
            where T12 : Behaviour
            where T13 : Behaviour
            where T14 : Behaviour
            where T15 : Behaviour
            where T16 : Behaviour
            where T17 : Behaviour
            where T18 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T12),
                typeof(T13),
                typeof(T14),
                typeof(T15),
                typeof(T16),
                typeof(T17),
                typeof(T18),
                typeof(T11));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
            where T12 : Behaviour
            where T13 : Behaviour
            where T14 : Behaviour
            where T15 : Behaviour
            where T16 : Behaviour
            where T17 : Behaviour
            where T18 : Behaviour
            where T19 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T12),
                typeof(T13),
                typeof(T14),
                typeof(T15),
                typeof(T16),
                typeof(T17),
                typeof(T18),
                typeof(T19),
                typeof(T11));
        }

        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>()
            where T1 : Behaviour
            where T2 : Behaviour
            where T3 : Behaviour
            where T4 : Behaviour
            where T5 : Behaviour
            where T6 : Behaviour
            where T7 : Behaviour
            where T8 : Behaviour
            where T9 : Behaviour
            where T10 : Behaviour
            where T11 : Behaviour
            where T12 : Behaviour
            where T13 : Behaviour
            where T14 : Behaviour
            where T15 : Behaviour
            where T16 : Behaviour
            where T17 : Behaviour
            where T18 : Behaviour
            where T19 : Behaviour
            where T20 : Behaviour
        {
            Execute(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T12),
                typeof(T13),
                typeof(T14),
                typeof(T15),
                typeof(T16),
                typeof(T17),
                typeof(T18),
                typeof(T19),
                typeof(T20),
                typeof(T11));
        }
    }
}