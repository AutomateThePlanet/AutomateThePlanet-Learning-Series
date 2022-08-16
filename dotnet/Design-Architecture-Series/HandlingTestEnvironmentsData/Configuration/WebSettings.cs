// <copyright file="WebSettings.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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

namespace HandlingTestEnvironmentsData;

public sealed class WebSettings
{
    public string BaseUrl { get; set; }
    public BrowserSettings Chrome { get; set; }
    public BrowserSettings Firefox { get; set; }
    public BrowserSettings Edge { get; set; }
    public BrowserSettings Opera { get; set; }
    public BrowserSettings InternetExplorer { get; set; }
    public BrowserSettings Safari { get; set; }
    public int ElementWaitTimeout { get; set; }
}
