// <copyright file="KendoDatePicker.cs" company="Automate The Planet Ltd.">
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
namespace AdvancedWebUiComponentsAutomation;
public class KendoDatePicker
{
    private readonly string _idLocator;
    private readonly IWebDriver _driver;

    public KendoDatePicker(IWebDriver driver, string idLocator)
    {
        _driver = driver;
        _idLocator = idLocator;
    }

    public void SetDate(DateTime dateTime)
    {
        string scriptToBeExecuted = string.Format("$('#{0}').kendoDatePicker({{ value: new Date({1}, {2}, {3}) }});", _idLocator, dateTime.Year, dateTime.Month - 1, dateTime.Day);
        var javaScriptExecutor = (IJavaScriptExecutor)_driver;
        javaScriptExecutor.ExecuteScript(scriptToBeExecuted);
    }
}