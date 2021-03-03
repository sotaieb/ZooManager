using St.Zoo.Core;
using St.Zoo.Models;
using System;
using System.Collections.Generic;

namespace St.Zoo.Data
{
    /// <summary>
    /// The food repository service.
    /// The store is a system file.
    /// </summary>
    public class FoodRepository : IFoodRepository
    {
        private readonly IFileService _fileService;
        private readonly string _path;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileService">The <see cref="FileService"/> object</param>
        /// <param name="path">The file path</param>
        public FoodRepository(IFileService fileService, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or empty", nameof(path));
            }

            this._fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            this._path = path;
        }
        
        /// <summary>
        /// Retrieve foods.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Food> FindAll()
        {
            var foods = _fileService.ReadLines(_path);

            // The file has a strict format, no need of any checks.
            foreach (var item in foods)
            {
                var row = item.Split('=');
                yield return new Food { 
                    FoodCategory = (FoodCategory)Enum.Parse(typeof(FoodCategory), row[0], true), 
                    PricePerKg = double.Parse(row[1]) 
                };
            }
        }
    }
}
