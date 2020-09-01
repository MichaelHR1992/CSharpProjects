using System;
using System.Collections.Generic;

namespace GradeBook 
/* Important to have this namespace so classes defined within it 
does not conflict with those defined by microsoft in the system namespace. */
{
    class Program
    {
        static void Main(string[] args)
        /*Static: something that is static can only be reference through
        its type name and not an instanciated type/object. When one e.g. has
        a static method it is often because that method is general purpose -
        it cannot be invoked on an instance but is a method within the class.*/
        {
            IBook book = new DiskBook("Michael's gradebook");
            book.GradeAdded += EventIfGradeAdded;
            book.GradeAdded += EventIfGradeAdded;
            EnterGrades(book);

            var statistics = book.ComputeStatistics();
            book.ShowStatistics(statistics);
            Console.WriteLine($"For the book named {book.Name}");
        }

        private static void EnterGrades(IBook book)
        {
            var done = false;

            while (done != true)
            {
                Console.WriteLine("Enter grade or write 'q' to quit.");
                var input = Console.ReadLine();


                if (input == "q")
                {
                    done = true;
                    continue;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void EventIfGradeAdded(object sender, EventArgs args)
        {
                Console.WriteLine("A grade was added!");
        }

    }
}
