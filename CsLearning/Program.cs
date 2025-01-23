// default system.
// my custom imports.
using CsLearning.basics;
using CsLearning.Colls;
using CsLearning.Condition;

using Humanizer;

using System;
//using CsLearning.basics.Bank;

namespace CsLearning
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            #region Numbers
            Numeric.NumberMain();
            #endregion

            #region Condition
            Conditional.ConditionMain();
            #endregion

            #region Condition
            Coll.collMain();
            #endregion

            #region Objects_Bank
            BankyStuff.BankAccountTest.BankAccountMain();
            #endregion

            #region Humanizer
            Console.WriteLine("car".Pluralize());
            Console.WriteLine("person".Pluralize());  // people correct, persons is wrong
            Console.WriteLine(36552.ToWords());
            #endregion


            // just to stop execusion and see the results.
            Console.ReadLine();
        }
    }
}