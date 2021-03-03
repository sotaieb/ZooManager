using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Microsoft.Extensions.FileProviders;
using Moq;
using NUnit.Framework;
using St.Zoo.Models;
using System.IO;
using System.Text;

namespace St.Zoo.Data.Tests
{
    public class FoodRepositoryTests
    {
        [Test]
        public static void EnsureConstructorArgumentsNotNull()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            // Act, Assert
            assertion.Verify(typeof(FoodRepository).GetConstructors());
        }

        [Test]
        public void FindAll_Should_Returns_Foods()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var fileInfo = fixture.Freeze<Mock<IFileInfo>>();
            var str = new StringBuilder();
            str.AppendLine("Meat=12.56");
            str.AppendLine("Fruit=5.60");
            fileInfo.Setup(x => x.Exists).Returns(true);
            fileInfo.Setup(x => x.CreateReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes(str.ToString())));

            var sut = fixture.Create<FoodRepository>();

            // Act
            var foods = sut.FindAll();

            // Assert
            Assert.IsNotNull(foods);
            Assert.IsTrue(foods.Count == 2);           
            Assert.AreEqual(foods[FoodCategory.Meat], 12.56);            
            Assert.AreEqual(foods[FoodCategory.Fruit], 5.60);
        }

        [Test]
        public void FindAll_When_File_NotExists_Throws_Exception()
        {
            // Arrange
            var provider = new PhysicalFileProvider("c:\\");
            var sut = new FoodRepository(provider.GetFileInfo("name.txt"));

            // Act, Assert
            Assert.Throws<FileNotFoundException>(() => sut.FindAll());
        }
    }
}