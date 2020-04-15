using System.Linq;
using System.Threading.Tasks;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FleatMarket.Base.Entities;
using System.Security.Claims;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;

namespace FleatMarket.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly ILogger<AccountController> logger;
        private readonly IStringLocalizer<AccountController> localizer;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(IUserService _userService, IRoleService _roleService, SignInManager<User> _signInManager,  
            UserManager<User> _userManager, ILogger<AccountController> _logger, IStringLocalizer<AccountController> _localizer)
        {
            userService = _userService;
            roleService = _roleService;
            logger = _logger;
            localizer = _localizer;

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
                        {
                            logger.LogInformation($"User '{user.Email}' logged in.");
                            return LocalRedirect(login.ReturnUrl);
                        }
                        else
                            RedirectToAction("Login", "Account");
                    }
                    else
                        ViewBag.PassError = localizer["PasswordError"];
                }
                else
                    ViewBag.MailError = localizer["MailError"];
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
                ViewBag.ErrorMessage = $"Error from external provider - {error}";
                logger.LogInformation($"Error from external provider - {error}");
                return View("Login",login);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                string mes = localizer["LoginError"];
                ViewBag.ErrorMessage = mes;
                logger.LogInformation(mes);
                return View("Login", login);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: false);
            if (signInResult.Succeeded)
            {
                logger.LogInformation($"User '{info.Principal.FindFirstValue(ClaimTypes.Email)}' logged in.");
                return LocalRedirect(url);
            }
            else
            {
                var EMail = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (EMail != null)
                {
                    var user = userService.GetUserByEmail(EMail);
                    if (user == null)
                    {
                        Role role = roleService.GetRoles().FirstOrDefault(r => r.Name == "User");
                        user = new User
                        {
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Name = info.Principal.FindFirstValue(ClaimTypes.Name),
                            Surname = info.Principal.FindFirstValue(ClaimTypes.Surname),
                            PhoneNumber = info.Principal.FindFirstValue(ClaimTypes.MobilePhone),
                            RoleId = role.Id,
                            ImageId = 2,
                            LastEditDate = DateTime.Now.ToString(),
                            RegistrationDate = DateTime.Now.ToString()
                        };

                        var roleClaim = new Claim(ClaimTypes.Role.ToString(), role.Name);
                        await userManager.CreateAsync(user);
                        await userManager.AddClaimAsync(user, roleClaim);
                        logger.LogInformation($"User '{user.Email}' has been registered.");
                    }
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    logger.LogInformation($"User '{user.Email}' logged in.");
                    return LocalRedirect(url);
                }
                var mess_title = localizer["MailClaimErrorTitle"] + info.LoginProvider + ".";
                var mess = localizer["MailClaimErrorMess"];
                ViewBag.ErrorTitle = mess_title;
                logger.LogInformation(mess_title + " " + mess);
                ViewBag.ErrorMessage = mess;
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
                var mail = userService.GetAllUsersWithRoles().Where(u => u.Email == register.EMail).FirstOrDefault();
                if (mail == null)
                {
                    ViewBag.ErrorEMail = "";
                    Role role = roleService.GetRoles().FirstOrDefault(r => r.Name == "User");
                    var user = new User
                    {
                        Email = register.EMail,
                        UserName = register.EMail,
                        PhoneNumber = register.Phone,
                        RoleId = role.Id,
                        Name = register.Name,
                        Surname = register.Surname,
                        ImageId = 2,
                        LastEditDate = DateTime.Now.ToString(),
                        RegistrationDate = DateTime.Now.ToString()
                    };

                    var result = await userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), role.Name));
                        logger.LogInformation($"User '{user.Email}' has been registered.");
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                            ModelState.AddModelError("", item.Description);
                    }
                }
                else
                {
                    ViewBag.ErrorEMail = localizer["NewUserMailError"];
                    logger.LogInformation($"User '{register.EMail}' tried to register with existing mail.");
                }
            }
            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            logger.LogInformation($"User '{User.Identity.Name}' logged out.");
            return RedirectToAction("Index","Home");
        }
    }
}