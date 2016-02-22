/// <remarks/>

[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TestRunUnitTestOwners
{
    private UnitTestOwnersOwner ownerField;

    /// <remarks/>
    public UnitTestOwnersOwner Owner
    {
        get
        {
            return this.ownerField;
        }
        set
        {
            this.ownerField = value;
        }
    }
}