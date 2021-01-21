// <copyright file="BuildService.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Net;
using System.Threading;
using JenkinsCSharpApi.Interfaces;

namespace JenkinsCSharpApi.Services
{
    /// <summary>
    /// Contains Methods that create Jenkins Build and Wait for its execution.
    /// </summary>
    public class BuildService
    {
        private readonly IBuildAdapter buildAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildService" /> class.
        /// </summary>
        /// <param name="buildAdapter">The build service.</param>
        /// <exception cref="System.ArgumentNullException">The Jenkins Server URL cannot be null or empty.
        /// or
        /// The Project Name cannot be null or empty.
        /// or
        /// The Build Name cannot be null or empty.</exception>
        public BuildService(IBuildAdapter buildAdapter)
        {
            if (buildAdapter == null)
            {
                throw new ArgumentNullException("The ArgumentNullException was not throwed in case of BuildService is equal to null.");
            }
            this.buildAdapter = buildAdapter;      
        }

        /// <summary>
        /// Runs the specified Jenkins build and waits for its execution.
        /// </summary>
        /// <param name="tfsBuildNumber">The TFS build number.</param>
        /// <returns>The build Result status.</returns>
        public string Run(string tfsBuildNumber)
        {
            // If the tfsBuildNumber passed by the WF Activity is not passed, we assign Guid in order the name to be unique.
            if (string.IsNullOrEmpty(tfsBuildNumber))
            {
                tfsBuildNumber = Guid.NewGuid().ToString();
            }

            // We get the nextBuildNumber from the Jenkins Build Status XML, but we don't know if this number will be really our build number when we trigger 
            // the build. A race condition may occur.
            string nextBuildNumber = this.GetNextBuildNumber();

            // We trigger the build.
            this.TriggerBuild(tfsBuildNumber, nextBuildNumber);

            // Wait until it starts -> Not throwing web exception 404 Not Found for the specific jenkins build URL.
            this.WaitUntilBuildStarts(nextBuildNumber);

            // When we are sure that the build is already triggered, we foreach all build numbers from the Project Build Status XML, 
            // if the custom parameter TfsBuildNumber is the parameter that we have passed the build number is returned.
            string realBuildNumber = this.GetRealBuildNumber(tfsBuildNumber);

            // Initialize the specific build URL with the potentially new build number.
            this.buildAdapter.InitializeSpecificBuildUrl(realBuildNumber);

            // Wait unitil the build finishes. We check the Specific Build XML for the property IsBuilding.
            this.WaitUntilBuildFinish(realBuildNumber);

            // When the build finishes, we get the build status from the Specific Build XML.
            string buildResult = this.GetBuildStatus(realBuildNumber);

            return buildResult;
        }

        /// <summary>
        /// Gets the build status from the Specific Build URL. Status XML node.
        /// </summary>
        /// <param name="realBuildNumber">The real build number.</param>
        /// <returns>The build status.</returns>
        internal string GetBuildStatus(string realBuildNumber)
        {
            string buildStatus = this.buildAdapter.GetSpecificBuildStatusXml(realBuildNumber);
            string buildResult = this.buildAdapter.GetBuildResult(buildStatus);
            Debug.WriteLine("Result from the build: {0}", buildResult);
           
            return buildResult;
        }

        /// <summary>
        /// Waits unitil the build finishes. We check the Specific Build XML for the property IsBuilding.
        /// </summary>
        /// <param name="realBuildNumber">The real build number.</param>
        internal void WaitUntilBuildFinish(string realBuildNumber)
        {
            bool shouldContinue = false;
            string buildStatus = string.Empty;
            do
            {
                buildStatus = this.buildAdapter.GetSpecificBuildStatusXml(realBuildNumber);
                bool isProjectBuilding = this.buildAdapter.IsProjectBuilding(buildStatus);
                if (!isProjectBuilding)
                {
                    shouldContinue = true;
                }
                Debug.WriteLine("Waits 5 seconds before the new check if the build is completed...");
                Thread.Sleep(5000);
            }
            while (!shouldContinue);
        }

        /// <summary>
        /// When we are sure that the build is already triggered, we foreach all build numbers from the Project Build Status XML, 
        /// if the custom parameter TfsBuildNumber is the parameter that we have passed the build number is returned.
        /// </summary>
        /// <param name="tfsBuildNumber">The TFS build number.</param>
        /// <returns>The real build number.</returns>
        internal string GetRealBuildNumber(string tfsBuildNumber)
        {
            string buildStatus = this.buildAdapter.GetBuildStatusXml();
            string nextBuildNumber = this.buildAdapter.GetQueuedBuildNumber(buildStatus, tfsBuildNumber).ToString();
            
            return nextBuildNumber;
        }

        /// <summary>
        /// Waits until the build starts -> Not throwing web exception 404 Not Found for the specific jenkins build URL.
        /// </summary>
        /// <param name="nextBuildNumber">The next build number.</param>
        internal void WaitUntilBuildStarts(string nextBuildNumber)
        {
            int retryCount = 30;
            bool isBuildtriggered = false;
            string buildStatus = string.Empty;
            do
            {
                if (!isBuildtriggered && retryCount == 0)
                {
                    throw new Exception("The build didn't start in 30 seconds.");
                }
                try
                {
                    buildStatus = this.buildAdapter.GetSpecificBuildStatusXml(nextBuildNumber);
                    Debug.WriteLine(buildStatus);
                    isBuildtriggered = true;
                }
                catch (WebException ex)
                {
                    if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
                    {
                        retryCount--;
                        Thread.Sleep(1000);
                        Debug.WriteLine("wait 1 second until the build is triggered...");
                    }
                }
            }
            while (!isBuildtriggered || retryCount == 0);
        }

        /// <summary>
        /// Triggers the build.
        /// </summary>
        /// <param name="tfsBuildNumber">The TFS build number.</param>
        /// <param name="nextBuildNumber">The next build number.</param>
        /// <returns>The response.</returns>
        /// <exception cref="System.Exception">Another build with the same build number is already triggered.</exception>
        internal string TriggerBuild(string tfsBuildNumber, string nextBuildNumber)
        {
            string buildStatus = string.Empty;
            bool isAlreadyBuildTriggered = false;
            try
            {
                buildStatus = this.buildAdapter.GetSpecificBuildStatusXml(nextBuildNumber);
                Debug.WriteLine(buildStatus);
            }
            catch (WebException ex)
            {
                if (!ex.Message.Equals("The remote server returned an error: (404) Not Found."))
                {
                    isAlreadyBuildTriggered = true;
                }
            }
            if (isAlreadyBuildTriggered)
            {
                throw new Exception("Another build with the same build number is already triggered.");
            }
            string response = this.buildAdapter.TriggerBuild(tfsBuildNumber);

            return response;
        }

        /// <summary>
        ///  We get the nextBuildNumber from the Jenkins Build Status XML, but we don't know if this number will be really our build number when we trigger 
        ///  the build. A race condition may occur.
        /// </summary>
        /// <returns>The next Build number.</returns>
        internal string GetNextBuildNumber()
        {
            string buildStatus = this.buildAdapter.GetBuildStatusXml();
            string nextBuildNumber = this.buildAdapter.GetNextBuildNumber(buildStatus);

            return nextBuildNumber;
        }
    }
}