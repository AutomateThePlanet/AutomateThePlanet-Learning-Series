////// <copyright file="MsExpressionEncoderVideoRecorder.cs" company="Automate The Planet Ltd.">
////// Copyright 2018 Automate The Planet Ltd.
////// Licensed under the Apache License, Version 2.0 (the "License");
////// You may not use this file except in compliance with the License.
////// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
////// Unless required by applicable law or agreed to in writing, software
////// distributed under the License is distributed on an "AS IS" BASIS,
////// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
////// See the License for the specific language governing permissions and
////// limitations under the License.
////// </copyright>
////// <author>Anton Angelov</author>
////// <site>http://automatetheplanet.com/</site>

////using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Enums;
////using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Interfaces;
////using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Model;
////using Microsoft.Expression.Encoder.ScreenCapture;
////using System;
////using System.Drawing;
////using System.IO;
////using System.Windows.Forms;

////namespace HybridTestFramework.UITests.Core.Utilities.VideoRecording
////{
////    /*
////    C:\Program Files (x86)\Microsoft Expression\Encoder 4\SDK\Microsoft.Expression.Encoder.dll
////    C:\Program Files (x86)\Microsoft Expression\Encoder 4\SDK\Microsoft.Expression.Encoder.Api2.dll
////    C:\Program Files (x86)\Microsoft Expression\Encoder 4\SDK\Microsoft.Expression.Encoder.Types.dll
////    C:\Program Files (x86)\Microsoft Expression\Encoder 4\SDK\Microsoft.Expression.Encoder.Utilities.dll
////    System.Drawing
////    System.Windows.Forms
////    */
////    public class MsExpressionEncoderVideoRecorder : IVideoRecorder
////    {
////        private const string VideoExtension = ".wmv";
////        private const string NewFileDateTimeFormat = "yyyyMMddHHmmssfff";
////        private const int FrameRate = 5;
////        private const int Quality = 20;
////        private readonly int _height =
////                                     Screen.PrimaryScreen.Bounds.Height - (Screen.PrimaryScreen.Bounds.Height % 16);
////        private readonly int _width =
////                                    Screen.PrimaryScreen.Bounds.Width - (Screen.PrimaryScreen.Bounds.Width % 16);
////        private ScreenCaptureJob _screenCaptureJob;
////        private bool _isDisposed;

////        public VideoRecordingStatus Status
////        {
////            get
////            {
////                return _screenCaptureJob != null ? (VideoRecordingStatus)_screenCaptureJob.Status : VideoRecordingStatus.NotStarted;
////            }
////        }

////        public VideoRecordingResult StartCapture()
////        {
////            var result = new VideoRecordingResult();
////            try
////            {
////                Initialize();
////                _screenCaptureJob.Start();
////            }
////            catch (Exception ex)
////            {
////                var argumentExceptionMessage =
////                    string.Format("Video capturing failed with the following exception:{0}. Resolution: width - {1}, height - {2}. ",
////                        ex.Message,
////                        _height,
////                        _width);
////                result.SavedException = new ArgumentException(argumentExceptionMessage);
////                result.IsSuccessfullySaved = false; 
////            }

////            return result;
////        }

////        public void StopCapture()
////        {
////            _screenCaptureJob.Stop();
////        }

////        public VideoRecordingResult SaveVideo(string saveLocation, string testName)
////        {
////            var result = new VideoRecordingResult();

////            try
////            {
////                StopCapture();
////            }
////            catch (Exception e)
////            {
////                result.SavedException = e;
////                result.IsSuccessfullySaved = false;
////            }
         
////            if (Directory.Exists(saveLocation))
////            {
////                var moveToPath = GenerateFinalFilePath(saveLocation, testName);
////                File.Move(_screenCaptureJob.OutputScreenCaptureFileName, moveToPath);
////            }
////            else
////            {
////                result.SavedException =
////                    new ArgumentException("The specified save location does not exists."); 
////                result.IsSuccessfullySaved = false; 
////            }

////            return result;
////        }

////        public void Dispose()
////        {
////            if (!_isDisposed)
////            {
////                if (Status == VideoRecordingStatus.Running)
////                {
////                    StopCapture();
////                }
////                DeleteTempVideo();
////                _isDisposed = true;
////            }
////        }

////        private void Initialize()
////        {
////            _screenCaptureJob = new ScreenCaptureJob()
////            {
////                CaptureRectangle = new Rectangle(0, 0, _width, _height)
////            };
////            _screenCaptureJob.ScreenCaptureVideoProfile.Force16Pixels = true;
////            _screenCaptureJob.ShowFlashingBoundary = true;
////            _screenCaptureJob.ScreenCaptureVideoProfile.FrameRate = FrameRate;
////            _screenCaptureJob.CaptureMouseCursor = true;
////            _screenCaptureJob.ScreenCaptureVideoProfile.Quality = Quality;
////            _screenCaptureJob.ScreenCaptureVideoProfile.Size = new Size(_width, _height);
////            _screenCaptureJob.ScreenCaptureVideoProfile.AutoFit = true;
////            _screenCaptureJob.OutputScreenCaptureFileName = GetTempFilePathWithExtension();
////            _isDisposed = false;
////        }

////        private string GenerateFinalFilePath(string saveLocation, string testName)
////        {
////            var newFileName =
////                string.Concat(
////                    testName,
////                    "-",
////                    DateTime.Now.ToString(NewFileDateTimeFormat),
////                    VideoExtension);
////            var moveToPath = Path.Combine(saveLocation, newFileName);
////            return moveToPath;
////        }

////        private string GetTempFilePathWithExtension()
////        {
////            var path = Path.GetTempPath();
////            var fileName = string.Concat(Guid.NewGuid().ToString(), VideoExtension);
////            return Path.Combine(path, fileName);
////        }

////        private void DeleteTempVideo()
////        {
////            if (_screenCaptureJob != null && File.Exists(_screenCaptureJob.OutputScreenCaptureFileName))
////            {
////                File.Delete(_screenCaptureJob.OutputScreenCaptureFileName);
////            }
////        }
////    }
////}