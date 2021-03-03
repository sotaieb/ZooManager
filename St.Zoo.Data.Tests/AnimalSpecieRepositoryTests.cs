using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Microsoft.Extensions.FileProviders;
using Moq;
using NUnit.Framework;
using St.Zoo.Models;
using System.IO;
using System.Linq;
using System.Text;

namespace St.Zoo.Data.Tests
{
    public class AnimalSpecieRepositoryTests
    {
        [Test]
        public static void EnsureConstructorArgumentsNotNull()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            // Act, Assert
            assertion.Verify(typeof(AnimalSpecieRepository).GetConstructors());
        }

        [Test]
        public void FindAllSpecies_Should_Returns_AnimalSpecies()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var fileInfo = fixture.Freeze<Mock<IFileInfo>>();
            var str = new StringBuilder();
            str.AppendLine("Lion;0.10;meat;");
            str.AppendLine("Tiger;0.09;meat;");
            str.AppendLine("Giraffe;0.08;fruit;");
            str.AppendLine("Zebra;0.08;fruit;");
            str.AppendLine("Wolf;0.07;both;90%");
            str.AppendLine("Piranha;0.5;both;50%");
            var x = str.ToString();
            fileInfo.Setup(x => x.Exists).Returns(true);
            fileInfo.Setup(x => x.CreateReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes(str.ToString())));

            var sut = fixture.Create<AnimalSpecieRepository>();

            // Act
            var species = sut.FindAll().ToList();

            // Assert
            Assert.IsNotNull(species);
            Assert.IsTrue(species.Count == 6);

            Assert.AreEqual(species[0].Specie, AnimalSpecieNames.Lion);
            Assert.AreEqual(species[0].Rate, 0.10);
            Assert.IsTrue(species[0] is Carnivore);

            Assert.AreEqual(species[1].Specie, AnimalSpecieNames.Tiger);
            Assert.AreEqual(species[1].Rate, 0.09);
            Assert.IsTrue(species[1] is Carnivore);

            Assert.AreEqual(species[2].Specie, AnimalSpecieNames.Giraffe);
            Assert.AreEqual(species[2].Rate, 0.08);
            Assert.IsTrue(species[2] is Herbivore);

            Assert.AreEqual(species[3].Specie, AnimalSpecieNames.Zebra);
            Assert.AreEqual(species[3].Rate, 0.08);
            Assert.IsTrue(species[3] is Herbivore);

            Assert.AreEqual(species[4].Specie, AnimalSpecieNames.Wolf);
            Assert.AreEqual(species[4].Rate, 0.07);
            Assert.IsTrue(species[4] is Omnivore);
            Assert.AreEqual((species[4] as Omnivore).MeatPercentage, 0.9);

            Assert.AreEqual(species[5].Specie, AnimalSpecieNames.Piranha);
            Assert.AreEqual(species[5].Rate, 0.5);
            Assert.IsTrue(species[5] is Omnivore);
            Assert.AreEqual((species[5] as Omnivore).MeatPercentage, 0.5);
        }

        [Test]
        public void FindAll_When_File_NotExists_Throws_Exception()
        {
            // Arrange
            var provider = new PhysicalFileProvider("c:\\");
            var sut = new AnimalSpecieRepository(provider.GetFileInfo("name.csv"));

            // Act, Assert
            Assert.Throws<FileNotFoundException>(() => sut.FindAll().ToList());
        }
    }
}
