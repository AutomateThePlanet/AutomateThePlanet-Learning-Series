﻿// <copyright file="HtmlInputControlExtensions.cs" company="Automate The Planet Ltd.">
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
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace AdvancedWebUiComponentsAutomation.ControlsExtensions
{
    public static class HtmlInputControlExtensions
    {
        public static void SimulateRealTextTyping(this HtmlInputControl control, string text)
        {
            control.ScrollToVisible(ScrollToVisibleType.ElementTopAtWindowTop);
            Manager.Current.ActiveBrowser.Window.SetFocus();
            control.Focus();
            control.MouseClick();
            Manager.Current.Desktop.KeyBoard.TypeText(text, 50, 100, true);
        }
    }
}
