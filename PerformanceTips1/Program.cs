using System;
using static System.Console;
using System.Linq;

namespace PerformanceTips1
{
    class Program
    {
        static void Main()
        {
            ///<summary>
            /// Böyle iyi ipuçları verdiğin için teşekkür ederim: https://www.youtube.com/watch?v=Tb2Fx9qku_o
            /// Thank you for giving such a good tricks: https://www.youtube.com/watch?v=Tb2Fx9qku_o
            /// LevelUp -> 5(Extreme) Performance Tips in C#
            /// </summary>
            var arr = Enumerable.Range(0, 100_000_000).ToArray();
            var Larr = Array.ConvertAll(arr, new Converter<int, long>(Convert.ToInt64));
            var ULarr = Array.ConvertAll(arr, new Converter<int, ulong>(Convert.ToUInt64));

            //long ac() => SumOdd(Larr);
            //long ac() => SumOdd_branchFree(Larr);

            //long ac() => SumOdd_BranchFree_Bit(Larr);

            //long ac() => SumOdd_BranchFree_Bit_Parallel1(Larr);  // default 2
            //long ac() => SumOdd_BranchFree_Bit_Parallel2(Larr);   // default 4
            //long ac() => SumOdd_BranchFree_Bit_MultiParallel_version0(Larr, howmanypieces: 2);
            //long ac() => SumOdd_BranchFree_Bit_MultiParallel_version0(Larr, howmanypieces: 4);
            //long ac() => SumOdd_BranchFree_Bit_MultiParallel_version0(Larr, howmanypieces: 10);

            //long ac() => SumOdd_BranchFree_Bit_Parallel_NoMul(arr);

            //long ac() => SumOdd_BranchFree_Bit_Parallel_NoBoundCheck1(Larr);
            //long ac() => SumOdd_BranchFree_Bit_Parallel_NoBoundCheck2(Larr);

            long ac() => SumOdd_BranchFree_Bit_Parallel_NoBoundCheck_BetterPorts(Larr);



            WriteLine($"result: {ac()}");
            uint howmanytimes = 5;
            Utils.Measure(ac, howmanytimes);
            ReadLine();
        }


