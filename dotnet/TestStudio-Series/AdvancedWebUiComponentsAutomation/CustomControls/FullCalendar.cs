﻿// <copyright file="FullCalendar.cs" company="Automate The Planet Ltd.">
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
using ArtOfTest.WebAii.Core;
using System;

namespace AdvancedWebUiComponentsAutomation.CustomControls
{
    public class FullCalendar
    {
        private readonly string fullCalendarMethodJqueryExpression =
            "$('#{0}').fullCalendar('{1}')";
        private readonly string idLocator;

        public FullCalendar(string idLocator)
        {
            this.idLocator = idLocator;
        }

        public void ClickNextButton()
        {
            string scriptToBeExecuted = 
                string.Format(fullCalendarMethodJqueryExpression, this.idLocator, "next");
            Manager.Current.ActiveBrowser.Actions.InvokeScript(scriptToBeExecuted);
        }

        public void ClickPreviousButton()
        {
            string scriptToBeExecuted = 
                string.Format(fullCalendarMethodJqueryExpression, this.idLocator, "prev");
            Manager.Current.ActiveBrowser.Actions.InvokeScript(scriptToBeExecuted);
        }

        public void GoToToday()
        {
            string scriptToBeExecuted =
                string.Format(fullCalendarMethodJqueryExpression, this.idLocator, "today");
            Manager.Current.ActiveBrowser.Actions.InvokeScript(scriptToBeExecuted);
        }

        public void GoToDate(DateTime date)
        {
            string scriptToBeExecuted = 
                string.Format("$('#{0}').fullCalendar('gotoDate', $.fullCalendar.moment('{1}-{2}-{3}'))", 
                this.idLocator, 
                date.Year, 
                date.Month - 1,
                date.Day);
            Manager.Current.ActiveBrowser.Actions.InvokeScript(scriptToBeExecuted);
        }
    }
}