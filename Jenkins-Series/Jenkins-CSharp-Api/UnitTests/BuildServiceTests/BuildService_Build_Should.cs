// <copyright file="BuildService_Build_Should.cs" company="Automate The Planet Ltd.">
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

namespace JenkinsCSharpApi.UnitTests.BuildServiceTests
{
    [TestClass]
    public class BuildService_Build_Should
    {
        [TestMethod]
        [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "The ArgumentNullException was not throwed in case of empty Jenkins Service URL.")]
        public void ShouldBuildThrowArgumentNullExceptionWhenJenkinsServiceUrlIsEmpty()
        {
            var jenkinsBuild = new BuildService(new BuildAdapter(new HttpAdapter(), string.Empty, "sampleProjectName"));
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "The ArgumentNullException was not throwed in case of empty project name.")]
        public void ShouldBuildThrowArgumentNullExceptionWhenProjectNameIsEmpty()
        {
            var jenkinsBuild = new BuildService(new BuildAdapter(new HttpAdapter(), JenkinsTestData.JenkinsUrl, string.Empty));
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "The ArgumentNullException was not throwed in case of httpAdapter is equal to null.")]
        public void ShouldBuildThrowArgumentNullExceptionWhenHttpHelperNotInitialized()
        {
            var jenkinsBuild = new BuildService(new BuildAdapter(null, JenkinsTestData.JenkinsUrl, JenkinsTestData.ProjectName));
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "The ArgumentNullException was not throwed in case of BuildAdapter is equal to null.")]
        public void ShouldBuildThrowArgumentNullExceptionWhenBuildServiceNotInitialized()
        {
            var jenkinsBuild = new BuildService(null);
        }
    }
}