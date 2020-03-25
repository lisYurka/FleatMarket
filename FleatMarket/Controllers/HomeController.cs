using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using FleatMarket.Service.Interfaces;
using FleatMarket.Base.Entities;

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
            userService.GetAllUsers().ToList().ForEach(u =>
            {
                User user = userService.GetUserById(u.Id);
                UserViewModel userViewModel = new UserViewModel
                {
                    EMail = u.EMail,
                    IsActive = u.IsActive,
                    Name = u.Name,
                    Phone = u.Phone,
                    Surname = u.Surname
                };
                showUsers.Add(userViewModel);
            });
            return View(showUsers);
        }
    }
}
