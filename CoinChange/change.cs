using System;

namespace CoinChange 
{
    class Change
    {
        public double coins2 = 0;
        public double bill5 = 0;
        public double bill10 = 0;

        public void WriteOutput()
        {
            Console.WriteLine($"Coins2 used: {this.coins2}");
            Console.WriteLine($"Coins2 used: {this.bill5}");
            Console.WriteLine($"Coins2 used: {this.bill10}");
        }

        public void Sum()
        {
            double sumIs = this.coins2 * 2 + this.bill5 * 5 + this.bill10 *10;
            Console.WriteLine($"The sum is: {sumIs}");
        }
    }
}