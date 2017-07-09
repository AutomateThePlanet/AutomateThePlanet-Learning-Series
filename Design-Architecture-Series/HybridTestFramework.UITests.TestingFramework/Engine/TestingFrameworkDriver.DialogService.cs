// <copyright file="TestingFrameworkDriver.DialogService.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core;
using System;
using Dialogs = ArtOfTest.WebAii.Win32.Dialogs;

namespace HybridTestFramework.UITests.TestingFramework.Engine
{
    public partial class TestingFrameworkDriver : IDialogService
    {
        public void Handle(
            Action action = null,
            DialogButton dialogButton = DialogButton.Ok)
        {
            var confirmDialog =
                new Dialogs.ConfirmDialog(
                    _driver.ActiveBrowser,
                    (Dialogs.DialogButton)Enum.Parse(typeof(Dialogs.DialogButton),
                        ((int)dialogButton).ToString()));
            try
            {
                _driver.DialogMonitor.AddDialog(confirmDialog);
                _driver.DialogMonitor.Start();
                if (action != null)
                {
                    action.Invoke();
                }
                confirmDialog.WaitUntilHandled();
                confirmDialog.Handle();
            }
            finally
            {
                _driver.DialogMonitor.RemoveDialog(confirmDialog);
                _driver.DialogMonitor.Stop();
            }
        }

        public void HandleLogonDialog(
            string userName,
            string password)
        {
            var logonDialog = new Dialogs.LogonDialog(
                _driver.ActiveBrowser,
                userName,
                password,
                Dialogs.DialogButton.OK);
            try
            {
                _driver.DialogMonitor.AddDialog(logonDialog);
                _driver.DialogMonitor.Start();
            }
            finally
            {
                _driver.DialogMonitor.RemoveDialog(logonDialog);
                _driver.DialogMonitor.Stop();
            }
        }

        public void Upload(string filePath)
        {
            var fileUploadDialog =
                new Dialogs.FileUploadDialog(
                    _driver.ActiveBrowser,
                    filePath,
                    Dialogs.DialogButton.CANCEL);
            try
            {
                _driver.DialogMonitor.AddDialog(fileUploadDialog);
                _driver.DialogMonitor.Start();
                fileUploadDialog.WaitUntilHandled();
                fileUploadDialog.Handle();
            }
            finally
            {
                _driver.DialogMonitor.RemoveDialog(fileUploadDialog);
                _driver.DialogMonitor.Stop();
            }
        }
    }
}