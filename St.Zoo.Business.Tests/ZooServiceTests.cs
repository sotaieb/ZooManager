using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Moq;
using NUnit.Framework;
using St.Zoo.Data;
using St.Zoo.Models;
using System.Collections.Generic;

namespace St.Zoo.Business.Tests
{
    public class ZooServiceTests
    {
        [Test]
        public static void EnsureConstructorArgumentsNotNull()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            // Act, Assert
            assertion.Verify(typeof(ZooService).GetConstructors());
        }

        [Test]
        public void GetTotalFoodPrice_Returns_Total_Price()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var animalRepository = fixture.Freeze<Mock<IAnimalRepository>>();
            var animalSpecieRepository = fixture.Freeze<Mock<IAnimalSpecieRepository>>();
            var foodRepository = fixture.Freeze<Mock<IFoodRepository>>();

            animalRepository.Setup(x => x.FindAll()).Returns(new List<Animal> {
                new Animal { Name="Simba",Specie= AnimalSpecieNames.Lion, Weight = 160 },
                new Animal { Name="Chip",Specie= AnimalSpecieNames.Zebra, Weight = 100 },
                new Animal { Name="Pin",Specie= AnimalSpecieNames.Wolf, Weight = 78 }

            });

            foodRepository.Setup(x => x.FindAll()).Returns(new Dictionary<FoodCategory, double> {
                { FoodCategory.Meat, 12.56 },{ FoodCategory.Fruit, 5.60 }

            });

            animalSpecieRepository.Setup(x => x.FindAll()).Returns(new List<AnimalSpecie> {
                new Carnivore {Specie = AnimalSpecieNames.Lion, Rate= 0.10 },
                new Carnivore {Specie = AnimalSpecieNames.Tiger, Rate= 0.09 },
                new Herbivore {Specie = AnimalSpecieNames.Giraffe, Rate= 0.08 },
                new Herbivore {Specie = AnimalSpecieNames.Zebra, Rate= 0.08 },
                new Omnivore(new Herbivore {Specie = AnimalSpecieNames.Wolf, Rate= 0.07 }) {Specie = AnimalSpecieNames.Wolf, Rate= 0.07, MeatPercentage = 0.9 },
                new Omnivore(new Herbivore { Specie = AnimalSpecieNames.Piranha, Rate= 0.5}) {Specie = AnimalSpecieNames.Piranha, Rate= 0.5, MeatPercentage = 0.5 },
            }) ;

            var sut = fixture.Create<ZooService>();

            // Act
            var total = sut.GetTotalFoodPrice();

            // Assert
            // carnivore => 12.56 * 160 * 0.10 = 200.96
            // herbivore => 5.60 * 100 * 0.08 = 44.8
            // omnivore => (12.56 * 78 * 0.07 * 0.9) + (5.60 * 78 * 0.07 * 0.1) = 64.77744
            // 310.53744
            Assert.AreEqual(total, 310.53744);
            
        }
    }
}