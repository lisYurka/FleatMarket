using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleatMarket.Base.Interfaces;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}