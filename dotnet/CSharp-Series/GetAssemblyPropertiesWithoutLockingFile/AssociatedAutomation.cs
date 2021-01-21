// <copyright file="AssociatedAutomation.cs" company="Automate The Planet Ltd.">
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
namespace GetAssemblyPropertiesWithoutLockingFile
{
    public class AssociatedAutomation
    {
        public const string NONE = "None";

        public AssociatedAutomation()
        {
            TestName = NONE;
            Assembly = NONE;
            Type = NONE;
        }

        public AssociatedAutomation(string testInfo)
        {
            var infos = testInfo.Split(',');
            TestName = infos[1];
            Assembly = infos[2];
            Type = infos[3];
        }

        public string Type { get; set; }

        public string Assembly { get; set; }

        public string TestName { get; set; }

        public override string ToString()
        {
            return string.Format("Type: {0} Assembly: {1} TestName: {2}", Type, Assembly, TestName);
        }
    }
}
