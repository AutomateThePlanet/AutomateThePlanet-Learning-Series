// <copyright file="MsExpressionEncoderVideoRecorder.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Interfaces;
using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Model;
using Microsoft.Expression.Encoder.ScreenCapture;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HybridTestFramework.UITests.Core.Utilities.VideoRecording
{
    /*
    C:\Program Files (x86)\Microsoft Expression\Encoder 4\SDK\Microsoft.Expression.Encoder.dll
    C:\Program Files (x86)\Microsoft Expression\Encoder 4\SDK\Microsoft.Expression.Encoder.Api2.dll
    C:\Program Files (x86)\Microsoft Expression\Encoder 4\SDK\Microsoft.Expression.Encoder.Types.dll
    C:\Program Files (x86)\Microsoft Expression\Encoder 4\SDK\Microsoft.Expression.Encoder.Utilities.dll
    System.Drawing
    System.Windows.Forms
    */
    public class MsExpressionEncoderVideoRecorder : IVideoRecorder
    {
        private const string VideoExtension = ".wmv";
        private const string NewFileDateTimeFormat = "yyyyMMddHHmmssfff";
        private const int FrameRate = 5;
        private const int Quality = 20;
        private readonly int height =
                                     Screen.PrimaryScreen.Bounds.Height - (Screen.PrimaryScreen.Bounds.Height % 16);
        private readonly int width =
                                    Screen.PrimaryScreen.Bounds.Width - (Screen.PrimaryScreen.Bounds.Width % 16);
        private ScreenCaptureJob screenCaptureJob;
        private bool isDisposed;

        public VideoRecordingStatus Status
        {
            get
            {
                return this.screenCaptureJob != null ? (VideoRecordingStatus)this.screenCaptureJob.Status : VideoRecordingStatus.NotStarted;
            }
        }

        public VideoRecordingResult StartCapture()
        {
            VideoRecordingResult result = new VideoRecordingResult();
            try
            {
                this.Initialize();
                this.screenCaptureJob.Start();
            }
            catch (Exception ex)
            {
                string argumentExceptionMessage =
                    string.Format("Video capturing failed with the following exception:{0}. Resolution: width - {1}, height - {2}. ",
                        ex.Message,
                        this.height,
                        this.width);
                result.SavedException = new ArgumentException(argumentExceptionMessage);
                result.IsSuccessfullySaved = false; 
            }

            return result;
        }

        public void StopCapture()
        {
            this.screenCaptureJob.Stop();
        }

        public VideoRecordingResult SaveVideo(string saveLocation, string testName)
        {
            VideoRecordingResult result = new VideoRecordingResult();

            try
            {
                this.StopCapture();
            }
            catch (Exception e)
            {
                result.SavedException = e;
                result.IsSuccessfullySaved = false;
            }
         
            if (Directory.Exists(saveLocation))
            {
                string moveToPath = this.GenerateFinalFilePath(saveLocation, testName);
                File.Move(this.screenCaptureJob.OutputScreenCaptureFileName, moveToPath);
            }
            else
            {
                result.SavedException =
                    new ArgumentException("The specified save location does not exists."); 
                result.IsSuccessfullySaved = false; 
            }

            return result;
        }

        public void Dispose()
        {
            if (!this.isDisposed)
            {
                if (this.Status == VideoRecordingStatus.Running)
                {
                    this.StopCapture();
                }
                this.DeleteTempVideo();
                this.isDisposed = true;
            }
        }

        private void Initialize()
        {
            this.screenCaptureJob = new ScreenCaptureJob();
            this.screenCaptureJob.CaptureRectangle = new Rectangle(0, 0, this.width, this.height);
            this.screenCaptureJob.ScreenCaptureVideoProfile.Force16Pixels = true;
            this.screenCaptureJob.ShowFlashingBoundary = true;
            this.screenCaptureJob.ScreenCaptureVideoProfile.FrameRate = FrameRate;
            this.screenCaptureJob.CaptureMouseCursor = true;
            this.screenCaptureJob.ScreenCaptureVideoProfile.Quality = Quality;
            this.screenCaptureJob.ScreenCaptureVideoProfile.Size = new Size(this.width, this.height);
            this.screenCaptureJob.ScreenCaptureVideoProfile.AutoFit = true;
            this.screenCaptureJob.OutputScreenCaptureFileName = this.GetTempFilePathWithExtension();
            this.isDisposed = false;
        }

        private string GenerateFinalFilePath(string saveLocation, string testName)
        {
            string newFileName =
                string.Concat(
                    testName,
                    "-",
                    DateTime.Now.ToString(NewFileDateTimeFormat),
                    VideoExtension);
            string moveToPath = Path.Combine(saveLocation, newFileName);
            return moveToPath;
        }

        private string GetTempFilePathWithExtension()
        {
            var path = Path.GetTempPath();
            var fileName = string.Concat(Guid.NewGuid().ToString(), VideoExtension);
            return Path.Combine(path, fileName);
        }

        private void DeleteTempVideo()
        {
            if (this.screenCaptureJob != null && File.Exists(this.screenCaptureJob.OutputScreenCaptureFileName))
            {
                File.Delete(this.screenCaptureJob.OutputScreenCaptureFileName);
            }
        }
    }
}