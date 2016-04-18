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
using AAngelov.Utilities.Managers;

namespace AAngelov.Utilities.UI.Managers
{
    /// <summary>
    /// Contains UI App related Registry methods
    /// </summary>
    public class UIRegistryManager : BaseRegistryManager
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static UIRegistryManager instance;

        /// <summary>
        /// The is window closed from executable sub key name
        /// </summary>
        private readonly string isWindowClosedFromXSubKeyName = "isWindowClosedFromX";

        /// <summary>
        /// The theme registry sub key name
        /// </summary>
        private readonly string themeRegistrySubKeyName = "theme";

        /// <summary>
        /// The color registry sub key name
        /// </summary>
        private readonly string colorRegistrySubKeyName = "color";

        /// <summary>
        /// The shouldOpenDropDownOnHover registry sub key name- shows if the drop downs will be opened on hover
        /// </summary>
        private readonly string shouldOpenDropDownOnHoverRegistrySubKeyName = "shouldOpenDropDrownOnHover";

        /// <summary>
        /// The title prompt dialog registry sub key name
        /// </summary>
        private readonly string titlePromptDialogRegistrySubKeyName = "titlePromptDialog";

        /// <summary>
        /// The checkbox prompt dialog is submitted registry sub key name
        /// </summary>
        private readonly string checkboxPromptDialogIsSubmittedRegistrySubKeyName = "checkboxPromptDialogIsSubmitted";

        /// <summary>
        /// The checked properties prompt dialog registry sub key name
        /// </summary>
        private readonly string checkedPropertiesPromptDialogRegistrySubKeyName = "checkedPropertiesPromptDialog";

        /// <summary>
        /// The title prompt dialog is canceled registry sub key name
        /// </summary>
        private readonly string isCanceledtitlePromptDialogRegistrySubKeyName = "titlePromptDialogIsCanceled";

        /// <summary>
        /// The title title prompt dialog is canceled registry sub key name
        /// </summary>
        private readonly string titleTitlePromptDialogIsCanceledRegistrySubKeyName = "titleTitlePromptDialog";

