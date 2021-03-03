
using Microsoft.Extensions.FileProviders;
using St.Zoo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace St.Zoo.Data
{
    /// <summary>
    /// The Animal repository
    /// </summary>
    public class AnimalRepository : IAnimalRepository
    {
        /// <summary>
        /// The animals file info
        /// </summary>
        private readonly IFileInfo _fileInfo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileInfo">The animals file store</param>
        public AnimalRepository(IFileInfo fileInfo)
        {
            this._fileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
        }

        /// <summary>
        /// Find all zoo animals
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Animal> FindAll()
        {
            if (!_fileInfo.Exists)
            {
                throw new FileNotFoundException("Animals file not exists.");
            }

            using (var stream = _fileInfo.CreateReadStream())
            {
                var document = new XmlDocument();
                document.Load(stream);

                foreach (AnimalSpecieNames specie in Enum.GetValues(typeof(AnimalSpecieNames)))
                {
                    var items = document.GetElementsByTagName(specie.ToString());
                    foreach (XmlNode item in items)
                    {
                        var name = item.Attributes["name"].Value;
                        string kg = item.Attributes["kg"].Value;
                        yield return new Animal { Name = name, Weight = double.Parse(kg), Specie = specie };
                    }
                }
            }
        }
    }
}
