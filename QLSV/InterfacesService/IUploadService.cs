using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace QLSV.InterfacesService
{
    public interface IUploadService
    {
        Task<string> SaveFile(IFormFile image);

        string GetFullPath(string fileName);

        Task DeleteFile(string fileName);
    }
}