using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStorage.Test.ImageVerification.mocks
{
    public class ImageStorageMock : IImageStorage
    {
        public Dictionary<string, byte[]> UploadedImages { get; }

        public ImageStorageMock()
        {
            UploadedImages = new Dictionary<string, byte[]>();
        }
        public Task<string> StoreImage(ImageUploadOutputDto outputDto)
        {
            UploadedImages.Add(outputDto.FileName, outputDto.ImageData);
            return Task.FromResult($"{outputDto.UserId}_{outputDto.FileName}");
        }

        public void ClearImages()
        {
            UploadedImages.Clear();
        }
    }
}
