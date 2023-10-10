
using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Service.status;

namespace ImageStorage.ImageVerification.Service
{
    public interface IImageUploadService
    {
        Task<RequestStatus> ValidateAndStoreImage(ImageUploadInputDto inputDto);
    }
}
