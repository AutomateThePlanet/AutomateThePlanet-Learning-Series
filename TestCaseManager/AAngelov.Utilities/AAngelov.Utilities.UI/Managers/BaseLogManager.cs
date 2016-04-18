// <copyright file="BaseLogManager.cs" company="Automate The Planet Ltd.">
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
namespace AAngelov.Utilities.UI.Managers
{
    using System;
    using System.IO;
    using AAngelov.Utilities.Entities;
    using AAngelov.Utilities.UI.Enums;

    /// <summary>
    /// Provides base logic for all log managers
    /// </summary>
    public class BaseLogManager
    {
        /// <summary>
        /// The full result file path
        /// </summary>
        public string FullResultFilePath { get; set; }

        /// <summary>
        /// Initializes the log files.
        /// </summary>
        public void Initialize(FileType fileType, string resultsFilePrefix, string resultsFolder)
        {
            string fileExtension = BaseFileTypeManager.GetExtensionByFileType(fileType);
            string uniqueFileName = String.Concat(resultsFilePrefix, DateTime.Now.ToString(DateTimeFormats.DateTimeShortFileFormat), fileExtension);
            this.FullResultFilePath = Path.Combine(resultsFolder, uniqueFileName);
        }

        /// <summary>
        /// Opens the config in notepad.
        /// </summary>
        /// <param name="configPath">The config path.</param>
        public void OpenConfigInNotepad(string configPath)
        {
            string notepadPlusPath = @"C:\Program Files (x86)\Notepad++\notepad++.exe";
            string notepadPath = File.Exists(notepadPlusPath) ? notepadPlusPath : "notepad";
            string configArg = File.Exists(notepadPlusPath) ? String.Format("{0} -lxml", configPath) : configPath;
            System.Diagnostics.ProcessStartInfo procStartInfo =
                new System.Diagnostics.ProcessStartInfo(notepadPath, configArg);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            string result = proc.StandardOutput.ReadToEnd();
        }
    }
}