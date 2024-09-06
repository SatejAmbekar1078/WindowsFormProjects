using AzureFileUpload.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AzureFileUpload.Models;

namespace AzureFileUpload.Services
{
    public class PdfService : IPdfService
    {
        private readonly IPdfRepository _pdfRepository;

        public PdfService(IPdfRepository pdfRepository)
        {
            _pdfRepository = pdfRepository;
        }

        public async Task<IEnumerable<PdfFile>> GetPdfListAsync()
        {
            return await _pdfRepository.GetAllPdfsAsync();
        }

        public async Task UploadPdfFileAsync(string fileName, Stream fileStream)
        {
            await _pdfRepository.UploadPdfAsync(fileName, fileStream);
        }

        //public async Task<string> GetSasTokenAsync()
        //{
        //    // Generate the SAS token using the correct repository method
        //    var sasUri = await _pdfRepository.GenerateSasTokenAsync();
        //    return sasUri.ToString();
        //}
    }
}
