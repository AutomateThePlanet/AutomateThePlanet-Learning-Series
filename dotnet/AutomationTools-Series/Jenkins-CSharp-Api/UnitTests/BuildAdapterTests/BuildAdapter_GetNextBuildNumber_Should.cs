// <copyright file="BuildAdapter_GetNextBuildNumber_Should.cs" company="Automate The Planet Ltd.">
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
using JenkinsCSharpApi.Infrastructure;
using JenkinsCSharpApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JenkinsCSharpApi.UnitTests.BuildAdapterTests
{
    [TestClass]
    public class BuildAdapter_GetNextBuildNumber_Should
    {
        [TestMethod]
        public void GetNextBuildNumber()
        {
            var buildAdapter = new BuildAdapter(new HttpAdapter(), JenkinsTestData.JenkinsUrl, JenkinsTestData.ProjectName);
            string nextBuildNumber = buildAdapter.GetNextBuildNumber(JenkinsTestData.BuildStatusXml);

            Assert.AreEqual<string>(
                "35",
                nextBuildNumber,
                "The returned next build number was not correct.");
        }
    }
}