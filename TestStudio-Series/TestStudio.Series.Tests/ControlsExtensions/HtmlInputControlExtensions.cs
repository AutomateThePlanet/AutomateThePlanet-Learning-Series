using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace TestStudio.Series.Tests.ControlsExtensions
{
    public static class HtmlInputControlExtensions
    {
        public static void SimulateRealTextTyping(this HtmlInputControl control, string text)
        {
            control.ScrollToVisible(ScrollToVisibleType.ElementTopAtWindowTop);
            Manager.Current.ActiveBrowser.Window.SetFocus();
            control.Focus();
            control.MouseClick();
            Manager.Current.Desktop.KeyBoard.TypeText(text, 50, 100, true);
        }
    }
}
