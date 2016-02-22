using System;

namespace CSharp.Series.Tests.PropertiesAsserter
{
    public static class DateTimeAssert
    {
        public static void AreEqual(DateTime? expectedDate, DateTime? actualDate, DateTimeDeltaType deltaType, int count)
        {
            if (expectedDate == null && actualDate == null)
            {
                return;
            }
            else if (expectedDate == null)
            {
                throw new NullReferenceException("The expected date was null");
            }
            else if (actualDate == null)
            {
                throw new NullReferenceException("The actual date was null");
            }
            TimeSpan expectedDelta = GetTimeSpanDeltaByType(deltaType, count);
            double totalSecondsDifference = Math.Abs(((DateTime)actualDate - (DateTime)expectedDate).TotalSeconds);

            if (totalSecondsDifference > expectedDelta.TotalSeconds)
            {
                throw new Exception(string.Format("Expected Date: {0}, Actual Date: {1} \nExpected Delta: {2}, Actual Delta in seconds- {3} (Delta Type: {4})",
                                                expectedDate,
                                                actualDate,
                                                expectedDelta,
                                                totalSecondsDifference,
                                                deltaType));
            }
        }

        private static TimeSpan GetTimeSpanDeltaByType(DateTimeDeltaType type, int count)
        {
            TimeSpan result = default(TimeSpan);

            switch (type)
            {
                case DateTimeDeltaType.Days:
                    result = new TimeSpan(count, 0, 0, 0);
                    break;
                case DateTimeDeltaType.Minutes:
                    result = new TimeSpan(0, count, 0);
                    break;
                default: throw new NotImplementedException("The delta type is not implemented.");
            }

            return result;
        }
    }
}
