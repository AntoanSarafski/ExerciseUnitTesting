namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;
        [SetUp]
        public void Setup()
        {
            car = new Car("Subaru", "Legacy", 11.1, 60);
        }
        [TearDown] public void Teardown()
        {
            car = null;
        }

        [Test]  
        public void CreateCar()
        {
            car = new Car("Subaru", "Legacy", 11.1, 60);

            Assert.AreEqual("Subaru", car.Make);
            Assert.AreEqual("Legacy", car.Model);
            Assert.AreEqual(11.1, car.FuelConsumption);
            Assert.AreEqual(60, car.FuelCapacity);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CreateCarFailIfMakeIsNullOrEmpty(string make)
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() =>  new Car(make, "Legacy", 11.1, 60));
            Assert.That(exception.Message, Is.EqualTo("Make cannot be null or empty!"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CreateCarFailIfModelIsNullOrEmpty(string model)
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() => new Car("Subaru", model, 11.1, 60));
            Assert.That(exception.Message, Is.EqualTo("Model cannot be null or empty!"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-3)]
        public void CreateCarFailIfFuelConsumptionIsLessOrEqualTo0(double fuelConsumption)
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() => new Car("Subaru", "Legacy", fuelConsumption, 60));
            Assert.That(exception.Message, Is.EqualTo("Fuel consumption cannot be zero or negative!"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-3)]
        public void CreateCarFailIfFuelCapacityIsLessOrEqualTo0(double fuelCapacity)
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() => new Car("Subaru", "Legacy", 11.1, fuelCapacity));
            Assert.That(exception.Message, Is.EqualTo("Fuel capacity cannot be zero or negative!"));
        }
    }
}