// <copyright file="UIRegistryManager.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
            MainRegistrySubKey = mainRegistrySubKey;
        }

        public static UIRegistryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var mainRegistrySubKey = ConfigurationManager.AppSettings["mainUIRegistrySubKey"];
                    instance = new UIRegistryManager(mainRegistrySubKey);
                }
                return instance;
            }
        }

        public void WriteCurrentTheme(string theme)
        {
            Write(GenerateMergedKey(themeRegistrySubKeyName), theme);
        }

        public void WriteIsWindowClosedFromX(bool isWindowClosedFromX)
        {
            Write(GenerateMergedKey(isWindowClosedFromXSubKeyName), isWindowClosedFromX);
        }

        public void WriteIsCheckboxDialogSubmitted(bool isCheckboxDialogSubmitted)
        {
            Write(GenerateMergedKey(checkboxPromptDialogIsSubmittedRegistrySubKeyName), isCheckboxDialogSubmitted);
        }

        public void WriteCheckedPropertiesToBeExported(string checkedPropertiesToBeExported)
        {
            Write(GenerateMergedKey(checkedPropertiesPromptDialogRegistrySubKeyName), checkedPropertiesToBeExported);
        }

        public void WriteDropDownBehavior(bool shouldOpenDropDownOnHover)
        {
            Write(GenerateMergedKey(shouldOpenDropDownOnHoverRegistrySubKeyName), shouldOpenDropDownOnHover);
        }

        public void WriteIsCanceledTitlePromtDialog(bool isCanceled)
        {
            Write(GenerateMergedKey(titlePromptDialogRegistrySubKeyName, isCanceledtitlePromptDialogRegistrySubKeyName), isCanceled);
        }

        public void WriteTitleTitlePromtDialog(string title)
        {
            Write(GenerateMergedKey(titlePromptDialogRegistrySubKeyName, titleTitlePromptDialogIsCanceledRegistrySubKeyName), title);
        }

        public void WriteCurrentColors(byte red, byte green, byte blue)
        {
            Write(GenerateMergedKey(colorRegistrySubKeyName), string.Format("{0}&{1}&{2}", red, green, blue));
        }

        public bool ReadIsWindowClosedFromX()
        {
            return ReadBool(GenerateMergedKey(isWindowClosedFromXSubKeyName));
        }

        public bool ReadIsCheckboxDialogSubmitted()
        {
            return ReadBool(GenerateMergedKey(checkboxPromptDialogIsSubmittedRegistrySubKeyName));
        }

        public string ReadCheckedPropertiesToBeExported()
        {
            return ReadStr(GenerateMergedKey(checkedPropertiesPromptDialogRegistrySubKeyName));
        }

        public string GetContentPromtDialog()
        {
            return ReadStr(GenerateMergedKey(titlePromptDialogRegistrySubKeyName, titleTitlePromptDialogIsCanceledRegistrySubKeyName));
        }

        public bool GetIsCanceledPromtDialog()
        {
            return ReadBool(GenerateMergedKey(titlePromptDialogRegistrySubKeyName, isCanceledtitlePromptDialogRegistrySubKeyName));
        }

        public string[] GetColors()
        {
            string[] colorsStr = null;
            var colors = ReadStr(GenerateMergedKey(colorRegistrySubKeyName));
            if (!string.IsNullOrEmpty(colors))
            {
                colorsStr = colors.Split('&');
            }

            return colorsStr;
        }

        public string GetTheme()
        {
            return ReadStr(GenerateMergedKey(themeRegistrySubKeyName));
        }

        public bool GetDropDownBehavior()
        {
            return ReadBool(GenerateMergedKey(shouldOpenDropDownOnHoverRegistrySubKeyName));
        }
    }
}
