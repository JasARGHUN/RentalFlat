using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace RentalFlat_Server.Services.IServices
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);
        bool DeleteFile(string fileName);
    }
}
