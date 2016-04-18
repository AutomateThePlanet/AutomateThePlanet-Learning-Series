// <copyright file="PromptDialogViewModel.cs" company="Automate The Planet Ltd.">
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
using AAngelov.Utilities.UI.Core;
using AAngelov.Utilities.UI.Managers;

namespace AAngelov.Utilities.UI.ViewModels
{
    /// <summary>
    /// Holds PromptDialogView Properties
    /// </summary>
    public class PromptDialogViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The content
        /// </summary>
        private string content;

        /// <summary>
        /// The is canceled
        /// </summary>
        private bool isCanceled;

        /// <summary>
        /// Gets or sets the Content.
        /// </summary>
        /// <value>
        /// The Content.
        /// </value>
        public string Content
        {
            get
            {
                if (this.content == null)
                {
                    this.content = UIRegistryManager.Instance.GetContentPromtDialog();
                }

                return this.content;
            }

            set
            {
                this.content = value;
                UIRegistryManager.Instance.WriteTitleTitlePromtDialog(this.content);
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is canceled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is canceled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsCanceled
        {
            get
            {
                return this.isCanceled;
            }

            set
            {
                this.isCanceled = value;
                UIRegistryManager.Instance.WriteIsCanceledTitlePromtDialog(this.isCanceled);
                this.NotifyPropertyChanged();
            }
        }
    }
}