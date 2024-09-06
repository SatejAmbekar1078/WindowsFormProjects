using AzureFileUpload.Models;

namespace AzureFileUpload.Services
{
    public interface IPdfService
    {
        Task<IEnumerable<PdfFile>> GetPdfListAsync();
        Task UploadPdfFileAsync(string fileName, Stream fileStream);
        //Task<string> GetSasTokenAsync();
    }
}
