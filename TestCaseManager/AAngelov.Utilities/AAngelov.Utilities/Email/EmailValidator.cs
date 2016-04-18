// <copyright file="EmailValidator.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AAngelov.Utilities.Email
{
    /// <summary>
    /// Contains Methods for Email Validation
    /// </summary>
    public class EmailValidator
    {
        /// <summary>
        /// Validates the emails.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <returns></returns>
        public static bool ValidateEmails(List<string> emails)
        {
            bool isEmailCorrect = true;
            foreach (string currentEmail in emails)
            {
                if (!Regex.IsMatch(currentEmail, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                {
                    isEmailCorrect = false;
                    break;
                }
            }

            return isEmailCorrect;
        }
    }
}