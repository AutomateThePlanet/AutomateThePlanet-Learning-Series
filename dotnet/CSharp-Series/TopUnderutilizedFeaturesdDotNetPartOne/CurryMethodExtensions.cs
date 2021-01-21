// <copyright file="CurryMethodExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

namespace TopUnderutilizedFeaturesdDotNetPartOne
{
    public static class CurryMethodExtensions
    {
        public static Func<A, Func<B, Func<C, R>>> Curry<A, B, C, R>(this Func<A, B, C, R> f)
        {
            return a => b => c => f(a, b, c);
        }

        public static Func<C, R> Partial<A, B, C, R>(this Func<A, B, C, R> f, A a, B b)
        {
            return c => f(a, b, c);
        }
    }
}
