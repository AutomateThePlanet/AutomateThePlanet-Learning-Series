// <copyright file="VideoWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Configuration;
using System.IO;
using System.Reflection;
using HybridTestFramework.UITests.Core.Behaviours.VideoRecording.Attributes;
using HybridTestFramework.UITests.Core.Behaviours.VideoRecording.Enums;
using HybridTestFramework.UITests.Core.Utilities.VideoRecording;
using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Interfaces;

namespace HybridTestFramework.UITests.Core.Behaviours.VideoRecording
{
    public class VideoWorkflowPlugin : BaseTestBehaviorObserver
    {
        private readonly IVideoRecorder _videoRecorder;
        private readonly IVideoRecorderOutputProvider _videoRecorderOutputProvider;
        private VideoRecordingMode _recordingMode;
        private string _videoRecordingPath;

        public VideoWorkflowPlugin(IVideoRecorder videoRecorder, IVideoRecorderOutputProvider videoRecorderOutputProvider)
        {
            _videoRecorder = videoRecorder;
            _videoRecorderOutputProvider = videoRecorderOutputProvider;
        }

        protected override void PostTestInit(object sender, TestExecutionEventArgs e)
        {
            _recordingMode = ConfigureTestVideoRecordingMode(e.MemberInfo);

            if (_recordingMode != VideoRecordingMode.DoNotRecord)
            {
                var fullTestName = $"{e.MemberInfo.DeclaringType.Name}.{e.TestName}";
                var videoRecordingDir = _videoRecorderOutputProvider.GetOutputFolder();
                var videoRecordingFileName = _videoRecorderOutputProvider.GetUniqueFileName(fullTestName);

                _videoRecordingPath = _videoRecorder.Record(videoRecordingDir, videoRecordingFileName);
            }
        }

        protected override void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
            if (_recordingMode != VideoRecordingMode.DoNotRecord)
            {
                try
                {
                    bool hasTestPassed = e.TestOutcome.Equals(TestOutcome.Passed);
                    DeleteVideoDependingOnTestOutcome(hasTestPassed);
                }
                finally
                {
                    _videoRecorder.Dispose();
                }
            }
        }

        private void DeleteVideoDependingOnTestOutcome(bool haveTestPassed)
        {
            if (_recordingMode != VideoRecordingMode.DoNotRecord)
            {
                bool shouldRecordAlways = _recordingMode == VideoRecordingMode.Always;
                bool shouldRecordAllPassedTests = haveTestPassed && _recordingMode.Equals(VideoRecordingMode.OnlyPass);
                bool shouldRecordAllFailedTests = !haveTestPassed && _recordingMode.Equals(VideoRecordingMode.OnlyFail);
                if (!(shouldRecordAlways || shouldRecordAllPassedTests || shouldRecordAllFailedTests))
                {
                    // Release the video file then delete it.
                    _videoRecorder.Stop();
                    if (File.Exists(_videoRecordingPath))
                    {
                        File.Delete(_videoRecordingPath);
                    }
                }
            }
        }

        private VideoRecordingMode ConfigureTestVideoRecordingMode(MemberInfo memberInfo)
        {
            VideoRecordingMode methodRecordingMode = GetVideoRecordingModeByMethodInfo(memberInfo);
            VideoRecordingMode classRecordingMode = GetVideoRecordingModeType(memberInfo.DeclaringType);
            VideoRecordingMode videoRecordingMode = VideoRecordingMode.DoNotRecord;

            // later refactor to support .NET core
            ////var shouldTakeVideos = bool.Parse(ConfigurationManager.AppSettings["shouldTakeVideosOnExecution"]);
            var shouldTakeVideos = true;

            if (methodRecordingMode != VideoRecordingMode.Ignore && shouldTakeVideos)
            {
                videoRecordingMode = methodRecordingMode;
            }
            else if (classRecordingMode != VideoRecordingMode.Ignore && shouldTakeVideos)
            {
                videoRecordingMode = classRecordingMode;
            }

            return videoRecordingMode;
        }

        private VideoRecordingMode GetVideoRecordingModeByMethodInfo(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException();
            }

            var recordingModeMethodAttribute = memberInfo.GetCustomAttribute<VideoRecordingAttribute>(true);
            if (recordingModeMethodAttribute != null)
            {
                return recordingModeMethodAttribute.VideoRecording;
            }

            return VideoRecordingMode.Ignore;
        }

        private VideoRecordingMode GetVideoRecordingModeType(Type currentType)
        {
            if (currentType == null)
            {
                throw new ArgumentNullException();
            }

            var recordingModeClassAttribute = currentType.GetCustomAttribute<VideoRecordingAttribute>(true);
            if (recordingModeClassAttribute != null)
            {
                return recordingModeClassAttribute.VideoRecording;
            }

            return VideoRecordingMode.Ignore;
        }
    }
}