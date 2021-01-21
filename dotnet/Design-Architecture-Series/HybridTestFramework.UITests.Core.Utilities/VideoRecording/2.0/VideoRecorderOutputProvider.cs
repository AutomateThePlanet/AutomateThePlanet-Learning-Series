// <copyright file="VideoRecorderOutputProvider.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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
using System.IO;

namespace HybridTestFramework.UITests.Core.Utilities.VideoRecording
{
    public class VideoRecorderOutputProvider : IVideoRecorderOutputProvider
    {
        public string GetOutputFolder()
        {
            // later refactor to support .NET core
            ////var outputDir = ConfigurationManager.AppSettings["videosFolderPath"];
            var outputDir = @"C:\TestVideos\";
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            return outputDir;
        }

        public string GetUniqueFileName(string testName) => string.Concat(testName, Guid.NewGuid().ToString());
    }
}
