using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact] 
        /* is an attribute - it is a little peace of data 
        that is attached to the following method. Xunit 
        looks for methods with this attribute to know which
        methods is actual tests it should run. */
        public void BooksComputesAverageGrade()
        {
            // Unit testting is often divided into three parts: arrange, act
            //arrange - the setup for the test
            var book = new InMemoryBook("");
            book.AddGrade(90);
            book.AddGrade(60);
            book.AddGrade(20);

            
            // act - handling of the setup 
            var result = book.ComputeStatistics();
         

            //assert - just assertion 
            Assert.Equal(56.6, result.averageGrade, 0);
            Assert.Equal(20, result.lowestGrade);
            Assert.Equal(90, result.highestGrade);
            Assert.Equal('F', result.letterGrade);

        }
    }
}
