using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleatMarket.Web.Controllers.AdministrationController
{
    public class AdministrationController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IImageService imageService;

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AdministrationController(IUserService _userService, IImageService _imageService,
            IRoleService _roleService, UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userService = _userService;
            roleService = _roleService;
            imageService = _imageService;
            userManager = _userManager;
            signInManager = _signInManager;
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
                    Name = u.UserName,
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
                userService.RemoveUser(id);

            else ViewBag.ErrorMessage = "Can't delete user!";
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
            var user = userService.GetWithRoleByStringId(userViewModel.Id);
            var img = imageService.GetImageId(userViewModel.ImagePath);

            user.Email = userViewModel.EMail;
            user.IsActive = userViewModel.IsActive;
            user.Name = userViewModel.Name;
            user.PhoneNumber = userViewModel.Phone;
            user.RoleId = userViewModel.Role;
            user.Surname = userViewModel.Surname;
            user.ImageId = img;

            userViewModel.Role = role.RoleName;

            userService.UpdateUser(user);
            //await userManager.UpdateAsync(user);
            //await userManager.RemoveClaimAsync(user, new ClaimsIdentity(User.Identity).FindFirst(ClaimTypes.Role.ToString()));
            //var claims = await userManager.GetClaimsAsync(user);
            //var roleClaim = claims.Where(c => c.Type == ClaimTypes.Role.ToString()).FirstOrDefault();
            //if(roleClaim != null)
            //{
            //    var result = await userManager.RemoveClaimAsync(user, roleClaim);
            //    if (result.Succeeded)
            //    {

            //    }
            //}
            //await userManager.RemoveClaimAsync(user, new ClaimsIdentity(User.Identity).FindFirst(ClaimTypes.Role.ToString())); 
            //await userManager.ReplaceClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), user.Role.Name), new Claim(ClaimTypes.Role.ToString(), role.RoleName));
            return PartialView("_User", userViewModel);
        }

        public string ChangeUserStatus(string id)
        {
            var user = userService.GetUserByStringId(id);
            if (user.IsActive)
            {
                user.IsActive = false;
                userService.UpdateUser(user);
                return "Заблокирован";
            }
            else
            {
                user.IsActive = true;
                userService.UpdateUser(user);
                return "Активный";
            }
        }
    }
}