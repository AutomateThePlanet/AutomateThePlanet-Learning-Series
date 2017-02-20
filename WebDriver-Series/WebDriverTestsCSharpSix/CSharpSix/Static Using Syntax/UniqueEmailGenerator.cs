// <copyright file="UniqueEmailGenerator.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
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

namespace WebDriverTestsCSharpSix.CSharpSix.StaticUsingSyntax
{
    public class UniqueEmailGenerator
    {
        public static string BuildUniqueEmail(string prefix, string sufix)
        {
            string result = string.Concat(prefix, "_", TimestampBuilder.GenerateUniqueText(), "@", sufix, ".com");
            return result;
        }

        public static string BuildUniqueEmailTimestamp()
        {
            string result = string.Format("bot-{0}@automatetheplanet.com", TimestampBuilder.GenerateUniqueText());
            return result;
        }

        public static string BuildUniqueEmailGuid()
        {
            string result = string.Format("bot-{0}@automatetheplanet.com", Guid.NewGuid().ToString());
            return result;
        }

        public static string BuildUniqueEmail(string prefix)
        {
            string result = string.Format("{0}{1}@automatetheplanet.com", prefix, TimestampBuilder.GenerateUniqueText());
            return result;
        }

        public static string BuildUniqueEmail(char specialSymbol)
        {
            string result = string.Format("bot-{0}{1}@automatetheplanet.com", TimestampBuilder.GenerateUniqueText(), specialSymbol);
            return result;
        }
    }
}
