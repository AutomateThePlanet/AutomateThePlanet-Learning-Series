#define LIVE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.IO.Compression;
using System.IO;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.Globalization;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CSharp.Series.Tests.GetValueOrDefaultVsNullCoalescingOperator;
using CSharp.Series.Tests.PropertiesAsLambdas;
using CSharp.Series.Tests.Asserters;

////using static System.Math;
////using static System.Console;
////using IntList = System.Collections.Generic.List<int>;

namespace CSharp.Series.Tests
{
    public class Program
    {
        ////bool shouldPartyContinue = true;
        // Use it to stop the party
        //volatile bool shouldPartyContinue = true;
        public static unsafe void Main(string[] args)
        {
            AntonMachineEventLogAsserter eventLogAsserter = new AntonMachineEventLogAsserter();
            eventLogAsserter.AssertMessageExistsInTop("The VSS service is shutting down due to idle timeout.", "VSS", 22);
            ////Client client = new Client();
            ////var propertyNames = client.GetMemberNames(c => c.FistName, c => c.LastName, c => c.City);
            ////foreach (var cPropertyName in propertyNames)
            ////{
            ////    Console.WriteLine(cPropertyName);
            ////}
            ////string nameOfTheMethod = client.GetMemberName(c => c.ToString());
            ////Console.WriteLine(nameOfTheMethod);

            ////for (int i = 0; i < 10; i++)
            ////{
            ////    GetValueOrDefaultVsNullCoalescingOperatorTest.ExecuteWithGetValueOrDefault();
            ////    GetValueOrDefaultVsNullCoalescingOperatorTest.ExecuteWithNullCoalescingOperator();
            ////}
            // 1.1. Curry Invoke Function Example
            ////Func<int, int, int, int> addNumbers = (x, y, z) => x + y + z;
            ////var f1 = addNumbers.Curry();
            ////Func<int, Func<int, int>> f2 = f1(3);
            ////Func<int, int> f3 = f2(4);
            ////Console.WriteLine(f3(5));
            // 1.2. Partial Invoke Function Example
            ////Func<int, int, int, int> sumNumbers = (x, y, z) => x + y + z;
            ////Func<int, int> f4 = sumNumbers.Partial(3, 4);
            ////Console.WriteLine(f4(5));
            // 2. Obsolete Attribute Test
            ////Console.WriteLine(ObsoleteExample.OrderDetailTotal);
            ////Console.WriteLine();
            ////Console.WriteLine(ObsoleteExample.CalculateOrderDetailTotal());
            // 3. Default Value Attribute Example
            ////DefaultValueAttributeTest defaultValueAtt = new DefaultValueAttributeTest();
            ////Console.WriteLine(defaultValueAtt);
            // 4. DebuggerBrowsable attribute example
            ////DebuggerBrowsableTest.SquirrelFirstNameName = "Hammy";
            ////DebuggerBrowsableTest.SquirrelLastNameName = "Ammy";
            // 5. Default Value Operator ?? example
            ////int? x = null;
            ////int y = x ?? -1;
            ////Console.WriteLine("y now equals -1 because x was null => {0}", y);
            ////int i = DefaultValueOperatorExample.GetNullableInt() ?? default(int);
            ////Console.WriteLine("i equals now 0 because GetNullableInt() returned null => {0}", i);
            ////string s = DefaultValueOperatorExample.GetStringValue();
            ////Console.WriteLine("Returns 'Unspecified' because s is null => {0}", s ?? "Unspecified");
            // 6. WeakReference example
            ////WeakReferenceTest hugeObject = new WeakReferenceTest();
            ////hugeObject.SharkFirstName = "Sharky";
            ////WeakReference w = new WeakReference(hugeObject);
            ////hugeObject = null;
            ////GC.Collect();
            ////Console.WriteLine((w.Target as WeakReferenceTest).SharkFirstName);
            // 8. BigInteger example
            ////string positiveString = "91389681247993671255432112000000";
            ////string negativeString = "-90315837410896312071002088037140000";
            ////BigInteger posBigInt = 0;
            ////BigInteger negBigInt = 0;
            ////posBigInt = BigInteger.Parse(positiveString);
            ////Console.WriteLine(posBigInt);
            ////negBigInt = BigInteger.Parse(negativeString);
            ////Console.WriteLine(negBigInt);
            // 9. Undocumented C# Types and Keywords __arglist __reftype __makeref __refvalue example
            ////int i = 21;
            ////TypedReference tr = __makeref(i);
            ////Type t = __reftype(tr);
            ////Console.WriteLine(t.ToString());
            ////int rv = __refvalue( tr,int);
            ////Console.WriteLine(rv);
            ////ArglistTest.DisplayNumbersOnConsole(__arglist(1, 2, 3, 5, 6));
            // 10. Environment.NewLine example
            ////Console.WriteLine("NewLine: {0}  first line{0}  second line{0}  third line", Environment.NewLine);
            // 11. ExceptionDispatcher example
            ////ExceptionDispatchInfo possibleException = null;
            ////try
            ////{
            ////    int.Parse("a");
            ////}
            ////catch (FormatException ex)
            ////{
            ////    possibleException = ExceptionDispatchInfo.Capture(ex);
            ////}
            ////if (possibleException != null)
            ////{
            ////    possibleException.Throw();
            ////}
            // 12. Environment.FailFast() example
            ////string s = Console.ReadLine(); 
            ////try 
            ////{ 
            ////    int i = int.Parse(s); 
            ////    if (i == 42) Environment.FailFast("Special number entered"); 
            ////} 
            ////finally 
            ////{ 
            ////    Console.WriteLine("Program complete."); 
            ////} 
            // 13. Debug.Assert and Debug.WriteIf example
            ////Debug.Assert(1 == 0, "The numbers are not equal! Oh my god!");
            ////Debug.WriteLineIf(1 == 1, "This message is going to be displayed in the Debug output! =)");
            ////Debug.WriteLine("What are ingredients to bake a cake?");
            ////Debug.Indent();
            ////Debug.WriteLine("1. 1 cup (2 sticks) butter, at room temperature.");
            ////Debug.WriteLine("2 cups sugar");
            ////Debug.WriteLine("3 cups sifted self-rising flour");
            ////Debug.WriteLine("4 eggs");
            ////Debug.WriteLine("1 cup milk");
            ////Debug.WriteLine("1 teaspoon pure vanilla extract");
            ////Debug.Unindent();
            ////Debug.WriteLine("End of list");
            // 14.1. Parallel For example
            ////int[] nums = Enumerable.Range(0, 1000000).ToArray();
            ////long total = 0;
            ////// Use type parameter to make subtotal a long, not an int
            ////Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            ////{
            ////    subtotal += nums[j];
            ////    return subtotal;
            ////},
            ////    (x) => Interlocked.Add(ref total, x)
            ////);
            ////Console.WriteLine("The total is {0:N0}", total);
            // 14.2 Parallel Foeach example
            ////int[] nums = Enumerable.Range(0, 1000000).ToArray();
            ////long total = 0;
            ////Parallel.ForEach<int, long>(nums, // source collection
            ////                            () => 0, // method to initialize the local variable
            ////    (j, loop, subtotal) => // method invoked by the loop on each iteration
            ////    {
            ////        subtotal += j; //modify local variable 
            ////        return subtotal; // value to be passed to next iteration
            ////    },
            ////    // Method to be executed when each partition has completed. 
            ////    // finalResult is the final value of subtotal for a particular partition.
            ////(finalResult) => Interlocked.Add(ref total, finalResult));
            ////Console.WriteLine("The total from Parallel.ForEach is {0:N0}", total);
            // 15. IsInfinity example
            ////Console.WriteLine("IsInfinity(3.0 / 0) == {0}.", Double.IsInfinity(3.0 / 0) ? "true" : "false");
            // ################### Top Hidden Features C# Part 2 ###################
            // 1. dynamic example
            ////dynamic dynamicVariable;
            ////int i = 20;
            ////dynamicVariable = (dynamic)i;
            ////Console.WriteLine(dynamicVariable);
            ////string stringVariable = "Example string.";
            ////dynamicVariable = (dynamic)stringVariable;
            ////Console.WriteLine(dynamicVariable);
            ////DateTime dateTimeVariable = DateTime.Today;
            ////dynamicVariable = (dynamic)dateTimeVariable;
            ////Console.WriteLine(dynamicVariable);
            ////// The expression returns true unless dynamicVariable has the value null. 
            ////if (dynamicVariable is dynamic)
            ////{
            ////    Console.WriteLine("d variable is dynamic");
            ////}
            ////// dynamic and the as operator.
            ////dynamicVariable = i as dynamic;
            ////// throw RuntimeBinderException if the associated object doesn't have the specified method.
            ////// The code is still compiling successfully.
            ////Console.WriteLine(dynamicVariable.ToNow1);
            // 2. ExpandoObject example
            ////dynamic samplefootballLegendObject = new ExpandoObject();
            ////samplefootballLegendObject.FirstName = "Joro";
            ////samplefootballLegendObject.LastName = "Beckham-a";
            ////samplefootballLegendObject.Team = "Loko Mezdra";
            ////samplefootballLegendObject.Salary = 380.5m;
            ////samplefootballLegendObject.AsString = new Action(
            ////          () =>
            ////          Console.WriteLine("{0} {1} {2} {3}",
            ////          samplefootballLegendObject.FirstName,
            ////          samplefootballLegendObject.LastName,
            ////          samplefootballLegendObject.Team,
            ////          samplefootballLegendObject.Salary)
            ////          );
            //// draw funny pic in console generation code and other fynny code exaples
            //samplefootballLegendObject.AsString();
            // 3.Nullable<T>.GetValueOrDefault Method example
            ////float? yourSingle = -1.0f;
            ////Console.WriteLine(yourSingle.GetValueOrDefault());
            ////yourSingle = null;
            ////Console.WriteLine(yourSingle.GetValueOrDefault());
            ////// assign different default value
            ////Console.WriteLine(yourSingle.GetValueOrDefault(-2.4f));
            ////// returns the same result as the above statement
            ////Console.WriteLine(yourSingle ?? -2.4f);
            ////// Use it to create protected dictionary get
            ////Dictionary<int, string> names = new Dictionary<int, string>();
            ////names.Add(0, "Willy");
            ////Console.WriteLine(names.GetValueOrDefault(1));
            // 4.ZipFile in .NET(.NET Framework 4.6 and 4.5)
            ////string startPath = Path.Combine(string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Start"));
            ////string resultPath = Path.Combine(string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Result"));
            ////Directory.CreateDirectory(startPath);
            ////Directory.CreateDirectory(resultPath);
            ////string zipPath = Path.Combine(string.Concat(resultPath, "\\", Guid.NewGuid().ToString(),".zip"));
            ////string extractPath = Path.Combine(string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Extract"));
            ////Directory.CreateDirectory(extractPath);
            ////ZipFile.CreateFromDirectory(startPath, zipPath);
            ////ZipFile.ExtractToDirectory(zipPath, extractPath);
            // 5.#warning #error #line #region #endregion #pragma #warning Examples
            // 5.1. #warning
            ////#if LIVE
            ////#warning A day without sunshine is like, you know, night.
            ////#endif
            // 5.2. #error
            ////#error Deprecated code in this method.
            // 5.3. #line
            //#line 100 "Pandiculation"
            //            int i;    // CS0168 on line 101
            //            int j;    // CS0168 on line 102
            //#line default
            //            char c;   // CS0168 on line 288
            //            float f;  // CS0168 on line 289
            //            // 5.4. #region
            //            #region Thomas Sowell Quote
            //            Console.WriteLine("It takes considerable knowledge just to realize the extent of your own ignorance.");
            //            #endregion
            // 6. StackAlloc example
            //Fibonacci();
            // 8.volatile example
            ////Program firstDimension = new Program();
            ////Thread secondDimension = new Thread(firstDimension.StartPartyInAnotherDimension);
            ////secondDimension.Start(firstDimension);
            ////Thread.Sleep(5000);
            ////firstDimension.shouldPartyContinue = false;
            ////Console.WriteLine("Party Grand Finish");
            // 9. global::  example
            //global::System.Console.WriteLine("Wine is constant proof that God loves us and loves to see us happy. -Benjamin Franklin");
            // 10. DebuggerDisplayAttribute & DebuggerStepThroughAttribute example
            // 11. DebuggerDisplayAttribute example
            ////DebuggerDisplayTest buf = new DebuggerDisplayTest();
            ////buf.SquirrelFirstNameName = "Buffalo";
            ////buf.SquirrelLastNameName = "Bill";
            ////buf.Age = 20;
            // 12. Conditional example
            ////StartBugsParty();
            // 13. using Directive VS 2015
            ////Console.WriteLine(Sqrt(42563));
            ////IntList intList = new IntList();
            ////intList.Add(1);
            ////intList.Add(2);
            ////intList.Add(3);
            ////intList.ForEach(x => WriteLine(x));
            // 14. Flag Enum Attribute example
            ////var minionsNames = (Minions.Bob | Minions.Dave).ToString();
            ////// Displays 'Bob, Dave'
            ////Console.WriteLine(minionsNames);
            ////var allowedMinionsToParticipate = Minions.Dave | Minions.Kevin | Minions.Stuart;
            ////// To retrieve the distinct values in you property one can do this
            ////Console.WriteLine(allowedMinionsToParticipate);
            ////if ((allowedMinionsToParticipate & Minions.Dave) == Minions.Dave)
            ////{
            ////    Console.WriteLine("Dave is allowed to be a party animal!");
            ////}
            ////// In .NET 4 and later
            ////if (allowedMinionsToParticipate.HasFlag(Minions.Bob))
            ////{
            ////    Console.WriteLine("Bob is allowed to be a party animal!");
            ////}
            ////else
            ////{
            ////    Console.WriteLine("No more tequila for Bob. :(");
            ////}
            // 15. Dynamically Compile and Execute C# Code Examples
            // 15.1 CodeDOM
            ////var sourceCode = @"class HelloKittyPrinter
            ////                    {
            ////                        public void Print()
            ////                        {
            ////                            System.Console.WriteLine(""Hello Hello Kitty!"");
            ////                        }
            ////                    }";
            ////var compiledAssembly = CompileSourceCodeDom(sourceCode);
            ////ExecuteFromAssembly(compiledAssembly);
            // 15.2 Roslyn
            ////var compiledAssembly = CompileSourceRoslyn(sourceCode);
            ////ExecuteFromAssembly(compiledAssembly);
            ////Console.WriteLine(1220.5.ToStringUsDigitsFormatting(DigitsFormattingSettings.PrefixDollar));
            ////// Result- $1,220.50
            ////Console.WriteLine(1220.5.ToStringUsDigitsFormatting(DigitsFormattingSettings.SufixDollar | DigitsFormattingSettings.NoComma));
            ////// Result- 1220.50$
            ////Console.WriteLine(1220.53645.ToStringUsDigitsFormatting(DigitsFormattingSettings.SufixDollar | DigitsFormattingSettings.NoComma | DigitsFormattingSettings.PrefixMinus, 4));
            // Result- -1220.5365$
        }

