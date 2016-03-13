using ArtOfTest.WebAii.Core;
using System;

namespace TestStudio.Series.Tests.CustomControls
{
    public class KendoColorPicker
    {
        private readonly string colorPickerSetColorExpression =
            "$('#{0}').data('colorpicker').value('#{1}');";
        private readonly string idLocator;

        public KendoColorPicker(string idLocator)
        {
            this.idLocator = idLocator;
        }

        public void SetColor(string hexValue)
        {
            string scriptToBeExecuted = string.Format(colorPickerSetColorExpression, this.idLocator, hexValue);
            Manager.Current.ActiveBrowser.Actions.InvokeScript(scriptToBeExecuted);
        }
    }
}