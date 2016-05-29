using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtOfTest.WebAii.Win32.Dialogs;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using System.IO;

namespace HowToCreateGenericDialogHandlers
{
    [TestClass]
    public class HandleDialogsTests
    {
        private Manager manager;

        [TestInitialize]
        public void TestInitialize()
        {
            Settings mySettings = new Settings();
            mySettings.Web.DefaultBrowser = BrowserType.FireFox;
            manager = new Manager(mySettings);
            manager.Start();
            manager.LaunchNewBrowser();
            manager.Settings.Web.RecycleBrowser = true;
            manager.Settings.AnnotateExecution = true;
            manager.Settings.Web.KillBrowserProcessOnClose = true;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            manager.Dispose();
        }

        [TestMethod]
        public void DownloadDialogNotUniversal()
        {
            string fileToDownload =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    @"myw3schoolsimage.jpg");
            if (File.Exists(fileToDownload))
            {
                File.Delete(fileToDownload);
            }
            var dialog = new DownloadDialogsHandler(
                manager.ActiveBrowser,
                DialogButton.SAVE,
                fileToDownload,
                manager.Desktop);

            manager.DialogMonitor.Start();
            manager.ActiveBrowser.NavigateTo(
                "http://www.w3schools.com/tags/tryit.asp?filename=tryhtml5_a_download");
            ArtOfTest.WebAii.Core.Browser myFrame =
                manager.ActiveBrowser.Frames.ById("iframeResult");
            HtmlImage image =
                myFrame.Find.AllByTagName<HtmlImage>("img")[0];
            image.Click(false);

            dialog.WaitUntilHandled();
        }

        [TestMethod]
        public void DownloadDialogUniversal()
        {
            string fileToDownload =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    @"myw3schoolsimage.jpg");
            if (File.Exists(fileToDownload))
            {
                File.Delete(fileToDownload);
            }

            manager.ActiveBrowser.NavigateTo(
                "http://www.w3schools.com/tags/tryit.asp?filename=tryhtml5_a_download");
            DownloadDialogHandler.DownloadFile(
                () =>
                {
                    Browser myFrame = manager.ActiveBrowser.Frames.ById("iframeResult");
                    HtmlImage image = myFrame.Find.AllByTagName<HtmlImage>("img")[0];
                    image.Click(false);
                },
                AppDomain.CurrentDomain.BaseDirectory);
        }

        [TestMethod]
        public void UploadFileDialogNotUniversal()
        {
            string fileToBeUploadedPath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    @"myw3schoolsimage.jpg");
            var fileDialog = new FileUploadDialog(
                manager.ActiveBrowser,
                fileToBeUploadedPath,
                DialogButton.OPEN);
            manager.DialogMonitor.AddDialog(fileDialog);
            manager.DialogMonitor.Start();

            manager.ActiveBrowser.NavigateTo(
                "http://nervgh.github.io/pages/angular-file-upload/examples/simple/");
            HtmlInputFile fileInput =
                manager.ActiveBrowser.Find.ByXPath<HtmlInputFile>(
                    "//*[@id='ng-app']/body/div/div[2]/div[1]/input[2]");
            fileInput.Click(false);
        }

        [TestMethod]
        public void UploadFileDialogUniversal()
        {
            string fileToBeUploadedPath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    @"myw3schoolsimage.jpg");
            manager.ActiveBrowser.NavigateTo(
                "http://nervgh.github.io/pages/angular-file-upload/examples/simple/");
            FileUploader.Upload(
                () =>
                {
                    HtmlInputFile fileInput =
                        manager.ActiveBrowser.Find.ByXPath<HtmlInputFile>(
                            "//*[@id='ng-app']/body/div/div[2]/div[1]/input[2]");
                    fileInput.Click(false);
                },
                fileToBeUploadedPath);
        }

        [TestMethod]
        public void ConfirmDialogNotUniversal()
        {
            var dialog = new ConfirmDialog(manager.ActiveBrowser, DialogButton.OK);
            manager.DialogMonitor.AddDialog(dialog);

            manager.DialogMonitor.Start();

            manager.ActiveBrowser.NavigateTo(
                "http://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_confirm");
            Browser myFrame = manager.ActiveBrowser.Frames.ById("iframeResult");
            HtmlButton alertButton = myFrame.Find.AllByTagName<HtmlButton>("button")[0];
            alertButton.Click(false);

            dialog.WaitUntilHandled();
            manager.DialogMonitor.RemoveDialog(dialog);
        }

        [TestMethod]
        public void ConfirmDialogUniversal()
        {
            manager.ActiveBrowser.NavigateTo(
                "http://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_confirm");
            ConfirmDialogHandler.Handle(
                () =>
                {
                    Browser myFrame = manager.ActiveBrowser.Frames.ById("iframeResult");
                    HtmlButton alertButton =
                        myFrame.Find.AllByTagName<HtmlButton>("button")[0];
                    alertButton.Click(false);
                });
        }
    }
}