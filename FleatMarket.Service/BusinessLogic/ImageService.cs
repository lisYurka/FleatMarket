using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using System.Linq;

namespace FleatMarket.Service.BusinessLogic
{
    public class ImageService : IImageService
    {
        private readonly IBaseRepository repository;

        public ImageService(IBaseRepository _repository)
        {
            repository = _repository;
        }

        public void CreateImage(Image img)
        {
            repository.Create(img);
        }
        public int GetImageId(string path)
        {
            Image img = repository.GetAll<Image>().Where(p => p.ImagePath == path).LastOrDefault();
            return img.Id;
        }

    }
}
