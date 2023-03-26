namespace Database.Tests
{
    using NUnit.Framework;

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
    }
}
