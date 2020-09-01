using System;

namespace GradeBook
{
    public class Statistics 
    {
        public Statistics() 
        {
            count = 0;
            sum = 0.0;
            averageGrade = 0.0;
            lowestGrade = double.MaxValue;
            highestGrade = double.MinValue;
        }

        public void AddToStat(double grade)
        {
            sum += grade;
            count++;
            averageGrade = sum / count;
            lowestGrade = Math.Min(grade, lowestGrade);
            highestGrade = Math.Max(grade, highestGrade);
        }

        public double averageGrade;
        public double lowestGrade;
        public double highestGrade;
        public char letterGrade 
        {
            get 
            {
                switch(averageGrade)
            {
                case var d when d >= 90.0:
                    return 'A';
                case var d when d >= 80.0:
                    return 'B';
                case var d when d >= 70.0:
                    return 'C';
                case var d when d >= 60.0:
                    return 'D';   
                default: 
                    return 'F';
            }
            }
        }
        public double sum;
        public int count;
    }
}