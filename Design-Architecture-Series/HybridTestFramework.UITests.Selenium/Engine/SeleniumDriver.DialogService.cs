using HybridTestFramework.UITests.Core;
using System;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : IDialogService
    {
        public void Handle(Action action = null, DialogButton dialogButton = DialogButton.OK)
        {
            throw new NotImplementedException();
        }

        public void HandleLogonDialog(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public void Upload(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
