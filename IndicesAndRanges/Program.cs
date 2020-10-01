using System;
using System.Linq;

namespace IndicesAndRanges
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-*-*-*-*-* Exercise.ExerciseMain *-*-*-*-*-");
            Exercise.ExerciseMain();

            Console.ReadLine();
        }
    }
    // Source: https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/ranges-indexes
    internal static class Exercise
    {
        public static void Exercise1()
        {
            string[] words = new string[]
            {
                            // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "Brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
            };              // 9 (or words.Length) ^0
            Console.WriteLine($"The last word is {words[words.Length-1]}");
            Console.WriteLine($"The last word is {words[^1]} \n");

            //
            string[] quickBrownFox = words[1..4];  // O(n)
            Console.WriteLine(@"///// [1..4] \\\\\");
            foreach (var word in quickBrownFox)
                Console.Write($"< {word} >");
            Console.WriteLine("\n");

            //
            string[] lazyDog = words[^2..^0];
            Console.WriteLine(@"///// [^2..^0] \\\\\");
            foreach (var word in lazyDog)
                Console.Write($"< {word} >");
            Console.WriteLine("\n");

            //
            string[] allWords = words[..]; // contains "The" through "dog".
            string[] firstPhrase = words[..4]; // contains "The" through "fox"
            string[] lastPhrase = words[6..]; // contains "the, "lazy" and "dog"
            foreach (var word in allWords)
                Console.Write($"< {word} >");
            Console.WriteLine();
            foreach (var word in firstPhrase)
                Console.Write($"< {word} >");
            Console.WriteLine();
            foreach (var word in lastPhrase)
                Console.Write($"< {word} >");
            Console.WriteLine("\n");

            //
            Console.WriteLine(@"///// indexes and ranges \\\\\");
            Index the = ^3;
            Console.WriteLine(words[the]);
            Range phrase = 1..4;
            string[] text = words[phrase];
            foreach (var word in text)
                Console.Write($"< {word} >");
            Console.WriteLine("\n");

            //
            ///<summary>
            ///The performance of code using the range operator depends on the type of the sequence operand.
            ///
            ///The time complexity of the range operator depends on the sequence type.For example, if the sequence is a string or an array, then the result is a copy of the specified section of the input, so the time complexity is O(N) (where N is the length of the range). On the other hand, if it's a System.Span<T> or a System.Memory<T>, the result references the same backing store, which means there is no copy and the operation is O(1).
            ///
            ///In addition to the time complexity, this causes extra allocations and copies, impacting performance. In performance sensitive code, consider using Span<T> or Memory<T> as the sequence type, since the range operator does not allocate for them.
            /// </summary>
            Console.WriteLine(@"///// Span \\\\\");
            int[] numbers = Enumerable.Range(0, 100).ToArray();
            const int x = 12;
            const int y = 25;
            const int z = 36;

            Console.WriteLine($"{numbers[^x]} is the same as {numbers[numbers.Length - x]}");
            Console.WriteLine($"{numbers[x..y].Length} is the same as {y - x}");

            Console.WriteLine("numbers[x..y] and numbers[y..z] are consecutive and disjoint:");
            Span<int> x_y = numbers[x..y];  // var -> int[]
            Span<int> y_z = numbers[y..z];
            Console.WriteLine($"\tnumbers[x..y] is {x_y[0]} through {x_y[^1]}, numbers[y..z] is {y_z[0]} through {y_z[^1]}");

            Console.WriteLine("numbers[x..^x] removes x elements at each end:");
            Span<int> x_x = numbers[x..^x];
            Console.WriteLine($"\tnumbers[x..^x] starts with {x_x[0]} and ends with {x_x[^1]}");

            Console.WriteLine("numbers[..x] means numbers[0..x] and numbers[x..] means numbers[x..^0]");
            Span<int> start_x = numbers[..x];
            Span<int> zero_x = numbers[0..x];
            Console.WriteLine($"\t{start_x[0]}..{start_x[^1]} is the same as {zero_x[0]}..{zero_x[^1]}");
            Span<int> z_end = numbers[z..];
            Span<int> z_zero = numbers[z..^0];
            Console.WriteLine($"\t{z_end[0]}..{z_end[^1]} is the same as {z_zero[0]}..{z_zero[^1]} \n");


            // Scenarios for indices and ranges
            Console.WriteLine(@"///// Scenarios for indices and ranges \\\\\");
            int[] sequence = Sequence(1000);

            for (int start = 0; start < sequence.Length; start += 100)
            {
                Range r = start..(start + 10);
                var (min, max, average) = MovingAverage(sequence, r);
                Console.WriteLine($"From {r.Start} to {r.End}:    \tMin: {min},\tMax: {max},\tAverage: {average}");
            }

            for (int start = 0; start < sequence.Length; start += 100)
            {
                Range r = ^(start + 10)..^start;
                var (min, max, average) = MovingAverage(sequence, r);
                Console.WriteLine($"From {r.Start} to {r.End}:  \tMin: {min},\tMax: {max},\tAverage: {average}");
            }

            (int min, int max, double average) MovingAverage(int[] subSequence, Range range) =>
                (
                    subSequence[range].Min(),
                    subSequence[range].Max(),
                    subSequence[range].Average()
                );

            int[] Sequence(int count) =>
                Enumerable.Range(0, count).Select(x => (int)(Math.Sqrt(x) * 100)).ToArray();

        }
        public static void ExerciseMain()
        {
            Console.WriteLine("-*-*-*-*-* Exercise1 *-*-*-*-*-");
            Exercise1();
        }
    }
}
