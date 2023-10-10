using ImageStorage.ImageVerification.ImageValidation.dto;

namespace ImageStorage.ImageVerification.Service.interfaces
{
    public interface IImageStorage
    {
        Task<string> StoreImage(ImageUploadOutputDto outputDto);
    }
}
