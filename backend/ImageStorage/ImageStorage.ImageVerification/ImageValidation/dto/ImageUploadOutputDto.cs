namespace ImageStorage.ImageVerification.ImageValidation.dto
{
    public class ImageUploadOutputDto
    {
        public Guid UserId { get; set; }
        public byte[] ImageData { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }    
    }
}
