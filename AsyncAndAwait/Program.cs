using static System.Console;
using System.Diagnostics;
using FooClasses;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
/// https://sharplab.io/
/// linqpad
/// </summary>

// Main function
var sw = Stopwatch.StartNew();
var WithoutAsyncClass = new WithoutAsync();
WithoutAsyncClass.ClassMain();
sw.Stop();
WriteLine($"\n-*-*-*-*-* WithoutAsync.ClassMain *-*-*-*-*-\n" + $"Ellapsed time: {sw.ElapsedMilliseconds}\n");

sw.Restart();
var WithAsyncClass = new WithAsync_VeryBad1();
await WithAsyncClass.ClassMain();
WriteLine("\n-*-*-*-*-* WithAsync1.ClassMain *-*-*-*-*-\n" +
    $"Ellapsed time: {sw.ElapsedMilliseconds}\n");

sw.Restart();
await WithAsyncClass.ClassMain_LittleOptimized1();
WriteLine("\n-*-*-*-*-* WithAsync1.ClassMain_LittleOptimized *-*-*-*-*-\n" + $"Ellapsed time: {sw.ElapsedMilliseconds}\n");

sw.Restart();
await WithAsyncClass.ClassMain_LittleOptimized2();
WriteLine("\n-*-*-*-*-* WithAsync1.ClassMain_LittleOptimized2 *-*-*-*-*-\n" + $"Ellapsed time: {sw.ElapsedMilliseconds}\n");

sw.Restart();
await WithAsyncClass.ClassMain_Efficient();
WriteLine("\n-*-*-*-*-* WithAsync1.ClassMain_Efficient *-*-*-*-*-\n" + $"Ellapsed time: {sw.ElapsedMilliseconds}\n");

// Stop the console.
ReadKey();
