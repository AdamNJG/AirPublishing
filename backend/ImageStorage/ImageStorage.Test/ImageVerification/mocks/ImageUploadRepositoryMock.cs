using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Repository;
using ImageStorage.ImageVerification.Service;
using ImageStorage.ImageVerification.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStorage.Test.ImageVerification.mocks
{
    internal class ImageUploadRepositoryMock : IImageUploadRepository
    {
        public List<ImageUploadPto> ImageUploadPtos { get; }
        public ImageUploadRepositoryMock() {
            ImageUploadPtos = new List<ImageUploadPto>();
        }

        public void InsertUpload(ImageUploadOutputDto outputDto)
        {
            ImageUploadPtos.Add(new ImageUploadPto() { UserId = outputDto.UserId, FileName = outputDto.FileName, Url = outputDto.Url } );
        }
    }
}
