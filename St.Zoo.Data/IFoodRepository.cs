﻿using St.Zoo.Models;
using System.Collections.Generic;

namespace St.Zoo.Data
{
    public interface IFoodRepository
    {
        IDictionary<FoodCategory, double> FindAll();
    }
}