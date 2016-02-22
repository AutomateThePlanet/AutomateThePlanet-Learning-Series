using CSharp.Series.Tests.Utils;

namespace CSharp.Series.Tests.GetValueOrDefaultVsNullCoalescingOperator
{
    public static class GetValueOrDefaultVsNullCoalescingOperatorTest
    {
        public static void ExecuteWithGetValueOrDefault()
        {
            int? a = null;
            int? b = 3;
            int? d = null;
            int? f = null;
            int? g = null;
            int? h = null;
            int? j = null;
            int? k = 7;

            var profileResult = Profiler.Profile(int.MaxValue,
               () =>
               {
                   var x = a.GetValueOrDefault(7);
                   var y = b.GetValueOrDefault(7);
                   var z = d.GetValueOrDefault(6) + f.GetValueOrDefault(3) + g.GetValueOrDefault(1) + h.GetValueOrDefault(1) + j.GetValueOrDefault(5) + k.GetValueOrDefault(8);
               });
            string formattedProfileResult = Profiler.FormatProfileResults(int.MaxValue, profileResult);
            FileWriter.WriteToDesktop("ExecuteWithGetValueOrDefaultT", formattedProfileResult);
        }

        public static void ExecuteWithNullCoalescingOperator()
        {
            int? a = null;
            int? b = 3;
            int? d = null;
            int? f = null;
            int? g = null;
            int? h = null;
            int? j = null;
            int? k = 7;

            var profileResult = Profiler.Profile(int.MaxValue,
               () =>
               {
                   var x = a ?? 7;
                   var y = b ?? 7;
                   var z = (d ?? 6) + (f ?? 3) + (g ?? 1) + (h ?? 1) + (j ?? 5) + (k ?? 8);
               });
            string formattedProfileResult = Profiler.FormatProfileResults(int.MaxValue, profileResult);
            FileWriter.WriteToDesktop("ExecuteWithNullCoalescingOperatorT", formattedProfileResult);
        }

        public static void ExecuteWithConditionalOperator()
        {
            int? a = null;
            int? b = 3;
            int? d = null;
            int? f = null;
            int? g = null;
            int? h = null;
            int? j = null;
            int? k = 7;

            var profileResult = Profiler.Profile(100000,
               () =>
               {
                   var x = a.HasValue ? a : 7;
                   var y = b.HasValue ? b : 7;
                   var z = (d.HasValue ? d : 6) + (f.HasValue ? f : 3) + (g.HasValue ? g : 1) + (h.HasValue ? h : 1) + (j.HasValue ? j : 5) + (k.HasValue ? k : 8);
               });
            string formattedProfileResult = Profiler.FormatProfileResults(100000, profileResult);
            FileWriter.WriteToDesktop("ExecuteWithConditionalOperatorT", formattedProfileResult);
        }

        public static void ExecuteWithGetValueOrDefaultZero()
        {
            int? a = null;

            var profileResult = Profiler.Profile(100000,
               () =>
               {
                   var x = a.GetValueOrDefault();
               });
            string formattedProfileResult = Profiler.FormatProfileResults(100000, profileResult);
            FileWriter.WriteToDesktop("ExecuteWithGetValueOrDefaultZeroT", formattedProfileResult);
        }

        public static void ExecuteWithNullCoalescingOperatorZero()
        {
            int? a = null;

            var profileResult = Profiler.Profile(100000,
               () =>
               {
                   var x = a ?? 0;
               });
            string formattedProfileResult = Profiler.FormatProfileResults(100000, profileResult);
            FileWriter.WriteToDesktop("ExecuteWithNullCoalescingOperatorZeroT", formattedProfileResult);
        }

        public static void ExecuteWithConditionalOperatorZero()
        {
            int? a = null;

            var profileResult = Profiler.Profile(100000,
               () =>
               {
                   var x = a.HasValue ? a : 0;
               });
            string formattedProfileResult = Profiler.FormatProfileResults(100000, profileResult);
            FileWriter.WriteToDesktop("ExecuteWithConditionalOperatorZeroT", formattedProfileResult);
        }
    }
}