        /// <summary>
        /// Initializes a new instance of the <see cref="UIRegistryManager"/> class.
        /// </summary>
        public UIRegistryManager(string mainRegistrySubKey)
        {
            this.MainRegistrySubKey = mainRegistrySubKey;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
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

        /// <summary>
        /// Writes the current theme to registry.
        /// </summary>
        /// <param name="theme">The theme name.</param>
        public void WriteCurrentTheme(string theme)
        {
            this.Write(this.GenerateMergedKey(this.themeRegistrySubKeyName), theme); 
        }

        /// <summary>
        /// Writes the is window closed from executable.
        /// </summary>
        /// <param name="isWindowClosedFromX">if set to <c>true</c> [is window closed from executable].</param>
        public void WriteIsWindowClosedFromX(bool isWindowClosedFromX)
        {
            this.Write(this.GenerateMergedKey(this.isWindowClosedFromXSubKeyName), isWindowClosedFromX); 
        }

        /// <summary>
        /// Writes the is checkbox dialog submitted.
        /// </summary>
        /// <param name="IsCheckboxDialogSubmitted">if set to <c>true</c> [is checkbox dialog submitted].</param>
        public void WriteIsCheckboxDialogSubmitted(bool isCheckboxDialogSubmitted)
        {
            this.Write(this.GenerateMergedKey(this.checkboxPromptDialogIsSubmittedRegistrySubKeyName), isCheckboxDialogSubmitted); 
        }

        /// <summary>
        /// Writes the checked properties automatic be exported.
        /// </summary>
        /// <param name="checkedPropertiesToBeExported">The checked properties automatic be exported.</param>
        public void WriteCheckedPropertiesToBeExported(string checkedPropertiesToBeExported)
        {
            this.Write(this.GenerateMergedKey(this.checkedPropertiesPromptDialogRegistrySubKeyName), checkedPropertiesToBeExported); 
        }

        /// <summary>
        /// Writes the drop down behavior to registry.
        /// </summary>
        /// <param name="shouldOpenDropDownOnHover">if set to <c>true</c> [should open drop down configuration hover].</param>
        public void WriteDropDownBehavior(bool shouldOpenDropDownOnHover)
        {
            this.Write(this.GenerateMergedKey(this.shouldOpenDropDownOnHoverRegistrySubKeyName), shouldOpenDropDownOnHover); 
        }

        /// <summary>
        /// Writes the if the title promt dialog is canceled .
        /// </summary>
        /// <param name="isCanceled">if set to <c>true</c> [is canceled].</param>
        public void WriteIsCanceledTitlePromtDialog(bool isCanceled)
        {
            this.Write(this.GenerateMergedKey(this.titlePromptDialogRegistrySubKeyName, this.isCanceledtitlePromptDialogRegistrySubKeyName), isCanceled); 
        }

        /// <summary>
        /// Writes the title in title promt dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        public void WriteTitleTitlePromtDialog(string title)
        {
            this.Write(this.GenerateMergedKey(this.titlePromptDialogRegistrySubKeyName, this.titleTitlePromptDialogIsCanceledRegistrySubKeyName), title); 
        }

        /// <summary>
        /// Writes the current colors to registry.
        /// </summary>
        /// <param name="red">The red part.</param>
        /// <param name="green">The green part.</param>
        /// <param name="blue">The blue part.</param>
        public void WriteCurrentColors(byte red, byte green, byte blue)
        {
            this.Write(this.GenerateMergedKey(this.colorRegistrySubKeyName), string.Format("{0}&{1}&{2}", red, green, blue)); 
        }

        /// <summary>
        /// Reads the is window closed from executable.
        /// </summary>
        /// <returns></returns>
        public bool ReadIsWindowClosedFromX()
        {
            return this.ReadBool(this.GenerateMergedKey(this.isWindowClosedFromXSubKeyName));
        }

        /// <summary>
        /// Reads the is checkbox dialog submitted.
        /// </summary>
        /// <returns>is checkbox dialog submitted</returns>
        public bool ReadIsCheckboxDialogSubmitted()
        {
            return this.ReadBool(this.GenerateMergedKey(this.checkboxPromptDialogIsSubmittedRegistrySubKeyName));
        }

        /// <summary>
        /// Reads the checked properties automatic be exported.
        /// </summary>
        /// <returns>check properties to be exported</returns>
        public string ReadCheckedPropertiesToBeExported()
        {
            return this.ReadStr(this.GenerateMergedKey(this.checkedPropertiesPromptDialogRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the title in title promt dialog.
        /// </summary>
        /// <returns>the title</returns>
        public string GetContentPromtDialog()
        {
            return this.ReadStr(this.GenerateMergedKey(this.titlePromptDialogRegistrySubKeyName, this.titleTitlePromptDialogIsCanceledRegistrySubKeyName));
        }

        /// <summary>
        /// Gets if the title promt dialog is canceled.
        /// </summary>
        /// <returns> if the title promt dialog was canceled</returns>
        public bool GetIsCanceledPromtDialog()
        {
            return this.ReadBool(this.GenerateMergedKey(this.titlePromptDialogRegistrySubKeyName, this.isCanceledtitlePromptDialogRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the colors from registry.
        /// </summary>
        /// <returns>the colors</returns>
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

        /// <summary>
        /// Gets the theme from registry.
        /// </summary>
        /// <returns>the theme</returns>
        public string GetTheme()
        {
            return this.ReadStr(this.GenerateMergedKey(this.themeRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the drop down behavior from registry.
        /// </summary>
        /// <returns>drop down behavior</returns>
        public bool GetDropDownBehavior()
        {
            return this.ReadBool(this.GenerateMergedKey(this.shouldOpenDropDownOnHoverRegistrySubKeyName));
        }
    }
}