        private static Assembly CompileSourceRoslyn(string sourceCode)
        {
            using (var memoryStream = new MemoryStream())
            {
                string assemblyFileName = string.Concat(Guid.NewGuid().ToString(), ".dll");

                CSharpCompilation compilation = CSharpCompilation.Create(assemblyFileName,
                    new[] { CSharpSyntaxTree.ParseText(sourceCode) },
                    new[]
                    {
                        MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
                    },
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                compilation.Emit(memoryStream);
                Assembly assembly = Assembly.Load(memoryStream.GetBuffer());
                return assembly;
            }
        }

        private static Assembly CompileSourceCodeDom(string sourceCode)
        {
            CodeDomProvider csharpCodeProvider = new CSharpCodeProvider();
            var cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.GenerateExecutable = false;
            CompilerResults cr = csharpCodeProvider.CompileAssemblyFromSource(cp, sourceCode);

            return cr.CompiledAssembly;
        }

        private static void ExecuteFromAssembly(Assembly assembly)
        {
            Type helloKittyPrinterType = assembly.GetType("HelloKittyPrinter");
            MethodInfo printMethod = helloKittyPrinterType.GetMethod("Print");
            object kitty = assembly.CreateInstance("HelloKittyPrinter");
            printMethod.Invoke(kitty, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
        }

        ////public class System { }

        ////// Define a constant called 'Console' to cause more problems.
        ////const int Console = 7;
        ////const int number = 66;

        static unsafe void Fibonacci()
        {
            const int arraySize = 20;
            int* fib = stackalloc int[arraySize];
            int* p = fib;
            *p++ = *p++ = 1;
            for (int i = 2; i < arraySize; ++i, ++p)
            {
                *p = p[-1] + p[-2];
            }
            for (int i = 0; i < arraySize; ++i)
            {
                Console.WriteLine(fib[i]);
            }
        }
        ////private void StartPartyInAnotherDimension(object input)
        ////{
        ////    Program currentDimensionInput = (Program)input;
        ////    Console.WriteLine("let the party begin");
        ////    while (currentDimensionInput.shouldPartyContinue)
        ////    {
        ////    }
        ////    Console.WriteLine("Party ends :(");
        ////}
        ////[Conditional("LIVE")]
        ////public static void StartBugsParty()
        ////{
        ////    Console.WriteLine("Let the bugs free. Start the Party.");
        ////}
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
        ////   MSExcel.Range currentRange, int macroId, int currentRow, int endCol, string buttonImagePath)
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