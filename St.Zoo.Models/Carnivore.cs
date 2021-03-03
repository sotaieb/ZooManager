namespace St.Zoo.Models
{
    /// <summary>
    /// Represents a carnivore animal.
    /// </summary>
    public class Carnivore : AnimalSpecie
    {
        /// <summary>
        /// <inheritdoc cref="AnimalSpecie"/>
        /// </summary>
        public override FoodCategory FoodCategory => FoodCategory.Meat;
    }
}
