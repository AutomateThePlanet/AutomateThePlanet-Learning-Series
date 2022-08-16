// <copyright file="WikipediaMainPageValidator.cs" company="Automate The Planet Ltd.">
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

using IoCContainerPageObjectPattern.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoCContainerPageObjectPattern.WikipediaMainPage;

public class WikipediaMainPageValidator : BasePageValidator<WikipediaMainPageMap>
{
    public void ToogleLinkTextShow()
    {
        Assert.AreEqual<string>("show", Map.ContentsToggleLink.Text, "The contents toggle button text was not as expected.");
    }

    public void ToogleLinkTextHide()
    {
        Assert.AreEqual<string>("hide", Map.ContentsToggleLink.Text, "The contents toggle button text was not as expected.");
    }

    public void ContentsListHidden()
    {
        var contentsListStyle = Map.ContentsList.GetAttribute("style");
        Assert.AreEqual<string>("display: none;", contentsListStyle, "The contents list is still visible.");
    }

    public void ContentsListVisible()
    {
        var contentsListStyle = Map.ContentsList.GetAttribute("style");
        Assert.AreEqual<string>("display: block;", contentsListStyle, "The contents list is still invisible.");
    }
}