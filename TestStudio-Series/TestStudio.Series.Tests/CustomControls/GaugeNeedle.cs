using ArtOfTest.WebAii.Core;
using System;

namespace TestStudio.Series.Tests.CustomControls
{
    public class GaugeNeedle
    {
        private readonly string idLocator;

        public GaugeNeedle(string idLocator)
        {
            this.idLocator = idLocator;
        }

        public void SetValue(int value)
        {
            string scriptToBeExecuted = string.Format("$('#{0}').igRadialGauge('option', 'value', '{1}');", this.idLocator, value);
            Manager.Current.ActiveBrowser.Actions.InvokeScript(scriptToBeExecuted);
        }
    }
}