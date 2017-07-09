// <copyright file="BasePage.cs" company="Automate The Planet Ltd.">
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

namespace HybridTestFramework.UITests.Core
{
    public abstract class BasePage
    {
        // Changed to be not readonly.
        private IElementFinder _elementFinder;
        private INavigationService _navigationService;

        public BasePage(IElementFinder elementFinder, INavigationService navigationService)
        {
            this._elementFinder = elementFinder;
            this._navigationService = navigationService;
        }

        // add an empty contstructor in order the decorators to be able to work.
        public BasePage()
        {
        }

        // changed to have setter.
        protected IElementFinder ElementFinder
        {
            get
            {
                return _elementFinder;
            }
            set
            {
                _elementFinder = value;
            }
        }

        // changed to have setter.
        protected INavigationService NavigationService
        {
            get
            {
                return _navigationService;
            }
            set
            {
                _navigationService = value;
            }
        }
    }
}