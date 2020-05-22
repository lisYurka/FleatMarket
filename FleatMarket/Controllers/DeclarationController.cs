using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using FleatMarket.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace FleatMarket.Web.Controllers
{
    public class DeclarationController : Controller
    {
        private readonly ILogger<DeclarationController> logger;
        private readonly ICategoryService categoryService;
        private readonly IDeclarationService declarationService;
        private readonly IDeclarationStatusService declarStatService;
        private readonly IUserService userService;
        private readonly IImageService imageService;
        private readonly INotificationService notificationService;

        public DeclarationController(ICategoryService _categoryService, IDeclarationService _declarationService,
            IDeclarationStatusService _declarStatService, IUserService _userService, IImageService _imageService,
             INotificationService _notificationService, ILogger<DeclarationController> _logger)
        {
            categoryService = _categoryService;
            declarationService = _declarationService;
            declarStatService = _declarStatService;
            userService = _userService;
            imageService = _imageService;
            notificationService = _notificationService;
            logger = _logger;
        }

        private User FindDeclarationAuthor(string mail)
        {
            if (!string.IsNullOrWhiteSpace(mail))
            {
                var result = userService.GetAllUsersWithRoles().SingleOrDefault(i => i.Email == mail);
                if (result != null)
                    return result;
                else return null;
            }
            else return null;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            categoryService.GetAllCategories().ToList().ForEach(c =>
            {
                CategoryViewModel category = new CategoryViewModel
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                };
                categories.Add(category);
            });

            var author = FindDeclarationAuthor(User.Identity.Name);
            AddDeclarationViewModel model = new AddDeclarationViewModel
            {
                Categories = categories,
                AuthorId = author.Id,
                AuthorMail = author.Email,
                AuthorPhone = author.PhoneNumber,
                AuthorName = author.Name
            };

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int declarId)
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            categoryService.GetAllCategories().ToList().ForEach(c =>
            {
                CategoryViewModel category = new CategoryViewModel
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                };
                categories.Add(category);
            });
            var declaration = declarationService.GetDeclarationById(declarId);

            AddDeclarationViewModel viewModel = new AddDeclarationViewModel
            {
                AuthorId = declaration.UserId,
                AuthorMail = declaration.User.Email,
                AuthorName = declaration.User.Name,
                AuthorPhone = declaration.User.PhoneNumber,
                CategoryId = declaration.CategoryId,
                Description = declaration.Description,
                Price = declaration.Price,
                Title = declaration.Title,
                Categories = categories,
                Id = declaration.Id,
                ImagePath = declaration.Image.ImagePath
            };
            return View(viewModel);
        }

        [HttpPost]
        public void RemoveDeclaration(int id)
        {
            var status = declarStatService.GetAllStats().Single(s => s.StatusName == "Удалено");
            var declaration = declarationService.GetDeclarationById(id);
            logger.LogInformation($"User '{User.Identity.Name}' changed declaration's status from '{declaration.DeclarationStatus.StatusName}' to 'Удалено'.");
            declaration.DeclarationStatusId = status.Id;
            declarationService.UpdateDeclaration(declaration); 
        }

        [HttpPost]
        public void RemoveDeclarationFromDb(int id_db)
        {
            var declaration = declarationService.GetDeclarationById(id_db);
            declarationService.RemoveDeclaration(id_db);
            logger.LogInformation($"Admin '{User.Identity.Name}' removed declaration '{declaration.Title}' with id = {declaration.Id} from database.");
        }

        [HttpPost]
        public void SoldDeclaration(int id)
        {
            var status = declarStatService.GetAllStats().Single(s => s.StatusName == "Продано");
            var declaration = declarationService.GetDeclarationById(id);
            logger.LogInformation($"User '{User.Identity.Name}' changed declaration's status from '{declaration.DeclarationStatus.StatusName}' to 'Продано'.");
            declaration.DeclarationStatusId = status.Id;
            declarationService.UpdateDeclaration(declaration);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(AddDeclarationViewModel addDeclaration, int category, string declarImgPath)
        {
            int imgId;
            if (declarImgPath == null)
                imgId = 1;
            else
                imgId = imageService.GetImageId(declarImgPath);

            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            categoryService.GetAllCategories().ToList().ForEach(c =>
            {
                CategoryViewModel category = new CategoryViewModel
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                };
                categories.Add(category);
            });
            addDeclaration.Categories = categories;

            if (ModelState.IsValid)
            {
                var declaration = new Declaration
                {
                    CategoryId = category,
                    DeclarationStatusId = 1,
                    Description = addDeclaration.Description,
                    TimeOfCreation = DateTime.Now,
                    Title = addDeclaration.Title,
                    UserId = addDeclaration.AuthorId,
                    Price = addDeclaration.Price,
                    ImageId = imgId
                };

                ///////////часть с питоном
                if (declaration.CategoryId != 5 && imgId != 1) { //не проверяем для категории "В дар" и объявления без фоток
                    var res = Run(@"..\FleatMarket\wwwroot\python\predict.py",
                        @$"..\FleatMarket\wwwroot{declarImgPath}",
                        @"..\FleatMarket\wwwroot\python\checkpoint.pth");
                    string[] parsedResult = res.Split("\n");
                    var toRemove = new string[] { "[", "]"};
                    string top_3_ctgs_toler = parsedResult[2];
                    foreach(var c in toRemove)
                    {
                        top_3_ctgs_toler = top_3_ctgs_toler.Replace(c,string.Empty);
                    }
                    string[] tolerance = top_3_ctgs_toler.Split(" ");
                    double firstToler = Convert.ToDouble(tolerance[0].Replace(".",","));
                    if (firstToler > 0.75)
                    {
                        string top_3_categs = parsedResult[1];
                        var charsToREmove = new string[] { "'", "[", "]", " " };
                        foreach (var c in charsToREmove)
                        {
                            top_3_categs = top_3_categs.Replace(c, string.Empty);
                        }
                        string[] categs = top_3_categs.Split(",");
                        int categId = parseJsonWithCategs(@"..\FleatMarket\wwwroot\python\categs_subcategs.json", categs[0]);

                        if (categId != declaration.CategoryId)
                        {
                            declaration.CategoryId = categId;
                            var categName = categoryService.GetCategoryById(declaration.CategoryId);
                            Notification notification = new Notification
                            {
                                Message = $"В объявлении '{declaration.Title}' была изменена категория на '{categName.CategoryName}'!",
                                UserId = declaration.UserId
                            };
                            var user = userService.GetUserByEmail(User.Identity.Name);
                            user.NotifCount++;
                            userService.UpdateUser(user);

                            notificationService.AddNotification(notification);
                        }
                    }
                    //если порог фотки не пройден, то сделать отсылание админу
                }
                ///////////

                declarationService.AddDeclaration(declaration);
                logger.LogInformation($"User '{User.Identity.Name}' add new declaration with title '{declaration.Title}'.");
                return RedirectToAction("Index", "Home");
            }
            return View(addDeclaration);
        }

        private int parseJsonWithCategs(string path, string predictedCateg)
        {
            var json = System.IO.File.ReadAllText($"{path}");
            var objs = JObject.Parse(json);
            int result = -1;
            foreach (KeyValuePair<string, JToken> app in objs)
            {
                string name = app.Key;
                if (predictedCateg == name)
                    result = Convert.ToInt32(app.Value);
            }
            return result;
        }

        public string Run(string cmd, string photo, string checkpoint)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";
            start.Arguments = string.Format("\"{0}\" --filepath \"{1}\" \"{2}\"", cmd, photo, checkpoint);
            start.UseShellExecute = false;// Do not use OS shell
            start.CreateNoWindow = true; // We don't need new window
            start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    return result;
                }
            }
        }

        [HttpPost]
        public IActionResult UpdateDeclaration(AddDeclarationViewModel viewModel, int id_for_declar, int category, string declarImgPath)
        {
            int imgId;
            if (declarImgPath == null)
                imgId = 1;
            else
                imgId = imageService.GetImageId(declarImgPath);

            Declaration old_declaration = declarationService.GetDeclarationById(id_for_declar);
            if (old_declaration != null)
            {
                if (old_declaration.Price != viewModel.Price)
                    logger.LogInformation($"User '{User.Identity.Name}' changed price " +
                        $"from {old_declaration.Price} BYN to {viewModel.Price} BYN in declaration with id '{old_declaration.Id}'.");
                if (old_declaration.CategoryId != category)
                    logger.LogInformation($"User '{User.Identity.Name}' changed category " +
                        $"from '{old_declaration.CategoryId}' to '{category}' in declaration with id '{old_declaration.Id}'.");
                if (old_declaration.Description != viewModel.Description)
                    logger.LogInformation($"User '{User.Identity.Name}' changed description " +
                        $"from '{old_declaration.Description}' to '{viewModel.Description}' in declaration with id '{old_declaration.Id}'.");
                if (old_declaration.Title != viewModel.Title)
                    logger.LogInformation($"User '{User.Identity.Name}' changed title " +
                        $"from '{old_declaration.Title}' to '{viewModel.Title}' in declaration with id '{old_declaration.Id}'.");
                if (old_declaration.ImageId != imgId)
                    logger.LogInformation($"User '{User.Identity.Name}' changed image " +
                        $"in declaration with id '{old_declaration.Id}'.");

                if (old_declaration.CategoryId != category)
                    old_declaration.CategoryId = category;
                old_declaration.Description = viewModel.Description;
                old_declaration.Price = viewModel.Price;
                old_declaration.Title = viewModel.Title;
                old_declaration.ImageId = imgId;

                ///////////часть с питоном
                if (old_declaration.CategoryId != 5 && imgId != 1)
                { //не проверяем для категории "В дар" и объявления без фоток
                    var res = Run(@"..\FleatMarket\wwwroot\python\predict.py",
                        @$"..\FleatMarket\wwwroot{declarImgPath}",
                        @"..\FleatMarket\wwwroot\python\checkpoint.pth");
                    string[] parsedResult = res.Split("\n");
                    var toRemove = new string[] { "[", "]" };
                    string top_3_ctgs_toler = parsedResult[2];
                    foreach (var c in toRemove)
                    {
                        top_3_ctgs_toler = top_3_ctgs_toler.Replace(c, string.Empty);
                    }
                    string[] tolerance = top_3_ctgs_toler.Split(" ");
                    double firstToler = Convert.ToDouble(tolerance[0].Replace(".", ","));
                    if (firstToler > 0.75)
                    {
                        string top_3_categs = parsedResult[1];
                        var charsToREmove = new string[] { "'", "[", "]", " " };
                        foreach (var c in charsToREmove)
                        {
                            top_3_categs = top_3_categs.Replace(c, string.Empty);
                        }
                        string[] categs = top_3_categs.Split(",");
                        int categId = parseJsonWithCategs(@"..\FleatMarket\wwwroot\python\categs_subcategs.json", categs[0]);

                        if (categId != old_declaration.CategoryId)
                        {
                            old_declaration.CategoryId = categId;
                            var categName = categoryService.GetCategoryById(old_declaration.CategoryId);
                            Notification notification = new Notification
                            {
                                Message = $"В объявлении '{old_declaration.Title}' была изменена категория на '{categName.CategoryName}'!",
                                UserId = old_declaration.UserId
                            };
                            var user = userService.GetUserByEmail(User.Identity.Name);
                            user.NotifCount++;
                            userService.UpdateUser(user);

                            notificationService.AddNotification(notification);
                        }
                    }
                    //если порог фотки не пройден, то сделать отсылание админу
                }
                ///////////

                declarationService.UpdateDeclaration(old_declaration);
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult OpenDeclaration(int id)
        {
            var declaration = declarationService.GetDeclarationById(id);
            var category = categoryService.GetCategoryById(declaration.CategoryId);
            var status = declarStatService.GetStatusById(declaration.DeclarationStatusId);
            var user = userService.GetUserByStringId(declaration.UserId);

            UserViewModel userView = new UserViewModel
            {
                EMail = user.Email,
                ImagePath = user.Image.ImagePath,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Surname = user.Surname
            };
            OneDeclarationViewModel viewModel = new OneDeclarationViewModel
            {
                Id = declaration.Id,
                AuthorMail = user.Email,
                CategoryName = category.CategoryName,
                CategoryId = category.Id,
                Date = declaration.TimeOfCreation,
                Description = declaration.Description,
                Price = declaration.Price,
                StatusName = status.StatusName,
                StatusId = status.Id,
                Title = declaration.Title,
                ImagePath = declaration.Image.ImagePath,
                User = userView
            };
            return View(viewModel);
        }
    }
}