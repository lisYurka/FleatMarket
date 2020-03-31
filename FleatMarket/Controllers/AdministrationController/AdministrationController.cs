using System;
using System.Collections.Generic;
using System.Linq;
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
        public AdministrationController(IUserService _userService,
            IRoleService _roleService)
        {
            userService = _userService;
            roleService = _roleService;
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
                    Role = u.Role.RoleName
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
                RoleList = roles
            };

            return PartialView("_ChangeData",userViewModel);
        }

        [HttpPost]
        public IActionResult UpdateUser(UserViewModel userViewModel)
        {
            var role = roleService.GetRoles().Where(r => r.Id == userViewModel.Role).FirstOrDefault();
            var user = userService.GetUserByStringId(userViewModel.Id);

            user.Email = userViewModel.EMail;
            user.IsActive = userViewModel.IsActive;
            user.Name = userViewModel.Name;
            user.PhoneNumber = userViewModel.Phone;
            user.RoleId = userViewModel.Role;
            user.Surname = userViewModel.Surname;

            userViewModel.Role = role.RoleName;

            userService.UpdateUser(user);
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