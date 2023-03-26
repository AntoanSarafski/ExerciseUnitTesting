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
    }
}
