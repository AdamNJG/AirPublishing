using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Service;
using ImageStorage.ImageVerification.Service.status;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ImageStorage.ImageVerification.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageVerificationController : ControllerBase
    {
        private readonly ILogger<ImageVerificationController> _logger;
        private readonly IImageUploadService _imageUploadService;

        public ImageVerificationController(ILogger<ImageVerificationController> logger, IImageUploadService imageUploadService)
        {
            _logger = logger;
            _imageUploadService = imageUploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromBody] ImageUploadInputDto inputDto)
        {
            RequestStatus status = await _imageUploadService.ValidateAndStoreImage(inputDto);

            _logger.LogInformation($"user: {inputDto.UserId}, fileName: {inputDto.FileName}, status: {status.StatusCode}, message: {status.Message}");

            return new ObjectResult(status.Message)
            {
                StatusCode = (int)status.StatusCode
            };
        }
    }
}