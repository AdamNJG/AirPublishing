using ImageStorage.ImageVerification.ImageValidation;
using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Service.interfaces;
using ImageStorage.ImageVerification.Service.status;
using System.Runtime.CompilerServices;

namespace ImageStorage.ImageVerification.Service
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IImageStorage _imageStorage;
        private readonly IImageUploadRepository _imageRepository;

        public ImageUploadService(IImageStorage imageStorage, IImageUploadRepository imageUpload)
        {
            _imageStorage = imageStorage;
            _imageRepository = imageUpload;
        }

        public async Task<RequestStatus> ValidateAndStoreImage(ImageUploadInputDto inputDto)
        {
            try
            {
                ImageUpload upload = new ImageUpload(inputDto);

                ImageUploadOutputDto outputDto = upload.GetOutput();

                outputDto.Url = await _imageStorage.StoreImage(outputDto);
                _imageRepository.InsertUpload(outputDto);

                return new RequestStatus(StatusCode.OK, outputDto.Url);
            } 
            catch (ImageValidationException ex)
            {
                return new RequestStatus(StatusCode.INPUT_ERROR, ex.Message);
            }
            catch
            {
                return new RequestStatus(StatusCode.SERVER_ERROR, "Internal server error");
            }
        }
    }
}
