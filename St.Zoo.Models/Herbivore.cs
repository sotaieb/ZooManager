namespace St.Zoo.Models
{
    /// <summary>
    /// Represents an herbivore animal
    /// </summary>
    public class Herbivore : AnimalSpecie
    {
        /// <summary>
        /// <inheritdoc cref="AnimalSpecie"/>
        /// </summary>
        public override FoodCategory FoodCategory => FoodCategory.Fruit;
    }
}
