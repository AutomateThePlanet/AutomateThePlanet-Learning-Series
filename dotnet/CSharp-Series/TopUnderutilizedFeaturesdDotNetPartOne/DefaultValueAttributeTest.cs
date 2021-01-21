// <copyright file="DefaultValueAttributeTest.cs" company="Automate The Planet Ltd.">
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
using System.ComponentModel;

namespace TopUnderutilizedFeaturesdDotNetPartOne
{
    public class DefaultValueAttributeTest
    {
        public DefaultValueAttributeTest()
        {
            // Use the DefaultValue propety of each property to actually set it, via reflection.
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
            {
                var attr = (DefaultValueAttribute)prop.Attributes[typeof(DefaultValueAttribute)];
                if (attr != null)
                {
                    prop.SetValue(this, attr.Value);
                }
            }
        }

        [DefaultValue(25)]
        public int Age { get; set; }

        [DefaultValue("Anton")]
        public string FirstName { get; set; }

        [DefaultValue("Angelov")]
        public string LastName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} is {2}.", FirstName, LastName, Age);
        }
    }
}