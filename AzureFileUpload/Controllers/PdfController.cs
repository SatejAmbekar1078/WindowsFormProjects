using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using AzureFileUpload.Models;
using AzureFileUpload.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFileUpload.Controllers
{
    public class PdfController : Controller
    {
        private readonly IPdfService _pdfService;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "democontainer";
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Inject IWebHostEnvironment into the controller
        public PdfController(IPdfService pdfService, BlobServiceClient blobServiceClient, IWebHostEnvironment webHostEnvironment)
        {
            _pdfService = pdfService;
            _blobServiceClient = blobServiceClient;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pdfList = await _pdfService.GetPdfListAsync();
            ViewData["ActivePage"] = "ViewPdf";
            return View(pdfList);
        }

        public async Task<IActionResult> ViewPdf(string fileName)
        {
            // Get the container client.
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Get the blob client for the specific PDF file.
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            try
            {
                // Check if the blob exists.
                if (await blobClient.ExistsAsync())
                {
                    // Generate a SAS token with read permissions for the blob
                    BlobSasBuilder sasBuilder = new BlobSasBuilder
                    {
                        BlobContainerName = blobContainerClient.Name,
                        BlobName = blobClient.Name,
                        Resource = "b", // b for Blob
                        StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5), // Optional: Start time, 5 minutes before now
                        ExpiresOn = DateTimeOffset.UtcNow.AddHours(1) // Set expiration time for 1 hour
                    };

                    // Set read permissions
                    sasBuilder.SetPermissions(BlobSasPermissions.Read);

                    // Build the SAS URI
                    Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
                    Debug.WriteLine(sasUri.ToString());

                    var rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "Pdfs");
                    var localFilePath = Path.Combine(rootPath, fileName);

                    // Ensure the Pdfs folder exists
                    if (!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    // Download the PDF file using HttpClient
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(sasUri);
                        response.EnsureSuccessStatusCode();

                        // Save the file to the specified path
                        await using (var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
                        {
                            await response.Content.CopyToAsync(fileStream);
                        }
                    }

                    // Pass the local file path to the view
                    //ViewData["PdfUrl"] = localFilePath;
                    string pdfUrl = Url.Content($"~/Pdfs/{fileName}");
                    ViewData["PdfUrl"] = pdfUrl;
                    ViewData["FileName"] = fileName;
                    return View();
                }
                else
                {
                    // Handle the case where the file doesn't exist.
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                Debug.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public IActionResult DeleteTempFile([FromBody] DeleteFileModel model)
        {
            // Get the path of the Pdfs folder in wwwroot
            var rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "Pdfs");
            var localFilePath = Path.Combine(rootPath, model.FileName);

            // Check if the file exists and delete it
            if (System.IO.File.Exists(localFilePath))
            {
                try
                {
                    System.IO.File.Delete(localFilePath);
                    Debug.WriteLine($"File {localFilePath} has been deleted.");
                    return Ok();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting file {localFilePath}: {ex.Message}");
                    return StatusCode(500, "File deletion failed");
                }
            }
            return NotFound();
        }
    }
}
