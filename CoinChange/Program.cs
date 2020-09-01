using System;

namespace CoinChange
{
    class Program
    {
        static void Main(string[] args)
        {
            double money = 1263;
            Change change = new Change();
            Change moneyBack = OptimalChange(money, change); 
            moneyBack.WriteOutput();
            moneyBack.Sum();
        }

        static public Change OptimalChange(double s, Change change)
        {
            void ChangeRecursion(double s, Change change)
            {
                // base cases:
                if(s < 0 || s == 1 || s == 3){change = null;}
            
                else if(s == 2){change.coins2++; s -= 2;}
            
                else if(s == 5){change.bill5++; s -= 5;}

                else if(s == 10){change.bill10++; s -= 10;}
            
                // recursion
                else if(s == 4 || s == 6 || s == 7 || s == 8 || s == 9){change.coins2 += 1; s -= 2; ChangeRecursion(s, change);}
            
                else if(s == 11 || s == 13){change.bill5++; s -= 5; ChangeRecursion(s, change);}
            
                else if(s == 12 || s > 13){change.bill10++; s -= 10; ChangeRecursion(s, change);}
            }

            ChangeRecursion(s, change);
            return change;
        }
    }
}
