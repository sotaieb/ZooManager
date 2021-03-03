using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using St.Zoo.Core;
using St.Zoo.Models;
using System.Linq;

namespace St.Zoo.Data.Tests
{
    public class FoodRepositoryTests
    {
        [Test]
        public void FindAll_Should_Returns_Foods()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var fileService = fixture.Freeze<Mock<IFileService>>();
            fileService.Setup(x => x.ReadLines(It.IsAny<string>())).Returns(new string[] { "Meat=12.56", "Fruit=5.60" });

            var sut = fixture.Create<FoodRepository>();

            // Act
            var foods = sut.FindAll().ToList();

            // Assert
            Assert.IsNotNull(foods);
            Assert.IsTrue(foods.Count == 2);
            Assert.AreEqual(foods[0].FoodCategory, FoodCategory.Meat);
            Assert.AreEqual(foods[0].PricePerKg, 12.56);
            Assert.AreEqual(foods[1].FoodCategory, FoodCategory.Fruit);
            Assert.AreEqual(foods[1].PricePerKg, 5.60);
        }
    }
}