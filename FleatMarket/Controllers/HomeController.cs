using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;

namespace FleatMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;

        public HomeController(ILogger<HomeController> logger, IUserService _userService)
        {
            _logger = logger;
            userService = _userService;
        }

        [HttpGet]
        public IActionResult Index()
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
                    //Surname = u.Surname
                };
                showUsers.Add(userViewModel);
            });
            return View(showUsers);
        }
    }
}
