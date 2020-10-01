using System;

using Xunit;

using BankyStuff;

namespace BankingTests
{
    public class BasicTests
    {
        [Fact]
        public void TrueIsTrue()
        {
            Assert.True(true);
        }

        [Fact]
        public void CantTakeMoreThanYouHave()
        {
            var account = new BankAccount("Mustafa", 10_000);

            Assert.Throws<InvalidOperationException>(
                () => account.MakeWithdrawal(75000000, DateTime.Now, "Attempt to overdraw")
            );  // failed so we successed. Expectred to be failed

            Assert.Throws<InvalidOperationException>(
            () => account.MakeWithdrawal(75000000, DateTime.Now, "Attempt to overdraw")
        );  // not failed so we failed. Expectred to be failed

        }

        [Fact]
        public void NeedMoneyToStart()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                ()=>(new BankAccount("invalid", -55))
                );

        }
    }
}
