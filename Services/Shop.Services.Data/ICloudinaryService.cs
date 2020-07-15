namespace Shop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);

        Task<List<string>> UploadAsync(Cloudinary cloudinary, ICollection<IFormFile> files);
    }
}
