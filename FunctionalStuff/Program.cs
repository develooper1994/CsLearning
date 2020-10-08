using System;
using System.Linq;

namespace FunctionalStuff
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-*-*-*-*-* DelegatesMain *-*-*-*-*-");
            Delegates.DelegatesMain();

            Console.WriteLine("-*-*-*-*-* OtherDelegatesMain *-*-*-*-*-");
            OtherDelegates.OtherDelegatesMain();

            Console.WriteLine("-*-*-*-*-* EventsWithDelegate.EventsMain *-*-*-*-*-");
            EventsWithDelegate.EventsMain();

            Console.WriteLine("-*-*-*-*-* EventsWithout_Delegate.EventsMain *-*-*-*-*-");
            EventsWithout_Delegate.EventsMain();

            Console.WriteLine("-*-*-*-*-* EventsWithParameter.EventsMain *-*-*-*-*-");
            EventsWithParameter.EventsMain();

            Console.ReadLine();
        }
    }
    internal static class Delegates
    {
        // delegates
        public delegate void MyDelegate(string str);

        // custom functions
        private static void Print(string str) => Console.WriteLine($"FUNCTION: Print | You printed {str}");
        private static void PrintEnviroment(string str) => Console.WriteLine($"FUNCTION: PrintEnviroment | The development enviroment is {str}");

        // Main function of module
        static public void DelegatesMain()
        {
            Console.WriteLine("-*-*-*-*-* Delegate1 *-*-*-*-*-");
            Delegate1();
            Console.WriteLine("-*-*-*-*-* Delegate2 *-*-*-*-*-");
            Delegate2();
        }
        // functions of module
        static public void Delegate1()
        {
            MyDelegate del1 = Print;
            del1.Invoke("dotnet");
            del1(".net");

            MyDelegate del2 = PrintEnviroment;
            // stack function to the line. apply them one by one.
            MyDelegate del = del1 + del2;
            Console.WriteLine("-*-*-*-*-* del1 + del2 *-*-*-*-*-");
            del("Hello World");

            MyDelegate del3 = (string msg) => Console.WriteLine("FUNCTION: anonymous | Called lambda expression: " + msg);
            del += del3;  // combines del1 + del2 + del3
            Console.WriteLine("-*-*-*-*-* del1 + del2 + del3 *-*-*-*-*-");
            del("Hello World");

            del -= del2; // removes del2
            Console.WriteLine("-*-*-*-*-* del1 + del2 + del3 - del2 *-*-*-*-*-");
            del("Hello World");

            del -= del1; // removes del1
            Console.WriteLine("-*-*-*-*-* del1 + del2 + del3 - del2 - del1 *-*-*-*-*-");
            del("Hello World");
        }

        // generic delegate
        public delegate T add<T>(params T[] param1);

        static public void Delegate2()
        {
            add<int> sum = (int[] numArr1) => numArr1.Sum();
            Console.WriteLine(sum(10, 20));

            add<string> conct = (string[] strArr1) => string.Join("", strArr1);
            Console.WriteLine(conct("Hello ", "World!!"));
        }
    }
    internal static class OtherDelegates
    {

    public static void OtherDelegatesMain()
        {
            Console.WriteLine("-*-*-*-*-* Func1 *-*-*-*-*-");
            Func1();
            Console.WriteLine("-*-*-*-*-* Action1 *-*-*-*-*-");
            Action1();
            Console.WriteLine("-*-*-*-*-* Predicate1 *-*-*-*-*-");
            Predicate1();
        }

        public static void Func1()
        {
            /// <summary>
            /// namespace System
            /// {
            ///    public delegate TResult Func<in T, out TResult>(T arg);
            /// }
            /// many input paramater and one output
            /// </summary>

            // it must return a value.
            // it can have 0 to 16 input parameters.
            // it does not allow ref and out parameters.
            // it can be used with an anonymous method or lambda expression.

            Func<int, int, int> add = (int num1, int num2) => num1 + num2;
            int result = add(10, 10);
            Console.WriteLine(result);

            Func<int> getRandomNumber = delegate ()
            {
                Random rnd = new Random();
                return rnd.Next(1, 100);
            };
            Console.WriteLine(getRandomNumber());


        }
        public static void Action1()
        {
            /// public delegate void Predicate<in T>(T obj);
            ///
            /// it must not return a value.
            /// it can have 0 to 16 input parameters.
            /// it does not allow ref and out parameters.
            /// it can be used with an anonymous method or lambda expression.

            Action<string> MyAction;
            MyAction = (string msg) => Console.WriteLine(msg);

            MyAction("Take an (Action)");

            Action<int> printActionDel = delegate (int i)
            {
                Console.WriteLine(i);
            };
            printActionDel(47);
        }
        public static void Predicate1()
        {
            ///<summary>
            /// public delegate bool Predicate<in T>(T obj);
            ///
            /// A predicate delegate methods must take one input parameter and return a boolean - true or false.
            /// </summary>
            Predicate<string> isUpper = (string str) => str.Equals(str.ToUpper());
            const string msg = "MANALI YAZI";
            var result = isUpper(msg) ? "Büyük yazı" : "Küçük yazı";
            Console.WriteLine(result);
        }
    }

    // Source: https://www.tutorialsteacher.com/csharp/csharp-event
    internal static class EventsWithDelegate
    {
        public static void EventsMain()
        {
            Events1();
        }
        public delegate void Notify();  // delegate

        public class ProcessBusinessLogic
        {
            public event Notify ProcessCompleted; // event

            public void StartProcess()
            {
                Console.WriteLine("Process Started!");
                // some code here..
                OnProcessCompleted();
            }

            protected virtual void OnProcessCompleted() //protected virtual method
            {
                //if ProcessCompleted is not null then call delegate
                ProcessCompleted?.Invoke();
            }
        }

        // event handler
        public static void bl_ProcessCompleted()
        {
            Console.WriteLine("Process Completed!");
        }

        public static void Events1()
        {
            ProcessBusinessLogic b1 = new ProcessBusinessLogic();
            b1.ProcessCompleted += bl_ProcessCompleted;
            b1.StartProcess();
        }


    }

    // Built-in EventHandler Delegate
    internal static class EventsWithout_Delegate
    {
        public static void EventsMain()
        {
            Events1();
        }
        public class ProcessBusinessLogic
        {
            // declaring an event using built-in EventHandler
            public event EventHandler ProcessCompleted;

            public void StartProcess()
            {
                Console.WriteLine("Process Started!");
                // some code here..
                OnProcessCompleted(EventArgs.Empty); //No event data
            }

            protected virtual void OnProcessCompleted(EventArgs e)
            {
                ProcessCompleted?.Invoke(this, e);
            }
        }
        // event handler
        public static void Bl_ProcessCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Process Completed!");
        }
        public static void Events1()
        {
            ProcessBusinessLogic b1 = new ProcessBusinessLogic();
            b1.ProcessCompleted += Bl_ProcessCompleted;
            b1.StartProcess();
        }
    }

    internal static class EventsWithParameter
    {
        public static void EventsMain()
        {
            Events1();
        }
        public class ProcessEventArgs : EventArgs
        {
            public bool IsSuccessful { get; set; }
            public DateTime CompletionTime { get; set; }
        }
        public class ProcessBusinessLogic
        {
            // declaring an event using built-in EventHandler
            public event EventHandler<ProcessEventArgs> ProcessCompleted;

            public void StartProcess()
            {
                var data = new ProcessEventArgs();

                try
                {
                    Console.WriteLine("Process Started!");
                    // some code here..
                    data.IsSuccessful = true;
                    data.CompletionTime = DateTime.Now;
                    OnProcessCompleted(data);
                }
                catch (Exception ex)
                {
                    data.IsSuccessful = false;
                    data.CompletionTime = DateTime.Now;
                    OnProcessCompleted(data);
                }
            }

            protected virtual void OnProcessCompleted(ProcessEventArgs e)
            {
                ProcessCompleted?.Invoke(this, e);
            }
        }
        // event handler
        public static void Bl_ProcessCompleted(object sender, ProcessEventArgs e)
        {
            Console.WriteLine("Process " + (e.IsSuccessful ? "Completed Successfully" : "failed"));
            Console.WriteLine("Completion Time: " + e.CompletionTime.ToLongDateString());
        }
        public static void Events1()
        {
            ProcessBusinessLogic b1 = new ProcessBusinessLogic();
            b1.ProcessCompleted += Bl_ProcessCompleted;
            b1.StartProcess();
        }
    }
}
