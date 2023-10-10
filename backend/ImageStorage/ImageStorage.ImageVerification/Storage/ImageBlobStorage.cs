using Azure.Storage.Blobs;
using ImageStorage.ImageVerification.ImageValidation.dto;
using ImageStorage.ImageVerification.Service.interfaces;
using System.IO;
using System.Runtime.CompilerServices;

namespace ImageStorage.ImageVerification.Storage
{
    public class ImageBlobStorage : IImageStorage
    {
        private readonly IConfiguration _configuration;

        public ImageBlobStorage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> StoreImage(ImageUploadOutputDto outputDto)
        {
            string connectionString = _configuration.GetConnectionString("AzureBlobStorage");
            string containerName = _configuration["AzureBlobContainerName"];
            string blobName = $"{outputDto.UserId}_{outputDto.FileName}"; 

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = await GetBlobContainerClient(blobServiceClient, containerName);

            BlobClient blobClient = containerClient.GetBlobClient(blobName.ToLower());

            using (MemoryStream stream = new MemoryStream(outputDto.ImageData))
            {
                blobClient.Upload(stream, true);
            }

            return blobClient.Uri.ToString();
        }

        private async Task<BlobContainerClient> GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
        {
            try
            {
                return await blobServiceClient.CreateBlobContainerAsync(containerName.ToLower());
            }
            catch
            {
                return blobServiceClient.GetBlobContainerClient(containerName.ToLower());
            }
        }
    }
}
