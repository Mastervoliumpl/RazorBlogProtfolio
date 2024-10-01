namespace RazorBlogProtfolio.Models
{
    public class Image
    {
        public Guid ImageID { get; init; }
        public byte[] ImageFile { get; set; } // or string ImageFilePath
        public string ImageSource { get; set; }

        public void UploadImage()
        {
            // Implementation for uploading image
        }

        public void DeleteImage()
        {
            // Implementation for deleting image
        }
    }
}
