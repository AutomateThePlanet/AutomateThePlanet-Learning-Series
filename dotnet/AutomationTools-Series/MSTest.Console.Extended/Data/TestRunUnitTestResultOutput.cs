﻿// <copyright file="TestRunUnitTestResultOutput.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Linq;

namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestResultOutput
    {
        private string _stdOutField;

        private string _debugTraceField;

        private TestRunUnitTestResultOutputErrorInfo _errorInfoField;

        /// <remarks/>
        public string StdOut
        {
            get
            {
                return _stdOutField;
            }
            set
            {
                _stdOutField = value;
            }
        }

        /// <remarks/>
        public string DebugTrace
        {
            get
            {
                return _debugTraceField;
            }
            set
            {
                _debugTraceField = value;
            }
        }

        /// <remarks/>
        public TestRunUnitTestResultOutputErrorInfo ErrorInfo
        {
            get
            {
                return _errorInfoField;
            }
            set
            {
                _errorInfoField = value;
            }
        }
    }
}