/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Fidely.Framework;
using System.Diagnostics;
using System.Windows.Input;

namespace Fidely.Demo.GettingStarted.WPF.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string ApplicationName
        {
            get
            {
                return "Fidely Demo Application " + Constants.ProductVersion;
            }
        }

        public string Message
        {
            get
            {
                return "This is a demo application of Fidely search query compilation framework. For more information about Fidely, please refer to the project web site.";
            }
        }

        public string LinkText
        {
            get
            {
                return "Fidely Project Web Site";
            }
        }

        public ICommand ProjectSiteCommand { get; private set; }


        public AboutViewModel()
        {
            ProjectSiteCommand = new RelayCommand(o => OpenProjectSite());
        }


        private void OpenProjectSite()
        {
            Process.Start("http://bit.ly/jUQtSk");
        }
    }
}
