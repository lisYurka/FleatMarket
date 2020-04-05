using FleatMarket.Base.Entities;
using System.Collections.Generic;

namespace FleatMarket.Base.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
    }
}
