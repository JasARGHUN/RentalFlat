using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace RentalFlat_Server.Services.IServices
{
    public interface IImageUpload
    {
        Task<string> UploadImage(IBrowserFile file);
        bool DeleteImage(string fileName);
    }
}
