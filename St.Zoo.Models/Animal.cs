namespace St.Zoo.Models
{
    /// <summary>
    /// The animal class
    /// </summary>
    public class Animal
    {       
        /// <summary>
        /// The name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The animal specie
        /// </summary>
        public AnimalSpecieNames Specie { get; set; }

        /// <summary>
        /// The animal weight
        /// </summary>
        public double Weight { get; set; }
    }
}
