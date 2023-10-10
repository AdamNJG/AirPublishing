using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Repository;

namespace ImageStorage.ImageVerification.Service.interfaces
{
    public interface IImageUploadRepository
    {
        void InsertUpload(ImageUploadOutputDto outputDto);
    }
}
