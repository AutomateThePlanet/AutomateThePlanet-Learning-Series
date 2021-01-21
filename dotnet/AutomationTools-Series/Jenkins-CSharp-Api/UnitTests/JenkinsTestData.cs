// <copyright file="JenkinsTestData.cs" company="Automate The Planet Ltd.">
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

using System.IO;
using System.Reflection;

namespace JenkinsCSharpApi.UnitTests
{
    /// <summary>
    /// Contains Common Jenkins Tests Data.
    /// </summary>
    public class JenkinsTestData
    {
        /// <summary>
        /// The specific build XML
        /// </summary>
        public static string SpecificBuildXml;
        public const string JenkinsUrl = @"http://localhost:8080";
        public const string ProjectName = "Jenkins-CSharp-Api.Parameterized";

        /// <summary>
        /// Initializes the <see cref="JenkinsTestData"/> class.
        /// </summary>
        static JenkinsTestData()
        {
            string currentExecutionFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
            string filePath = Path.Combine(currentExecutionFolder, "UnitTests", "XmlTestFiles", "SpecificBuildXml.xml");
            SpecificBuildXml = File.ReadAllText(filePath);
            filePath = Path.Combine(currentExecutionFolder, "UnitTests", "XmlTestFiles", "BuildStatusSingleBuildNodeXml.xml");
            BuildStatusSingleBuildNodeXml = File.ReadAllText(filePath);
            filePath = Path.Combine(currentExecutionFolder, "UnitTests", "XmlTestFiles", "BuildStatusXml.xml");
            BuildStatusXml = File.ReadAllText(filePath);
        }

        /// <summary>
        /// Gets or sets the build status single build node XML.
        /// </summary>
        /// <value>
        /// The build status single build node XML.
        /// </value>
        public static string BuildStatusSingleBuildNodeXml { get; set; }

        /// <summary>
        /// Gets or sets the build status XML.
        /// </summary>
        /// <value>
        /// The build status XML.
        /// </value>
        public static string BuildStatusXml { get; set; }
    }
}