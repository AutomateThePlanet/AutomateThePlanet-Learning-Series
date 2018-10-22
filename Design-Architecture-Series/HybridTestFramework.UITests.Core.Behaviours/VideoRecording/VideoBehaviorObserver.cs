// <copyright file="VideoBehaviorObserver.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;
using HybridTestFramework.UITests.Core.Behaviours.VideoRecording.Attributes;
using HybridTestFramework.UITests.Core.Behaviours.VideoRecording.Enums;
using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Enums;
using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Interfaces;

namespace HybridTestFramework.UITests.Core.Behaviours.VideoRecording
{
    public class VideoBehaviorObserver : BaseTestBehaviorObserver
    {
        private readonly IVideoRecorder _videoRecorder;
        private VideoRecordingMode _recordingMode;
        
        public VideoBehaviorObserver(IVideoRecorder videoRecorder)
        {
            this._videoRecorder = videoRecorder;
        }

        protected override void PostTestInit(object sender, TestExecutionEventArgs e)
        {
            _recordingMode = ConfigureTestVideoRecordingMode(e.MemberInfo);

            if (_recordingMode != VideoRecordingMode.DoNotRecord)
            {
                _videoRecorder.StartCapture();
            }
        }

        protected override void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
            try
            {
                var videosFolderPath = ConfigurationManager.AppSettings["videosFolderPath"];
                var testName = e.TestName;
                var hasTestPassed = e.TestOutcome.Equals(TestOutcome.Passed);
                SaveVideoDependingOnTestoutcome(videosFolderPath, testName, hasTestPassed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            finally
            {
                _videoRecorder.Dispose();
            }
        }

        private void SaveVideoDependingOnTestoutcome(string videoFolderPath, string testName, bool haveTestPassed)
        {
            if (_recordingMode != VideoRecordingMode.DoNotRecord &&
                _videoRecorder.Status == VideoRecordingStatus.Running)
            {
                var shouldRecordAlways = _recordingMode == VideoRecordingMode.Always;
                var shouldRecordAllPassedTests = haveTestPassed && _recordingMode.Equals(VideoRecordingMode.OnlyPass);
                var shouldRecordAllFailedTests = !haveTestPassed && _recordingMode.Equals(VideoRecordingMode.OnlyFail);
                if (shouldRecordAlways || shouldRecordAllPassedTests || shouldRecordAllFailedTests)
                {
                    _videoRecorder.SaveVideo(videoFolderPath, testName);
                }
            }
        }

        private VideoRecordingMode ConfigureTestVideoRecordingMode(MemberInfo memberInfo)
        {
            var methodRecordingMode = GetVideoRecordingModeByMethodInfo(memberInfo);
            var classRecordingMode = GetVideoRecordingModeType(memberInfo.DeclaringType);
            var videoRecordingMode = VideoRecordingMode.DoNotRecord;
            var shouldTakeVideos = bool.Parse(ConfigurationManager.AppSettings["shouldTakeVideosOnExecution"]);
            
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
                throw new ArgumentNullException("The test method's info cannot be null.");
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
                throw new ArgumentNullException("The test method's type cannot be null.");
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