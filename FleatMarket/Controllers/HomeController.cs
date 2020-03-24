﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FleatMarket.Models;
using FleatMarket.Model.Interface;
using FleatMarket.ViewModel;

namespace FleatMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository userRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository _userRepository)
        {
            _logger = logger;
            userRepository = _userRepository;
        }

        public IActionResult Index()
        {
            var users = userRepository.GetAllUsers();
            List<UserViewModel> showUsers = new List<UserViewModel>();
            foreach(var item in users)
            {
                UserViewModel user = new UserViewModel
                {
                    EMail = item.EMail,
                    Id = item.Id,
                    IsActive = item.IsActive,
                    Name = item.Name,
                    Password = item.Password,
                    Phone = item.Phone,
                    Surname = item.Surname
                };
                showUsers.Add(user);
            }
            return View(showUsers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
