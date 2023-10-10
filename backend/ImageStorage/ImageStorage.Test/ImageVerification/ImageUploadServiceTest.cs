using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Repository;
using ImageStorage.ImageVerification.Service;
using ImageStorage.ImageVerification.Service.interfaces;
using ImageStorage.ImageVerification.Service.status;
using ImageStorage.Test.ImageVerification.helpers;
using ImageStorage.Test.ImageVerification.mocks;
using Microsoft.Extensions.DependencyInjection;

namespace ImageStorage.Test.ImageVerification
{
    [TestClass]
    public class ImageUploadServiceTest
    {
        private static IServiceProvider _serviceProvider;
        private IImageUploadService _imageUploadService;
        private ImageStorageMock _imageStorage;
        private ImageUploadRepositoryMock _imageUploadRepositoryMock;

        public object RequestCode { get; private set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _serviceProvider = ServiceConfiguration.BuildServiceProvider();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _imageUploadService = _serviceProvider.GetRequiredService<IImageUploadService>();
            _imageStorage = (ImageStorageMock)_serviceProvider.GetRequiredService<IImageStorage>();
            _imageUploadRepositoryMock = (ImageUploadRepositoryMock)_serviceProvider.GetRequiredService<IImageUploadRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _imageStorage.ClearImages();
        }

        [TestMethod]
        public async Task ImageUploadService_ValidateAndStoreImage_ValidRequestStatus()
        {
            byte[] data = FileHelpers.GetValidJpeg(256, 256);
            ImageUploadInputDto imageUploadInputDto = new ImageUploadInputDto()
            {
                FileName = "Test.jpg",
                ImageData = Convert.ToBase64String(data),
                UserId = Guid.NewGuid()
            };

            RequestStatus status = await _imageUploadService.ValidateAndStoreImage(imageUploadInputDto);

            _imageStorage.UploadedImages.TryGetValue(imageUploadInputDto.FileName, out byte[] storedImage);
            ImageUploadPto pto = _imageUploadRepositoryMock.ImageUploadPtos.FirstOrDefault((u) => u.FileName == imageUploadInputDto.FileName && u.UserId == imageUploadInputDto.UserId);

            Assert.AreEqual(StatusCode.OK, status.StatusCode);
            Assert.AreEqual($"{imageUploadInputDto.UserId}_{imageUploadInputDto.FileName}", status.Message);
            CollectionAssert.AreEqual(data, storedImage);
            Assert.IsNotNull(pto);
            Assert.AreEqual($"{imageUploadInputDto.UserId}_{imageUploadInputDto.FileName}", pto.Url);
        }

        [TestMethod]
        public async Task ImageUploadService_ValidateAndStoreImage_InvalidRequestStatus()
        {

            ImageUploadInputDto imageUploadInputDto = new ImageUploadInputDto()
            {
                FileName = "Test.jpg",
                ImageData = Convert.ToBase64String(FileHelpers.GetInvalidImage()),
                UserId = Guid.NewGuid()
            };

            RequestStatus status = await _imageUploadService.ValidateAndStoreImage(imageUploadInputDto);

            Assert.AreEqual(_imageStorage.UploadedImages.Count, 0);
            Assert.AreEqual(StatusCode.INPUT_ERROR, status.StatusCode);
            Assert.AreEqual("Image is not a valid JPEG", status.Message);
        }

        // I am not going to try and get an unhandled exception within the system, but added code to that anyway.
    }
}
