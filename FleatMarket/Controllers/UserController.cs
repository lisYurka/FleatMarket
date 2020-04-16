using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FleatMarket.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IDeclarationService declarationService;
        private readonly IImageService imageService;
        private readonly UserManager<User> userManager;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService _userService, IDeclarationService _declarationService,
            IImageService _imageService, ILogger<UserController> _logger, UserManager<User> _userManager)
        {
            userService = _userService;
            declarationService = _declarationService;
            imageService = _imageService;
            logger = _logger;
            userManager = _userManager;
        }

        [Authorize]
        public IActionResult UserArea()
        {
            var userDeclarations = declarationService.GetAllDeclarations().Where(d => d.User.Email == User.Identity.Name);
            var currentUser = userService.GetUserByEmail(User.Identity.Name);
            var soldDeclarats = userDeclarations.Count(d => d.DeclarationStatusId == 2);
            PersonalAreaViewModel model = new PersonalAreaViewModel
            {
                ImagePath = currentUser.Image.ImagePath,
                AllDeclarationsCount = userDeclarations.Count(),
                UserName = currentUser.Name,
                SoldDeclarationsCount = soldDeclarats,
                LastDateOfEdit = currentUser.LastEditDate,
                RegistrationDate = currentUser.RegistrationDate
            };
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUserDeclarations()
        {
            var user = userService.GetUserByEmail(User.Identity.Name);
            List<OneDeclarationViewModel> viewModel = new List<OneDeclarationViewModel>();
            var declarations = declarationService.GetAllDeclarations().Where(d => d.UserId == user.Id && 
                (d.DeclarationStatusId == 1 || d.DeclarationStatusId == 2)).ToList();
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
                    Title = q.Title,
                    ImagePath = q.Image.ImagePath
                };
                viewModel.Add(model);
            });
            if (viewModel.Count == 0)
                ViewBag.Nothing = "Ничего не найдено!";
            else
                ViewBag.Nothing = "";
            return PartialView("_UserDeclarations", viewModel);
        }

        [HttpGet]
        [Authorize]
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
                Surname = user.Surname,
                ImagePath = user.Image.ImagePath
            };
            return PartialView("_UserProfile", viewModel);
        }

        [HttpPost]
        public void UpdateUser(UserViewModel user)
        {
            var u = userService.GetUserByEmail(User.Identity.Name);

            if (u.PhoneNumber != user.Phone)
                logger.LogInformation($"User '{User.Identity.Name}' changed mobile number from '{u.PhoneNumber}' to '{user.Phone}'.");
            if (u.Name != user.Name)
                logger.LogInformation($"User '{User.Identity.Name}' changed own name from '{u.Name}' to '{user.Name}'.");
            if (u.Surname != user.Surname)
                logger.LogInformation($"User '{User.Identity.Name}' changed own surname from '{u.Surname}' to '{user.Surname}'.");

            u.LastEditDate = user.LastEditTime;
            u.PhoneNumber = user.Phone;
            u.Name = user.Name;
            u.Surname = user.Surname;
            userService.UpdateUser(u);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
                    Title = d.Title,
                    ImagePath = d.Image.ImagePath
                };
                viewModel.Add(oneDeclaration);
            }); 
            if (viewModel.Count == 0)
                ViewBag.Nthng = "Ничего не найдено!";
            else
                ViewBag.Nthng = "";
            return PartialView("_RemovedDeclars", viewModel);
        }

        [HttpPost]
        public void UpdateProfileImage(UserViewModel user)
        {
            var u = userService.GetUserByStringId(user.Id);
            var imageId = imageService.GetImageId(user.ImagePath);

            if (u.ImageId != imageId)
                logger.LogInformation($"User '{User.Identity.Name}' changed profile photo.");

            u.ImageId = imageId;
            userService.UpdateUser(u);
        }

        [HttpPost]
        public async Task ChangePassword(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    var passwordValidator = HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var passwordHasher = HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    var result = await passwordValidator.ValidateAsync(userManager, user, model.ChangePassword.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, model.ChangePassword.NewPassword);
                        await userManager.UpdateAsync(user);
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                            ModelState.AddModelError("", item.Description);
                    }
                }
                else ModelState.AddModelError("", "Пользователь не найден!");
            }
        }
    }
}