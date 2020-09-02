using System;
using Xunit;

namespace CoinChange.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Random rnd = new Random();
            for(int i = 0; i < 100; i++)
            {
                int money = rnd.Next(-10, 1000);
                Change change = new Change();
                Change moneyBack = OptimalChange(money, change); 
                Assert.Equal(money, moneyBack);
            }
        }
    }
}
