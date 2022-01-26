// <copyright file="FullCalendar.cs" company="Automate The Planet Ltd.">
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
namespace AdvancedWebUIComponentsAutomation;
public class FullCalendar
{
    private readonly string _fullCalendarMethodJqueryExpression = "$('#{0}').fullCalendar('{1}')";
    private readonly string _idLocator;
    private readonly IWebDriver _driver;

    public FullCalendar(IWebDriver driver, string idLocator)
    {
        _driver = driver;
        _idLocator = idLocator;
    }

    public void ClickNextButton()
    {
        string scriptToBeExecuted = string.Format(_fullCalendarMethodJqueryExpression, _idLocator, "next");
        var javaScriptExecutor = (IJavaScriptExecutor)_driver;
        javaScriptExecutor.ExecuteScript(scriptToBeExecuted);
    }

    public void ClickPreviousButton()
    {
        string scriptToBeExecuted = string.Format(_fullCalendarMethodJqueryExpression, _idLocator, "prev");
        var javaScriptExecutor = (IJavaScriptExecutor)_driver;
        javaScriptExecutor.ExecuteScript(scriptToBeExecuted);
    }

    public void GoToToday()
    {
        string scriptToBeExecuted = string.Format(_fullCalendarMethodJqueryExpression, _idLocator, "today");
        var javaScriptExecutor = (IJavaScriptExecutor)_driver;
        javaScriptExecutor.ExecuteScript(scriptToBeExecuted); ;
    }

    public void GoToDate(DateTime date)
    {
        string scriptToBeExecuted = string.Format("$('#{0}').fullCalendar('gotoDate', $.fullCalendar.moment('{1}-{2}-{3}'))", _idLocator, date.Year, date.Month - 1, date.Day);
        var javaScriptExecutor = (IJavaScriptExecutor)_driver;
        javaScriptExecutor.ExecuteScript(scriptToBeExecuted);
    }
}
