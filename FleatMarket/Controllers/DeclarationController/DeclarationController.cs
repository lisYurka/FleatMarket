using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FleatMarket.Web.Controllers.DeclarationController
{
    public class DeclarationController : Controller
    {
        private readonly ICategoryService categoryService;

        public DeclarationController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public IActionResult Create()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            categoryService.GetAllCategories().ToList().ForEach(c =>
            {
                CategoryViewModel category = new CategoryViewModel
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                };
                categories.Add(category);
            });

            AddDeclarationViewModel model = new AddDeclarationViewModel
            {
                Categories = categories
            };

            return View(model);
        }
    }
}