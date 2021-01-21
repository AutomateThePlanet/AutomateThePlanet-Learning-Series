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
        private readonly string _isWindowClosedFromXSubKeyName = "isWindowClosedFromX";
        private readonly string _themeRegistrySubKeyName = "theme";
        private readonly string _colorRegistrySubKeyName = "color";
        private readonly string _shouldOpenDropDownOnHoverRegistrySubKeyName = "shouldOpenDropDrownOnHover";
        private readonly string _titlePromptDialogRegistrySubKeyName = "titlePromptDialog";
        private readonly string _checkboxPromptDialogIsSubmittedRegistrySubKeyName = "checkboxPromptDialogIsSubmitted";
        private readonly string _checkedPropertiesPromptDialogRegistrySubKeyName = "checkedPropertiesPromptDialog";
        private readonly string _isCanceledtitlePromptDialogRegistrySubKeyName = "titlePromptDialogIsCanceled";
        private readonly string _titleTitlePromptDialogIsCanceledRegistrySubKeyName = "titleTitlePromptDialog";

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
            Write(GenerateMergedKey(_themeRegistrySubKeyName), theme);
        }

        public void WriteIsWindowClosedFromX(bool isWindowClosedFromX)
        {
            Write(GenerateMergedKey(_isWindowClosedFromXSubKeyName), isWindowClosedFromX);
        }

        public void WriteIsCheckboxDialogSubmitted(bool isCheckboxDialogSubmitted)
        {
            Write(GenerateMergedKey(_checkboxPromptDialogIsSubmittedRegistrySubKeyName), isCheckboxDialogSubmitted);
        }

        public void WriteCheckedPropertiesToBeExported(string checkedPropertiesToBeExported)
        {
            Write(GenerateMergedKey(_checkedPropertiesPromptDialogRegistrySubKeyName), checkedPropertiesToBeExported);
        }

        public void WriteDropDownBehavior(bool shouldOpenDropDownOnHover)
        {
            Write(GenerateMergedKey(_shouldOpenDropDownOnHoverRegistrySubKeyName), shouldOpenDropDownOnHover);
        }

        public void WriteIsCanceledTitlePromtDialog(bool isCanceled)
        {
            Write(GenerateMergedKey(_titlePromptDialogRegistrySubKeyName, _isCanceledtitlePromptDialogRegistrySubKeyName), isCanceled);
        }

        public void WriteTitleTitlePromtDialog(string title)
        {
            Write(GenerateMergedKey(_titlePromptDialogRegistrySubKeyName, _titleTitlePromptDialogIsCanceledRegistrySubKeyName), title);
        }

        public void WriteCurrentColors(byte red, byte green, byte blue)
        {
            Write(GenerateMergedKey(_colorRegistrySubKeyName), string.Format("{0}&{1}&{2}", red, green, blue));
        }

        public bool ReadIsWindowClosedFromX()
        {
            return ReadBool(GenerateMergedKey(_isWindowClosedFromXSubKeyName));
        }

        public bool ReadIsCheckboxDialogSubmitted()
        {
            return ReadBool(GenerateMergedKey(_checkboxPromptDialogIsSubmittedRegistrySubKeyName));
        }

        public string ReadCheckedPropertiesToBeExported()
        {
            return ReadStr(GenerateMergedKey(_checkedPropertiesPromptDialogRegistrySubKeyName));
        }

        public string GetContentPromtDialog()
        {
            return ReadStr(GenerateMergedKey(_titlePromptDialogRegistrySubKeyName, _titleTitlePromptDialogIsCanceledRegistrySubKeyName));
        }

        public bool GetIsCanceledPromtDialog()
        {
            return ReadBool(GenerateMergedKey(_titlePromptDialogRegistrySubKeyName, _isCanceledtitlePromptDialogRegistrySubKeyName));
        }

        public string[] GetColors()
        {
            string[] colorsStr = null;
            var colors = ReadStr(GenerateMergedKey(_colorRegistrySubKeyName));
            if (!string.IsNullOrEmpty(colors))
            {
                colorsStr = colors.Split('&');
            }

            return colorsStr;
        }

        public string GetTheme()
        {
            return ReadStr(GenerateMergedKey(_themeRegistrySubKeyName));
        }

        public bool GetDropDownBehavior()
        {
            return ReadBool(GenerateMergedKey(_shouldOpenDropDownOnHoverRegistrySubKeyName));
        }
    }
}
