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
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            try
            {
                if (await blobClient.ExistsAsync())
                {
                    BlobSasBuilder sasBuilder = new BlobSasBuilder
                    {
                        BlobContainerName = blobContainerClient.Name,
                        BlobName = blobClient.Name,
                        Resource = "b", 
                        StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5), 
                        ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
                    };

                    sasBuilder.SetPermissions(BlobSasPermissions.Read);

                    Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
                    Debug.WriteLine(sasUri.ToString());

                    var rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "Pdfs");
                    var localFilePath = Path.Combine(rootPath, fileName);

                    if (!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(sasUri);
                        response.EnsureSuccessStatusCode();

                        await using (var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
                        {
                            await response.Content.CopyToAsync(fileStream);
                        }
                    }

                    string pdfUrl = Url.Content($"~/Pdfs/{fileName}");
                    ViewData["PdfUrl"] = pdfUrl;
                    ViewData["FileName"] = fileName;
                    return View();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public IActionResult DeleteTempFile([FromBody] DeleteFileModel model)
        {
            var rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "Pdfs");
            var localFilePath = Path.Combine(rootPath, model.FileName);

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
