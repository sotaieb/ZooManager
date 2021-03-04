using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using St.Zoo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace St.Zoo.Business.Tests
{
    public class AnimalSpecieTests
    {
        [Test]
        public void GetFoodPrice_When_Carnivore_Returns_Price()
        {
            // Arrange

            var specie = new Carnivore { Specie = AnimalSpecieNames.Lion, Rate = 0.5, PricePerKg = 2 };

            // Act
            var price = specie.GetFoodPrice(100);

            // Assert
            Assert.AreEqual(100, price);
        }

        [Test]
        public void GetFoodPrice_When_Omnivore_Returns_Price()
        {
            // Arrange

            var specie = new Omnivore(new Herbivore { Specie = AnimalSpecieNames.Wolf, Rate = 0.5  }) 
            { Specie = AnimalSpecieNames.Wolf, Rate = 0.5, PricePerKg = 2, FruitPricePerKg = 1, MeatPercentage = 0.5 };

            // Act
            var price = specie.GetFoodPrice(100);

            // Assert
            Assert.AreEqual(75, price);
        }

        [Test]
        public void GetFoodPrice_When_Invalid_PricePerPerKg_Throws_Exception()
        {
            // Arrange

            var specie = new Herbivore { Specie = AnimalSpecieNames.Giraffe, Rate = 0.5, PricePerKg = -2 };

            
            // Act, Assert
            Assert.Throws<ArgumentException>(() => specie.GetFoodPrice(100));
        }

        [Test]
        public void GetFoodPrice_When_Invalid_Weight_Throws_Exception()
        {
            // Arrange

            var specie = new Herbivore { Specie = AnimalSpecieNames.Giraffe, Rate = 0.5, PricePerKg = 2 };


            // Act, Assert
            Assert.Throws<ArgumentException>(() => specie.GetFoodPrice(-100));
        }
    }
}
