using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FleatMarket.Service.BusinessLogic
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository repository;

        public CategoryService(IBaseRepository _repository)
        {
            repository = _repository;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return repository.GetAll<Category>();
        }

        public Category GetCategoryById(int id)
        {
            var result = repository.GetAll<Category>().Single(cat => cat.Id == id);
            return result;
        }
    }
}