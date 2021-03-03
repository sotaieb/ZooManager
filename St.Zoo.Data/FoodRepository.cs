using Microsoft.Extensions.FileProviders;
using St.Zoo.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace St.Zoo.Data
{
    /// <summary>
    /// The food repository service.
    /// The store is a system file.
    /// </summary>
    public class FoodRepository : IFoodRepository
    {
        /// <summary>
        /// The food file info
        /// </summary>
        private readonly IFileInfo _fileInfo;
        /// <summary>
        /// Constructor
        /// </summary>        
        /// <param name="fileInfo">The food file store</param>
        public FoodRepository(IFileInfo fileInfo)
        {
            this._fileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
        }
        
        /// <summary>
        /// Retrieve foods.
        /// </summary>
        /// <returns></returns>
        public IDictionary<FoodCategory, double> FindAll()
        {
            if (!_fileInfo.Exists)
            {
                throw new FileNotFoundException("Food file not exists.");
            }

            var foods = new Dictionary<FoodCategory, double>();

            // The file has a strict format, no need of any checks.
            using (var stream = _fileInfo.CreateReadStream()) {
                using (var reader = new StreamReader(stream))
                {
                    while (reader.Peek() >= 0)
                    {
                        var row = reader.ReadLine().Split('=');
                        foods.Add((FoodCategory)Enum.Parse(typeof(FoodCategory), row[0], true), double.Parse(row[1]));
                    }
                }
            }
            return foods;
        }
    }
}
