using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);
    public class TypeTests
    // We always pass parameters by value! 
    {
        int count = 0;
  
        [Fact]
        public void DelegateCanPointToMethod()
        {
            //WriteLogDelegate log = new WriteLogDelegate(ReturnMessage);
            WriteLogDelegate log = ReturnMessage; // same as line above
            log += AllToUpper; // delegates can reference/invoke multiple methods

            var result = log("Hello!");
            Assert.Equal(2, count);
        }

        string ReturnMessage(string s)
        {
            count += 1;
            return s;
        }

        string AllToUpper(string s)
        {
            count += 1;
            return s.ToUpper();
        }


        [Fact]
        public void StringBehavesLikeValType()
        {
            string name = "Michael";
            MakeUpperCase(name);

            Assert.Equal("Michael", name);
            /* It is not uppercase as strings are immutable
            - therefore even though strings are reference types
            they often behave like value types!*/
            
        }

        private void MakeUpperCase(string name)
        {
            name.ToUpper(); //makes string full of uppercase!
        }

        [Fact] 
        public void GetBookReturnsDifferentObjects()
        {
            //arrange
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            //act
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2); //effectively same as two lines above
            

            //assert
        }

        [Fact]
        public void CanSetNewBookName()
        {
            //arrange
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            //assert
            Assert.Equal("New Name", book1.Name);

        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;    
        }

        [Fact]
        public void Test1()
        {
            //arrange
            var book1 = GetBook("Book 1");
            GetBookAndSetName(ref book1, "New Name");

            //assert
            Assert.Equal("New Name", book1.Name);

        }

        private void GetBookAndSetName(ref InMemoryBook book, string name)
        /* "ref" keyword changes it to pass by reference!*/
        {
            book = new InMemoryBook(name);    
        }

        [Fact]
        public void TwoVarsCanPointToOneObject()
        {
            //arrange
            var book1 = new InMemoryBook("Book 1");
            var book2 = book1;

            //assert
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }
        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
