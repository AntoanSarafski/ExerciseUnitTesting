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
                .Throws<InvalidOperationException>(() => _database.Add(new Person(17, "Pesho")));
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

        [Test]
        public void AddShouldThrowIfNotUniqueId()
        {
            _database.Add(new Person(1, "Gosho"));

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => _database.Add(new Person(1, "Peter")));
            Assert.That(exception.Message, Is.EqualTo("There is already user with this Id!"));

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


        [Test]
        public void CreateDatabaseWithTwoElements()
        {
            _database = new Database(new Person(1, "Pesho"), new Person(2, "Gosho"));
            Person first = _database.FindById(1);
            Person second = _database.FindById(2);

            Assert.AreEqual(2, _database.Count);
            Assert.AreEqual("Pesho", first.UserName);
            Assert.AreEqual("Gosho", second.UserName);
        }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() => _database.Remove());
            //This is the way to check only Exception, without any message,
            //cuz InvalidOperationExc have deffault string message and we dont wanna use it.
            // Checking if we remove one element from empty database will we throw expected exception.
        }

        [Test]
        public void RemoveFromDatabase()
        {
            _database = new Database(new Person(1, "Pesho"), new Person(2, "Gosho"));
            _database.Remove();
            Person first = _database.FindById(1);

            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual("Pesho", first.UserName);
            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => _database.FindByUsername("Gosho"));
            Assert.That(exception.Message, Is.EqualTo("No user is present by this username!"));

            // Check if we remove correct element.

        }

        [Test]
        public void FindByUserNameShouldThrowEmptyOrNullUsernameException()
        {
            ArgumentNullException exception = Assert
                .Throws<ArgumentNullException>(() => _database.FindByUsername(null));
            Assert.That(exception.ParamName, Is.EqualTo("Username parameter is null!"));

            ArgumentNullException emptyEx = Assert
                .Throws<ArgumentNullException>(() => _database.FindByUsername(string.Empty));
            Assert.That(emptyEx.ParamName, Is.EqualTo("Username parameter is null!"));
        }

        [Test]
        public void FindByUserNameShouldThrowIfUserNameDoesNotExist()
        {
            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => _database.FindByUsername("Ivan"));
            Assert.That(exception.Message, Is.EqualTo("No user is present by this username!"));
        }

        [Test]  
        public void FindByUserNameReturnsCorrectUser()
        {
            _database = new Database(new Person(1, "Pesho"), new Person(2, "Gosho"));
            Person person = _database.FindByUsername("Gosho");

            Assert.AreEqual("Gosho", person.UserName);
            Assert.AreEqual(2, person.Id);

        }

        [Test]
        public void FindByIdShouldThrowNegativeIdException()
        {
            ArgumentOutOfRangeException zeroException = Assert
                .Throws<ArgumentOutOfRangeException>(() => _database.FindById(-1));
            Assert.That(zeroException.ParamName, Is.EqualTo("Id should be a positive number!"));

        }

        [Test]
        public void FindByIdShouldThrowIfIdDoesNotExist()
        {
            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => _database.FindById(1488));
            Assert.That(exception.Message, Is.EqualTo("No user is present by this ID!"));
        }

        //[Test]
        //public void FindByUserNameReturnsCorrectUser()
        //{
        //    _database = new Database(new Person(1, "Pesho"), new Person(2, "Gosho"));
        //    Person person = _database.FindByUsername("Gosho");

        //    Assert.AreEqual("Gosho", person.UserName);
        //    Assert.AreEqual(2, person.Id);

        //}

    }
}