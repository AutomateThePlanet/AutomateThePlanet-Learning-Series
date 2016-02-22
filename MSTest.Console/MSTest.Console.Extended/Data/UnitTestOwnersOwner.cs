/// <remarks/>

[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class UnitTestOwnersOwner
{
    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}