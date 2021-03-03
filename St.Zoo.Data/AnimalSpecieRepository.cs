using Microsoft.Extensions.FileProviders;
using St.Zoo.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace St.Zoo.Data
{
    /// <summary>
    /// The animal specie repository.
    /// </summary>
    public class AnimalSpecieRepository : IAnimalSpecieRepository
    {
        /// <summary>
        /// The specie file info
        /// </summary>
        private readonly IFileInfo _fileInfo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileInfo">The specie file store</param>
        public AnimalSpecieRepository(IFileInfo fileInfo)
        {
            this._fileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
        }

        /// <summary>
        /// Retrieve zoo species.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AnimalSpecie> FindAll()
        {
            if (!_fileInfo.Exists)
            {
                throw new FileNotFoundException("Specie file not exists.");
            }

            using (var stream = _fileInfo.CreateReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    while (reader.Peek() >= 0)
                    {
                        var row = reader.ReadLine().Split(';');
                        var food = (FoodCategory)Enum.Parse(typeof(FoodCategory), row[2], true);
                        var specieName = (AnimalSpecieNames)Enum.Parse(typeof(AnimalSpecieNames), row[0], true);
                        switch (food)
                        {
                            case FoodCategory.Meat:
                                yield return new Carnivore
                                {
                                    Specie = specieName,
                                    Rate = double.Parse(row[1])
                                };
                                break;
                            case FoodCategory.Fruit:
                                yield return new Herbivore
                                {
                                    Specie = specieName,
                                    Rate = double.Parse(row[1])
                                };
                                break;
                            case FoodCategory.Both:
                                var rawPercentage = row[3].Remove(row[3].Length - 1);
                                var percentage = double.Parse(rawPercentage) / 100;
                                var rate = double.Parse(row[1]);

                                yield return new Omnivore(new Herbivore { Rate = rate, Specie = specieName })
                                {
                                    Specie = specieName,
                                    Rate = rate,
                                    MeatPercentage = percentage,
                                };
                                break;
                            default:
                                throw new Exception("Invalid Sepcie.");
                        }
                    }
                }
            }

        }
    }
}

