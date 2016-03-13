using ArtOfTest.WebAii.Core;

namespace TestStudio.Series.Tests.ControlsExtensions
{
    public static class HtmlSelectExtensions
    {
        private static readonly string jqueryExpression =
            @"document.getElementById('{0}').value = '{1}';";

        public static void JQuerySelectByText(this string expression, string text)
        {
            string javaScriptToBeExecuted = string.Format(jqueryExpression, expression, text);
            Manager.Current.ActiveBrowser.Actions.InvokeScript(javaScriptToBeExecuted);
        }
    }
}
