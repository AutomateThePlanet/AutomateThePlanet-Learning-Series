// <copyright file="TestRunTestSettingsDeployment.cs" company="Automate The Planet Ltd.">
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
using System.Xml.Serialization;

namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestSettingsDeployment
    {
        private string _userDeploymentRootField;

        private bool _useDefaultDeploymentRootField;

        private string _runDeploymentRootField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string UserDeploymentRoot
        {
            get
            {
                return _userDeploymentRootField;
            }
            set
            {
                _userDeploymentRootField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public bool UseDefaultDeploymentRoot
        {
            get
            {
                return _useDefaultDeploymentRootField;
            }
            set
            {
                _useDefaultDeploymentRootField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string RunDeploymentRoot
        {
            get
            {
                return _runDeploymentRootField;
            }
            set
            {
                _runDeploymentRootField = value;
            }
        }
    }
}