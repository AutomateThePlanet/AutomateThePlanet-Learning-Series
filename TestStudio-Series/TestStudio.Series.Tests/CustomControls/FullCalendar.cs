using ArtOfTest.WebAii.Core;
using System;

namespace TestStudio.Series.Tests.CustomControls
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