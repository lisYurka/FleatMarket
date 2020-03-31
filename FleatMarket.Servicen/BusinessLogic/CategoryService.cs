using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleatMarket.Service.BusinessLogic
{
    public class CategoryService:ICategoryService
    {
        private readonly IBaseRepository repository;

        public CategoryService(IBaseRepository _repository)
        {
            repository = _repository;
        }

        public IEnumerable<Category> GetAllCategories() {
            return repository.GetAll<Category>();
        }
    }
}
