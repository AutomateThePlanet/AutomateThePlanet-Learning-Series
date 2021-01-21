// <copyright file="AdbCommand.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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
// <site>https://automatetheplanet.com/</site>
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AdbCommandsLibraryAppium
{
    public class AdbCommand
    {
        public AdbCommand(string command)
        {
            Command = command;
            Args = new List<string>();
        }

        public AdbCommand(string command, params string[] args)
        {
            Command = command;
            Args = new List<string>(args);
        }

        [JsonProperty("command")]
        public string Command { get; set; }
        [JsonProperty("args")]
        public List<string> Args { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}