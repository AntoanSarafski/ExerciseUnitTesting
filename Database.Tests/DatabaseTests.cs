namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database _database;

        [SetUp]
        public void Setup()
        {
            _database = new Database();
        }

        [TearDown]  
        public void TearDown()
        {
            _database = null;
        }

        [Test]  
        public void AddMethodTest() 
        {
            _database.Add(1488);
            int[] result = _database.Fetch();

            Assert.IsTrue(1 == _database.Count);
            Assert.AreEqual(1488, result[0]);
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void ShouldThrowIfMoreThanMaximumLength()
        {
            _database = new Database(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16);

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => _database.Add(1948)); 
            // Checking if we add one more element will we throw expected exception.
            Assert.That(exception.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
            // Checking the exception message.

        }

        [Test]
        public void CreateDatabaseWith10Elements()
        {
            _database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            Assert.AreEqual(10, _database.Count);
        }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrow()
        {
            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => _database.Remove());
            // Checking if we remove one element from empty database will we throw expected exception.
            Assert.That(exception.Message, Is.EqualTo("The collection is empty!"));
        }

        [Test]  
        public void RemoveFromDatabase()
        {
            _database = new Database(1948, 1488);
            _database.Remove();
            int[] result = _database.Fetch();

            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(1948, result[0]);   
            // Check if we remove correct element.

        }

    }
}
