// <copyright file="ComboBox.cs" company="Automate The Planet Ltd.">
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

using System;
using ArtOfTest.WebAii.Controls.HtmlControls;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;
using Microsoft.Practices.Unity;

namespace HybridTestFramework.UITests.TestingFramework.Controls
{
    public class ComboBox : ContentElement<HtmlInputCheckBox>, IComboBox
    {
        private readonly string _jqueryExpression = 
            "$(\"select[id$='{0}'] option\").each(function() {{if (this.text == '{1}') {{ $(this).parent().val(this.value).change() }} }});";
        
        public ComboBox(IDriver driver,
            ArtOfTest.WebAii.ObjectModel.Element element,
            IUnityContainer container) : base(driver, element, container)
        {
        }

        public string SelectedValue
        {
            get
            {
                return HtmlControl.BaseElement.InnerText;
            }
        }

        public void SelectValue(string value)
        {
            JQuerySelectByText(HtmlControl.ID, value);
        }

        private void JQuerySelectByText(string expression, string text)
        {
            var javaScriptToBeExecuted = string.Format(_jqueryExpression, expression, text);
            Driver.InvokeScript(javaScriptToBeExecuted);
            Driver.WaitForAjax();
        }
    }
}