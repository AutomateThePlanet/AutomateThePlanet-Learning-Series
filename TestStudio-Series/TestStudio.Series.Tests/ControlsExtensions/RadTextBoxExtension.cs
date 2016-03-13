using ArtOfTest.WebAii.Core;

namespace TestStudio.Series.Tests.ControlsExtensions
{
    public static class RadTextBoxExtension
    {
        private static readonly string radTextBoxSetValueJqueryExpression = 
            "$find($(\"input[id$='{0}']\").id()).set_value('{1}')";

        public static void JQueryType(this string expression, string text)
        {
            Manager.Current.ActiveBrowser.Actions.InvokeScript(string.Format(radTextBoxSetValueJqueryExpression, expression, text));
        }
    }
}