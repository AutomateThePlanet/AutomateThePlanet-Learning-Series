/// <remarks/>

[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TestRunUnitTestTestCategory
{
    private UnitTestTestCategoryTestCategoryItem testCategoryItemField;

    /// <remarks/>
    public UnitTestTestCategoryTestCategoryItem TestCategoryItem
    {
        get
        {
            return this.testCategoryItemField;
        }
        set
        {
            this.testCategoryItemField = value;
        }
    }
}