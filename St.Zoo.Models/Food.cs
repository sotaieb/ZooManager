namespace St.Zoo.Models
{
    /// <summary>
    /// Describes food class
    /// </summary>
    public class Food
    {
        /// <summary>
        /// The food category
        /// </summary>
        public FoodCategory FoodCategory { get; set; }

        /// <summary>
        /// The food price per Kg
        /// </summary>
        public double PricePerKg { get; set; }
    }
}
