using FleatMarket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleatMarket.Base.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
    }
}
