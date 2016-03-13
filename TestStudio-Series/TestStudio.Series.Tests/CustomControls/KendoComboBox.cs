using ArtOfTest.WebAii.Controls.HtmlControls;

namespace TestStudio.Series.Tests.CustomControls
{
    public class KendoComboBox
    {
        private readonly HtmlInputText htmlInputText;
        public KendoComboBox(HtmlInputText htmlInputText)
        {
            this.htmlInputText = htmlInputText;
        }

        public void SelectItemByText(string text)
        {
            this.htmlInputText.AsjQueryControl().InvokejQueryFunction(string.Format("val('{0}');", text));
        }
    }
}