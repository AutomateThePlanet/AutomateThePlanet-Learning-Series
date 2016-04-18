// <copyright file="TestStepPropertiesConverter.cs" company="Automate The Planet Ltd.">
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
namespace TestCaseManagerCore.BusinessLogic.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Contains test step properties converter from html to plain text
    /// </summary>
    public class TestStepPropertiesConverter : IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestStepPropertiesConverter"/> class.
        /// </summary>
        public TestStepPropertiesConverter()
        {
        }

        /// <summary>
        /// Converts a test step title/expected result from html to plain text.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ParameterizedString currentTestStepProperty = value as ParameterizedString;
            string propertyPlainText = currentTestStepProperty.ToPlainText();

            return propertyPlainText;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns> A converted value. If the method returns null, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}