using System.Collections.Generic;
using System;
using System.IO;

namespace GradeBook 
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);
    public class NamedObject
    {
        public NamedObject(string name)
        {
                Name = name; // name property equals nam
        }
        public string Name {get; set;}
    }

    public interface IBook //Interfaces often start with I!
    {
        void AddGrade(double grade);
        Statistics ComputeStatistics();

        void ShowStatistics(Statistics stat);
        string Name {get;} // we only require that this interface has a get accessor
        event GradeAddedDelegate GradeAdded;
    } 
    public abstract class Book : NamedObject, IBook
    {
        protected Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade); //abstract: i cannot provide an implementation, maybe the classes that derive from this can!

        public abstract Statistics ComputeStatistics(); // could also have made it virtual

        public abstract void ShowStatistics(Statistics stat);
    }



    public class InMemoryBook : Book //inheritance - book inherits NamedObjects properties
    {
    

        /* Below is an explicit constructor where we provide the initialisations 
        we want. If we did not have this C# would have a default implicit 
        initialization of our class. This has to have the same name as the class
        and can have no return type. This constructor (which is a method)
        will always be executed before any other method that is called! 
        base(name): this ensures that the name passed to book is passed to the
        base class from which book inherits its properties. Thus the
        property Name of NamedObject (and thus of book) is populated by name.*/
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
            /* The field name is set to the name passed to the Book method!*/
        }
        public override void AddGrade(double grade) 
        /* If this did not have public keyword the method could not be invoked
        in the program class. This is also the reason why we cannot access
        "grades" below directly in the program class as we have not given it 
        the public keyword (we could have done that).*/
        {
            if(grade >= 0 && grade <= 100) 
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }

            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public void AddLetterGrade(char letter) // char: single character string
        {
            switch(letter)
            {
                case 'A': AddGrade(90); break;
                case 'B': AddGrade(80); break;
                case 'C': AddGrade(70); break;
                case 'D': AddGrade(60); break;
                default: AddGrade(0); break;
            }
            /*
            if(letter == 'A') {AddGrade(90);}
            else if(letter == 'B') {AddGrade(80);}
            else if(letter == 'C') {AddGrade(70);}
            */

        }

        public override Statistics ComputeStatistics()
        /* Computes the average, lowest and highest grade from grades.*/
        {
            var res = new Statistics();
            foreach(var grade in grades)
            {
                res.AddToStat(grade);
            }
            //  Could also do:
            /*var index = 0;
            while (index < grades.Count)
            {
                res.highestGrade = Math.Max(grades[index], res.highestGrade);
                res.lowestGrade = Math.Min(grades[index], res.lowestGrade);
                res.averageGrade += grades[index];
                index += 1;
            }
            for(var index = 0; index < grades.Count; index += 1)
            {
                res.highestGrade = Math.Max(grades[index], res.highestGrade);
                res.lowestGrade = Math.Min(grades[index], res.lowestGrade);
                res.averageGrade += grades[index];
            }*/
            // jumping statements that can be used in loops: break, continue;

            

            return res;
        }

        public override void ShowStatistics(Statistics stat)
        {
            Console.WriteLine($"The average grade is {stat.averageGrade:N1}");
            /* N1 means that the numbers should be formatted as a number 
            with 1 digit */ 
            Console.WriteLine($"The lowest grade is {stat.lowestGrade}");
            Console.WriteLine($"The highest grade is {stat.highestGrade}");
            Console.WriteLine($"The average letter grade is {stat.letterGrade}");
        }

        private List<double> grades; 
        //private string name;
        /* This is a field statement - a field is a variable that is 
        declared within a class and we use it to store data about the class.
         */
        public const string CATEGORY = "Science";
        

        //public string Name 
        /* 
        This is a property (has "{}") and would in many cases behave like a public field.
        However, one nice thing about properties is the possibility to make
        access private or public on either accessor below: 
        
        { 
            get; // this is an accessor 
            private set; // this is a private accessor could also have done for above
        } 
        */
        public override event GradeAddedDelegate GradeAdded;
        /* From the delegate we can define an event member of the class!
        Also, the event is technically a delegate. However, it imposes some 
        nice restrictions that one would like for events.*/
    }
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override void AddGrade(double grade)
        {
            string DiskBookFile = @"DiskBook.txt";
            using(StreamWriter sw = File.AppendText(DiskBookFile)) 
            {
                sw.WriteLine($"{grade}");
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }

            }
            /*
            This using makes sure that we do not get the error saying that
            we cannot access the file as it is being used by another process.
            Basicly, we tell C# when we are using the file and after the "}"
            it can dispose of the file handler and other resources.
            We could also have written sw.dispose().
            */
        }

        public override Statistics ComputeStatistics()
        {
            var result = new Statistics();
            string DiskBookFile = @"DiskBook.txt";
            using(StreamReader sr = File.OpenText(DiskBookFile))
            {
                var line = sr.ReadLine();
                while(line != null)
                {
                    var grade = double.Parse(line);
                    result.AddToStat(grade);
                    line = sr.ReadLine();
                }

            }
            return result;
        }

        public override void ShowStatistics(Statistics stat)
        {
            Console.WriteLine($"The average grade is {stat.averageGrade:N1}");
            /* N1 means that the numbers should be formatted as a number 
            with 1 digit */ 
            Console.WriteLine($"The lowest grade is {stat.lowestGrade}");
            Console.WriteLine($"The highest grade is {stat.highestGrade}");
            Console.WriteLine($"The average letter grade is {stat.letterGrade}");
        }

        public override event GradeAddedDelegate GradeAdded;
    }
}