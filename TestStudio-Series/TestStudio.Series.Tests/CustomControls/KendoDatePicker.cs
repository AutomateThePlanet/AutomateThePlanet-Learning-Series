using ArtOfTest.WebAii.Core;
using System;

namespace TestStudio.Series.Tests.CustomControls
{
    public class KendoDatePicker
    {
        private readonly string datePickerSetValueJqueryExpression =
            "$('#{0}').kendoDatePicker({{ value: new Date({1}, {2}, {3}) }});";
        private readonly string idLocator;

        public KendoDatePicker(string idLocator)
        {
            this.idLocator = idLocator;
        }

        public void SetDate(DateTime dateTime)
        {
            string scriptToBeExecuted = string.Format(datePickerSetValueJqueryExpression, this.idLocator, dateTime.Year, dateTime.Month - 1, dateTime.Day);
            Manager.Current.ActiveBrowser.Actions.InvokeScript(scriptToBeExecuted);
        }
    }
}