// <copyright file="DateTimeAssert.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using System;

namespace GenericPropertiesValidator
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
            var expectedDelta = GetTimeSpanDeltaByType(deltaType, count);
            var totalSecondsDifference = Math.Abs(((DateTime)actualDate - (DateTime)expectedDate).TotalSeconds);

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
            var result = default(TimeSpan);

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
