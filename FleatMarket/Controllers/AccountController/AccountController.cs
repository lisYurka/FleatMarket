using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FleatMarket.Base.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace FleatMarket.Web.Controllers.UserController
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(IUserService _userService, IRoleService _roleService, SignInManager<User> _signInManager,  
            UserManager<User> _userManager)
        {
            userService = _userService;
            roleService = _roleService;

            signInManager = _signInManager;
            userManager = _userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var redirectUrl = Url.Action("Index", "Home", new { ReturnUrl = returnUrl });
            LoginViewModel login = new LoginViewModel
            {
                ReturnUrl = redirectUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            login.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = userService.GetAllUsersWithRoles().FirstOrDefault(q => q.Email == login.EMail);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, login.Password, isPersistent: false, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(login.ReturnUrl) && Url.IsLocalUrl(login.ReturnUrl))
                            return LocalRedirect(login.ReturnUrl);
                        else
                            RedirectToAction("Login", "Account");
                    }
                }
            }
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string url)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = url });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string url = null, string error = null)
        {
            url = url ?? Url.Content("~/");

            LoginViewModel login = new LoginViewModel
            {
                ReturnUrl = url,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (error != null)
            {
                ViewBag.ErrorMessage("", $"Error from external provider - {error}");
                return View("Login",login);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();

            if(info == null)
            {
                ViewBag.ErrorMessage("", "Error loading login info!");
                return View("Login", login);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: false);

            if (signInResult.Succeeded)
                return LocalRedirect(url);

            else
            {
                var EMail = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (EMail != null)
                {
                    //var user = await userManager.FindByEmailAsync(EMail);
                    var user = userService.GetUserByEmail(EMail);
                    if (user == null)
                    {
                        Role role = roleService.GetRoles().FirstOrDefault(r => r.Name == "User");
                        user = new User
                        {
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
                            PhoneNumber = info.Principal.FindFirstValue(ClaimTypes.MobilePhone),
                            RoleId = role.Id
                        };

                        //await userManager.CreateAsync(user);
                        await userService.CreateUserAsync(user);
                    }
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(url);
                }
                ViewBag.ErrorTitle = $"EMail claim doesn't received from - {info.LoginProvider}";
                ViewBag.ErrorMessage = "Pleae contact support!";
                return View("Error");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                Role role = roleService.GetRoles().FirstOrDefault(r => r.Name == "User");
                var user = new User
                {
                    Email = register.EMail,
                    UserName = register.Name,
                    PhoneNumber = register.Phone,
                    RoleId = role.Id,
                    Name = register.Name,
                    Surname = register.Surname
                };

                var result = await userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login", "Account");
                else
                {
                    foreach (var item in result.Errors)
                        ModelState.AddModelError("", item.Description);
                }
            }
            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
    }
}