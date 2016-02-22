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