
namespace CSharp.Series.Tests.GetValueOrDefaultVsNullCoalescingOperator
{
    public class GetValueOrDefaultAndNullCoalescingOperatorInternals
    {
        public void GetValueOrDefaultInternals()
        {
            int? a = null;
            var x = a.GetValueOrDefault(7);
        }

        public void NullCoalescingOperatorInternals()
        {
            int? a = null;
            var x = a ?? 7;
        }
    }
}
