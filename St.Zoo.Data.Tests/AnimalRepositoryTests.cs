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
    public class AnimalRepositoryTests
    {
        [Test]
        public static void EnsureConstructorArgumentsNotNull()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            // Act, Assert
            assertion.Verify(typeof(AnimalRepository).GetConstructors());
        }

        [Test]
        public void FindAllSpecies_Should_Returns_AnimalSpecies()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var fileInfo = fixture.Freeze<Mock<IFileInfo>>();
            var content = @"<Zoo>
                          <Lions>
                            <Lion name='Simba' kg='160'/>
                            <Lion name='Nala' kg='172'/>
                            <Lion name='Mufasa' kg='190'/>
                          </Lions>
                          <Giraffes>
                            <Giraffe name='Hanna' kg='200'/>
                            <Giraffe name='Anna' kg='202'/>
                            <Giraffe name='Susanna' kg='199'/>
                          </Giraffes>
                          <Tigers>
                            <Tiger name='Dante' kg='150'/>
                            <Tiger name='Asimov' kg='142'/>
                            <Tiger name='Tolkien' kg='139'/>
                          </Tigers>
                          <Zebras>
                            <Zebra name='Chip' kg='100'/>
                            <Zebra name='Dale' kg='62'/>
                          </Zebras>
                          <Wolves>
                            <Wolf name='Pin' kg='78'/>
                            <Wolf name='Pon' kg='69'/>
                          </Wolves>
                          <Piranhas>
                            <Piranha name='Anastasia' kg='0.5'/>
                          </Piranhas>
                        </Zoo>";
            fileInfo.Setup(x => x.Exists).Returns(true);
            fileInfo.Setup(x => x.CreateReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes(content)));

            var sut = fixture.Create<AnimalRepository>();

            // Act
            var animals = sut.FindAll().ToList();

            // Assert
            Assert.IsNotNull(animals);
            Assert.IsTrue(animals.Count == 14);

            Assert.AreEqual(animals[0].Name, "Simba");
            Assert.AreEqual(animals[0].Weight, 160);
            Assert.AreEqual(animals[0].Specie, AnimalSpecieNames.Lion);

            // Todo: continue checks ...
        }

        [Test]
        public void FindAll_When_File_NotExists_Throws_Exception()
        {
            // Arrange
            var provider = new PhysicalFileProvider("c:\\");
            var sut = new AnimalRepository(provider.GetFileInfo("name.csv"));

            // Act, Assert
            Assert.Throws<FileNotFoundException>(() => sut.FindAll().ToList());
        }
    }
}
