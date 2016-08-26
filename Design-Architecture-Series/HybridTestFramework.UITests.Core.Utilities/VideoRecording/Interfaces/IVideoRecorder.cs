// <copyright file="IVideoRecorder.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Enums;
using System;
using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Model;

namespace HybridTestFramework.UITests.Core.Utilities.VideoRecording.Interfaces
{
    public interface IVideoRecorder : IDisposable
    {
        VideoRecordingStatus Status { get; }

        HybridTestFramework.UITests.Core.Utilities.VideoRecording.Model.VideoRecordingResult StartCapture();

        VideoRecordingResult SaveVideo(string saveLocation, string testName);
    }
}