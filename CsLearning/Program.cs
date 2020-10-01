// default system.
using System;

// my custom imports.
using CsLearning.Numbers;
using CsLearning.Condition;
using CsLearning.Colls;
using Humanizer;
//using CsLearning.basics.Bank;

namespace CsLearning
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            #region Numbers
            //Numeric.numberMain();
            #endregion

            #region Condition
            //Conditional.conditionMain();
            #endregion

            #region Condition
            //Coll.collMain();
            #endregion

            #region Objects_Bank
            BankyStuff.BankAccountTest.BankAccountMain();
            #endregion

            #region Humanizer
            //Console.WriteLine("car".Pluralize());
            //Console.WriteLine("person".Pluralize());  // people correct, persons is wrong
            //Console.WriteLine(36552.ToWords());
            #endregion


            // just to stop execusion and see the results.
            Console.ReadLine();
        }
    }
}