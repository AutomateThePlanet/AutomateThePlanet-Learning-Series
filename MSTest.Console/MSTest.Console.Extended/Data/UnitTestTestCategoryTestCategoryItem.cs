/// <remarks/>

[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class UnitTestTestCategoryTestCategoryItem
{
    private string testCategoryField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string TestCategory
    {
        get
        {
            return this.testCategoryField;
        }
        set
        {
            this.testCategoryField = value;
        }
    }
}