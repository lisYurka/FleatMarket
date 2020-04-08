using System;
using System.Collections.Generic;
using System.Linq;
using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FleatMarket.Web.Controllers.DeclarationController
{
    public class DeclarationController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IDeclarationService declarationService;
        private readonly IDeclarationStatusService declarStatService;
        private readonly IUserService userService;

        public DeclarationController(ICategoryService _categoryService, IDeclarationService _declarationService,
            IDeclarationStatusService _declarStatService, IUserService _userService)
        {
            categoryService = _categoryService;
            declarationService = _declarationService;
            declarStatService = _declarStatService;
            userService = _userService;
        }

        private User FindDeclarationAuthor(string mail)
        {
            if (!string.IsNullOrWhiteSpace(mail))
            {
                var result = userService.GetAllUsersWithRoles().SingleOrDefault(i => i.Email == mail);
                if (result != null)
                    return result;
                else return null;
            }
            else return null;
        }

        [HttpGet]
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

            var author = FindDeclarationAuthor(User.Identity.Name);
            AddDeclarationViewModel model = new AddDeclarationViewModel
            {
                Categories = categories,
                AuthorId = author.Id,
                AuthorMail = author.Email,
                AuthorPhone = author.PhoneNumber,
                AuthorName = author.Name
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int declarId)
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
            var declaration = declarationService.GetDeclarationById(declarId);

            AddDeclarationViewModel viewModel = new AddDeclarationViewModel
            {
                AuthorId = declaration.UserId,
                AuthorMail = declaration.User.Email,
                AuthorName = declaration.User.Name,
                AuthorPhone = declaration.User.PhoneNumber,
                CategoryId = declaration.CategoryId,
                Description = declaration.Description,
                Price = declaration.Price,
                Title = declaration.Title,
                Categories = categories,
                Id = declaration.Id
            };
            return View(viewModel);
        }

        [HttpPost]
        public void RemoveDeclaration(int id)
        {
            var status = declarStatService.GetAllStats().Single(s => s.StatusName == "Удалено");
            var declaration = declarationService.GetDeclarationById(id);
            declaration.DeclarationStatusId = status.Id;
            declarationService.UpdateDeclaration(declaration);
        }

        [HttpPost]
        public void RemoveDeclarationFromDb(int id_db)
        {
            declarationService.RemoveDeclaration(id_db);
        }

        [HttpPost]
        public void SoldDeclaration(int id)
        {
            var status = declarStatService.GetAllStats().Single(s => s.StatusName == "Продано");
            var declaration = declarationService.GetDeclarationById(id);
            declaration.DeclarationStatusId = status.Id;
            declarationService.UpdateDeclaration(declaration);
        }

        [HttpPost]
        public IActionResult AddDeclaration(AddDeclarationViewModel addDeclaration, int category)
        {
            if (ModelState.IsValid)
            {
                var declaration = new Declaration
                {
                    CategoryId = category,
                    DeclarationStatusId = 1,
                    Description = addDeclaration.Description,
                    TimeOfCreation = DateTime.Now,
                    Title = addDeclaration.Title,
                    UserId = addDeclaration.AuthorId,//"2f8deb5e-7794-4224-8e34-daef110f826f"
                    Price = addDeclaration.Price
                };
                declarationService.AddDeclaration(declaration);
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public IActionResult UpdateDeclaration(AddDeclarationViewModel viewModel, int id_for_declar, int category)
        {
            Declaration old_declaration = declarationService.GetDeclarationById(id_for_declar);
            if (old_declaration != null)
            {
                old_declaration.Price = viewModel.Price;
                if (old_declaration.CategoryId != category)
                    old_declaration.CategoryId = category;
                old_declaration.Description = viewModel.Description;
                old_declaration.Price = viewModel.Price;
                old_declaration.Title = viewModel.Title;
                declarationService.UpdateDeclaration(old_declaration);
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult OpenDeclaration(int id)
        {
            var declaration = declarationService.GetDeclarationById(id);
            var category = categoryService.GetCategoryById(declaration.CategoryId);
            var status = declarStatService.GetStatusById(declaration.DeclarationStatusId);
            var user = userService.GetUserByStringId(declaration.UserId);

            OneDeclarationViewModel viewModel = new OneDeclarationViewModel
            {
                Id = declaration.Id,
                AuthorMail = user.Email,
                CategoryName = category.CategoryName,
                CategoryId = category.Id,
                Date = declaration.TimeOfCreation,
                Description = declaration.Description,
                Price = declaration.Price,
                StatusName = status.StatusName,
                StatusId = status.Id,
                Title = declaration.Title
            };

            return View(viewModel);
        }
    }
}