// <copyright file="BaseLogManager.cs" company="AANGELOV">
// http://aangelov.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

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