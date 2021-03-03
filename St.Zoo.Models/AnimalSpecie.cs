namespace St.Zoo.Models
{
    /// <summary>
    /// The animal specie.
    /// </summary>
    public abstract class AnimalSpecie
    {
        /// <summary>
        /// The food category: meat/fruit
        /// </summary>
        public abstract FoodCategory FoodCategory { get; }

        /// <summary>
        /// The weight rate.
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// The specie name.
        /// </summary>
        public AnimalSpecieNames Specie { get; set; }

        /// <summary>
        /// Calculates food price
        /// </summary>
        /// <param name="weight">The animal weight</param>
        /// <param name="price">The food price per kg</param>
        /// <returns></returns>
        public virtual double GetFoodPrice(double weight, double price)
        {
            return weight * Rate * price;
        }
    }
}
