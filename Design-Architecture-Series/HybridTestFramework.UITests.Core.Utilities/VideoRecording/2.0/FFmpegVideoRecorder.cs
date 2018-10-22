// <copyright file="FFmpegVideoRecorder.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.UITests.Core.Utilities.VideoRecording.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace HybridTestFramework.UITests.Core.Utilities.VideoRecording
{
    public class FFmpegVideoRecorder : IVideoRecorder
    {
        private Process _recorderProcess;
        private bool _isRunning;

        public void Dispose()
        {
            if (_isRunning)
            {
                // Wait for 500 milliseconds before finishing video
                Thread.Sleep(500);
                if (!_recorderProcess.HasExited)
                {
                    _recorderProcess?.Kill();
                    _recorderProcess?.WaitForExit();
                }

                _isRunning = false;
            }
        }

        public string Record(string filePath, string fileName)
        {
            string videoPath = $"{Path.Combine(filePath, fileName)}";
            string videoFilePathWithExtension = GetFilePathWithExtensionByOS(videoPath);
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"A problem occurred trying to initialize the create the directory you have specified. - {filePath}", ex);
            }

            if (!_isRunning)
            {
                // TODO: different options for the different OS
                // TODO: add settings classes to control the quality?
                var startInfo = GetProcessStartInfoByOS(videoFilePathWithExtension);
                _recorderProcess = Process.Start(startInfo);
                _isRunning = true;
            }

            return videoFilePathWithExtension;
        }

        public void Stop() => Dispose();

        private string GetFFmpegPath() => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(), "ffmpeg.exe");

        // TODO: Presentation: 5.1. Video Recording Implementation
        private ProcessStartInfo GetProcessStartInfoByOS(string videoFilePathWithExtension)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = GetFFmpegPath(),
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false,
            };
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                startInfo.Arguments = $"-f gdigrab -framerate 30 -i desktop {videoFilePathWithExtension}";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                startInfo.Arguments = $"-f avfoundation -framerate 30 -i default {videoFilePathWithExtension}";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                startInfo.Arguments = $"-f x11grab -framerate 30 -i :0.0+100,200 {videoFilePathWithExtension}";
            }
            else
            {
                throw new NotSupportedException("The OS is not supported by FFmpeg video recorder. Currently supported OS are Windows, MacOS, Linux.");
            }

            return startInfo;
        }

        private string GetFilePathWithExtensionByOS(string videoPathNoExtension)
        {
            string videoPathWithExtension;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                videoPathWithExtension = $"{videoPathNoExtension}.mpg";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                videoPathWithExtension = $"{videoPathNoExtension}.mov";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                videoPathWithExtension = $"{videoPathNoExtension}.mp4";
            }
            else
            {
                throw new NotSupportedException("The OS is not supported by FFmpeg video recorder. Currently supported OS are Windows, MacOS, Linux.");
            }

            return videoPathWithExtension;
        }
    }
}
