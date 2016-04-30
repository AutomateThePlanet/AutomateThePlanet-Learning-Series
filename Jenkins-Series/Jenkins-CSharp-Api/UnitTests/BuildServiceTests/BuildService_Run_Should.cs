// <copyright file="BuildService_Run_Should.cs" company="Automate The Planet Ltd.">
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
using System.Net;
using JenkinsCSharpApi.Interfaces;
using JenkinsCSharpApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace JenkinsCSharpApi.UnitTests.BuildServiceTests
{
    [TestClass]
    public class BuildService_Run_Should
    {
        [TestMethod]
        public void GetNextBuildNumber()
        {
            IHttpAdapter httpAdapter = Mock.Create<IHttpAdapter>();
            Mock.Arrange(() => httpAdapter.Get(Arg.IsAny<string>())).Returns(JenkinsTestData.BuildStatusXml);
            var jenkinsBuild = new BuildService(new BuildAdapter(httpAdapter, JenkinsTestData.JenkinsUrl, JenkinsTestData.ProjectName));
            string nextBuildNumber = jenkinsBuild.GetNextBuildNumber();

            Assert.AreEqual<string>(
                "35",
                nextBuildNumber,
                "The returned next build number was not correct.");
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(Exception), noExceptionMessage: "Another build with the same build number is already triggered.")]
        public void ThrowAlreadyTriggeredBuildException_WhenBuildIsAlreadyTriggered()
        {
            IBuildAdapter buildAdapter = Mock.Create<IBuildAdapter>();
            Mock.Arrange(() => buildAdapter.GetSpecificBuildStatusXml(Arg.IsAny<string>())).Throws<WebException>();

            var jenkinsBuild = new BuildService(buildAdapter);
            jenkinsBuild.TriggerBuild(string.Empty, "35");
        }

        [TestMethod]
        public void TriggerBuild()
        {
            IBuildAdapter buildAdapter = Mock.Create<IBuildAdapter>();
            Mock.Arrange(() => buildAdapter.GetSpecificBuildStatusXml(Arg.IsAny<string>())).Throws<WebException>("The remote server returned an error: (404) Not Found.");
            Mock.Arrange(() => buildAdapter.TriggerBuild(Arg.IsAny<string>())).Returns("SUCCESS");

            var jenkinsBuild = new BuildService(buildAdapter);
            string response = jenkinsBuild.TriggerBuild(string.Empty, "35");

            Assert.AreEqual<string>(
                "SUCCESS",
                response,
                "The build was not triggered successfully.");
        }

        [TestMethod]
        public void GetBuildStatus()
        {
            IBuildAdapter buildAdapter = Mock.Create<IBuildAdapter>();
            Mock.Arrange(() => buildAdapter.GetSpecificBuildStatusXml(Arg.IsAny<string>())).Returns(string.Empty);
            Mock.Arrange(() => buildAdapter.GetBuildResult(Arg.IsAny<string>())).Returns("SUCCESS");

            var jenkinsBuild = new BuildService(buildAdapter);
            string response = jenkinsBuild.GetBuildStatus("35");

            Assert.AreEqual<string>(
                "SUCCESS",
                response,
                "The build was not triggered successfully.");
        }

        [TestMethod]
        public void GetRealBuildNumber()
        {
            IBuildAdapter buildAdapter = Mock.Create<IBuildAdapter>();
            Mock.Arrange(() => buildAdapter.GetBuildStatusXml()).Returns(string.Empty);
            Mock.Arrange(() => buildAdapter.GetQueuedBuildNumber(Arg.IsAny<string>(), Arg.IsAny<string>())).Returns(32);

            var jenkinsBuild = new BuildService(buildAdapter);
            string nextBuildNumber = jenkinsBuild.GetRealBuildNumber("32");

            Assert.AreEqual<string>(
                "32",
                nextBuildNumber,
                "The next build number was incorrect.");
        }

        [TestMethod]
        public void WaitUntilBuildStarts()
        {
            IBuildAdapter buildAdapter = Mock.Create<IBuildAdapter>();
            Mock.Arrange(() => buildAdapter.GetSpecificBuildStatusXml(Arg.IsAny<string>())).Returns(string.Empty);

            var jenkinsBuild = new BuildService(buildAdapter);
            jenkinsBuild.WaitUntilBuildStarts("32");
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(Exception), noExceptionMessage: "The build didn't start in 30 seconds.")]
        public void ThrowException_When30SecondsWebExceptionsAreThrown()
        {
            IBuildAdapter buildAdapter = Mock.Create<IBuildAdapter>();
            Mock.Arrange(() => buildAdapter.GetSpecificBuildStatusXml(Arg.IsAny<string>())).Throws<WebException>("The remote server returned an error: (404) Not Found.");
            DateTime startTime = DateTime.Now;
            var jenkinsBuild = new BuildService(buildAdapter);
            jenkinsBuild.WaitUntilBuildStarts("32");
            DateTime endTime = DateTime.Now;
            double seconds = (endTime - startTime).TotalSeconds;
            Assert.IsTrue(seconds > 30 && seconds < 31, "The wait time in case of web exceptions was incorrect.");
        }

        [TestMethod]
        public void WaitUntilBuildFinish()
        {
            IBuildAdapter buildAdapter = Mock.Create<IBuildAdapter>();
            Mock.Arrange(() => buildAdapter.GetSpecificBuildStatusXml(Arg.IsAny<string>())).Returns(string.Empty);
            Mock.Arrange(() => buildAdapter.IsProjectBuilding(Arg.IsAny<string>())).Returns(false);

            var jenkinsBuild = new BuildService(buildAdapter);
            jenkinsBuild.WaitUntilBuildFinish("32");
        }
    }
}