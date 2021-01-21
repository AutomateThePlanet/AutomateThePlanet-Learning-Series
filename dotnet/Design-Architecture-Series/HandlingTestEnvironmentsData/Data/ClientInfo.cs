// <copyright file="ClientInfo.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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

namespace HandlingTestEnvironmentsData.Data.Second
{
    public class ClientInfo
    {
        public string FirstName { get; set; } = ConfigurationService.Instance.GetBillingInfoDefaultValues().FirstName;

        public string LastName { get; set; } = ConfigurationService.Instance.GetBillingInfoDefaultValues().LastName;

        public string Country { get; set; } = ConfigurationService.Instance.GetBillingInfoDefaultValues().Country;

        public string Address1 { get; set; } = ConfigurationService.Instance.GetBillingInfoDefaultValues().Address1;

        public string City { get; set; } = ConfigurationService.Instance.GetBillingInfoDefaultValues().City;

        public string Phone { get; set; } = ConfigurationService.Instance.GetBillingInfoDefaultValues().Phone;

        public string Zip { get; set; } = ConfigurationService.Instance.GetBillingInfoDefaultValues().Zip;

        public string Email { get; set; } = ConfigurationService.Instance.GetBillingInfoDefaultValues().Email;
    }
}