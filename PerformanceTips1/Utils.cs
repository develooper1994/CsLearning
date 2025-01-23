using System;
using static System.Console;
using System.Diagnostics;

namespace PerformanceTips1
{
    public static class Utils
    {
        public static double Measure<T>(Func<T> ac, uint howmanytimes=1)
        {
            ///<summary>
            /// Measures the performance
            /// This measurement method came from
            /// https://youtu.be/T9DH3SkiHB8?t=1169
            ///
            /// There is benchmarkDotnet but i don't know how to use it for now.
            /// </summary>

            WriteLine("-*-*-*-*-* Quick Wormup *-*-*-*-*-");
            for (int i = 0; i < 2; i++)
            {
                ac();
            }

            WriteLine("-*-*-*-*-* Benchmark Started *-*-*-*-*-");

            long avg = 0;
            for (int i = 0; i < howmanytimes; i++)
            {
                var sw = new Stopwatch();
                sw.Start();
                {
                    _ = ac();
                }
                sw.Stop();
                avg += sw.ElapsedMilliseconds;
                WriteLine($"--->>> {i}) elapsed time: {sw.ElapsedMilliseconds}");
            }
            WriteLine("-*-*-*-*-* Benchmark Finished *-*-*-*-*-");

            avg /= howmanytimes;
            WriteLine($"--->>> elapsed average time: {avg}");

            return avg;
        }
    }
}
