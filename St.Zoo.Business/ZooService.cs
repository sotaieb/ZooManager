using St.Zoo.Data;
using St.Zoo.Models;
using System;
using System.Linq;

namespace St.Zoo.Business
{
    public class ZooService : IZooService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IAnimalSpecieRepository _animalSpecieRepository;
        private readonly IFoodRepository _foodRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="animalRepository">The <see cref="IAnimalRepository"/> class</param>
        /// <param name="animalSpecieRepository">The <see cref="IAnimalSpecieRepository"/> class</param>
        /// <param name="foodRepository">The <see cref="IFoodRepository"/> class</param>
        public ZooService(IAnimalRepository animalRepository, IAnimalSpecieRepository animalSpecieRepository, IFoodRepository foodRepository)
        {
            this._animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
            this._animalSpecieRepository = animalSpecieRepository ?? throw new ArgumentNullException(nameof(animalSpecieRepository));
            this._foodRepository = foodRepository ?? throw new ArgumentNullException(nameof(foodRepository));
        }
        /// <summary>
        /// Calculates the total food price of zoo animals.
        /// </summary>
        /// <returns></returns>
        public double GetTotalFoodPrice()
        {
            var foodPrices = _foodRepository.FindAll();            
            var animals = _animalRepository.FindAll();
            var species = _animalSpecieRepository.FindAll();

            var amount = 0.0;
            foreach (var animal in animals)
            {
                var specie = species.SingleOrDefault(x => x.Specie == animal.Specie);
               
                if (specie is Omnivore omnivore)
                {
                    omnivore.PricePerKg = foodPrices[FoodCategory.Meat];
                    omnivore.FruitPricePerKg = foodPrices[FoodCategory.Fruit];
                }
                else {
                    specie.PricePerKg = foodPrices[specie is Carnivore ?FoodCategory.Meat: FoodCategory.Fruit];
                }
                amount += specie.GetFoodPrice(animal.Weight);
            }
            return amount;
        }
    }
}
