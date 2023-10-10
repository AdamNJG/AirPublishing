using Microsoft.EntityFrameworkCore;

namespace ImageStorage.ImageVerification.Repository
{
    public class ImageUploadContext : DbContext
    {
        public DbSet<ImageUploadPto> ImageUploads { get; set; }

        public ImageUploadContext(DbContextOptions<ImageUploadContext> options)
        : base(options)
        {
        }
    }
}
