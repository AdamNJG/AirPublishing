using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Service.interfaces;

namespace ImageStorage.ImageVerification.Storage
{
    public class LocalImageStorage : IImageStorage
    { 

        private readonly IConfiguration _configuration;

        public LocalImageStorage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    public Task<string> StoreImage(ImageUploadOutputDto outputDto)
        {
            string directory = _configuration["LocalDirectory"];
            string fileName = $"{outputDto.UserId}_{outputDto.FileName}";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string path = Path.Combine(directory, fileName);

            File.WriteAllBytes(path, outputDto.ImageData);
            return Task.FromResult(path);
        }
    }
}
