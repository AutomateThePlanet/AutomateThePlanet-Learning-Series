using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace TopUnderutilizedFeaturesdDotNetPartTwo
{
    [TestClass]
    public class ExamplesTests
    {
        [TestMethod]
        public void DynamicExample()
        {
            // 1. dynamic example
            dynamic dynamicVariable;
            var i = 20;
            dynamicVariable = (dynamic)i;
            Console.WriteLine(dynamicVariable);
            var stringVariable = "Example string.";
            dynamicVariable = (dynamic)stringVariable;
            Console.WriteLine(dynamicVariable);
            var dateTimeVariable = DateTime.Today;
            dynamicVariable = (dynamic)dateTimeVariable;
            Console.WriteLine(dynamicVariable);
            // The expression returns true unless dynamicVariable has the value null. 
            if (dynamicVariable is dynamic)
            {
                Console.WriteLine("d variable is dynamic");
            }
            // dynamic and the as operator.
            dynamicVariable = i as dynamic;
            // throw RuntimeBinderException if the associated object doesn't have the specified method.
            // The code is still compiling successfully.
            Console.WriteLine(dynamicVariable.ToNow1);
        }

        [TestMethod]
        public void ExpandoObjectExample()
        {
            // 2. ExpandoObject example
            dynamic samplefootballLegendObject = new ExpandoObject();
            samplefootballLegendObject.FirstName = "Joro";
            samplefootballLegendObject.LastName = "Beckham-a";
            samplefootballLegendObject.Team = "Loko Mezdra";
            samplefootballLegendObject.Salary = 380.5m;
            samplefootballLegendObject.AsString = new Action(
                      () =>
                      Console.WriteLine("{0} {1} {2} {3}",
                      samplefootballLegendObject.FirstName,
                      samplefootballLegendObject.LastName,
                      samplefootballLegendObject.Team,
                      samplefootballLegendObject.Salary)
                      );
             //draw funny pic in console generation code and other fynny code exaples
            samplefootballLegendObject.AsString();
        }

        [TestMethod]
        public void NullableGetValueOrDefaultMethodExample()
        {
            // 3.Nullable<T>.GetValueOrDefault Method example
            float? yourSingle = -1.0f;
            Console.WriteLine(yourSingle.GetValueOrDefault());
            yourSingle = null;
            Console.WriteLine(yourSingle.GetValueOrDefault());
            // assign different default value
            Console.WriteLine(yourSingle.GetValueOrDefault(-2.4f));
            // returns the same result as the above statement
            Console.WriteLine(yourSingle ?? -2.4f);
            // Use it to create protected dictionary get
            var names = new Dictionary<int, string>();
            names.Add(0, "Willy");
            Console.WriteLine(names.GetValueOrDefault(1));
        }

        [TestMethod]
        public void ZipFileFrameworkFourExample()
        {
            // 4.ZipFile in .NET(.NET Framework 4.6 and 4.5)
            var startPath = Path.Combine(string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Start"));
            var resultPath = Path.Combine(string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Result"));
            Directory.CreateDirectory(startPath);
            Directory.CreateDirectory(resultPath);
            var zipPath = Path.Combine(string.Concat(resultPath, "\\", Guid.NewGuid().ToString(), ".zip"));
            var extractPath = Path.Combine(string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Extract"));
            Directory.CreateDirectory(extractPath);
            ZipFile.CreateFromDirectory(startPath, zipPath);
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }

        [TestMethod]
        public void PreprocessorDirectivesExample()
        {
            // 5.#warning #error #line #region #endregion #pragma #warning Examples
            // 5.1. #warning
            #if LIVE
            #warning A day without sunshine is like, you know, night.
            #endif
            // 5.2. #error
            ////#error Deprecated code in this method.
            // 5.3. #line
            #line 100 "Pandiculation"
            int i;    // CS0168 on line 101
            int j;    // CS0168 on line 102
            #line default
            char c;   // CS0168 on line 288
            float f;  // CS0168 on line 289
            // 5.4. #region
            #region Thomas Sowell Quote
            Console.WriteLine("It takes considerable knowledge just to realize the extent of your own ignorance.");
            #endregion
        }

        [TestMethod]
        public void StackAllocExample()
        {
            // 6. StackAlloc example
            ////Fibonacci();
        }

        [TestMethod]
        public void VolatileExample()
        {
            // 8.volatile example
            var firstDimension = new DimensionTester();
            var secondDimension = new Thread(firstDimension.StartPartyInAnotherDimension);
            secondDimension.Start(firstDimension);
            Thread.Sleep(5000);
            firstDimension.shouldPartyContinue = false;
            Console.WriteLine("Party Grand Finish");
        }

        [TestMethod]
        public void GlobalExample()
        {
            // 9. global::  example
            Console.WriteLine("Wine is constant proof that God loves us and loves to see us happy. -Benjamin Franklin");
        }

        [TestMethod]
        public void DebuggerDisplayAttributeAndDebuggerStepThroughAttributeExample()
        {
            // 10. DebuggerDisplayAttribute & DebuggerStepThroughAttribute example
            // 11. DebuggerDisplayAttribute example
            var buf = new DebuggerDisplayTest();
            buf.SquirrelFirstNameName = "Buffalo";
            buf.SquirrelLastNameName = "Bill";
            buf.Age = 20;
        }

        [TestMethod]
        public void ConditionalExample()
        {
            // 12. Conditional example
            StartBugsParty();
        }

        [TestMethod]
        public void UsingDirectivesVS2015Example()
        {
            // 13. using Directive VS 2015
            ////Console.WriteLine(Sqrt(42563));
            ////IntList intList = new IntList();
            ////intList.Add(1);
            ////intList.Add(2);
            ////intList.Add(3);
            ////intList.ForEach(x => WriteLine(x));
        }

        [TestMethod]
        public void FlagEnumAttributeExample()
        {
            // 14. Flag Enum Attribute example
            var minionsNames = (Minions.Bob | Minions.Dave).ToString();
            // Displays 'Bob, Dave'
            Console.WriteLine(minionsNames);
            var allowedMinionsToParticipate = Minions.Dave | Minions.Kevin | Minions.Stuart;
            // To retrieve the distinct values in you property one can do this
            Console.WriteLine(allowedMinionsToParticipate);
            if ((allowedMinionsToParticipate & Minions.Dave) == Minions.Dave)
            {
                Console.WriteLine("Dave is allowed to be a party animal!");
            }
            // In .NET 4 and later
            if (allowedMinionsToParticipate.HasFlag(Minions.Bob))
            {
                Console.WriteLine("Bob is allowed to be a party animal!");
            }
            else
            {
                Console.WriteLine("No more tequila for Bob. :(");
            }
        }

        [TestMethod]
        public void CodeDOMExample()
        {
            // 15. Dynamically Compile and Execute C# Code Examples
            // 15.1 CodeDOM
            var sourceCode = @"class HelloKittyPrinter
                                {
                                    public void Print()
                                    {
                                        System.Console.WriteLine(""Hello Hello Kitty!"");
                                    }
                                }";
            var compiledAssembly = CompileSourceCodeDom(sourceCode);
            ExecuteFromAssembly(compiledAssembly);
          
        }

        [TestMethod]
        public void RoslynExample()
        {
            // 15.2 Roslyn
            ////var compiledAssembly = CompileSourceRoslyn(sourceCode);
            ////ExecuteFromAssembly(compiledAssembly);
        }

        ////private static Assembly CompileSourceRoslyn(string sourceCode)
        ////{
        ////    using (var memoryStream = new MemoryStream())
        ////    {
        ////        string assemblyFileName = string.Concat(Guid.NewGuid().ToString(), ".dll");

        ////        CSharpCompilation compilation = CSharpCompilation.Create(assemblyFileName,
        ////            new[] { CSharpSyntaxTree.ParseText(sourceCode) },
        ////            new[]
        ////            {
        ////                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        ////            },
        ////            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        ////        compilation.Emit(memoryStream);
        ////        Assembly assembly = Assembly.Load(memoryStream.GetBuffer());
        ////        return assembly;
        ////    }
        ////}

        private static Assembly CompileSourceCodeDom(string sourceCode)
        {
            CodeDomProvider csharpCodeProvider = new CSharpCodeProvider();
            var cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.GenerateExecutable = false;
            var cr = csharpCodeProvider.CompileAssemblyFromSource(cp, sourceCode);

            return cr.CompiledAssembly;
        }

        private static void ExecuteFromAssembly(Assembly assembly)
        {
            var helloKittyPrinterType = assembly.GetType("HelloKittyPrinter");
            var printMethod = helloKittyPrinterType.GetMethod("Print");
            var kitty = assembly.CreateInstance("HelloKittyPrinter");
            printMethod.Invoke(kitty, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
        }

        [Conditional("LIVE")]
        public static void StartBugsParty()
        {
            Console.WriteLine("Let the bugs free. Start the Party.");
        }

        ////static unsafe void Fibonacci()
        ////{
        ////    const int arraySize = 20;
        ////    int* fib = stackalloc int[arraySize];
        ////    int* p = fib;
        ////    *p++ = *p++ = 1;
        ////    for (int i = 2; i < arraySize; ++i, ++p)
        ////    {
        ////        *p = p[-1] + p[-2];
        ////    }
        ////    for (int i = 0; i < arraySize; ++i)
        ////    {
        ////        Console.WriteLine(fib[i]);
        ////    }
        ////}

        // 7. Execute VB code via C#
        ////private static string GetMacro(int macroId, int row, int endCol)
        ////{
        ////    StringBuilder sb = new StringBuilder();
        ////    string range = "ActiveSheet.Range(Cells(" + row + "," + 3 +
        ////        "), Cells(" + row + "," + (endCol + 3) + ")).Select";
        ////    sb.AppendLine("Sub Macro" + macroId + "()");
        ////    sb.AppendLine("On Error Resume Next");
        ////    sb.AppendLine(range);
        ////    sb.AppendLine("ActiveSheet.Shapes.AddChart.Select");
        ////    sb.AppendLine("ActiveChart.ChartType = xlLine");
        ////    sb.AppendLine("ActiveChart.SetSourceData Source:=" + range);
        ////    sb.AppendLine("On Error GoTo 0");
        ////    sb.AppendLine("End Sub");

        ////    return sb.ToString();
        ////}

        ////private static void AddChartButton(MSExcel.Workbook workBook, MSExcel.Worksheet xlWorkSheetNew,
        ////    MSExcel.Range currentRange, int macroId, int currentRow, int endCol, string buttonImagePath)
        ////{
        ////    MSExcel.Range cell = currentRange.Next;
        ////    var width = cell.Width;
        ////    var height = 15;
        ////    var left = cell.Left;
        ////    var top = Math.Max(cell.Top + cell.Height - height, 0);
        ////    MSExcel.Shape button = xlWorkSheetNew.Shapes.AddPicture(@buttonImagePath, MsoTriState.msoFalse,
        ////        MsoTriState.msoCTrue, left, top, width, height);

        ////    VBIDE.VBComponent module = workBook.VBProject.VBComponents.Add(VBIDE.vbext_ComponentType.vbext_ct_StdModule);
        ////    module.CodeModule.AddFromString(GetMacro(macroId, currentRow, endCol));
        ////    button.OnAction = "Macro" + macroId;

        ////}
    }
}
