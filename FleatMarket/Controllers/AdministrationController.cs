using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IImageService imageService;
        private readonly ILogger<AdministrationController> logger;

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AdministrationController(IUserService _userService, IImageService _imageService,
            IRoleService _roleService, UserManager<User> _userManager, SignInManager<User> _signInManager,
            ILogger<AdministrationController> _logger)
        {
            userService = _userService;
            roleService = _roleService;
            imageService = _imageService;
            userManager = _userManager;
            signInManager = _signInManager;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult AllUsers()
        {
            List<UserViewModel> showUsers = new List<UserViewModel>();
            userService.GetAllUsersWithRoles().ToList().ForEach(u =>
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    Id = u.Id,
                    IsActive = u.IsActive,
                    EMail = u.Email,
                    Name = u.Name,
                    Surname = u.Surname,
                    Phone = u.PhoneNumber,
                    Role = u.Role.RoleName,
                    ImagePath = u.Image.ImagePath
                };
                showUsers.Add(userViewModel);
            });
            return View(showUsers);
        }

        public void RemoveUser(string id)
        {
            if (id != null)
            {
                userService.RemoveUser(id);
                logger.LogInformation($"Admin '{User.Identity.Name}' deleted user with id = '{id}' from database.");
            }
            else ViewBag.ErrorMessage = "Невозможно удалить пользователя!";
        }

        [HttpPost]
        public IActionResult UpdateUserInput(string id)
        {
            var user = userService.GetUserByStringId(id);
            var getRoles = roleService.GetRoles();

            List<UserRoleViewModel> roles = new List<UserRoleViewModel>();

            foreach(var item in getRoles)
            {
                UserRoleViewModel role = new UserRoleViewModel
                {
                    RoleId = item.Id,
                    RoleName = item.RoleName
                };
                roles.Add(role);
            }

            UserViewModel userViewModel = new UserViewModel
            {
                Id = user.Id,
                EMail = user.Email,
                IsActive = user.IsActive,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Role = user.Role.RoleName,
                Surname = user.Surname,
                RoleList = roles,
                ImagePath = user.Image.ImagePath
            };

            return PartialView("_ChangeData",userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserViewModel userViewModel)
        {
            var role = roleService.GetRoles().Where(r => r.Id == userViewModel.Role).FirstOrDefault();
            var user = await userManager.FindByIdAsync(userViewModel.Id);
            var img = imageService.GetImageId(userViewModel.ImagePath);

            if (user.Name != userViewModel.Name)
                logger.LogInformation($"Admin '{User.Identity.Name}' changed name of user with id '{user.Id}' from '{user.Name}' to '{userViewModel.Name}'.");
            if (user.PhoneNumber != userViewModel.Phone)
                logger.LogInformation($"Admin '{User.Identity.Name}' changed phone of user with id '{user.Id}' from '{user.PhoneNumber}' to '{userViewModel.Phone}'.");
            if (user.RoleId != userViewModel.Role)
                logger.LogInformation($"Admin '{User.Identity.Name}' changed role of user with id '{user.Id}' from '{user.RoleId}' to '{userViewModel.Role}'.");
            if (user.Surname != userViewModel.Surname)
                logger.LogInformation($"Admin '{User.Identity.Name}' changed surname of user with id '{user.Id}' from '{user.Surname}' to '{userViewModel.Surname}'.");

            user.Name = userViewModel.Name;
            user.PhoneNumber = userViewModel.Phone;
            user.RoleId = userViewModel.Role;
            user.Surname = userViewModel.Surname;
            userViewModel.Role = role.Name;

            var userRole = userService.GetUserRole(user.Id);
            await userManager.ReplaceClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), userRole), new Claim(ClaimTypes.Role.ToString(), role.Name));
            await userManager.UpdateAsync(user);

            return PartialView("_User", userViewModel);
        }

        public string ChangeUserStatus(string id)
        {
            var user = userService.GetUserByStringId(id);
            if (user.IsActive)
            {
                user.IsActive = false;
                userService.UpdateUser(user);
                logger.LogInformation($"Admin '{User.Identity.Name}' changed activity of user with id '{user.Id}' from 'Активный' to 'Заблокирован'.");
                return "Заблокирован";
            }
            else
            {
                user.IsActive = true;
                userService.UpdateUser(user);
                logger.LogInformation($"Admin '{User.Identity.Name}' changed activity of user with id '{user.Id}' from 'Заблокирован' to 'Активный'.");
                return "Активный";
            }
        }
    }
}