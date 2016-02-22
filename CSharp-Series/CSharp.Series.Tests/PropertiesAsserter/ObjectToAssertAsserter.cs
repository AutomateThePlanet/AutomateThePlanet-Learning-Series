namespace CSharp.Series.Tests.PropertiesAsserter
{
    public class ObjectToAssertAsserter : PropertiesAsserter<ObjectToAssertAsserter, ObjectToAssert>
    {
        public void Assert(ObjectToAssert expected, ObjectToAssert actual)
        {
            this.Assert(expected,
                        actual,
                        e => e.LastName,
                        e => e.FirstName, 
                        e => e.PoNumber);
        }
    }
}