using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Service.interfaces;

namespace ImageStorage.ImageVerification.Repository
{
    public class ImageUploadRepository : IImageUploadRepository
    {
        private readonly ImageUploadContext _context;

        public ImageUploadRepository(ImageUploadContext context)
        {
            _context = context;
        }

        public void InsertUpload(ImageUploadOutputDto outputDto)
        {
            _context.ImageUploads.Add(new ImageUploadPto()
            {
                UserId = outputDto.UserId,
                FileName = outputDto.FileName,
                Url = outputDto.Url
            });

            _context.SaveChanges();
        }
    }
}
