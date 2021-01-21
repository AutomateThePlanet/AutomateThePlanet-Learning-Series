// <copyright file="AppiumDriverAbdExtensionMethods.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.Android;

namespace AdbCommandsLibraryAppium
{
    public static class AppiumDriverAbdExtensionMethods
    {
        public static string GetLogs(this AndroidDriver<AndroidElement> androidDriver)
        {
            return ExecuteShellAdbCommand(androidDriver, "logcat"); ;
        }

        public static void ChangeBatteryLevel(this AndroidDriver<AndroidElement> androidDriver, int level)
        {
            ExecuteShellAdbCommand(androidDriver, $"dumpsys battery set level {level}");
        }

        public static void ResetBattery(this AndroidDriver<AndroidElement> androidDriver)
        {
           ExecuteShellAdbCommand(androidDriver, "adb shell dumpsys battery reset");
        }

        public static string GetBatteryStatus(this AndroidDriver<AndroidElement> androidDriver)
        {
            return ExecuteShellAdbCommand(androidDriver, "adb shell dumpsys battery"); ;
        }

        private static string ExecuteShellAdbCommand(AndroidDriver<AndroidElement> androidDriver, string command, params string[] args)
        {
            return androidDriver.ExecuteScript("mobile: shell", new AdbCommand(command, args).ToString()).ToString();
        }
    }
}
