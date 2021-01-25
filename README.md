![](https://github.com/angelovstanton/AutomateThePlanet/blob/master/images/atp-logo.png)

Welcome to the source code repository of Automate The Planet Learning Series!

**Please STAR the repository.**

![](https://github.com/angelovstanton/AutomateThePlanet/blob/master/images/start-automatetheplanet.png)

This way, you will have a **bookmark for easier access** and you will **show appreciation to our work**! 

**Thank you** for being an **awesome supporter** of the automation testing!

# C# Series #


- [Design & Architecture Series](https://github.com/AutomateThePlanet/AutomateThePlanet-Learning-Series/tree/master/dotnet/Design-Architecture-Series "Design & Architecture Series")
- [Web Automation Series](https://github.com/AutomateThePlanet/AutomateThePlanet-Learning-Series/tree/master/dotnet/WebAutomation-Series "Web Automation Series")
- [Mobile Automation Series](https://github.com/AutomateThePlanet/AutomateThePlanet-Learning-Series/tree/master/dotnet/MobileAutomation-Series "Mobile Automation Series")
- [Desktop Automation Series](https://github.com/AutomateThePlanet/AutomateThePlanet-Learning-Series/tree/master/dotnet/DesktopAutomation-Series "Desktop Automation Series")
- [Automation Tools Series](https://github.com/AutomateThePlanet/AutomateThePlanet-Learning-Series/tree/master/dotnet/AutomationTools-Series "Automation Tools Series")
- [Development Series](https://github.com/angelovstanton/AutomateThePlanet/tree/master/CSharp-Series "Development Series")

# Java Series #

- [Design & Architecture Series](https://github.com/AutomateThePlanet/AutomateThePlanet-Learning-Series/tree/master/java/DesignPatternsInAutomatedTestingJava-Series "Design & Architecture Series")
- [Web Automation Series](https://github.com/AutomateThePlanet/AutomateThePlanet-Learning-Series/tree/master/java/WebDriverJava-Series "Web Automation Series")

Under each folder, you will find a separate solution file. Every article from the series has its folder and an info file. To run the examples from particular series you need only its folder.

![](https://github.com/angelovstanton/AutomateThePlanet/blob/master/images/series-folder-explanation.png)

Running Tests through CLI
--------------------------
 To execute your tests via command line in Continues Integration (CI), you can use the native .NET Core test runner.
1. Navigate to the folder of your test project.
2. Open the CMD there.
3. Execute the following command:

```
dotnet test
```
For applying filters and other more advanced configuration check the official documentation [https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test "dotnet test") and [https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-vstest](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-vstest "dotnet vstest").

Both MSTest and NUnit are supported.

Supported Code Editors
----------------------
The recommended code editor for writing tests is Visual Studio 2019 or higher (preferably installed on Windows).

NOTE: After the support for .NET Core/.NET 5.0 and higher, Microsoft officially does not support .NET Core development in older versions of Visual Studio 2015, 2017 and so on.

### Other Supported Editors: ###
- Visual Studio Code
- Visual Studio for Mac
- Rider: Cross-platform .NET IDE

SDKs and Frameworks Prerequisites
-------------------------------- 
[**.NET Core SDK 5**](https://www.microsoft.com/net/download/windows) or higher (usually comes with Visual Studio installation or updates)

For desktop modules you need to download [**WinAppDriver**](https://github.com/Microsoft/WinAppDriver/releases). You need to make sure it is started before running any desktop tests.

For mobile modules you need to download and install [**Appium**](http://appium.io/). You need to make sure it is started before running any mobile tests.
