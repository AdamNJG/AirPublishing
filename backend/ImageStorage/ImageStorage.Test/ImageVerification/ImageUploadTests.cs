using ImageStorage.Test.ImageVerification.helpers;
using ImageStorage.ImageVerification.ImageValidation;
using ImageStorage.ImageVerification.ImageValidation.dto;

namespace ImageStorage.Test
{
    // I have been deleting tests as I go to enforce encapsulation
    [TestClass]
    public class ImageUploadTests
    {
        [TestMethod]
        [DataRow("test.jpg")]
        [DataRow("test.jpeg")]
        [DataRow("test.jpe")]
        [DataRow("test.jif")]
        [DataRow("test.jfif")]
        public void ImageUploadConstructor_ValidJpeg_ValidExtension(string fileName)
        {
            byte[] data = FileHelpers.GetValidJpeg(256, 256);
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = fileName,
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            ImageUpload image = new(inputDto);

            Assert.IsNotNull(image);
        }

        [TestMethod]
        public void ImageUploadConstructor_InvalidImage_ValidExtensionJPEG()
        {
            byte[] data = FileHelpers.GetInvalidImage();
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = "test.jpg",
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            var exception = Assert.ThrowsException<ImageValidationException>(() => new ImageUpload(inputDto));

            Assert.AreEqual("Image is not a valid JPEG", exception.Message);
        }

        [TestMethod]
        [DataRow("test.bmp", "bmp")]
        [DataRow("", "")]
        [DataRow("test", "test")]
        public void ImageUploadConstructor_ValidImage_InvalidExtension(string fileName, string extension)
        {
            byte[] data = FileHelpers.GetValidPng(256, 256);
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = fileName,
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            var exception = Assert.ThrowsException<ImageValidationException>(() => new ImageUpload(inputDto));

            Assert.AreEqual($"File extension: {extension} is not accepted, it must be a jpg or png", exception.Message);
        }

        [TestMethod]
        public void ImageUploadConstructor_ValidImage_WrongExtension()
        {
            byte[] pngData = FileHelpers.GetValidPng(256, 256);
            byte[] jpgData = FileHelpers.GetValidJpeg(256, 256);
            string encodedPNG = Convert.ToBase64String(pngData);
            string encodedJPG = Convert.ToBase64String(jpgData);

            ImageUploadInputDto pngDataJpgFileName = new ImageUploadInputDto()
            {
                FileName = "test.jpg",
                ImageData = encodedPNG,
                UserId = Guid.NewGuid()
            };

            ImageUploadInputDto jpgDataPngFileName = new ImageUploadInputDto()
            {
                FileName = "test.png",
                ImageData = encodedJPG,
                UserId = Guid.NewGuid()
            };

            var exception = Assert.ThrowsException<ImageValidationException>(() => new ImageUpload(pngDataJpgFileName));
            var exception2 = Assert.ThrowsException<ImageValidationException>(() => new ImageUpload(jpgDataPngFileName));

            Assert.AreEqual($"Image is not a valid JPEG", exception.Message);
            Assert.AreEqual($"Image is not a valid PNG", exception2.Message);
        }

        [TestMethod]
        public void ImageUploadConstructor_ValidPng_ValidExtension()
        {
            byte[] data = FileHelpers.GetValidPng(256, 256);
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = "test.png",
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            ImageUpload image = new(inputDto);

            Assert.IsNotNull(image);
        }

        [TestMethod]
        public void ImageUpload_GetDto_Jpg()
        {
            byte[] data = FileHelpers.GetValidJpeg(256, 256);
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = "test.jpg",
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            ImageUpload image = new(inputDto);

            ImageUploadOutputDto outputDto = image.GetOutput();

            Assert.IsNotNull(outputDto);
            Assert.AreEqual(inputDto.FileName, outputDto.FileName);
            Console.WriteLine(data);
            Console.WriteLine(outputDto.ImageData);
            CollectionAssert.AreEqual(data, outputDto.ImageData);
        }

        [TestMethod]
        public void ImageUpload_GetDto_Png()
        {
            byte[] data = FileHelpers.GetValidPng(256, 256);
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = "test.png",
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            ImageUpload image = new(inputDto);

            ImageUploadOutputDto outputDto = image.GetOutput();

            Assert.IsNotNull(outputDto);
            Assert.AreEqual(inputDto.FileName, outputDto.FileName);
            CollectionAssert.AreEqual(data, outputDto.ImageData);
            Assert.AreEqual(inputDto.UserId, outputDto.UserId);
        }

        [TestMethod]
        public void ImageUpload_Oversized_Png()
        {
            byte[] data = FileHelpers.GetValidPng(1025, 1025);
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = "test.png",
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            var exception = Assert.ThrowsException<ImageValidationException>(() => new ImageUpload(inputDto));

            Assert.AreEqual($"Image is not a valid PNG", exception.Message);
        }

        [TestMethod]
        public void ImageUpload_Oversized_Jpeg()
        {
            byte[] data = FileHelpers.GetValidJpeg(1025, 1025);
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = "test.jpeg",
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            var exception = Assert.ThrowsException<ImageValidationException>(() => new ImageUpload(inputDto));

            Assert.AreEqual($"Image is not a valid JPEG", exception.Message);
        }


        [TestMethod]
        public void ImageUpload_MaxSize_Png()
        {
            byte[] data = FileHelpers.GetValidPng(1024, 1024);
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = "test.png",
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            ImageUpload upload = new ImageUpload(inputDto);

            ImageUploadOutputDto output = upload.GetOutput();

            Assert.AreEqual(inputDto.FileName, output.FileName);
            CollectionAssert.AreEqual(data, output.ImageData);
            Assert.AreEqual(inputDto.UserId, output.UserId);
        }

        [TestMethod]
        public void ImageUpload_MaxSize_Jpeg()
        {
            byte[] data = FileHelpers.GetValidJpeg(1024, 1024);
            string encodedData = Convert.ToBase64String(data);

            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = "test.jpeg",
                ImageData = encodedData,
                UserId = Guid.NewGuid()
            };

            ImageUpload upload = new ImageUpload(inputDto);

            ImageUploadOutputDto output = upload.GetOutput();

            Assert.AreEqual(inputDto.FileName, output.FileName);
            CollectionAssert.AreEqual(data, output.ImageData);
            Assert.AreEqual(inputDto.UserId, output.UserId);
        }

        [TestMethod]

         public void ImageUpload_InvalidBase64() 
         {
            ImageUploadInputDto inputDto = new ImageUploadInputDto()
            {
                FileName = "test.jpeg",
                ImageData = "notBase64",
                UserId = Guid.NewGuid()
            };

            var exception = Assert.ThrowsException<ImageValidationException>(() => new ImageUpload(inputDto));

            Assert.AreEqual("ImageData was not a base64 encoded string of an image", exception.Message);
         }
    }
}