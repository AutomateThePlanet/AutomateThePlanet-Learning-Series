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

namespace SingletonDesignPattern.Core
{
    public class BasePage<M>
        where M : BasePageElementMap, new()
    {
        protected readonly string url;

        private static BasePage<M> instance;

        public BasePage(string url)
        {
            this.url = url;
        }

        public BasePage()
        {
            this.url = null;
        }

        public static BasePage<M> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BasePage<M>();
                }

                return instance;
            }
        }

        protected M Map
        {
            get
            {
                return new M();
            }
        }

        public virtual void Navigate(string part = "")
        {
            Driver.Browser.Navigate().GoToUrl(string.Concat(this.url, part));
        }
    }

    public class BasePage<M, V> : BasePage<M>
        where M : BasePageElementMap, new()
        where V : BasePageValidator<M>, new()
    {
        public BasePage(string url) : base(url)
        {
        }

        public BasePage()
        {
        }

        public V Validate()
        {
            return new V();
        }
    }
}