using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FleatMarket.Web.Controllers.UserController
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IDeclarationService declarationService;

        public UserController(IUserService _userService, IDeclarationService _declarationService)
        {
            userService = _userService;
            declarationService = _declarationService;
        }

        public IActionResult UserArea(int userAction)
        {
            return View(userAction);
        }

        [HttpGet]
        public IActionResult GetUserDeclarations()
        {
            var user = userService.GetUserByEmail(User.Identity.Name);
            List<OneDeclarationViewModel> viewModel = new List<OneDeclarationViewModel>();
            var declarations = declarationService.GetAllDeclarations().Where(d => d.UserId == user.Id).ToList();
            declarations.ForEach(q => {
                OneDeclarationViewModel model = new OneDeclarationViewModel
                {
                    AuthorMail = user.Email,
                    CategoryName = q.Category.CategoryName,
                    CategoryId = q.CategoryId,
                    Date = q.TimeOfCreation,
                    Id = q.Id,
                    Price = q.Price,
                    StatusName = q.DeclarationStatus.StatusName,
                    StatusId = q.DeclarationStatusId,
                    Title = q.Title
                };
                viewModel.Add(model);
            });

            return PartialView("_UserDeclarations", viewModel);
        }

        [HttpGet]
        public IActionResult MyProfile()
        {
            var user = userService.GetUserByEmail(User.Identity.Name);
            UserViewModel viewModel = new UserViewModel
            {
                EMail = user.Email,
                Id = user.Id,
                IsActive = user.IsActive,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Role = user.Role.Name,
                Surname = user.Surname
            };
            return PartialView("_UserProfile", viewModel);
        }

        [HttpPost]
        public void UpdateUser(UserViewModel user)
        {
            var u = userService.GetUserByEmail(User.Identity.Name);
            u.Email = user.EMail;
            u.PhoneNumber = user.Phone;
            u.Name = user.Name;
            u.Surname = user.Surname;

            userService.UpdateUser(u);
        }

        [HttpGet]
        public IActionResult RemovedDeclarations()
        {
            var declarations = declarationService.GetAllDeclarations().Where(q => q.DeclarationStatusId == 3).ToList();

            List<OneDeclarationViewModel> viewModel = new List<OneDeclarationViewModel>();
            declarations.ForEach(d =>
            {
                OneDeclarationViewModel oneDeclaration = new OneDeclarationViewModel
                {
                    AuthorId = d.UserId,
                    AuthorMail = d.User.Email,
                    CategoryId = d.CategoryId,
                    CategoryName = d.Category.CategoryName,
                    Date = d.TimeOfCreation,
                    Description = d.Description,
                    Id = d.Id,
                    Price = d.Price,
                    StatusId = d.DeclarationStatusId,
                    StatusName = d.DeclarationStatus.StatusName,
                    Title = d.Title
                };
                viewModel.Add(oneDeclaration);
            });
            return PartialView("_RemovedDeclars", viewModel);
        }
    }
}