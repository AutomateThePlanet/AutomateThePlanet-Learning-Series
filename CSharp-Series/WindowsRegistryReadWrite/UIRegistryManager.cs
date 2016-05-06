// <copyright file="UIRegistryManager.cs" company="Automate The Planet Ltd.">
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
using System.Configuration;

namespace WindowsRegistryReadWrite
{
    public class UIRegistryManager : BaseRegistryManager
    {
        private static UIRegistryManager instance;
        private readonly string isWindowClosedFromXSubKeyName = "isWindowClosedFromX";
        private readonly string themeRegistrySubKeyName = "theme";
        private readonly string colorRegistrySubKeyName = "color";
        private readonly string shouldOpenDropDownOnHoverRegistrySubKeyName = "shouldOpenDropDrownOnHover";
        private readonly string titlePromptDialogRegistrySubKeyName = "titlePromptDialog";
        private readonly string checkboxPromptDialogIsSubmittedRegistrySubKeyName = "checkboxPromptDialogIsSubmitted";
        private readonly string checkedPropertiesPromptDialogRegistrySubKeyName = "checkedPropertiesPromptDialog";
        private readonly string isCanceledtitlePromptDialogRegistrySubKeyName = "titlePromptDialogIsCanceled";
        private readonly string titleTitlePromptDialogIsCanceledRegistrySubKeyName = "titleTitlePromptDialog";

        public UIRegistryManager(string mainRegistrySubKey)
        {
            this.MainRegistrySubKey = mainRegistrySubKey;
        }

        public static UIRegistryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    string mainRegistrySubKey = ConfigurationManager.AppSettings["mainUIRegistrySubKey"];
                    instance = new UIRegistryManager(mainRegistrySubKey);
                }
                return instance;
            }
        }

        public void WriteCurrentTheme(string theme)
        {
            this.Write(this.GenerateMergedKey(this.themeRegistrySubKeyName), theme);
        }

        public void WriteIsWindowClosedFromX(bool isWindowClosedFromX)
        {
            this.Write(this.GenerateMergedKey(this.isWindowClosedFromXSubKeyName), isWindowClosedFromX);
        }

        public void WriteIsCheckboxDialogSubmitted(bool isCheckboxDialogSubmitted)
        {
            this.Write(this.GenerateMergedKey(this.checkboxPromptDialogIsSubmittedRegistrySubKeyName), isCheckboxDialogSubmitted);
        }

        public void WriteCheckedPropertiesToBeExported(string checkedPropertiesToBeExported)
        {
            this.Write(this.GenerateMergedKey(this.checkedPropertiesPromptDialogRegistrySubKeyName), checkedPropertiesToBeExported);
        }

        public void WriteDropDownBehavior(bool shouldOpenDropDownOnHover)
        {
            this.Write(this.GenerateMergedKey(this.shouldOpenDropDownOnHoverRegistrySubKeyName), shouldOpenDropDownOnHover);
        }

        public void WriteIsCanceledTitlePromtDialog(bool isCanceled)
        {
            this.Write(this.GenerateMergedKey(this.titlePromptDialogRegistrySubKeyName, this.isCanceledtitlePromptDialogRegistrySubKeyName), isCanceled);
        }

        public void WriteTitleTitlePromtDialog(string title)
        {
            this.Write(this.GenerateMergedKey(this.titlePromptDialogRegistrySubKeyName, this.titleTitlePromptDialogIsCanceledRegistrySubKeyName), title);
        }

        public void WriteCurrentColors(byte red, byte green, byte blue)
        {
            this.Write(this.GenerateMergedKey(this.colorRegistrySubKeyName), string.Format("{0}&{1}&{2}", red, green, blue));
        }

        public bool ReadIsWindowClosedFromX()
        {
            return this.ReadBool(this.GenerateMergedKey(this.isWindowClosedFromXSubKeyName));
        }

        public bool ReadIsCheckboxDialogSubmitted()
        {
            return this.ReadBool(this.GenerateMergedKey(this.checkboxPromptDialogIsSubmittedRegistrySubKeyName));
        }

        public string ReadCheckedPropertiesToBeExported()
        {
            return this.ReadStr(this.GenerateMergedKey(this.checkedPropertiesPromptDialogRegistrySubKeyName));
        }

        public string GetContentPromtDialog()
        {
            return this.ReadStr(this.GenerateMergedKey(this.titlePromptDialogRegistrySubKeyName, this.titleTitlePromptDialogIsCanceledRegistrySubKeyName));
        }

        public bool GetIsCanceledPromtDialog()
        {
            return this.ReadBool(this.GenerateMergedKey(this.titlePromptDialogRegistrySubKeyName, this.isCanceledtitlePromptDialogRegistrySubKeyName));
        }

        public string[] GetColors()
        {
            string[] colorsStr = null;
            string colors = this.ReadStr(this.GenerateMergedKey(this.colorRegistrySubKeyName));
            if (!string.IsNullOrEmpty(colors))
            {
                colorsStr = colors.Split('&');
            }

            return colorsStr;
        }

        public string GetTheme()
        {
            return this.ReadStr(this.GenerateMergedKey(this.themeRegistrySubKeyName));
        }

        public bool GetDropDownBehavior()
        {
            return this.ReadBool(this.GenerateMergedKey(this.shouldOpenDropDownOnHoverRegistrySubKeyName));
        }
    }
}
