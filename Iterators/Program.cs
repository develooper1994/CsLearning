using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/iterators
// "yield return", "yield break"
// "foreach", "in"
// The return type of an iterator method or get accessor can be IEnumerable, IEnumerable<T>, IEnumerator, or IEnumerator<T>.

namespace Iterators
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-*-*-*-*-* BasicIterator.BasicIteratorMain() *-*-*-*-*-");
            BasicIterator.BasicIteratorMain();

            Console.WriteLine("\n-*-*-*-*-* CreatingCollectionClass.CreatingCollectionClassMain() *-*-*-*-*-\n");
            CreatingCollectionClass.CreatingCollectionClassMain();

            Console.WriteLine("\n-*-*-*-*-* CreatingCollectionClass2.CreatingCollectionClassMain() *-*-*-*-*-\n");
            CreatingCollectionClass2.CreatingCollectionClassMain();


            Console.ReadKey();
        }
    }
    internal static class BasicIterator
    {
        public static void BasicIteratorMain()
        {
            Console.WriteLine("-*-*-*-*-* SomeNumbers() *-*-*-*-*-");
            var results = SomeNumbers();
            foreach (int number in results)
            {
                Console.Write(number.ToString() + " ");
            }

            Console.WriteLine("\n-*-*-*-*-* Fibonacci() *-*-*-*-*-\n");
            var resultsFibonacci = Fibonacci();
            Console.WriteLine(resultsFibonacci.GetType());
            foreach (int number in resultsFibonacci)
            {
                Console.Write(number.ToString() + " ");
            }

            Console.WriteLine("\n-*-*-*-*-* Casting to List *-*-*-*-*-\n");
            var enumToList = resultsFibonacci.ToList();
            Console.WriteLine(enumToList.GetType());
            foreach (int number in enumToList)
            {
                Console.Write(number.ToString() + " ");
            }

        }

        public static IEnumerable SomeNumbers()
        {
            yield return 3;
            yield return 5;
            yield return 8;
        }

        public static IEnumerable<int> Fibonacci(int n=10)
        {
            int a = 0;
            int b = 1;
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
                yield return a;
            }
        }

    }

    internal static class CreatingCollectionClass
    {
        public static void CreatingCollectionClassMain()
        {
            DaysOfTheWeek days = new DaysOfTheWeek();

            foreach (string day in days)
            {
                Console.Write(day + " ");
            }
            // Output: Sun Mon Tue Wed Thu Fri Sat
        }

        public class DaysOfTheWeek : IEnumerable
        {
            /// <summary>
            /// The compiler implicitly calls the GetEnumerator method, which returns an IEnumerator.
            /// </summary>
            private string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            public IEnumerator GetEnumerator()
            {
                for (int index = 0; index < days.Length; index++)
                {
                    // Yield each day of the week.
                    yield return days[index];
                }
            }
        }
    }

    internal static class CreatingCollectionClass2
    {
        public static void CreatingCollectionClassMain()
        {
            Stack<int> theStack = new Stack<int>();

            //  Add items to the stack.
            for (int number = 0; number <= 9; number++)
            {
                theStack.Push(number);
            }

            // Retrieve items from the stack.
            // foreach is allowed because theStack implements IEnumerable<int>.
            foreach (int number in theStack)
            {
                Console.Write("{0} ", number);
            }
            Console.WriteLine();
            // Output: 9 8 7 6 5 4 3 2 1 0

            // foreach is allowed, because theStack.TopToBottom returns IEnumerable(Of Integer).
            foreach (int number in theStack.TopToBottom)
            {
                Console.Write("{0} ", number);
            }
            Console.WriteLine();
            // Output: 9 8 7 6 5 4 3 2 1 0

            foreach (int number in theStack.BottomToTop)
            {
                Console.Write("{0} ", number);
            }
            Console.WriteLine();
            // Output: 0 1 2 3 4 5 6 7 8 9

            foreach (int number in theStack.TopN(7))
            {
                Console.Write("{0} ", number);
            }
            Console.WriteLine();
            // Output: 9 8 7 6 5 4 3
        }

        public class Stack<T> : IEnumerable<T>
        {
            private T[] values = new T[100];
            private int top = 0;

            public void Push(T t)
            {
                values[top] = t;
                top++;
            }
            public T Pop()
            {
                top--;
                return values[top];
            }

            // This method implements the GetEnumerator method. It allows
            // an instance of the class to be used in a foreach statement.
            public IEnumerator<T> GetEnumerator()
            {
                for (int index = top - 1; index >= 0; index--)
                {
                    yield return values[index];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerable<T> TopToBottom
            {
                get { return this; }
            }

            public IEnumerable<T> BottomToTop
            {
                get
                {
                    for (int index = 0; index <= top - 1; index++)
                    {
                        yield return values[index];
                    }
                }
            }

            public IEnumerable<T> TopN(int itemsFromTop)
            {
                // Return less than itemsFromTop if necessary.
                int startIndex = itemsFromTop >= top ? 0 : top - itemsFromTop;

                for (int index = top - 1; index >= startIndex; index--)
                {
                    yield return values[index];
                }
            }

        }

    }
}
