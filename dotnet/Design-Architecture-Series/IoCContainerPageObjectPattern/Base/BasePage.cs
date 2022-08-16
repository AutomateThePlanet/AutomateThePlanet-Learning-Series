// <copyright file="BasePage.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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

using IoCContainerPageObjectPattern.Core;

namespace IoCContainerPageObjectPattern.Base;

public class BasePage<TM>
    where TM : BasePageElementMap, new()
{
    protected readonly string Url;

    public BasePage(string url)
    {
        Url = url;
    }

    public BasePage()
    {
        Url = null;
    }

    protected TM Map
    {
        get
        {
            return new TM();
        }
    }

    public virtual void Navigate(string part = "")
    {
        Driver.Browser.Navigate().GoToUrl(string.Concat(Url, part));
    }
}

public class BasePage<TM, TV> : BasePage<TM>
    where TM : BasePageElementMap, new()
    where TV : BasePageValidator<TM>, new()
{
    public BasePage(string url) : base(url)
    {
    }

    public BasePage()
    {
    }

    public TV Validate()
    {
        return new TV();
    }
}