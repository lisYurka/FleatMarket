using System;
using System.IO;
using System.Threading.Tasks;
using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleatMarket.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment hostingService;
        private readonly IImageService imageService;
        public ImageController(IHostingEnvironment hosting, IImageService _imageService)
        {
            hostingService = hosting;
            imageService = _imageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = "/images/" + fileName;

                using (var stream = new FileStream(hostingService.WebRootPath + path, FileMode.Create))
                    await file.CopyToAsync(stream);

                Image image = new Image
                {
                    ImageName = fileName,
                    ImagePath = path
                };
                imageService.CreateImage(image);
                return Content(image.ImagePath);
            }
            else
                throw new Exception("Что-то не так с параметром");
        }
    }
}