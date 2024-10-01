using RazorBlogProtfolio.Models;

namespace RazorBlogProtfolio.Interfaces
{
    public interface IImageRepo
    {
        void UploadImage();
        void DeleteImage();
    }
}