        private static long SumOdd(long[] arr)
        {
            ///<summary>
            /// Classic, well known and EXTREMLY SLOW
            /// </summary>
            long counter = default;
            for (var i = 0; i < arr.Length; i++)
            {
                var element = arr[i];
                var control = element % 2;
                if (control != 0)
                    counter += element;
            }
            return counter;
        }
        private static long SumOdd_branchFree(long[] arr)
        {
            ///<summary>
            /// Braches removed. There is no if and switch statements.
            /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
            /// </summary>
            long counter = default;
            for (var i = 0; i < arr.Length; i++)
            {
                var element = arr[i];
                var control = element % 2;
                counter += control * element;
            }
            return counter;
        }
        private static long SumOdd_BranchFree_Bit(long[] arr)
        {
            ///<summary>
            /// Braches removed. There is no if and switch statements.
            /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
            ///
            /// JIT complier generates when it can be used but
            /// I want to get all the control of generated code.
            /// WITH BIT HACKS.
            /// BIT operations are much more lightweight operations.
            /// </summary>
            long counter = default;
            for (var i = 0; i < arr.Length; i++)
            {
                var element = arr[i];
                var control = (element & 1);
                counter += control * element;
            }
            return counter;
        }
        private static long SumOdd_BranchFree_Bit_Parallel1(long[] arr)
        {
            ///<summary>
            /// Braches removed. There is no if and switch statements.
            /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
            ///
            /// JIT complier generates when it can be used but
            /// I want to get all the control of generated code.
            /// WITH BIT HACKS.
            /// BIT operations are much more lightweight operations.
            ///
            /// If you move more than one step in the loop, you will move faster in one go.
            ///
            /// </summary>
            long counter1 = default;
            long counter2 = default;

            for (var i = 0; i < arr.Length; i+=2)
            {
                var element1 = arr[i];
                var element2 = arr[i+1];

                var control1 = (element1 & 1);
                var control2 = (element2 & 1);

                counter1 += control1 * element1;
                counter2 += control2 * element2;
            }
            return counter1 + counter2;
        }
        private static long SumOdd_BranchFree_Bit_Parallel2(long[] arr)
        {
            ///<summary>
            /// Braches removed. There is no if and switch statements.
            /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
            ///
            /// JIT complier generates when it can be used but
            /// I want to get all the control of generated code.
            /// WITH BIT HACKS.
            /// BIT operations are much more lightweight operations.
            ///
            /// If you move more than one step in the loop, you will move faster in one go.
            ///
            /// </summary>
            long counter1 = default;
            long counter2 = default;
            long counter3 = default;
            long counter4 = default;

            for (var i = 0; i < arr.Length; i += 4)
            {
                var element1 = arr[i];
                var element2 = arr[i + 1];
                var element3 = arr[i + 2];
                var element4 = arr[i + 3];

                var control1 = (element1 & 1);
                var control2 = (element2 & 1);
                var control3 = (element3 & 1);
                var control4 = (element4 & 1);

                counter1 += control1 * element1;
                counter2 += control2 * element2;
                counter3 += control3 * element3;
                counter4 += control4 * element4;
            }
            return counter1 + counter2 + counter3 + counter4;
        }
        private static long SumOdd_BranchFree_Bit_MultiParallel_version0(long[] arr, int howmanypieces = 2)
        {
            ///<summary>
            /// Braches removed. There is no if and switch statements.
            /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
            ///
            /// JIT complier generates when it can be used but
            /// I want to get all the control of generated code.
            /// WITH BIT HACKS.
            /// BIT operations are much more lightweight operations.
            ///
            /// If you move more than one step in the loop, you will move faster in one go.
            ///
            /// </summary>
            ///

            var arrLenght = arr.Length;

            var element = new long[howmanypieces];
            var control = new long[howmanypieces];
            var counter = new long[howmanypieces];

            // initialize the variables
            //for (var i = 0; i < counter.Length; ++i)
            //{
            //    element[i] = default;
            //    control[i] = default;
            //    counter[i] = default;
            //}

            // magic happens here
            for (int pieces = 0; pieces < howmanypieces; ++pieces)
            {
                for (var i = 0; i < arrLenght; i += howmanypieces)
                {
                    element[pieces] = arr[i + pieces];
                    control[pieces] = (element[pieces] & 1);
                    counter[pieces] += control[pieces] * element[pieces];
                }
            }
            var accumulation = counter.Sum();

            return accumulation;
        }
        private static long SumOdd_BranchFree_Bit_Parallel_NoMul(int[] arr)
        {
            ///<summary>
            /// Braches removed. There is no if and switch statements.
            /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
            ///
            /// JIT complier generates when it can be used but
            /// I want to get all the control of generated code.
            /// WITH BIT HACKS.
            /// BIT operations are much more lightweight operations.(except shift operators)
            ///
            /// If you move more than one step in the loop, you will move faster in one go.
            ///
            /// </summary>
            long counter1 = default;
            long counter2 = default;

            for (var i = 0; i < arr.Length; i += 2)
            {
                int element1 = arr[i];
                int element2 = arr[i + 1];

                int control1 = (element1 & 1);
                int control2 = (element2 & 1);

                // SLOWER THAN SumOdd_BranchFree_Bit_Parallel1
                counter1 += (element1 << control1) - element1;
                counter2 += (element2 << control2) - element2;
            }
            return counter1 + counter2;
        }
        private unsafe static long SumOdd_BranchFree_Bit_Parallel_NoBoundCheck1(long[] arr)
        {
            ///<summary>
            /// Braches removed. There is no if and switch statements.
            /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
            ///
            /// JIT complier generates when it can be used but
            /// I want to get all the control of generated code.
            /// WITH BIT HACKS.
            /// BIT operations are much more lightweight operations.(except shift operators)
            ///
            /// If you move more than one step in the loop, you will move faster in one go.
            ///
            /// Complier generates array bound checking automatically. If i can get around the checking i am going to be faster.
            ///
            /// </summary>
            long counter1 = default;
            long counter2 = default;
            var howmany = 2;

            fixed (long* data = &arr[0])
            {
                var p = data;

                for (var i = 0; i < arr.Length; i += howmany)
                {
                    var element1 = p[0];
                    var element2 = p[1];

                    var control1 = (element1 & 1);
                    var control2 = (element2 & 1);

                    counter1 += control1 * element1;
                    counter2 += control2 * element2;

                    p += howmany;
                }
            }

            return counter1 + counter2;
        }
        private unsafe static long SumOdd_BranchFree_Bit_Parallel_NoBoundCheck2(long[] arr)
        {
            ///<summary>
            /// Braches removed. There is no if and switch statements.
            /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
            ///
            /// JIT complier generates when it can be used but
            /// I want to get all the control of generated code.
            /// WITH BIT HACKS.
            /// BIT operations are much more lightweight operations.(except shift operators)
            ///
            /// If you move more than one step in the loop, you will move faster in one go.
            ///
            /// Complier generates array bound checking automatically. If i can get around the checking i am going to be faster.
            ///
            /// </summary>
            long counter1 = default;
            long counter2 = default;
            long counter3 = default;
            long counter4 = default;
            var howmany = 4;

            fixed (long* data = &arr[0])
            {
                var p = data;

                for (var i = 0; i < arr.Length; i += howmany)
                {
                    var element1 = p[0];
                    var element2 = p[1];
                    var element3 = p[2];
                    var element4 = p[3];

                    var control1 = (element1 & 1);
                    var control2 = (element2 & 1);
                    var control3 = (element3 & 1);
                    var control4 = (element4 & 1);

                    counter1 += control1 * element1;
                    counter2 += control2 * element2;
                    counter3 += control3 * element3;
                    counter4 += control4 * element4;

                    p += howmany;
                }
            }

            return counter1 + counter2 + counter3 + counter4;
        }
        private unsafe static long SumOdd_BranchFree_Bit_Parallel_NoBoundCheck_BetterPorts(long[] arr)
        {
            ///<summary>
            /// Braches removed. There is no if and switch statements.
            /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
            ///
            /// JIT complier generates when it can be used but
            /// I want to get all the control of generated code.
            /// WITH BIT HACKS.
            /// BIT operations are much more lightweight operations.(except shift operators)
            ///
            /// If you move more than one step in the loop, you will move faster in one go.
            ///
            /// Complier generates array bound checking automatically. If i can get around the checking i am going to be faster.
            ///
            /// Memory vs Registers.If i define second pointer it gets faster. Because computer can access data in many ways. This is very detailed topic.
            /// </summary>
            long counter1 = default;
            long counter2 = default;
            long counter3 = default;
            long counter4 = default;
            var howmany = 4;

            fixed (long* data = &arr[0])
            {
                var p = data;
                var n = data;

                for (var i = 0; i < arr.Length; i += howmany)
                {
                    var element1 = n[0];
                    var element2 = n[1];
                    var element3 = n[2];
                    var element4 = n[3];

                    var control1 = (element1 & 1);
                    var control2 = (element2 & 1);
                    var control3 = (element3 & 1);
                    var control4 = (element4 & 1);

                    counter1 += control1 * p[0];
                    counter2 += control2 * p[1];
                    counter3 += control3 * p[2];
                    counter4 += control4 * p[3];

                    p += howmany;
                }
            }

            return counter1 + counter2 + counter3 + counter4;
        }












