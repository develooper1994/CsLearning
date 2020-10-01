using System;
using System.Collections.Generic;

namespace Generics
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-*-*-*-*-* GenericClass1.GenericClass1Main *-*-*-*-*-");
            GenericClass1.GenericClass1Main();

            Console.ReadLine();
        }
    }

    internal static class GenericClass1
    {
        // Linked-List
        // type parameter T in angle brackets
        public class GenericList<T>
        {
            // The nested class is also generic on T.
            private class Node
            {
                // T used in non-generic constructor.
                public Node(T t)
                {
                    Next = null;
                    Data = t;
                }
                public Node Next { get; set; }
                // T as return type of property.
                public T Data { get; set; }
            }

            private Node head;

            // constructor
            public GenericList() => head = null;

            // T as method parameter type:
            public void AddHead(T t)
            {
                Node n = new Node(t)
                {
                    Next = head
                };
                head = n;
            }

            public IEnumerator<T> GetEnumerator()
            {
                Node current = head;

                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
        }
        public static void GenericClass1Method1()
        {
            // int is the type argument
            GenericList<int> list = new GenericList<int>();

            for (int x = 0; x < 10; x++)
                list.AddHead(x);

            foreach (int i in list)
                System.Console.Write($"{i} ");
            System.Console.WriteLine("\nDone");
        }
        public static void GenericClass1Main()
        {
            GenericClass1Method1();
        }
    }
}
