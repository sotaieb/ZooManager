namespace St.Zoo.Models
{
    /// <summary>
    /// Reapresents an omnivore animal
    /// </summary>
    public class Omnivore : AnimalSpecie
    {
        public override FoodCategory FoodCategory => FoodCategory.Both;
        public double MeatPercentage { get; set; }
        /// <summary>
        /// Deorate the class with the fruit price per kg.
        /// </summary>
        public double FruitPrice { get; set; }

        private readonly Herbivore _herbivore;

        /// <summary>
        /// Constructor,
        /// An omnivore animal is Carnivore AND herbivore
        /// </summary>
        /// <param name="herbivore">An <see cref="Herbivore"/> animal object</param>
        public Omnivore(Herbivore herbivore)
        {
            this._herbivore = herbivore ?? throw new System.ArgumentNullException(nameof(herbivore));
        }
        
        /// <summary>
        /// Calculates the food price.
        /// </summary>
        /// <param name="weight">the animal weight</param>
        /// <param name="price">The meat price</param>
        /// <returns>The food price</returns>
        public override double GetFoodPrice(double weight, double price)
        {
            // Price of meat * % + Price of fruit * (1-%)
            return (base.GetFoodPrice(weight, price) * MeatPercentage) + (_herbivore.GetFoodPrice(weight, FruitPrice) * (1 - MeatPercentage));
        }

    }
}
