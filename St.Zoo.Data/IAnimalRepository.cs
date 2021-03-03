using St.Zoo.Models;
using System.Collections.Generic;

namespace St.Zoo.Data
{
    public interface IAnimalRepository
    {
        IEnumerable<Animal> FindAll();
    }
}