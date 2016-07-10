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
            DialogButton dialogButton = DialogButton.OK)
        {
            Dialogs.ConfirmDialog confirmDialog =
                new Dialogs.ConfirmDialog(
                    this.driver.ActiveBrowser,
                    (Dialogs.DialogButton)Enum.Parse(typeof(Dialogs.DialogButton),
                        ((int)dialogButton).ToString()));
            try
            {
                this.driver.DialogMonitor.AddDialog(confirmDialog);
                this.driver.DialogMonitor.Start();
                if (action != null)
                {
                    action.Invoke();
                }
                confirmDialog.WaitUntilHandled();
                confirmDialog.Handle();
            }
            finally
            {
                this.driver.DialogMonitor.RemoveDialog(confirmDialog);
                this.driver.DialogMonitor.Stop();
            }
        }

        public void HandleLogonDialog(
            string userName,
            string password)
        {
            var logonDialog = new Dialogs.LogonDialog(
                this.driver.ActiveBrowser,
                userName,
                password,
                Dialogs.DialogButton.OK);
            try
            {
                this.driver.DialogMonitor.AddDialog(logonDialog);
                this.driver.DialogMonitor.Start();
            }
            finally
            {
                this.driver.DialogMonitor.RemoveDialog(logonDialog);
                this.driver.DialogMonitor.Stop();
            }
        }

        public void Upload(string filePath)
        {
            Dialogs.FileUploadDialog fileUploadDialog =
                new Dialogs.FileUploadDialog(
                    this.driver.ActiveBrowser,
                    filePath,
                    Dialogs.DialogButton.CANCEL);
            try
            {
                this.driver.DialogMonitor.AddDialog(fileUploadDialog);
                this.driver.DialogMonitor.Start();
                fileUploadDialog.WaitUntilHandled();
                fileUploadDialog.Handle();
            }
            finally
            {
                this.driver.DialogMonitor.RemoveDialog(fileUploadDialog);
                this.driver.DialogMonitor.Stop();
            }
        }
    }
}