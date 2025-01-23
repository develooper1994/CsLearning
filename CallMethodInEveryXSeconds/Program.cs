using System;
using System.Threading;

namespace CallMethodInEveryXSeconds
{
    class Program
    {
        private static void Main(string[] args)
        {
            var milisec = 1000;
            var seconds = milisec * 5;
            var timer = new Timer(Method, null, 0, seconds);

            Console.ReadKey();
        }

        static void Method(object o)
        {
            Console.WriteLine($"Time is now: {DateTime.Now}");
        }
    }
}
