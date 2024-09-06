using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AzureFileUpload.Models;
using Azure.Storage;
using Azure.Storage.Blobs.Models;
using System.Reflection.Metadata;

namespace AzureFileUpload.Repositories
{
    public class PdfRepository : IPdfRepository
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public PdfRepository(string connectionString, string containerName)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerName = containerName;
           
        }

       
        public async Task<IEnumerable<PdfFile>> GetAllPdfsAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobs = containerClient.GetBlobsAsync();
            var pdfFiles = new List<PdfFile>();

            await foreach (var blobItem in blobs)
            {
                pdfFiles.Add(new PdfFile
                {
                    FileName = blobItem.Name,
                    FileUrl = containerClient.GetBlobClient(blobItem.Name).Uri.ToString()
                });
            }

            return pdfFiles;
        }

        public async Task UploadPdfAsync(string fileName, Stream fileStream)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
            await blobClient.SetHttpHeadersAsync(new BlobHttpHeaders { ContentType = "application/pdf" });
        }

        //public async Task<string> GenerateSasTokenAsync()
        //{
        //    var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

        //    // Create a SAS token builder for the container
        //    var sasBuilder = new BlobSasBuilder
        //    {
        //        BlobContainerName = _containerName,
        //        Resource = "c", // 'c' for container
        //        StartsOn = DateTimeOffset.UtcNow,
        //        ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
        //    };

        //    // Set permissions (e.g., Read)
        //    sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);

        //    // Create SAS token
        //    var sasToken = sasBuilder.ToSasQueryParameters(CreateStorageSharedKeyCredential(_blobServiceClient.Uri.ToString())).ToString();

        //    // Build the SAS URI
        //    var sasUri = new Uri(containerClient.Uri + "?" + sasToken);

        //    return sasUri.ToString();
        //}
    }
}
