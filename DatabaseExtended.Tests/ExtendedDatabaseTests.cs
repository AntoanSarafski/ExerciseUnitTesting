namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
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
            _database.Add(new Person(1, "Ivo"));
            Person result = _database.FindById(1);

            Assert.IsTrue(1 == _database.Count);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Ivo", result.UserName);
        }

        [Test]
        public void ShouldThrowIfMoreThanMaximumLength()
        {
            Person[] people = CreateFullArray();
            _database = new Database(people);

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => _database.Add(new Person(17,"Pesho")));
            // Checking if we add one more person will we throw expected exception. Max is 16, we try to add 17.
            Assert.That(exception.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
            // Checking the exception message.

        }


        [Test]  
        public void AddShouldThrowIfNotUniqueUsername()
        {
            _database.Add(new Person(1, "Gosho"));

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => _database.Add(new Person(17, "Gosho")));
            Assert.That(exception.Message, Is.EqualTo("There is already user with this username!"));

        }

        private Person[] CreateFullArray()
        {
            Person[] persons = new Person[16];

            for (int i = 0; i < persons.Length; i++)
            {
                persons[i] = new Person(i, i.ToString());
            }
            return persons;
        }


        //[Test]
        //public void CreateDatabaseWith10Elements()
        //{
        //    _database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

        //    Assert.AreEqual(10, _database.Count);
        //}

        //[Test]
        //public void RemoveFromEmptyDatabaseShouldThrow()
        //{
        //    InvalidOperationException exception = Assert
        //       .Throws<InvalidOperationException>(() => _database.Remove());
        //    // Checking if we remove one element from empty database will we throw expected exception.
        //    Assert.That(exception.Message, Is.EqualTo("The collection is empty!"));
        //}

        //[Test]
        //public void RemoveFromDatabase()
        //{
        //    _database = new Database(1948, 1488);
        //    _database.Remove();
        //    int[] result = _database.Fetch();

        //    Assert.AreEqual(1, _database.Count);
        //    Assert.AreEqual(1, result.Length);
        //    Assert.AreEqual(1948, result[0]);
        //    // Check if we remove correct element.

        //}

        //[Test]
        //public void FetchDataFromDatabase()
        //{
        //    _database = new Database(1, 2, 3);
        //    int[] result = _database.Fetch();

        //    Assert.That(new int[] { 1, 2, 3 }, Is.EquivalentTo(result));
        //}
    }
}