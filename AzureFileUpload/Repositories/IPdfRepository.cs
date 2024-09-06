using AzureFileUpload.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AzureFileUpload.Repositories
{
    public interface IPdfRepository
    {
        Task<IEnumerable<PdfFile>> GetAllPdfsAsync();
        Task UploadPdfAsync(string fileName, Stream fileStream);
        //Task<String> GenerateSasTokenAsync();
    }
}
