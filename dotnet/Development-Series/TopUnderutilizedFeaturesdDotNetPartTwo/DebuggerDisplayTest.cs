// <copyright file="DebuggerDisplayTest.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;

namespace TopUnderutilizedFeaturesdDotNetPartTwo
{
    ////[DebuggerDisplay("{DebuggerDisplay}")]
    [DebuggerDisplay("Age {Age > 0 ? Age : 5}")]
    [DebuggerStepThroughAttribute]
    public class DebuggerDisplayTest
    {
        private string _squirrelFirstNameName;
        private string _squirrelLastNameName;

        public string SquirrelFirstNameName
        {
            get
            {
                return _squirrelFirstNameName;
            }
            set
            {
                _squirrelFirstNameName = value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public string SquirrelLastNameName
        {
            get
            {
                return _squirrelLastNameName;
            }
            set
            {
                _squirrelLastNameName = value;
            }
        }

        public int Age { get; set; }

        private string DebuggerDisplay
        {
            get { return string.Format("{0} de {1}", SquirrelFirstNameName, SquirrelLastNameName); }
        }
    }
}