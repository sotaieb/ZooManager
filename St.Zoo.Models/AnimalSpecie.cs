using System;

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
        /// Food price per kg
        /// </summary>
        public double PricePerKg { get; set; }

        /// <summary>
        /// The specie name.
        /// </summary>
        public AnimalSpecieNames Specie { get; set; }

        /// <summary>
        /// Calculates food price
        /// </summary>
        /// <param name="weight">The animal weight</param>
        /// <returns></returns>
        public virtual double GetFoodPrice(double weight)
        {
            if (weight < 0) {
                throw new ArgumentException("Invalid weight value.");
            }
            if (PricePerKg < 0)
            {
                throw new ArgumentException("Invalid food price value.");
            }
            return weight * Rate * PricePerKg;
        }
    }
}
