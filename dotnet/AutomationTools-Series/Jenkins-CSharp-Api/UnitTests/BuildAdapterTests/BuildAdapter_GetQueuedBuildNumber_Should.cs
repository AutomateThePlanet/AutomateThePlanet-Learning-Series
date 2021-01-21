// <copyright file="BuildAdapter_GetQueuedBuildNumber_Should.cs" company="Automate The Planet Ltd.">
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
using JenkinsCSharpApi.Interfaces;
using JenkinsCSharpApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace JenkinsCSharpApi.UnitTests.BuildAdapterTests
{
    [TestClass]
    public class BuildAdapter_GetQueuedBuildNumber_Should
    {
        [TestMethod]
        public void GetQueuedBuildNumber_WhenSingleBuildNodeUsed()
        {
            IHttpAdapter httpAdapter = Mock.Create<IHttpAdapter>();
            Mock.Arrange(() => httpAdapter.Get(Arg.IsAny<string>())).Returns(JenkinsTestData.SpecificBuildXml);

            var buildAdapter = new BuildAdapter(httpAdapter, JenkinsTestData.JenkinsUrl, JenkinsTestData.ProjectName);
            int nextBuildNumber = buildAdapter.GetQueuedBuildNumber(
                JenkinsTestData.BuildStatusSingleBuildNodeXml,
                "7757ae58-5ba5-4690-b49c-997779f62338");

            Assert.AreEqual<int>(
                1,
                nextBuildNumber,
                "The returned next build number was not correct.");
        }

        [TestMethod]
        public void GetQueuedBuildNumber_WhenTwoBuildNodesUsed()
        {
            IHttpAdapter httpHelper = Mock.Create<IHttpAdapter>();
            Mock.Arrange(() => httpHelper.Get(Arg.Is<string>(@"http://localhost:8080/job/Jenkins-CSharp-Api.Parameterized/34/api/xml"))).Returns(JenkinsTestData.SpecificBuildXml);
            string newSpecificBuildXml = JenkinsTestData.SpecificBuildXml.Replace(
                "<value>7757ae58-5ba5-4690-b49c-997779f62338</value>",
                "<value>5ba5-4690-b49c-997779f62338</value>");
            Mock.Arrange(() => httpHelper.Get(Arg.Is<string>(@"http://localhost:8080/job/Jenkins-CSharp-Api.Parameterized/1/api/xml"))).Returns(newSpecificBuildXml);
            Mock.Arrange(() => httpHelper.Get(Arg.Is<string>(@"http://localhost:8080/job/Jenkins-CSharp-Api.Parameterized/2/api/xml"))).Returns(newSpecificBuildXml);
            var buildAdapter = new BuildAdapter(httpHelper, JenkinsTestData.JenkinsUrl, JenkinsTestData.ProjectName);
            int nextBuildNumber = buildAdapter.GetQueuedBuildNumber(
                JenkinsTestData.BuildStatusXml,
                "7757ae58-5ba5-4690-b49c-997779f62338");

            Assert.AreEqual<int>(
                34,
                nextBuildNumber,
                "The returned next build number was not correct.");
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(Exception), noExceptionMessage: "The Argument exception was not throwed in case of not queued build.")]
        public void ThrowException_WhenNoBuildIsQueued()
        {
            IHttpAdapter httpAdapter = Mock.Create<IHttpAdapter>();
            string newSpecificBuildXml =
                JenkinsTestData.SpecificBuildXml.Replace(
                    "<value>7757ae58-5ba5-4690-b49c-997779f62338</value>",
                    "<value>5ba5-4690-b49c-997779f62338</value>");
            Mock.Arrange(() => httpAdapter.Get(Arg.IsAny<string>())).Returns(newSpecificBuildXml);

            var buildAdapter = new BuildAdapter(httpAdapter, JenkinsTestData.JenkinsUrl, JenkinsTestData.ProjectName);
            buildAdapter.GetQueuedBuildNumber(
                JenkinsTestData.BuildStatusSingleBuildNodeXml,
                "7757ae58-5ba5-4690-b49c-997779f62338");
        }
    }
}