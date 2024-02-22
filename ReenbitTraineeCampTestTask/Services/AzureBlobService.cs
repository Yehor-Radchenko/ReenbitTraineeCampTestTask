using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Reflection.Metadata;

namespace ReenbitTraineeCampTestTask.Services
{
    public class AzureBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _blobContainerClient;
        private readonly string _azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=ypradchenkoreenbit;AccountKey=7ma9zsS/ZJeN7ujRuqmHRZcnTWnEw/UbCgOIosXG7YeB/m037O40kpvBXPAWxclVBbJe/v5NGQiu+AStoa7uJw==;EndpointSuffix=core.windows.net";

        public AzureBlobService()
        {
            _blobServiceClient = new BlobServiceClient(_azureConnectionString);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("filecontainer");
        }

        public async Task<BlobContentInfo> UploadFile(IFormFile file)
        {
            if (Path.GetExtension(file.FileName) != ".docx")
                throw new Exception("Selected file is Incorrect format. Select .docx");

            BlobContentInfo azureResponse; 
            string fileName = file.FileName;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;

                azureResponse = await _blobContainerClient.UploadBlobAsync(fileName, memoryStream, default);
            }
            return azureResponse;
        }

        public async Task<List<BlobItem>> GetUploadedBlobs()
        {
            var items = new List<BlobItem>();
            var UploadedFiles = _blobContainerClient.GetBlobsAsync();
            await foreach (var file in UploadedFiles)
            {
                items.Add(file);
            }
            return items;
        }
    }
}
