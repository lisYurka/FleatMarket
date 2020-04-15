using System;
using System.Collections.Generic;
using System.Linq;
using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FleatMarket.Web.Controllers
{
    public class DeclarationController : Controller
    {
        private readonly ILogger<DeclarationController> logger;
        private readonly ICategoryService categoryService;
        private readonly IDeclarationService declarationService;
        private readonly IDeclarationStatusService declarStatService;
        private readonly IUserService userService;
        private readonly IImageService imageService;

        public DeclarationController(ICategoryService _categoryService, IDeclarationService _declarationService,
            IDeclarationStatusService _declarStatService, IUserService _userService, IImageService _imageService,
             ILogger<DeclarationController> _logger)
        {
            categoryService = _categoryService;
            declarationService = _declarationService;
            declarStatService = _declarStatService;
            userService = _userService;
            imageService = _imageService;
            logger = _logger;
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
        [Authorize]
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
        [Authorize]
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
                Id = declaration.Id,
                ImagePath = declaration.Image.ImagePath
            };
            return View(viewModel);
        }

        [HttpPost]
        public void RemoveDeclaration(int id)
        {
            var status = declarStatService.GetAllStats().Single(s => s.StatusName == "Удалено");
            var declaration = declarationService.GetDeclarationById(id);
            logger.LogInformation($"User '{User.Identity.Name}' changed declaration's status from '{declaration.DeclarationStatus.StatusName}' to 'Удалено'.");
            declaration.DeclarationStatusId = status.Id;
            declarationService.UpdateDeclaration(declaration); 
        }

        [HttpPost]
        public void RemoveDeclarationFromDb(int id_db)
        {
            var declaration = declarationService.GetDeclarationById(id_db);
            declarationService.RemoveDeclaration(id_db);
            logger.LogInformation($"Admin '{User.Identity.Name}' removed declaration '{declaration.Title}' with id = {declaration.Id} from database.");
        }

        [HttpPost]
        public void SoldDeclaration(int id)
        {
            var status = declarStatService.GetAllStats().Single(s => s.StatusName == "Продано");
            var declaration = declarationService.GetDeclarationById(id);
            logger.LogInformation($"User '{User.Identity.Name}' changed declaration's status from '{declaration.DeclarationStatus.StatusName}' to 'Продано'.");
            declaration.DeclarationStatusId = status.Id;
            declarationService.UpdateDeclaration(declaration);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(AddDeclarationViewModel addDeclaration, int category, string declarImgPath)
        {
            int imgId;
            if (declarImgPath == null)
                imgId = 1;
            else
                imgId = imageService.GetImageId(declarImgPath);

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
            addDeclaration.Categories = categories;

            if (ModelState.IsValid)
            {
                var declaration = new Declaration
                {
                    CategoryId = category,
                    DeclarationStatusId = 1,
                    Description = addDeclaration.Description,
                    TimeOfCreation = DateTime.Now,
                    Title = addDeclaration.Title,
                    UserId = addDeclaration.AuthorId,
                    Price = addDeclaration.Price,
                    ImageId = imgId
                };
                declarationService.AddDeclaration(declaration);
                logger.LogInformation($"User '{User.Identity.Name}' add new declaration with title '{declaration.Title}'.");
                return RedirectToAction("Index", "Home");
            }
            return View(addDeclaration);
        }

        [HttpPost]
        public IActionResult UpdateDeclaration(AddDeclarationViewModel viewModel, int id_for_declar, int category, string declarImgPath)
        {
            int imgId;
            if (declarImgPath == null)
                imgId = 1;
            else
                imgId = imageService.GetImageId(declarImgPath);

            Declaration old_declaration = declarationService.GetDeclarationById(id_for_declar);
            if (old_declaration != null)
            {
                if (old_declaration.Price != viewModel.Price)
                    logger.LogInformation($"User '{User.Identity.Name}' changed price " +
                        $"from {old_declaration.Price} BYN to {viewModel.Price} BYN in declaration with id '{old_declaration.Id}'.");
                if (old_declaration.CategoryId != category)
                    logger.LogInformation($"User '{User.Identity.Name}' changed category " +
                        $"from '{old_declaration.CategoryId}' to '{category}' in declaration with id '{old_declaration.Id}'.");
                if (old_declaration.Description != viewModel.Description)
                    logger.LogInformation($"User '{User.Identity.Name}' changed description " +
                        $"from '{old_declaration.Description}' to '{viewModel.Description}' in declaration with id '{old_declaration.Id}'.");
                if (old_declaration.Title != viewModel.Title)
                    logger.LogInformation($"User '{User.Identity.Name}' changed title " +
                        $"from '{old_declaration.Title}' to '{viewModel.Title}' in declaration with id '{old_declaration.Id}'.");
                if (old_declaration.ImageId != imgId)
                    logger.LogInformation($"User '{User.Identity.Name}' changed image " +
                        $"in declaration with id '{old_declaration.Id}'.");

                if (old_declaration.CategoryId != category)
                    old_declaration.CategoryId = category;
                old_declaration.Description = viewModel.Description;
                old_declaration.Price = viewModel.Price;
                old_declaration.Title = viewModel.Title;
                old_declaration.ImageId = imgId;
                declarationService.UpdateDeclaration(old_declaration);
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult OpenDeclaration(int id)
        {
            var declaration = declarationService.GetDeclarationById(id);
            var category = categoryService.GetCategoryById(declaration.CategoryId);
            var status = declarStatService.GetStatusById(declaration.DeclarationStatusId);
            var user = userService.GetUserByStringId(declaration.UserId);

            UserViewModel userView = new UserViewModel
            {
                EMail = user.Email,
                ImagePath = user.Image.ImagePath,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Surname = user.Surname
            };
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
                Title = declaration.Title,
                ImagePath = declaration.Image.ImagePath,
                User = userView
            };
            return View(viewModel);
        }
    }
}