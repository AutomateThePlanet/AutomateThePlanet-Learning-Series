// <copyright file="IBuildAdapter.cs" company="Automate The Planet Ltd.">
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

namespace JenkinsCSharpApi.Interfaces
{
    /// <summary>
    /// Contains Methods that create Jenkins Build and Wait for its execution.
    /// </summary>
    public interface IBuildAdapter
    {
        /// <summary>
        /// Gets the queued build number.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <param name="queuedBuildName">Name of the queued build.</param>
        /// <returns>
        /// The Next Build Number.
        /// </returns>
        /// <exception cref="System.Exception">If the build with the provided name is not queued.</exception>
        int GetQueuedBuildNumber(string xmlContent, string queuedBuildName);

        /// <summary>
        /// Gets the build TFS build number.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>The TFS Build Number.</returns>
        /// <exception cref="System.ArgumentException">The TfsBuildNumber was not set!</exception>
        string GetBuildTfsBuildNumber(string xmlContent);

        /// <summary>
        /// Determines whether [is project building] [the specified XML content].
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>Is the project still building.</returns>
        bool IsProjectBuilding(string xmlContent);

        /// <summary>
        /// Gets the build result.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>The build result.</returns>
        string GetBuildResult(string xmlContent);

        /// <summary>
        /// Gets the next build number.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>The Next Build Number.</returns>
        string GetNextBuildNumber(string xmlContent);

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns>The user name.</returns>
        string GetUserName(string xmlContent);

        /// <summary>
        /// Gets the build status XML.
        /// </summary>
        /// <returns>The build status XML.</returns>
        string GetBuildStatusXml();

        /// <summary>
        /// Gets the specific build status XML.
        /// </summary>
        /// <param name="nextBuildNumber">The next build number.</param>
        /// <returns>
        /// The build status XML.
        /// </returns>
        string GetSpecificBuildStatusXml(string nextBuildNumber);

        /// <summary>
        /// Triggers the build.
        /// </summary>
        /// <param name="tfsBuildNumber">The TFS build number.</param>
        /// <returns>The response.</returns>
        string TriggerBuild(string tfsBuildNumber);

        /// <summary>
        /// Initializes the specific build URL.
        /// </summary>
        /// <param name="nextBuildNumber">The next build number.</param>
        void InitializeSpecificBuildUrl(string nextBuildNumber);
    }
}