namespace ImageStorage.ImageVerification.ImageValidation
{
    public class ImageValidationException: Exception
    {
        public ImageValidationException() { }
        public ImageValidationException(string message) : base(message) { }
    }
}
