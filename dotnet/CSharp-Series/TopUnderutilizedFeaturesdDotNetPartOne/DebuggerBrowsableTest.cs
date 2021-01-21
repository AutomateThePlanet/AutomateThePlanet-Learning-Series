﻿// <copyright file="DebuggerBrowsableTest.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
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
using System.Diagnostics;

namespace TopUnderutilizedFeaturesdDotNetPartOne
{
    public static class DebuggerBrowsableTest
    {
        private static string squirrelFirstNameName;
        private static string squirrelLastNameName;

        // The following DebuggerBrowsableAttribute prevents the property following it 
        // from appearing in the debug window for the class.
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static string SquirrelFirstNameName 
        {
            get
            {
                return squirrelFirstNameName;
            }
            set
            {
                squirrelFirstNameName = value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public static string SquirrelLastNameName
        {
            get
            {
                return squirrelLastNameName;
            }
            set
            {
                squirrelLastNameName = value;
            }
        }
    }
}