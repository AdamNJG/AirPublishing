namespace ImageStorage.ImageVerification.ImageValidation.dto
{
    public class ImageUploadInputDto
    {
        public Guid UserId { get; set; }
        public string ImageData { get; set; }
        public string FileName { get; set; }
    }
}