        // NOT WORKING
        //private static async Task<long> SumOdd_BranchFree_Bit_MultiParallel_version1(long[] arr, int howmanypieces = 2)
        //{
        //    ///<summary>
        //    /// Braches removed. There is no if and switch statements.
        //    /// looks like logictic regression formula ;). y*f(x) + (1-y)*f(x)
        //    ///
        //    /// JIT complier generates when it can be used but
        //    /// I want to get all the control of generated code.
        //    /// WITH BIT HACKS.
        //    /// BIT operations are much more lightweight operations.
        //    ///
        //    /// If you move more than one step in the loop, you will move faster in one go.
        //    ///
        //    /// </summary>
        //    ///

        //    var arrLenght = arr.Length;
        //    var residual = arrLenght % howmanypieces;
        //    var div = arrLenght / howmanypieces;
        //    // check the residual elements.
        //    /*
        //    if (residual != 0)
        //    {
        //        for (int i = 0; i < residual; i++)
        //        {

        //        }
        //    }
        //    */

        //    var element = new long[howmanypieces];
        //    var control = new long[howmanypieces];
        //    var counter = new long[howmanypieces];

        //    // initialize the variables
        //    for (var i = 0; i < counter.Length; ++i)
        //    {
        //        element[i] = default;
        //        control[i] = default;
        //        counter[i] = default;
        //    }

        //    static async Task<long[]> summution(int pieces, int arrLenght, int howmanypieces, long[] arr, long[] element, long[] control, long[] counter)
        //    {
        //        for (var i = 0; i < arrLenght; i += howmanypieces)
        //        {
        //            element[pieces] = arr[i + pieces];
        //            control[pieces] = (element[pieces] & 1);
        //            counter[pieces] += control[pieces] * element[pieces];
        //        }
        //        return counter;
        //    }

        //    // magic happens here
        //    var pieceBlocks = Enumerable.Range(0, howmanypieces).ToArray();
        //    var summationTasks =
        //        from pieces in pieceBlocks
        //        select summution(pieces, arrLenght, howmanypieces, arr, element, control, counter);
        //    var summutionTaskList = summationTasks.ToList();

        //    while (summutionTaskList.Any())
        //    {
        //        var completedTask = await Task.WhenAny(summutionTaskList);
        //        summutionTaskList.Remove(completedTask);
        //        counter = await completedTask;
        //    }
        //    var accumulation = Convert.ToInt64(counter.Average());

        //    return accumulation;
        //}
    }
}
