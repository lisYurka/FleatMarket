using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using System;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;

namespace FleatMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IDeclarationService declarationService;
        private readonly ICategoryService categoryService;
        private readonly IDeclarationStatusService declarStatService;
        private readonly IUserService userService;

        public IConfiguration Configuration { get; set; }

        public HomeController(ILogger<HomeController> _logger, IDeclarationService _declarationService,
            ICategoryService _categoryService, IDeclarationStatusService _declarStatService, IUserService _userService)
        {
            logger = _logger;
            declarationService = _declarationService;
            categoryService = _categoryService;
            declarStatService = _declarStatService;
            userService = _userService;
        }

        private List<OneDeclarationViewModel> GetDeclarationsOnPage(List<OneDeclarationViewModel> declars, int page = 1)
        {
            int postOnPage = Int16.Parse(Configuration["postOnPage"]);
            var itemsToSkip = page * postOnPage;
            return declars.Skip(itemsToSkip).Take(postOnPage).ToList();
        }

        [HttpGet]
        public IActionResult Index(int? id)
        {
            int page = id ?? 0; 
            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";

            List<OneDeclarationViewModel> declarations = new List<OneDeclarationViewModel>();
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            List<DeclarationStatusViewModel> statuses = new List<DeclarationStatusViewModel>();

            categoryService.GetAllCategories().ToList().ForEach(c => 
            {
                CategoryViewModel category = new CategoryViewModel
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                };
                categories.Add(category);
            });
            ViewBag.Categories = categories;

            declarStatService.GetAllStats().ToList().ForEach(ds => 
            {
                DeclarationStatusViewModel status = new DeclarationStatusViewModel
                {
                    Id = ds.Id,
                    StatusName = ds.StatusName
                };
                statuses.Add(status);
            });
            ViewBag.Statuses = statuses;

            declarationService.GetAllDeclarations().OrderByDescending(d => d.TimeOfCreation).ToList().ForEach(d =>
            {
                OneDeclarationViewModel declaration = new OneDeclarationViewModel
                {
                    AuthorId = d.UserId,
                    AuthorMail = d.User.Email,
                    CategoryId = d.CategoryId,
                    CategoryName = d.Category.CategoryName,
                    Date = d.TimeOfCreation,
                    Description = d.Description,
                    Id = d.Id,
                    StatusId = d.DeclarationStatusId,
                    StatusName = d.DeclarationStatus.StatusName,
                    Title = d.Title,
                    Price = d.Price,
                    ImagePath = d.Image.ImagePath
                };
                declarations.Add(declaration);
            });
            //if (isAjaxCall)
            //{
            //    return PartialView("/Views/Declaration/_OneDeclaration.cshtml", GetDeclarationsOnPage(declarations, page));
            //}
            return View(declarations);//View(GetDeclarationsOnPage(declarations, page));//View(declarations);
        }

        [HttpGet]
        public IActionResult SearchByCategory(int key)
        {
            List<OneDeclarationViewModel> declarations = new List<OneDeclarationViewModel>();
            var category = categoryService.GetCategoryById(key);
            declarationService.GetAllDeclarations().Where(r => r.CategoryId == category.Id).ToList().ForEach(d => 
            {
                OneDeclarationViewModel viewModel = new OneDeclarationViewModel
                {
                    AuthorId = d.UserId,
                    AuthorMail = d.User.Email,
                    CategoryId = d.CategoryId,
                    CategoryName = d.Category.CategoryName,
                    Date = d.TimeOfCreation,
                    Description = d.Description,
                    Id = d.Id,
                    StatusId = d.DeclarationStatusId,
                    StatusName = d.DeclarationStatus.StatusName,
                    Title = d.Title
                };
                declarations.Add(viewModel);
            });

            return PartialView("/Views/Declaration/_OneDeclaration.cshtml", declarations);//View("Index",declarations);//
        }
    }
}
