// <copyright file="BuildAdapter.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using JenkinsCSharpApi.Interfaces;

namespace JenkinsCSharpApi.Services
{
    /// <summary>
    /// Contains Methods that create Jenkins Build and Wait for its execution.
    /// </summary>
    public class BuildAdapter : IBuildAdapter
    {
        /// <summary>
        /// The HTTP adapter
        /// </summary>
        private readonly IHttpAdapter httpAdapter;

        /// <summary>
        /// The parameterized queue build URL
        /// </summary>
        private readonly string parameterizedQueueBuildUrl;

        /// <summary>
        /// The build status URL
        /// </summary>
        private readonly string buildStatusUrl;

        /// <summary>
        /// The jekins server URL
        /// </summary>
        private readonly string jenkinsServerUrl;

        /// <summary>
        /// The project name
        /// </summary>
        private readonly string projectName;

        /// <summary>
        /// The specific build number status URL
        /// </summary>
        private string specificBuildNumberStatusUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildAdapter" /> class.
        /// </summary>
        /// <param name="httpAdapter">The HTTP helper.</param>
        /// <param name="jenkinsServerUrl">The jenkins server URL.</param>
        /// <param name="projectName">Name of the project.</param>
        public BuildAdapter(IHttpAdapter httpAdapter, string jenkinsServerUrl, string projectName) : this(httpAdapter)
        {
            if (string.IsNullOrEmpty(jenkinsServerUrl))
            {
                throw new ArgumentNullException("The ArgumentNullException was not throwed in case of empty Jenkins Service URL.");
            }
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException("The ArgumentNullException was not throwed in case of empty project name.");
            }
            this.jenkinsServerUrl = jenkinsServerUrl;
            this.projectName = projectName;
            this.buildStatusUrl = this.GenerateBuildStatusUrl(this.jenkinsServerUrl, this.projectName);
            this.parameterizedQueueBuildUrl = this.GenerateParameterizedQueueBuildUrl(this.jenkinsServerUrl, this.projectName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildAdapter"/> class.
        /// </summary>
        /// <param name="httpAdapter">The HTTP adapter.</param>
        /// <exception cref="System.ArgumentNullException">The ArgumentNullException was not throwed in case of httpAdapter is equal to null.</exception>
        public BuildAdapter(IHttpAdapter httpAdapter)
        {
            if (httpAdapter == null)
            {
                throw new ArgumentNullException("The ArgumentNullException was not throwed in case of httpAdapter is equal to null.");
            }
            this.httpAdapter = httpAdapter;
        }

        /// <summary>
        /// Gets the build status XML.
        /// </summary>
        /// <returns>The build status XML.</returns>
        public string GetBuildStatusXml()
        {
            string buildStatus = this.httpAdapter.Get(this.buildStatusUrl);

            return buildStatus;
        }

        /// <summary>
        /// Triggers the build.
        /// </summary>
        /// <param name="tfsBuildNumber">The TFS build number.</param>
        /// <returns>The response.</returns>
        public string TriggerBuild(string tfsBuildNumber)
        {
            string response = this.httpAdapter.Post(this.parameterizedQueueBuildUrl, string.Concat("TfsBuildNumber=", tfsBuildNumber));

            return response;
        }

        /// <summary>
        /// Gets the specific build status XML.
        /// </summary>
        /// <param name="nextBuildNumber">The next build number.</param>
        /// <returns>
        /// The build status XML.
        /// </returns>
        public string GetSpecificBuildStatusXml(string nextBuildNumber)
        {
            this.InitializeSpecificBuildUrl(nextBuildNumber);
            string buildStatus = this.httpAdapter.Get(this.specificBuildNumberStatusUrl);

            return buildStatus;
        }

        /// <summary>
        /// Initializes the specific build URL.
        /// </summary>
        /// <param name="nextBuildNumber">The next build number.</param>
        public void InitializeSpecificBuildUrl(string nextBuildNumber)
        {
            this.specificBuildNumberStatusUrl = this.GenerateSpecificBuildNumberStatusUrl(nextBuildNumber, this.jenkinsServerUrl, this.projectName);
        }

        /// <summary>
        /// Gets the queued build number.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <param name="queuedBuildName">Name of the queued build.</param>
        /// <returns>
        /// The Next Build Number.
        /// </returns>
        /// <exception cref="System.Exception">If the build with the provided name is not queued.</exception>
        public int GetQueuedBuildNumber(string xmlContent, string queuedBuildName)
        {
            IEnumerable<XElement> buildElements = this.GetAllElementsWithNodeName(xmlContent, "build");
            string nextBuildNumberStr = string.Empty;
            int nextBuildNumber = -1;

            foreach (XElement currentElement in buildElements)
            {
                nextBuildNumberStr = currentElement.Element("number").Value;
                string currentBuildSpecificUrl = this.GenerateSpecificBuildNumberStatusUrl(nextBuildNumberStr, this.jenkinsServerUrl, this.projectName);
                string newBuildStatus = this.httpAdapter.Get(currentBuildSpecificUrl);
                string currentBuildName = this.GetBuildTfsBuildNumber(newBuildStatus);
                if (queuedBuildName.Equals(currentBuildName))
                {
                    nextBuildNumber = int.Parse(nextBuildNumberStr);
                    Debug.WriteLine("The real build number is {0}", nextBuildNumber);
                    break;
                }
            }
            if (nextBuildNumber == -1)
            {
                throw new Exception(string.Format("Build with name {0} was not find in the queued builds.", queuedBuildName));
            }

            return nextBuildNumber;
        }

        /// <summary>
        /// Gets the build TFS build number.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>The TFS Build Number.</returns>
        /// <exception cref="System.ArgumentException">The TfsBuildNumber was not set!</exception>
        public string GetBuildTfsBuildNumber(string xmlContent)
        {
            IEnumerable<XElement> foundElements = from el in this.GetAllElementsWithNodeName(xmlContent, "parameter").Elements()
                                                  where el.Value == "TfsBuildNumber"
                                                  select el;

            if (foundElements.Count() == 0)
            {
                throw new ArgumentException("The TfsBuildNumber was not set!");
            }
            string tfsBuildNumber = foundElements.First().NodesAfterSelf().OfType<XElement>().First().Value;

            return tfsBuildNumber;
        }

        /// <summary>
        /// Determines whether [is project building] [the specified XML content].
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>Is the project still building.</returns>
        public bool IsProjectBuilding(string xmlContent)
        {
            bool isBuilding = false;
            string isBuildingStr = this.GetXmlNodeValue(xmlContent, "building");
            isBuilding = bool.Parse(isBuildingStr);

            return isBuilding;
        }

        /// <summary>
        /// Gets the build result.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>The build result.</returns>
        public string GetBuildResult(string xmlContent)
        {
            string buildResult = this.GetXmlNodeValue(xmlContent, "result");
            return buildResult;
        }

        /// <summary>
        /// Gets the next build number.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>The Next Build Number.</returns>
        public string GetNextBuildNumber(string xmlContent)
        {
            string nextBuildNumber = this.GetXmlNodeValue(xmlContent, "nextBuildNumber");
            return nextBuildNumber;
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>The user name.</returns>
        public string GetUserName(string xmlContent)
        {
            string userName = this.GetXmlNodeValue(xmlContent, "userName");
            return userName;
        }

        /// <summary>
        /// Generates the build status URL.
        /// </summary>
        /// <param name="jenkinsServerUrl">The jenkins server URL.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>
        /// The generated build status URL.
        /// </returns>
        /// <exception cref="System.ArgumentException">The Build status Url was not created correctly.</exception>
        internal string GenerateBuildStatusUrl(string jenkinsServerUrl, string projectName)
        {
            string resultUrl = string.Empty;
            Uri result = default(Uri);
            if (Uri.TryCreate(string.Concat(jenkinsServerUrl, "/job/", projectName, "/api/xml"), UriKind.Absolute, out result))
            {
                resultUrl = result.AbsoluteUri;
            }
            else
            {
                throw new ArgumentException("The Build status Url was not created correctly.");
            }

            return resultUrl;
        }

        /// <summary>
        /// Generates the parameterized queue build URL.
        /// </summary>
        /// <param name="jenkinsServerUrl">The jenkins server URL.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>
        /// The generated Parameterized Queue Build Url.
        /// </returns>
        /// <exception cref="System.ArgumentException">The Parameterized Queue Build Url was not created correctly.</exception>
        internal string GenerateParameterizedQueueBuildUrl(string jenkinsServerUrl, string projectName)
        {
            string resultUrl = string.Empty;
            Uri result = default(Uri);
            if (Uri.TryCreate(string.Concat(jenkinsServerUrl, "/job/", projectName, "/buildWithParameters"), UriKind.Absolute, out result))
            {
                resultUrl = result.AbsoluteUri;
            }
            else
            {
                throw new ArgumentException("The Parameterized Queue Build Url was not created correctly.");
            }

            return resultUrl;
        }

        /// <summary>
        /// Generates the specific build number status URL.
        /// </summary>
        /// <param name="buildNumber">The build number.</param>
        /// <param name="jenkinsServerUrl">The jenkins server URL.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>
        /// The generated specific build number status URL.
        /// </returns>
        /// <exception cref="System.ArgumentException">The Specific Build Number Url was not created correctly.</exception>
        internal string GenerateSpecificBuildNumberStatusUrl(string buildNumber, string jenkinsServerUrl, string projectName)
        {
            string generatedUrl = string.Empty;
            Uri result = default(Uri);
            if (Uri.TryCreate(string.Concat(jenkinsServerUrl, "/job/", projectName, "/", buildNumber, "/api/xml"), UriKind.Absolute, out result))
            {
                generatedUrl = result.AbsoluteUri;
            }
            else
            {
                throw new ArgumentException("The Specific Build Number Url was not created correctly.");
            }

            return generatedUrl;
        }

        /// <summary>
        /// Gets the XML node value.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <param name="xmlNodeName">Name of the XML node.</param>
        /// <returns>
        /// The XML Node value.
        /// </returns>
        /// <exception cref="System.Exception">Throws exception when no elements are found for the specified node name.</exception>
        internal string GetXmlNodeValue(string xmlContent, string xmlNodeName)
        {
            IEnumerable<XElement> foundElemenets = this.GetAllElementsWithNodeName(xmlContent, xmlNodeName);

            if (foundElemenets.Count() == 0)
            {
                throw new Exception(string.Format("No elements were found for node {0}", xmlNodeName));
            }
            string elementValue = foundElemenets.First().Value;

            return elementValue;
        }

        /// <summary>
        /// Gets the name of all elements with node.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <param name="xmlNodeName">Name of the XML node.</param>
        /// <returns>the found elements.</returns>
        internal IEnumerable<XElement> GetAllElementsWithNodeName(string xmlContent, string xmlNodeName)
        {
            XDocument document = XDocument.Parse(xmlContent);
            XElement root = document.Root;
            IEnumerable<XElement> foundElemenets = from element in root.Descendants(xmlNodeName)
                                                   select element;

            return foundElemenets;
        }
    }
}