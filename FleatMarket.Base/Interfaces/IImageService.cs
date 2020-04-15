using FleatMarket.Base.Entities;

namespace FleatMarket.Base.Interfaces
{
    public interface IImageService
    {
        void CreateImage(Image img);
        int GetImageId(string path);
    }
}
