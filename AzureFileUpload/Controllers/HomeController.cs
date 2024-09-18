using AzureFileUpload.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace AzureFileUpload.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPdfService _pdfService;

        public HomeController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Home";
            return View();
        }

        [HttpGet]
        public IActionResult UploadPdf()
        {
            ViewData["ActivePage"] = "Upload";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension == ".pdf")
                {
                    try
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            await _pdfService.UploadPdfFileAsync(file.FileName, stream);
                        }
                        ViewData["Message"] = "File uploaded successfully!";
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or handle it as needed
                        ViewData["Message"] = "An error occurred while uploading the file.";
                    }
                }
                else
                {
                    ViewData["Message"] = "Only PDF files are allowed!";
                }
            }
            else
            {
                ViewData["Message"] = "No file selected!";
            }

            return View("Index");
        }
        public IActionResult Privacy()
        {
            ViewData["ActivePage"] = "Privacy";
            return View();
        }
    }
}
