using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly IServiceProvider _provider;       
        private readonly IConfiguration _configuration;
        private readonly IOptions<AppProfile> _appProfile;
        private readonly ILogger<ZooService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="animalRepository">The <see cref="IAnimalRepository"/> class</param>
        /// <param name="animalSpecieRepository">The <see cref="IAnimalSpecieRepository"/> class</param>
        /// <param name="foodRepository">The <see cref="IFoodRepository"/> class</param>
        public ZooService(IAnimalRepository animalRepository,
            IAnimalSpecieRepository animalSpecieRepository,
            IFoodRepository foodRepository,
            IServiceProvider provider,
            IConfiguration configuration,
            IOptions<AppProfile> appProfile,
            ILogger<ZooService> logger)
        {
            
            this._animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
            this._animalSpecieRepository = animalSpecieRepository ?? throw new ArgumentNullException(nameof(animalSpecieRepository));
            this._foodRepository = foodRepository ?? throw new ArgumentNullException(nameof(foodRepository));
            this._provider = provider ?? throw new ArgumentNullException(nameof(provider));            
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._appProfile = appProfile ?? throw new ArgumentNullException(nameof(appProfile));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Calculates the total food price of zoo animals.
        /// </summary>
        /// <returns></returns>
        public double GetTotalFoodPrice()
        {
            _logger.LogInformation($"{_configuration.Get<AppConfig>().AppSettings.ApplicationName} Processing...");
            _logger.LogInformation($"Profile: {_appProfile.Value.Label}");
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
