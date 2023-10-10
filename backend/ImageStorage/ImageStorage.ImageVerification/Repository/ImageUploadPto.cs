namespace ImageStorage.ImageVerification.Repository
{
    public class ImageUploadPto
    {
        public Guid Id { get; set; }
        public Guid UserId{ get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
    }
}
