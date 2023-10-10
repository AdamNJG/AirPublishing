using ImageStorage.ImageVerification.ImageValidation.dto;

namespace ImageStorage.ImageVerification.ImageValidation
{
    public class ImageUpload
    {
        private byte[] _imageData;
        private ImageType _imageType;
        private string _fileName;
        private Guid _userId;

        public ImageUpload(ImageUploadInputDto inputDto)
        {
            _imageType = GetImageType(inputDto.FileName);
            _imageData = ValidateImage(TryConvertFromBase64(inputDto.ImageData));
            _fileName = inputDto.FileName;
            _userId = inputDto.UserId;
        }

        private byte[] TryConvertFromBase64(string base64)
        {
            try
            {
                return Convert.FromBase64String(base64);
            }
            catch
            {
                throw new ImageValidationException("ImageData was not a base64 encoded string of an image");
            }
        }

        private byte[] ValidateImage(byte[] imageData)
        {
            if (_imageType == ImageType.JPEG && (!IsValidJpeg(imageData) || !ImageSizeValid(imageData)))
            {
                throw new ImageValidationException("Image is not a valid JPEG");
            }

            if (_imageType == ImageType.PNG && (!IsValidPng(imageData) || !ImageSizeValid(imageData)))
            {
                throw new ImageValidationException("Image is not a valid PNG");
            }
            return imageData;
        }

        private static bool IsValidJpeg(byte[] imageData)
        {
            return imageData.Length >= 2 && imageData[0] == 0xFF && imageData[1] == 0xD8;
        }

        private static bool IsValidPng(byte[] imageData)
        {
            if (imageData.Length < 8)
            {
                return false;
            }

            byte[] _pngSingature = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
            for (int i = 0; i < 8; i++)
            {
                if (imageData[i] != _pngSingature[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static ImageType GetImageType(string fileName)
        {
            string extension = fileName.Split('.').LastOrDefault("").ToLower();

            if (extension.Equals(""))
            {
                throw new ImageValidationException($"File extension: {extension} is not accepted, it must be a jpg or png");
            }

            if (extension.Contains("png"))
            {
                return ImageType.PNG;
            }

            List<string> jpegExtensions = new List<string>() { "jpg", "jpeg", "jpe", "jif", "jfif" };
            {                                             
                foreach (string jpegExt in jpegExtensions)
                {
                    if (extension.Contains(jpegExt))
                    {
                        return ImageType.JPEG;
                    }
                }
            }

            throw new ImageValidationException($"File extension: {extension} is not accepted, it must be a jpg or png");
        }

        public ImageUploadOutputDto GetOutput()
        {
            return new ImageUploadOutputDto()
            {
                FileName = _fileName,
                ImageData = _imageData,
                UserId = _userId
            };
        }

        private static bool ImageSizeValid(byte[] imageData)
        {
            using (MemoryStream stream = new MemoryStream(imageData))
            using (Image image = Image.Load(stream))
            {
                return (image.Size.Width <= 1024 && image.Size.Height <= 1024);
            }
        }
    }
}
